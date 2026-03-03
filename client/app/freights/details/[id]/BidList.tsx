"use client";

import { getBidsForTender } from "@/app/actions/auctionActions";
import StatusBadge from "@/app/components/StatusBadge";
import { useBidStore } from "@/app/hooks/useBidStore";
import { Bid, Freight } from "@/types";
import { User } from "next-auth";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import BidItem from "./BidItem";
import BidPlacingForm from "./BidPlacingForm";

type Props = {
  freight: Freight;
  user: User | null;
};

export default function BidList({ freight, user }: Props) {
  const [isLoading, setIsLoading] = useState(true);
  const bids = useBidStore((state) => state.bids);
  const setBids = useBidStore((state) => state.setBids);
  const isOpen = useBidStore((state) => state.open);
  const setOpen = useBidStore((state) => state.setOpen);

  useEffect(() => {
    const isClosed =
      new Date(freight.auctionEnd) < new Date() && freight.status !== "Live";
    setOpen(!isClosed);

    getBidsForTender(freight.id)
      .then((res: any) => {
        if (res.error) throw res.error;
        setBids(res as Bid[]);
      })
      .catch((err: any) => toast.error(err.message))
      .finally(() => setIsLoading(false));

    return () => setOpen(true);
  }, [freight.id, freight.auctionEnd, freight.status, setBids, setOpen]);

  const sortedBids = [...bids].sort((a, b) => a.amount - b.amount);
  const acceptedBids = bids.filter((b) => b.bidStatus.includes("Accepted"));

  const lowBid =
    acceptedBids.length > 0
      ? Math.min(...acceptedBids.map((b) => b.amount))
      : freight.reservePrice;

  if (isLoading) return <div className="p-6 text-center">Loading bids...</div>;

  return (
    <div className="lg:col-span-5 flex flex-col gap-6">
      <div className="bg-white rounded-3xl shadow-2xl border border-gray-100 flex flex-col h-full overflow-hidden">
        {/* Header Section */}
        <div className="bg-gray-900 p-6 text-center">
          <p className="text-gray-400 text-[10px] font-bold uppercase tracking-widest mb-2">
            {isOpen
              ? "Final Result"
              : sortedBids.length > 0
                ? "Current Best Offer"
                : "Starting Reserve Price"}
          </p>
          <p className="text-4xl font-black text-white mb-3">${lowBid}</p>
          <StatusBadge
            status={freight.status}
            position="inline"
            fixed={false}
          />
        </div>

        {/* Bids List */}
        <div className="flex-1 overflow-y-auto p-6 max-h-125 scrollbar-thin scrollbar-thumb-gray-200">
          <div className="flex flex-col h-full">
            <BidItem data={freight} bids={bids} />

            {/* Bottom Section: Validation & Form */}
            <div className="mt-auto pt-6 bg-white sticky bottom-0 border-t border-gray-50">
              {!isOpen ? (
                <div className="bg-gray-100 p-6 rounded-2xl text-center border-2 border-dashed border-gray-200">
                  <p className="text-gray-600 font-black uppercase tracking-tight text-sm">
                    This tender is now closed
                  </p>
                  <p className="text-gray-400 text-xs mt-1">
                    No further bids can be accepted.
                  </p>
                </div>
              ) : !user ? (
                <div className="bg-gray-100 p-6 rounded-2xl text-center border-2 border-dashed border-gray-200">
                  <p className="text-gray-600 font-black uppercase tracking-tight text-sm">
                    Please log in to place a bid
                  </p>
                </div>
              ) : user.username === freight.seller ? (
                <div className="bg-gray-100 p-6 rounded-2xl text-center border-2 border-dashed border-gray-200">
                  <p className="text-gray-600 font-black uppercase tracking-tight text-sm">
                    You cannot bid on your own tender
                  </p>
                </div>
              ) : (
                <BidPlacingForm lowBid={lowBid} freightId={freight.id} />
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
