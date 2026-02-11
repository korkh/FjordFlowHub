import { Button } from "flowbite-react";
import { useParamsStore } from "../hooks/useParamsStore";
import Heading from "./Heading";

type Props = {
  title?: string;
  subtitle?: string;
  showReset?: boolean;
};
export default function EmptyFilter({
  title = "No results found",
  subtitle = "Try to change or remove some of your filters or search term.",
  showReset,
}: Props) {
  const reset = useParamsStore((state) => state.resetParams);
  return (
    <div className="flex flex-col items-center justify-center h-40 shadow-lg w-full">
      <Heading title={title} subtitle={subtitle} center />
      {showReset && (
        <Button outline onClick={reset}>
          Reset filters
        </Button>
      )}
    </div>
  );
}
