"use client";
import Image from "next/image";
import { useState } from "react";

type Props = {
  imageUrl: string;
  isFinished?: boolean;
};
export default function CargoImage({ imageUrl, isFinished }: Props) {
  const [isLoading, setIsLoading] = useState(true);
  return (
    <>
      <Image
        alt="image of a freight"
        src={imageUrl}
        fill
        className={`object-cover duration-700 ease-in-out ${
          isLoading
            ? "scale-110 blur-xl grayscale"
            : "scale-100 blur-0 grayscale-0"
        } ${isFinished ? "opacity-70 grayscale-40" : ""}`}
        priority
        sizes="(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 25vw"
        onLoad={() => setIsLoading(false)}
      />
    </>
  );
}
