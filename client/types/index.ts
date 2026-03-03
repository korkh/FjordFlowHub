export type PagedResult<T> = {
  results: T[];
  pageCount: number;
  totalCount: number;
};

export type Freight = {
  reservePrice: number;
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
  widthMeters: number;
  volumeM3: number;
  pickupCity: string;
  deliveryCity: string;
  imageUrl: string;
  id: string;
};

export type TenderCounter = {
  days: number;
  hours: number;
  minutes: number;
  seconds: number;
  completed: boolean;
};

export type Bid = {
  id: string;
  freightId: string;
  bidder: string;
  bidTime: string;
  amount: number;
  bidStatus: string;
};

export type FreightFinished = {
  freightId: string;
  freightSold: boolean;
  seller: string;
  winner?: string;
  amount?: number;
};
