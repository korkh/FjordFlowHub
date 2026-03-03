type Props = {
  amount?: number;
  reservePrice: number;
  closed?: boolean;
};

export default function CurrentBid({ amount, reservePrice, closed }: Props) {
  const text = amount ? "$" + amount : "No bids";
  const colorClass = amount
    ? amount > reservePrice
      ? "bg-green-600"
      : "bg-amber-600"
    : "bg-red-600";

  return (
    <>
      {!closed && (
        <div
          className={`border-2 border-white text-white py-1 px-2 rounded-lg flex justify-center ${colorClass}`}
        >
          {text}
        </div>
      )}
    </>
  );
}
