"use client";

import { Freight } from "@/types";
import Image from "next/image";
import Link from "next/link";

type Props = {
  freight: Freight;
};

export default function TenderCreatedToast({ freight }: Props) {
  return (
    <Link
      href={`/freights/details/${freight.id}`}
      className="flex flex-row items-center gap-4 no-underline text-inherit"
    >
      <div className="relative h-16 w-20 shrink-0 overflow-hidden rounded-lg">
        <Image
          src={freight.imageUrl}
          alt="New Tender"
          fill
          className="object-cover"
        />
      </div>
      <div className="flex flex-col">
        <span className="font-bold text-sm">New Tender Created!</span>
        <span className="text-xs text-gray-600">
          {freight.pickupCity} → {freight.deliveryCity}
        </span>
        <span className="font-black text-amber-600 text-sm">
          Price: ${freight.reservePrice}
        </span>
      </div>
    </Link>
  );
}
