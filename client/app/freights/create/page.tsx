import Heading from "@/app/components/Heading";
import FreightForm from "../FreightForm";

export default function Create() {
  return (
    <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
      <Heading
        title="Create Freight"
        subtitle="Please enter the details of your freight"
      />
      <FreightForm />
    </div>
  );
}
