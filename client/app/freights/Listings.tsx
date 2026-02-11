"use client";

import { Freight, PagedResult } from "@/types";
import queryString from "query-string";
import { useEffect, useState } from "react";
import { useShallow } from "zustand/shallow";
import { getData } from "../actions/auctionActions";
import AppPagination from "../components/AppPagination";
import EmptyFilter from "../components/EmptyFilter";
import { useParamsStore } from "../hooks/useParamsStore";
import Filters from "./Filters";
import FreightCard from "./FreightCard";

export default function Listings() {
  const [data, setData] = useState<PagedResult<Freight>>();
  const params = useParamsStore(
    useShallow((state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy,
    })),
  );

  const setParams = useParamsStore((state) => state.setParams);
  const url = queryString.stringifyUrl(
    {
      url: "",
      query: params,
    },
    { skipEmptyString: true, skipNull: true },
  );

  function setPageNumber(pageNumber: number) {
    setParams({ pageNumber });
  }

  useEffect(() => {
    getData(url).then((data) => {
      setData(data);
    });
  }, [url]);

  if (!data) {
    return <h3>Loading...</h3>;
  }

  return (
    <>
      <Filters />
      {data.totalCount === 0 ? (
        <div className="flex items-center justify-center h-40">
          <EmptyFilter showReset />
        </div>
      ) : (
        <div className="grid grid-cols-4 gap-6">
          {data.results.map((freight) => (
            <FreightCard key={freight.id} freight={freight} />
          ))}
        </div>
      )}
      {data.pageCount > 0 && (
        <div className="flex justify-center mt-4 ">
          <AppPagination
            pageChange={setPageNumber}
            currentPage={params.pageNumber}
            pageCount={data.pageCount}
          />
        </div>
      )}
    </>
  );
}
