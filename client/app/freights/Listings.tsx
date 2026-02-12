"use client";

import { useQuery } from "@tanstack/react-query";
import queryString from "query-string";
import { useShallow } from "zustand/shallow";
import { getData } from "../actions/auctionActions";
import AppPagination from "../components/AppPagination";
import EmptyFilter from "../components/EmptyFilter";
import { useParamsStore } from "../hooks/useParamsStore";
import Filters from "./Filters";
import FreightCard from "./FreightCard";

export default function Listings() {
  const params = useParamsStore(
    useShallow((state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy,
    })),
  );

  const url = queryString.stringifyUrl(
    {
      url: "",
      query: params,
    },
    { skipEmptyString: true, skipNull: true },
  );

  const setParams = useParamsStore((state) => state.setParams);
  function setPageNumber(pageNumber: number) {
    setParams({ pageNumber });
  }

  const { data, isLoading, isFetching, isError } = useQuery({
    queryKey: ["freights", url], // every time url changes, the query will refetch
    queryFn: () => getData(url),
    placeholderData: (previousData) => previousData,
  });

  if (isLoading) return <h3>Loading...</h3>;
  if (isError) return <h3>Error loading freights. Please try again later.</h3>;

  return (
    <>
      <Filters />
      {data && data.totalCount === 0 ? (
        <div className="flex items-center justify-center h-40">
          <EmptyFilter showReset />
        </div>
      ) : (
        <div className="grid grid-cols-4 gap-6">
          {data &&
            data.results.map((freight) => (
              <FreightCard key={freight.id} freight={freight} />
            ))}
        </div>
      )}
      {data && data.pageCount > 0 && (
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
