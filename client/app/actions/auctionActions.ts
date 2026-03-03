"use server";

import { fetchWrapper } from "@/lib/fetchWrapper";
import { Bid, Freight, PagedResult } from "@/types";
import { FieldValues } from "react-hook-form";

export async function updateFreightTest(): Promise<{
  status: number;
  message: string;
}> {
  const data = {
    weightKg: Math.floor(Math.random() * 10000) + 1, // Random weight between 1 and 1000 kg
  };

  return fetchWrapper.put(
    "freights/019c568e-5bdd-7cb9-890c-25eb5a87362b",
    data,
  );
}

export async function getData(params: string): Promise<PagedResult<Freight>> {
  return fetchWrapper.get(`search${params}`);
}

export async function createFreight(data: FieldValues) {
  return fetchWrapper.post(`freights`, data);
}

export async function getFreightDetails(id: string): Promise<Freight> {
  return fetchWrapper.get(`freights/${id}`);
}

export async function deleteFreight(id: string) {
  return fetchWrapper.del(`freights/${id}`);
}

export async function updateFreight(id: string, data: FieldValues) {
  return fetchWrapper.put(`freights/${id}`, data);
}

export async function getBidsForTender(id: string): Promise<Bid[]> {
  return fetchWrapper.get(`bids/${id}`);
}

export async function createBid(freightId: string, amount: number) {
  return fetchWrapper.post(`bids?freightId=${freightId}&amount=${amount}`, {});
}
