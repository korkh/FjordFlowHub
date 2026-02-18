"use client";

import { Freight } from "@/types";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeadCell,
  TableRow,
} from "flowbite-react";
import Image from "next/image";

type Props = {
  data: Freight;
};

export default function TenderDetailsSpec({ data }: Props) {
  return (
    <div className="lg:col-span-7 space-y-6">
      {/* Main Image Container with elegant shadow and hover effect */}
      <div className="relative aspect-video rounded-3xl overflow-hidden shadow-xl border border-gray-100 bg-white">
        <Image
          src={data.imageUrl || "/images/placeholder.png"}
          alt={data.description}
          fill
          className="object-cover hover:scale-105 transition-transform duration-500 ease-in-out"
          priority
        />
      </div>

      {/* Modern Info Grid */}
      <div className="bg-white rounded-3xl shadow-sm border border-gray-100 overflow-hidden">
        <div className="p-6">
          <div className="flex justify-between items-center mb-6 px-2">
            <h3 className="text-xl font-black text-gray-900 uppercase tracking-tighter">
              Cargo Logistics Profile
            </h3>
            <div className="text-right">
              <p className="text-[10px] font-bold text-gray-400 uppercase">
                Shipper
              </p>
              <p className="text-sm font-black text-blue-600 underline decoration-2 underline-offset-4 cursor-pointer hover:text-blue-800 transition-colors">
                @{data.seller}
              </p>
            </div>
          </div>

          <div className="overflow-x-auto">
            <Table hoverable className="text-gray-700">
              <TableHead className="bg-gray-50/50 border-b border-gray-100">
                {/* English: We must wrap HeadCells in a TableRow to prevent HTML validation errors 
                   (th cannot be a direct child of thead).
                */}
                <TableRow>
                  <TableHeadCell className="py-4 lowercase font-bold text-gray-400 bg-gray-50/30">
                    Parameter
                  </TableHeadCell>
                  <TableHeadCell className="py-4 lowercase font-bold text-gray-400 text-right bg-gray-50/30">
                    Value
                  </TableHeadCell>
                </TableRow>
              </TableHead>
              <TableBody className="divide-y divide-gray-50">
                <TableRow className="bg-white">
                  <TableCell className="font-medium text-gray-600">
                    Total Gross Weight
                  </TableCell>
                  <TableCell className="text-right font-black text-gray-900">
                    {data.weightKg} kg
                  </TableCell>
                </TableRow>
                <TableRow className="bg-white">
                  <TableCell className="font-medium text-gray-600">
                    Calculated Volume
                  </TableCell>
                  <TableCell className="text-right font-black text-gray-900">
                    {data.volumeM3} m³
                  </TableCell>
                </TableRow>
                <TableRow className="bg-white">
                  <TableCell className="font-medium text-gray-600">
                    Dimensions (L × W × H)
                  </TableCell>
                  <TableCell className="text-right font-black text-gray-900">
                    {data.lengthMeters}m × {data.widthMeters}m ×{" "}
                    {data.heightMeters}m
                  </TableCell>
                </TableRow>
                <TableRow className="bg-white">
                  <TableCell className="font-medium text-green-700">
                    Reserve (Ceiling) Price
                  </TableCell>
                  <TableCell className="text-right font-black text-green-600 text-lg">
                    ${data.reservePrice}
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </div>
        </div>

        {/* Route Section - Nordic style split info */}
        <div className="grid grid-cols-2 border-t border-gray-100 text-center">
          <div className="p-6 border-r border-gray-100 bg-gray-50/30">
            <p className="text-[10px] font-bold text-gray-400 uppercase mb-1">
              Origin City
            </p>
            <p className="text-lg font-black text-gray-800 tracking-tight">
              {data.pickupCity}
            </p>
          </div>
          <div className="p-6 bg-gray-50/30">
            <p className="text-[10px] font-bold text-gray-400 uppercase mb-1">
              Destination
            </p>
            <p className="text-lg font-black text-gray-800 tracking-tight">
              {data.deliveryCity}
            </p>
          </div>
        </div>
      </div>

      {/* Meta timestamps for transparency */}
      <div className="flex justify-between px-6 text-[10px] font-bold text-gray-400 uppercase italic">
        <span>Listed: {new Date(data.createdAt).toLocaleDateString()}</span>
        <span>
          Last Update: {new Date(data.updatedAt).toLocaleDateString()}
        </span>
      </div>

      {/* NB! Alert Box - High visibility for carrier compliance */}
      <div className="bg-amber-50 border-2 border-amber-200 p-6 rounded-3xl flex items-start gap-4 transition-all hover:bg-amber-100/50 shadow-sm">
        <span className="text-2xl animate-pulse">⚠️</span>
        <div className="space-y-1">
          <p className="text-amber-800 font-black text-sm uppercase tracking-tight">
            NB! Carrier Action Required
          </p>
          <p className="text-amber-700 text-xs leading-relaxed">
            Please verify vehicle capacity before bidding. Ensure your truck can
            handle <strong>{data.weightKg} kg</strong>. Confirm the loading
            aperture is wider than <strong>{data.widthMeters}m</strong> and
            height is <strong>{data.heightMeters}m</strong>.
          </p>
        </div>
      </div>
    </div>
  );
}
