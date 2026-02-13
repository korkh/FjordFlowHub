import { Freight } from "@/types";
import Link from "next/link";
import CargoImage from "./CargoImage";
import CountdownTimer from "./CountdownTimer";

type Props = {
  freight: Freight;
};

export default function FreightCard({ freight }: Props) {
  const isFinished =
    freight.status === "Finished" ||
    freight.status === "ReserveNotMet" ||
    new Date(freight.auctionEnd).getFullYear() < 2024;

  return (
    <Link href={`/freights/details/${freight.id}`} className="group">
      <div className="relative w-full bg-gray-200 aspect-video rounded-lg overflow-hidden shadow-sm">
        <CargoImage imageUrl={freight.imageUrl} isFinished={isFinished} />

        <div className="absolute bottom-2 left-2">
          <CountdownTimer auctionEnd={freight.auctionEnd} />
        </div>
        {freight.status === "ReserveNotMet" && (
          <div className="absolute top-2 right-2 bg-amber-600 text-white px-2 py-1 rounded text-[10px] font-bold uppercase">
            Reserve Not Met
          </div>
        )}
      </div>

      <div className="flex justify-between items-start mt-4">
        <div className="flex flex-col">
          <h3 className="text-gray-900 font-bold">
            {freight.pickupCity} → {freight.deliveryCity}
          </h3>
          <p className="text-gray-500 text-sm">{freight.seller}</p>
        </div>

        <div className="text-right">
          <p className="font-bold text-amber-600">
            ${freight.reservePrice?.toLocaleString()}
          </p>
          <p className="text-[10px] text-gray-400 uppercase font-semibold">
            weight: {freight.weightKg} kg
          </p>
        </div>
      </div>
    </Link>
  );
}
