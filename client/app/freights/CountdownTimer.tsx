"use client";

import dynamic from "next/dynamic";
import Countdown, { zeroPad } from "react-countdown";

const renderer = ({
  days,
  hours,
  minutes,
  seconds,
  completed,
}: {
  days: number;
  hours: number;
  minutes: number;
  seconds: number;
  completed: boolean;
}) => {
  // Логика "Ending Soon": меньше 6 часов и еще не завершено
  const isEndingSoon = !completed && days === 0 && hours < 6;

  return (
    <div
      className={`
        border border-white px-2 py-1 rounded-lg flex justify-center text-white text-xs
        ${
          completed
            ? "bg-red-600"
            : isEndingSoon
              ? "bg-amber-500"
              : "bg-green-500"
        }
      `}
    >
      {completed ? (
        <span className="font-bold">Finished</span>
      ) : (
        <span suppressHydrationWarning={true}>
          {days > 0 ? `${days}d ` : ""}
          {zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}
        </span>
      )}
    </div>
  );
};

type Props = {
  auctionEnd: string;
};

const CountdownTimer = ({ auctionEnd }: Props) => {
  // Convert the auctionEnd string to a Date object
  const endDate = new Date(auctionEnd);

  return <Countdown date={endDate} renderer={renderer} />;
};

export default dynamic(() => Promise.resolve(CountdownTimer), {
  ssr: false,
});
