import { Freight, PagedResult } from "@/types";
import { create } from "zustand";

type State = {
  freights: Freight[];
  totalCount: number;
  pageCount: number;
};

type Freights = {
  setData: (data: PagedResult<Freight>) => void;
  setCurrentLowBid: (freightId: string, amount: number) => void;
};

const initialState: State = {
  freights: [],
  totalCount: 0,
  pageCount: 0,
};

export const useTenderStore = create<State & Freights>((set) => ({
  ...initialState,
  setData: (data: PagedResult<Freight>) =>
    set(() => ({
      freights: data.results,
      totalCount: data.totalCount,
      pageCount: data.pageCount,
    })),
  setCurrentLowBid: (freightId: string, amount: number) =>
    set((state) => ({
      freights: state.freights.map((freight) =>
        freight.id === freightId
          ? { ...freight, currentLowBid: amount }
          : freight,
      ),
    })),
}));
