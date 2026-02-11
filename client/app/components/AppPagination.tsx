"use client";
import { Pagination } from "flowbite-react";

type Props = {
  currentPage: number;
  pageCount: number;
  pageChange: (page: number) => void;
};
export default function AppPagination({
  currentPage,
  pageCount,
  pageChange,
}: Props) {
  if (pageCount <= 0) return null;
  return (
    <Pagination
      currentPage={currentPage}
      totalPages={pageCount}
      layout="pagination"
      showIcons
      previousLabel="Go back"
      nextLabel="Go forward"
      className="text-blue-500 mb-5"
      onPageChange={(e) => pageChange(e)}
    />
  );
}
