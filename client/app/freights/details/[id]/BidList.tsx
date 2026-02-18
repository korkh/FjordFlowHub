// app/freights/details/[id]/BidList.tsx
"use client";

import StatusBadge from "@/app/components/StatusBadge";
import { Freight } from "@/types";

type Props = {
  data: Freight;
};

export default function BidList({ data }: Props) {
  // Demo bids - In reality, fetch from BiddingService
  const bids = [
    { id: 1, bidder: "NordicExpress", amount: 1150, time: "1m ago" },
    { id: 2, bidder: "VikingTransport", amount: 1200, time: "5m ago" },
    { id: 3, bidder: "OsloHulks", amount: 1250, time: "15m ago" },
    { id: 4, bidder: "BergenFreight", amount: 1300, time: "1h ago" },
    { id: 5, bidder: "ArcticLogistics", amount: 1350, time: "2h ago" },
  ].sort((a, b) => a.amount - b.amount); // Сортировка: самая низкая цена сверху

  // Логика: если ставок еще нет, ориентируемся на ReservePrice
  const highDisplayPrice = data.currentLowBid || data.reservePrice;

  return (
    <div className="lg:col-span-5 flex flex-col gap-6">
      <div className="bg-white rounded-3xl shadow-2xl border border-gray-100 flex flex-col h-full overflow-hidden">
        <div className="bg-gray-900 p-6 text-center">
          <p className="text-gray-400 text-[10px] font-bold uppercase tracking-widest mb-2">
            Current Best Offer
          </p>
          <p className="text-4xl font-black text-white mb-3">
            ${bids[0]?.amount || highDisplayPrice}
          </p>
          <StatusBadge status={data.status} position="inline" fixed={false} />
        </div>

        <div className="flex-1 overflow-y-auto p-6 scrollbar-thin scrollbar-thumb-gray-200">
          <div className="flex flex-col h-full">
            <div className="space-y-3">
              {bids.length > 0 ? (
                bids.map((bid) => (
                  <div
                    key={bid.id}
                    className={`flex justify-between items-center p-4 rounded-2xl border transition-all ${
                      bid.amount === bids[0].amount
                        ? "bg-blue-50 border-blue-200 ring-1 ring-blue-100"
                        : "bg-gray-50 border-transparent hover:bg-white hover:border-gray-200"
                    }`}
                  >
                    <div className="flex flex-col">
                      <span className="text-xs font-black text-gray-800 flex items-center gap-2">
                        {bid.bidder}
                        {bid.amount === bids[0].amount && (
                          <span className="bg-blue-600 text-white text-[8px] px-1.5 py-0.5 rounded-full uppercase">
                            Best
                          </span>
                        )}
                      </span>
                      <span className="text-[10px] text-gray-400">
                        {bid.time}
                      </span>
                    </div>
                    <div className="text-right">
                      <span className="text-lg font-black text-blue-600">
                        ${bid.amount}
                      </span>
                    </div>
                  </div>
                ))
              ) : (
                <div className="text-center py-10 text-gray-400 italic text-sm">
                  No offers yet.
                </div>
              )}
            </div>

            <div className="mt-auto pt-6 bg-white sticky bottom-0">
              <div className="bg-amber-50 p-3 rounded-xl mb-4 text-center border border-amber-100">
                <p className="text-[10px] font-bold text-amber-700 uppercase">
                  Next bid must be lower than $
                  {bids[0]?.amount || data.reservePrice}
                </p>
              </div>
              <div className="relative">
                <span className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 font-bold">
                  $
                </span>
                <input
                  type="number"
                  placeholder="Enter your price"
                  className="w-full pl-8 pr-4 py-4 rounded-2xl border-2 border-gray-100 focus:border-blue-500 focus:outline-none font-black text-lg"
                />
              </div>
              <button className="w-full mt-4 bg-blue-600 text-white font-black py-4 rounded-2xl hover:bg-blue-700 transition-all shadow-lg uppercase tracking-widest text-xs">
                Place Tender Bid
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
