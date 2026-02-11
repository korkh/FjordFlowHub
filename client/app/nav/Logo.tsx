"use client";
import { AiOutlineDeploymentUnit } from "react-icons/ai";
import { useParamsStore } from "../hooks/useParamsStore";

export default function Logo() {
  const reset = useParamsStore((state) => state.resetParams);

  return (
    <div
      onClick={reset}
      className="flex items-center gap-2 text-3xl font-semibold text-amber-500 cursor-pointer"
    >
      <AiOutlineDeploymentUnit size={34} />
      <div>FFH Freights</div>
    </div>
  );
}
