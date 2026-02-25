using FreightService.Data;
using FreightService.Protos;
using Grpc.Core;

namespace FreightService.Services
{
    /* * Service to handle gRPC requests from other microservices
     */
    public class GrpcFreightService(FreightDbContext dbContext) : GrpcFreight.GrpcFreightBase
    {
        private readonly FreightDbContext _dbContext = dbContext;

        public override async Task<GrpcFreightResponse> GetFreight(
            GetFreightRequest request,
            ServerCallContext context
        )
        {
            Console.WriteLine(
                "===> Received Grpc request for GetFreight called with id: " + request.Id
            );

            // 1. Await the async find method
            var freight =
                await _dbContext.Freights.FindAsync(Guid.Parse(request.Id))
                ?? throw new RpcException(new Status(StatusCode.NotFound, "Not found"));

            var response = new GrpcFreightResponse
            {
                Freight = new GrpcFreightModel
                {
                    Id = freight.Id.ToString(),
                    Seller = freight.Seller,
                    AuctionEnd = freight.AuctionEnd.ToString("o"),
                    ReservePrice = freight.ReservePrice,
                },
            };

            return response;
        }
    }
}
