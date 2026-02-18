import { getFreightDetails } from "@/app/actions/auctionActions";

import { getCurrentUser } from "@/app/actions/authActions";
import { notFound } from "next/navigation";
import BidList from "./BidList";
import BidListHeader from "./BidListHeader";
import TenderDetailsSpec from "./TenderDetailsSpec";

export default async function Details({ params }: { params: { id: string } }) {
  const { id } = await params;
  const data = await getFreightDetails(id);
  const user = await getCurrentUser();

  if (!data) return notFound();

  return (
    <>
      <BidListHeader data={data} user={user} />
      <div className="grid grid-cols-1 lg:grid-cols-12 gap-8">
        <TenderDetailsSpec data={data} />
        <BidList data={data} />
      </div>
    </>
  );
}
