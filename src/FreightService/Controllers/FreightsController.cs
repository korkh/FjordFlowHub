using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using FreightService.Data;
using FreightService.DTOs;
using FreightService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreightService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreightsController(
        FreightDbContext context,
        IMapper mapper,
        IPublishEndpoint publishEndpoint
    ) : ControllerBase
    {
        private readonly FreightDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        [HttpGet]
        public async Task<ActionResult<List<FreightDto>>> GetAllFreights(string date)
        {
            var query = _context.Freights.OrderBy(x => x.Cargo.Description).AsQueryable();
            if (!string.IsNullOrEmpty(date))
            {
                //returning option where is greater than particular date
                query = query.Where(x =>
                    x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0
                );
            }
            return await query.ProjectTo<FreightDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FreightDto>> GetFreightById(Guid id)
        {
            var freight = await _context
                .Freights.Include(x => x.Cargo)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (freight == null)
            {
                return NotFound();
            }

            return _mapper.Map<FreightDto>(freight);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FreightDto>> CreateFreight(CreateFreightDto freightDto)
        {
            // Check if the user has already posted a similar tender
            var exists = await _context.Freights.AnyAsync(x =>
                x.Seller == User.Identity.Name
                && x.Cargo.Description == freightDto.Description
                && x.Status == Status.Live
            );

            if (exists)
            {
                return BadRequest(
                    new
                    {
                        message = "This item is already live. You cannot create a duplicate while it is active.",
                    }
                );
            }

            var freight = _mapper.Map<Freight>(freightDto);
            freight.Seller =
                User.Identity?.Name ?? throw new InvalidOperationException("User not found");

            _context.Freights.Add(freight);

            // English: Map to DTO for publishing
            var newFreightDto = _mapper.Map<FreightDto>(freight);
            await _publishEndpoint.Publish(_mapper.Map<FreightCreated>(newFreightDto));

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
                return BadRequest(new { message = "Could not save changes to the DB" });

            return CreatedAtAction(
                nameof(GetFreightById),
                new { id = freight.Id },
                _mapper.Map<FreightDto>(freight)
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFreight(Guid id, UpdateFreightDto updateDto)
        {
            // Fetch freight with its associated cargo
            var freight = await _context
                .Freights.Include(x => x.Cargo)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (freight == null)
                return NotFound("Cannot find your freight");

            if (freight.Seller != User.Identity.Name)
                return Forbid();

            // Manually update cargo properties
            freight.ReservePrice = updateDto.ReservePrice ?? freight.ReservePrice;
            freight.Cargo.Description = updateDto.Description ?? freight.Cargo.Description;

            freight.Cargo.WeightKg = updateDto.WeightKg ?? freight.Cargo.WeightKg;

            freight.Cargo.LengthMeters = updateDto.LengthMeters ?? freight.Cargo.LengthMeters;

            freight.Cargo.WidthMeters = updateDto.WidthMeters ?? freight.Cargo.WidthMeters;

            freight.Cargo.HeightMeters = updateDto.HeightMeters ?? freight.Cargo.HeightMeters;

            freight.Cargo.PickupCity = updateDto.PickupCity ?? freight.Cargo.PickupCity;
            freight.Cargo.DeliveryCity = updateDto.DeliveryCity ?? freight.Cargo.DeliveryCity;

            //Publishing to the bus
            await _publishEndpoint.Publish(_mapper.Map<FreightUpdated>(freight));
            // Save changes to the database
            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return Ok();

            return BadRequest("Problem saving changes");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFreight(Guid id)
        {
            // Find the freight entity
            var freight = await _context.Freights.FindAsync(id);

            if (freight == null)
                return NotFound();

            if (freight.Seller != User.Identity.Name)
                return Forbid();

            _context.Freights.Remove(freight);

            //Pusblishing to the bus
            await _publishEndpoint.Publish<FreightDeleted>(new { Id = freight.Id.ToString() }); //due to Id is a Guid but needs string in mongodb

            // Save changes and check result
            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
                return BadRequest("Could not update DB");

            return Ok();
        }
    }
}
