"use client";
import { usePathname, useRouter } from "next/navigation";
import { AiOutlineDeploymentUnit } from "react-icons/ai";
import { useParamsStore } from "../hooks/useParamsStore";

export default function Logo() {
  const router = useRouter();
  const pathname = usePathname();
  const reset = useParamsStore((state) => state.resetParams);

  function handleReset() {
    if (pathname !== "/") {
      router.push("/");
    }
    reset();
  }

  return (
    <div
      onClick={handleReset}
      className="flex items-center gap-2 text-3xl font-semibold text-amber-500 cursor-pointer"
    >
      <AiOutlineDeploymentUnit size={34} />
      <div>FFH Freights</div>
    </div>
  );
}
