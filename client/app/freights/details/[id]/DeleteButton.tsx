"use client";

import { deleteFreight } from "@/app/actions/auctionActions";
import ModalWindow from "@/app/components/ModalWindow";
import { Button, Spinner } from "flowbite-react";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { toast } from "react-toastify";

type Props = {
  id: string;
};

export default function DeleteButton({ id }: Props) {
  const [isLoading, setIsLoading] = useState(false);
  const [openModal, setOpenModal] = useState(false);
  const router = useRouter();

  async function handleDelete() {
    setIsLoading(true);
    setOpenModal(false);

    try {
      const res = await deleteFreight(id);

      if (res && res.error) {
        throw new Error(res.error.message || "Failed to delete");
      }

      toast.success("Tender deleted successfully");
      router.push("/");
    } catch (err: any) {
      console.error(err);
      toast.error(err.status + " " + err.message);
      setIsLoading(false);
    }
  }

  return (
    <>
      <Button
        outline
        color="red"
        onClick={() => setOpenModal(true)}
        disabled={isLoading}
      >
        {isLoading && <Spinner size="sm" />} Delete Tender
      </Button>

      <ModalWindow
        openModal={openModal}
        setOpenModal={setOpenModal}
        handleDelete={handleDelete}
      />
    </>
  );
}
