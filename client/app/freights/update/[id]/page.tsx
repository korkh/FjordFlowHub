import { getFreightDetails } from "@/app/actions/auctionActions";
import Heading from "@/app/components/Heading";
import FreightForm from "../../FreightForm";

export default async function Update({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  const data = await getFreightDetails(id);

  return (
    <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
      <Heading
        title={`Update your tender: ${data.description}`}
        subtitle="Please update the details of your tender (only following properties can be updated)"
      />
      <FreightForm freight={data} />
    </div>
  );
}
