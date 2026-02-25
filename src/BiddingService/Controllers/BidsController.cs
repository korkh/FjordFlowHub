using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using BiddingService.Services;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController(
        IMapper mapper,
        IPublishEndpoint publisher,
        GrpcFreightClient grpcClient
    ) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publisher = publisher;
        private readonly GrpcFreightClient _grpcClient = grpcClient;

        [Authorize]
        [HttpPost]
        // Logic snippet for BidsController
        public async Task<ActionResult<Bid>> PlaceBid(string freightId, int amount)
        {
            // 1. Get the freight/auction info from the local MongoDB (BiddingService)
            var tender = await DB.Find<Freight>().OneAsync(freightId);

            if (tender == null)
            {
                tender = _grpcClient.GetFreight(freightId);
                if (tender == null)
                {
                    return BadRequest(
                        "Bid cannot be accepted at this time. Please try again later."
                    );
                }
            }

            //2. Do not allow bids on the own tender
            if (tender.Seller == User.Identity.Name)
            {
                return BadRequest("You cannot bid on your own tender");
            }

            // if (User.Identity?.Name == null)
            //     return Unauthorized();

            // 3. Place the bid
            var bid = new Bid
            {
                Amount = amount,
                FreightId = freightId,
                Bidder = User.Identity.Name,
                BidTime = DateTime.UtcNow,
            };

            // Determine Bid Status
            if (tender.AuctionEnd < DateTime.UtcNow)
            {
                bid.BidStatus = BidStatus.Finished;
            }
            else
            {
                var lowestBid = await DB.Find<Bid>()
                    .Match(a => a.FreightId == freightId)
                    .Match(b => b.BidStatus == BidStatus.Accepted)
                    .Sort(b => b.Ascending(x => x.Amount))
                    .ExecuteFirstAsync();

                if (amount > tender.ReservePrice)
                {
                    bid.BidStatus = BidStatus.AboveReserve;
                }
                else if (lowestBid != null && amount >= lowestBid.Amount)
                {
                    bid.BidStatus = BidStatus.TooHigh;
                }
                else
                {
                    bid.BidStatus = BidStatus.Accepted;
                }
            }

            await DB.SaveAsync(bid);

            // 3. Publish to MassTransit
            await _publisher.Publish(_mapper.Map<BidPlaced>(bid));

            return Ok(_mapper.Map<BidDto>(bid));
        }

        [HttpGet("{freightId}")]
        public async Task<ActionResult<List<BidDto>>> GetBidsForTender(string freightId)
        {
            var bids = await DB.Find<Bid>()
                .Match(a => a.FreightId == freightId)
                .Sort(b => b.Descending(a => a.BidTime))
                .ExecuteAsync();

            return bids.Select(_mapper.Map<BidDto>).ToList();
        }
    }
}
