"use client";

import { createBid } from "@/app/actions/auctionActions";
import { useBidStore } from "@/app/hooks/useBidStore";
import { currencyFormat } from "@/lib/currencyFormat";
import { FieldValues, useForm } from "react-hook-form"; // Убрали Form из импорта
import { toast } from "react-toastify";

type Props = {
  freightId: string;
  lowBid: number;
};

export default function BidPlacingForm({ lowBid, freightId }: Props) {
  const { register, handleSubmit, reset } = useForm();
  const addBid = useBidStore((state) => state.addBid);

  function onSubmit(data: FieldValues) {
    if (data.amount >= lowBid) {
      reset();
      return toast.error("Bid must be less than $" + currencyFormat(lowBid));
    }

    createBid(freightId, +data.amount)
      .then((res) => {
        if (res.error) {
          reset();
          throw res.error;
        }
        addBid(res);
        reset();
      })
      .catch((err: any) => {
        toast.error(err.status + " " + err.message);
      });
  }

  return (
    <>
      <div className="bg-amber-50 p-3 rounded-xl mb-4 text-center border border-amber-100">
        <p className="text-[15px] font-bold text-amber-700 uppercase">
          Next bid must be lower than ${lowBid}
        </p>
      </div>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="relative">
          <span className="absolute left-4 top-1/2 -translate-y-1/2 text-gray-400 font-bold">
            $
          </span>
          <input
            type="number"
            {...register("amount", { required: true })}
            placeholder="Enter your price"
            className="w-full pl-8 pr-4 py-4 rounded-2xl border-2 border-gray-100 focus:border-blue-500 focus:outline-none font-black text-lg"
          />
        </div>
        <button
          type="submit"
          className="w-full mt-4 bg-blue-600 text-white font-black py-4 rounded-2xl hover:bg-blue-700 disabled:bg-gray-100 disabled:text-gray-300 transition-all shadow-lg uppercase tracking-widest text-xs"
        >
          Place your bid
        </button>
      </form>
    </>
  );
}
