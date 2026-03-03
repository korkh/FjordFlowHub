import { currencyFormat } from "@/lib/currencyFormat";
import { Bid, Freight } from "@/types";
import { format } from "date-fns";

type Props = {
  data: Freight;
  bids: Bid[];
};
export default function BidItem({ data, bids }: Props) {
  const sortedBids = [...bids].sort((a, b) => a.amount - b.amount);
  const lowBid =
    sortedBids.length > 0 ? sortedBids[0].amount : data.reservePrice;
  // Check if the auction is over
  const isAuctionClosed =
    new Date(data.auctionEnd) < new Date() ||
    data.status === "Finished" ||
    data.status === "ReservedNotMet";

  function getTenderBidInfo(bid: Bid) {
    let text = "";
    let color = "";
    let bgColor = "";
    let colors = "";
    switch (bid.bidStatus) {
      case "Accepted":
        color = "text-green-700";
        bgColor = "bg-blue-700";
        colors = "bg-green-100 border-green-200 ring-1 ring-green-100";
        text = "Bid is accepted";
        break;
      case "TooHigh":
        color = "text-amber-700";
        bgColor = "bg-amber-700";
        colors = "bg-amber-100 border-amber-200 ring-1 ring-amber-100";
        text = "Bid is higher than best bid";
        break;
      case "AboveReserve":
        color = "text-red-700";
        bgColor = "bg-red-700";
        colors = "bg-red-100 border-red-200 ring-1 ring-red-100";
        text = "Bid is above reserve price!";
        break;
      default:
        color = "text-red-700";
        bgColor = "bg-red-700";
        colors = "bg-red-100 border-red-200 ring-1 ring-red-100";
        text = "Bid placed after tender finished";
        break;
    }
    return { text, color, bgColor, colors };
  }

  return (
    <div className="space-y-3 mb-6">
      {sortedBids.length > 0 ? (
        sortedBids.map((bid) => (
          <div
            key={bid.id}
            className={`flex justify-between items-center p-4 rounded-2xl border transition-all ${
              getTenderBidInfo(bid).colors
            }`}
          >
            <div className="flex flex-col">
              <span className="text-gray-700 text-sm flex items-center gap-2">
                Bidder:
                <span className="font-black text-xl text-gray-500">
                  {bid.bidder}
                </span>
                {bid.amount === lowBid && (
                  <span
                    className={`${getTenderBidInfo(bid).bgColor} text-white text-[8px] px-1.5 py-0.5 rounded-full uppercase`}
                  >
                    {isAuctionClosed ? "Winner" : "Best"}
                  </span>
                )}
              </span>
              <span className="text-sm text-gray-700">
                Time:
                {format(bid.bidTime, " dd.MM.yyyy hh:mm:ss")}
              </span>
            </div>
            <div className="flex flex-col text-right">
              <span
                className={`text-lg font-black ${getTenderBidInfo(bid).color}`}
              >
                ${currencyFormat(bid.amount)}
              </span>
              <span className={`${getTenderBidInfo(bid).color}`}>
                {getTenderBidInfo(bid).text}
              </span>
            </div>
          </div>
        ))
      ) : (
        <div className="text-center py-10 text-gray-400 italic text-sm">
          No offers were made.
        </div>
      )}
    </div>
  );
}
