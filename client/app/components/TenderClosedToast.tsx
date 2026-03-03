"use client";

import { Freight, FreightFinished } from "@/types";
import Image from "next/image";
import Link from "next/link";

type Props = {
  closedTender: FreightFinished;
  freight: Freight;
};

export default function TenderClosedToast({ closedTender, freight }: Props) {
  return (
    <Link
      href={`/freights/details/${freight.id}`}
      className="flex flex-row items-center gap-4 no-underline text-inherit"
    >
      <div className="relative h-16 w-20 shrink-0 overflow-hidden rounded-lg">
        <Image
          src={freight.imageUrl}
          alt="Closed tender"
          fill
          className="object-cover"
        />
      </div>
      <div className="flex flex-col gap-1">
        <span className="font-bold text-sm leading-tight">
          Tender {freight.pickupCity} → {freight.deliveryCity} is closed
        </span>
        {closedTender.freightSold ? (
          <p className="text-xs">
            Winner:
            <span className="font-bold text-green-400">
              {closedTender.winner}
            </span>
            (${closedTender.amount})
          </p>
        ) : (
          <p className="text-xs text-black italic">No winner for this tender</p>
        )}
      </div>
    </Link>
  );
}
