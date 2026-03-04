"use client";

import { Bid, Freight, FreightFinished } from "@/types";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useSession } from "next-auth/react";
import { useParams } from "next/navigation";
import { ReactNode, useCallback, useEffect, useRef } from "react";
import { toast } from "react-toastify";
import { getFreightDetails } from "../actions/auctionActions";
import TenderClosedToast from "../components/TenderClosedToast";
import TenderCreatedToast from "../components/TenderCreatedToast";
import { useBidStore } from "../hooks/useBidStore";
import { useTenderStore } from "../hooks/useTenderStore";

type Props = {
  children: ReactNode;
};
export default function SignalRProvider({ children }: Props) {
  const { status, data: session } = useSession();
  const user = session?.user;
  //Store SignalR connection
  const connectionHub = useRef<HubConnection | null>(null);
  const setCurrentLowBid = useTenderStore((state) => state.setCurrentLowBid);
  const addBid = useBidStore((state) => state.addBid);
  const params = useParams<{ id: string }>();

  const handleTenderCreated = useCallback(
    (freight: Freight) => {
      if (status === "loading") return;
      if (user?.username !== freight.seller) {
        return toast(<TenderCreatedToast freight={freight} />, {
          autoClose: 10000,
          icon: false,
        });
      }
    },
    [user?.username],
  );

  const handleTenderClosed = useCallback(
    async (closedTender: FreightFinished) => {
      const id = toast.loading("Closing tender details...");

      try {
        const freight = await getFreightDetails(closedTender.freightId);

        toast.update(id, {
          render: (
            <TenderClosedToast closedTender={closedTender} freight={freight} />
          ),
          type: "warning",
          isLoading: false,
          autoClose: 10000,
          icon: false,
          closeButton: true,
        });
      } catch (error) {
        toast.update(id, {
          render: "Tender is closed now!",
          type: "error",
          isLoading: false,
          autoClose: 5000,
        });
      }
    },
    [],
  );

  const handleBidPlaced = useCallback(
    (bid: Bid) => {
      if (bid.bidStatus === "Accepted") {
        setCurrentLowBid(bid.freightId, bid.amount);
      }
      if (params.id === bid.freightId) {
        addBid(bid);
      }
    },
    [setCurrentLowBid, addBid, params.id],
  );

  useEffect(() => {
    if (!connectionHub.current) {
      connectionHub.current = new HubConnectionBuilder()
        .withUrl(process.env.NEXT_PUBLIC_NOTIFY_URL!)
        .withAutomaticReconnect()
        .build();

      connectionHub.current
        .start()
        .then(() =>
          console.log(
            "Connection to notifications hub started! Listening for messages.",
          ),
        )
        .catch((err) => console.error("SignalR Connection Error: ", err));
    }
    //as per BidPlacedConsumer ==> await _hubContext.Clients.All.SendAsync("BidPlaced", context.Message);
    connectionHub.current.on("BidPlaced", handleBidPlaced);
    connectionHub.current.on("FreightCreated", handleTenderCreated);
    connectionHub.current.on("FreightFinished", handleTenderClosed);
    //Cleanup
    return () => {
      connectionHub.current?.off("BidPlaced", handleBidPlaced);
      connectionHub.current?.off("FreightCreated", handleTenderCreated);
      connectionHub.current?.off("FreightFinished", handleTenderClosed);
    };
  }, [
    setCurrentLowBid,
    handleBidPlaced,
    handleTenderCreated,
    handleTenderClosed,
  ]);
  return children;
}
