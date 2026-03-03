"use client";

import queryString from "query-string";
import { useEffect, useState } from "react"; // Добавляем useEffect
import { useShallow } from "zustand/shallow";
import { getData } from "../actions/auctionActions";
import AppPagination from "../components/AppPagination";
import EmptyFilter from "../components/EmptyFilter";
import { useParamsStore } from "../hooks/useParamsStore";
import { useTenderStore } from "../hooks/useTenderStore"; // Импорт твоего нового стора
import Filters from "./Filters";
import FreightCard from "./FreightCard";

export default function Listings() {
  const [loading, setLoading] = useState(true);
  // 1. Filter params
  const params = useParamsStore(
    useShallow((state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy,
      seller: state.seller,
      winner: state.winner,
    })),
  );

  // 2. Get data from Zustand
  const { freights, totalCount, pageCount, setData } = useTenderStore(
    useShallow((state) => ({
      freights: state.freights,
      totalCount: state.totalCount,
      pageCount: state.pageCount,
      setData: state.setData,
    })),
  );

  const url = queryString.stringifyUrl(
    { url: "", query: params },
    { skipEmptyString: true, skipNull: true },
  );

  const setParams = useParamsStore((state) => state.setParams);
  function setPageNumber(pageNumber: number) {
    setParams({ pageNumber });
  }

  // 4. Sinchronize Zustand with React Query
  useEffect(() => {
    getData(url).then((data) => {
      setData(data);
      setLoading(false);
    });
  }, [url, setData]);

  if (loading) return <h3>Loading...</h3>;

  return (
    <>
      <Filters />
      {totalCount === 0 ? (
        <div className="flex items-center justify-center h-40">
          <EmptyFilter showReset />
        </div>
      ) : (
        <div className="grid grid-cols-4 gap-6">
          {freights.map((freight) => (
            <FreightCard key={freight.id} freight={freight} />
          ))}
        </div>
      )}
      {pageCount > 0 && (
        <div className="flex justify-center mt-4 ">
          <AppPagination
            pageChange={setPageNumber}
            currentPage={params.pageNumber}
            pageCount={pageCount}
          />
        </div>
      )}
    </>
  );
}
