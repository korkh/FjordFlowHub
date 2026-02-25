using BiddingService.Models;
using FreightService.Protos;
using Grpc.Net.Client;

namespace BiddingService.Services
{
    public class GrpcFreightClient(ILogger<GrpcFreightClient> logger, IConfiguration config)
    {
        private readonly ILogger<GrpcFreightClient> _logger = logger;
        private readonly IConfiguration _config = config;

        public Freight GetFreight(string id)
        {
            _logger.LogInformation("===> Invoking Grpc Service to get Freight with id: " + id);

            var channel = GrpcChannel.ForAddress(_config["GrpcFreight"]);
            var client = new GrpcFreight.GrpcFreightClient(channel);
            var request = new GetFreightRequest { Id = id };

            try
            {
                var reply = client.GetFreight(request);
                var freight = new Freight
                {
                    ID = reply.Freight.Id,
                    Seller = reply.Freight.Seller,
                    AuctionEnd = DateTime.Parse(reply.Freight.AuctionEnd),
                    ReservePrice = reply.Freight.ReservePrice,
                };
                return freight;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invoking Grpc Service");
                return null;
            }
        }
    }
}
