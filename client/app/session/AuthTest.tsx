"use client";

import { Button, Spinner } from "flowbite-react";
import { useState } from "react";
import { updateFreightTest } from "../actions/auctionActions";

export default function AuthTest() {
  const [isLoading, setIsLoading] = useState(false);
  const [result, setResult] = useState<{
    status: number;
    message: string;
  } | null>(null);

  function handleUpdate() {
    setIsLoading(true);
    setResult(null);
    updateFreightTest()
      .then((data) => {
        setResult(data);
      })
      .catch((error) => {
        setResult(error);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }
  return (
    <div className="flex items-center gap-4">
      <Button outline onClick={handleUpdate} disabled={isLoading}>
        {isLoading ? (
          <Spinner size="sm" className="me-3" light />
        ) : (
          "Test authentication"
        )}
      </Button>
      <div>{JSON.stringify(result, null, 2)}</div>
    </div>
  );
}
