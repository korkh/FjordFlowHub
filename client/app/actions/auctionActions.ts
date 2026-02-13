"use server";

import { auth } from "@/auth";
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

export async function updateFreightTest(): Promise<{
  status: number;
  message: string;
}> {
  const data = {
    weightKg: Math.floor(Math.random() * 10000) + 1, // Random weight between 1 and 1000 kg
  };

  const session = await auth();
  const res = await fetch(
    "http://localhost:6001/freights/019c568e-5bdd-7cb9-890c-25eb5a87362b", //id extracted from Postman
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${session?.accessToken}`,
      },
      body: JSON.stringify(data),
    },
  );
  if (!res.ok) {
    return { status: res.status, message: res.statusText };
  }
  return { status: res.status, message: res.statusText };
}
