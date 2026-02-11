export type PagedResult<T> = {
  results: T[];
  pageCount: number;
  totalCount: number;
};

export type Freight = {
  reservePrice?: number;
  seller: string;
  winner?: string;
  soldAmount?: number;
  currentLowBid?: number;
  createdAt: string;
  updatedAt: string;
  auctionEnd: string;
  status: string;
  description: string;
  weightKg: number;
  lengthMeters: number;
  heightMeters: number;
  pickupCity: string;
  deliveryCity: string;
  imageUrl: string;
  id: string;
};
