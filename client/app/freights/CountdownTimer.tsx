"use client";

import { TenderCounter } from "@/types";
import dynamic from "next/dynamic";
import Countdown, { zeroPad } from "react-countdown";

const renderer = ({
  days,
  hours,
  minutes,
  seconds,
  completed,
  textSize = "[10px]",
}: TenderCounter & { textSize?: string }) => {
  const isEndingSoon = !completed && days === 0 && hours < 6;

  return (
    <div
      className={`
        border border-white px-2 py-1 rounded-lg flex justify-center text-white text-${textSize} font-bold lowercase shadow-md
        ${
          completed
            ? "bg-gray-500 uppercase"
            : isEndingSoon
              ? "bg-amber-600 animate-pulse"
              : "bg-green-600"
        }
      `}
    >
      {completed ? (
        <span>Closed</span>
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

const CountdownTimer = ({
  auctionEnd,
  textSize,
}: Props & { textSize?: string }) => {
  const endDate = new Date(auctionEnd);
  return (
    <Countdown
      date={endDate}
      renderer={(props) => renderer({ ...props, textSize })}
    />
  );
};

export default dynamic(() => Promise.resolve(CountdownTimer), {
  ssr: false,
});
