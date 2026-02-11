"use server";

import { Freight, PagedResult } from "@/types";

export async function getData(query: string): Promise<PagedResult<Freight>> {
  const baseUrl = "http://localhost:6001/search";
  const fullUrl = query.startsWith("?")
    ? baseUrl + query
    : `${baseUrl}?${query}`;

  const res = await fetch(fullUrl);

  if (!res.ok) {
    throw new Error("Failed to fetch data");
  }
  return res.json();
}
