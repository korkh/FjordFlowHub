import Heading from "@/app/components/Heading";
import { Freight } from "@/types";
import CountdownTimer from "../../CountdownTimer";
import DeleteButton from "./DeleteButton";
import EditButton from "./EditButton";

type Props = {
  data: Freight;
  user: any;
};

export default function BidListHeader({ data, user }: Props) {
  return (
    <div className="flex justify-between">
      <Heading title={data.description} />
      <div className="flex items-center gap-2">
        {user?.username === data.seller && (
          <>
            <EditButton id={data.id} />
            <DeleteButton id={data.id} />
          </>
        )}
      </div>

      <div className="flex items-center bg-gray-50 p-2 mb-2 rounded-xl border border-gray-100 shadow-sm">
        <span className="text-[15px] font-black text-gray-400 uppercase tracking-widest px-4">
          Time Remaining:
        </span>
        <CountdownTimer auctionEnd={data.auctionEnd} textSize="xs" />
      </div>
    </div>
  );
}
