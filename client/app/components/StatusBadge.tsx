import { Freight } from "@/types";

type Props = {
  status: Freight["status"];
  position?:
    | "top-right"
    | "bottom-left"
    | "top-left"
    | "bottom-right"
    | "center"
    | "inline";
  fixed?: boolean; // Если true, используем absolute позиционирование
};

export default function StatusBadge({
  status,
  position = "top-right",
  fixed = true,
}: Props) {
  // English: Mapping of positions to Tailwind CSS classes
  const positionClasses: Record<string, string> = {
    "top-right": "top-2 right-2",
    "top-left": "top-2 left-2",
    "bottom-right": "bottom-2 right-2",
    "bottom-left": "bottom-2 left-2",
    center: "top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2",
    inline: "static",
  };

  // Determine container style based on 'fixed' and 'position' props
  const containerClasses = fixed
    ? `absolute ${positionClasses[position] || positionClasses["top-right"]}`
    : "relative inline-block";

  return (
    <div className={`z-10 ${containerClasses}`}>
      {status === "ReserveNotMet" && (
        <div className="bg-amber-600 text-white px-2 py-1 rounded text-[10px] font-bold uppercase shadow-md whitespace-nowrap">
          Reserve Not Met
        </div>
      )}

      {status === "Finished" && (
        <div className="bg-red-600 text-white px-2 py-1 rounded text-[10px] font-bold uppercase shadow-md whitespace-nowrap">
          Finished
        </div>
      )}

      {status === "Live" && (
        <div className="bg-green-500/20 text-green-400 text-[10px] px-3 py-1 rounded-full border border-green-500/30 uppercase font-bold whitespace-nowrap">
          Tender is Active
        </div>
      )}
    </div>
  );
}
