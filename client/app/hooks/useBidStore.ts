import { Bid } from "@/types";
import { create } from "zustand";

type State = {
  bids: Bid[];
  open: boolean;
};

type Tenders = {
  setBids: (bids: Bid[]) => void;
  addBid: (bid: Bid) => void;
  removeBid: (bid: Bid) => void;
  setOpen: (value: boolean) => void;
};

export const useBidStore = create<State & Tenders>((set) => ({
  bids: [],
  open: true,
  setBids: (bids: Bid[]) => set({ bids }),
  addBid: (bid: Bid) =>
    set((state) => ({
      bids: !state.bids.find((b) => b.id === bid.id)
        ? [bid, ...state.bids]
        : [...state.bids],
    })),
  removeBid: (bid) =>
    set((state) => ({ bids: state.bids.filter((b) => b.id !== bid.id) })),
  setOpen: (value: boolean) => set({ open: value }),
}));
