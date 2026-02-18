"use client";
import { Freight } from "@/types";
import { Button, Spinner } from "flowbite-react";
import { usePathname, useRouter } from "next/navigation";
import { useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { createFreight, updateFreight } from "../actions/auctionActions";
import DateInput from "../components/DateInput";
import Input from "../components/Input";

type Props = {
  freight?: Freight;
};

export default function FreightForm({ freight }: Props) {
  const router = useRouter();
  const pathname = usePathname();
  const {
    control,
    handleSubmit,
    setFocus,
    reset,
    formState: { isDirty, isValid, isSubmitting },
  } = useForm({
    mode: "onTouched",
  });

  useEffect(() => {
    if (freight) {
      const { description, lengthMeters, widthMeters, heightMeters, weightKg } =
        freight;
      reset({
        description,
        lengthMeters,
        widthMeters,
        heightMeters,
        weightKg,
      });
    }
    setFocus("description");
  }, [setFocus, reset, freight]);

  async function onSubmit(data: FieldValues) {
    try {
      let id = "";
      let response;
      if (pathname === "/freights/create") {
        response = await createFreight(data);
        id = response.id;
      } else {
        if (freight) {
          response = await updateFreight(freight.id, data);
          id = freight.id;
        }
      }

      if (response.error) {
        throw response.error;
      }
      toast.success("Tender created successfully!");
      router.push(`/freights/details/${id}`);
    } catch (error: any) {
      toast.error(error.status + " " + error.message);
    }
  }

  return (
    <form
      className="flex flex-col mt-3 gap-3"
      onSubmit={handleSubmit(onSubmit)}
    >
      <Input
        label="Description"
        name="description"
        control={control}
        rules={{ required: "Description is required" }}
      />
      <Input
        label="Length (m)"
        name="lengthMeters"
        type="number"
        control={control}
        rules={{ required: "Required" }}
      />
      <Input
        label="Width (m)"
        name="widthMeters"
        type="number"
        control={control}
        rules={{ required: "Required" }}
      />
      <Input
        label="Height (m)"
        name="heightMeters"
        type="number"
        control={control}
        rules={{ required: "Required" }}
      />
      <Input
        label="Weight (kg)"
        name="weightKg"
        type="number"
        control={control}
        rules={{ required: "Weight is required" }}
      />
      {pathname === "/freights/create" && (
        <>
          <div className="grid grid-cols-2 gap-3">
            <Input
              label="Pickup City"
              name="pickupCity"
              control={control}
              rules={{ required: "Pickup City is required" }}
            />
            <Input
              label="Delivery City"
              name="deliveryCity"
              control={control}
              rules={{ required: "Delivery City is required" }}
            />
          </div>
          <Input
            label="Reserve Price (USD) enter 0 for no reserve"
            name="reservePrice"
            type="number"
            control={control}
            rules={{ required: "Reserve Price is required" }}
          />
          <Input
            label="Image URL"
            name="imageUrl"
            control={control}
            rules={{ required: "Image URL is required" }}
          />
          <div className="mb-3">
            <DateInput
              label="Tender End Date/Time"
              name="auctionEnd"
              control={control}
              showTimeSelect
              minDate={new Date()}
              rules={{ required: "Auction end date is required" }}
            />
          </div>
        </>
      )}

      <div className="flex justify-between mt-4">
        <Button color="light" onClick={() => router.push("/")}>
          Cancel
        </Button>
        <Button
          outline
          color="green"
          type="submit"
          disabled={!isDirty || !isValid || isSubmitting}
        >
          {isSubmitting ? <Spinner size="sm" className="mr-2" /> : null}
          Submit
        </Button>
      </div>
    </form>
  );
}
