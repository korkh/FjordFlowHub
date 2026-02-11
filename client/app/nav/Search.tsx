"use client";

import { ChangeEvent, useEffect, useState } from "react";
import { FaSearch } from "react-icons/fa";
import { useParamsStore } from "../hooks/useParamsStore";

export default function Search() {
  const setParams = useParamsStore((state) => state.setParams);
  const searchTerm = useParamsStore((state) => state.searchTerm);
  const [searchValue, setSearchValue] = useState(searchTerm);

  useEffect(() => {
    setSearchValue(searchTerm);
  }, [searchTerm]);

  function handleChange(event: ChangeEvent<HTMLInputElement>) {
    setSearchValue(event.target.value);
  }

  function handleSearch() {
    setParams({ searchTerm: searchValue });
  }

  return (
    <div className="flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm bg-white focus-within:border-amber-500 transition-all duration-300">
      <input
        onKeyDown={(e) => {
          if (e.key === "Enter") handleSearch();
        }}
        value={searchValue}
        onChange={handleChange}
        type="text"
        placeholder="Search freights by description, pickup or delivery city"
        className="
          grow
          pl-5
          bg-transparent
          focus:outline-none
          border-none
          text-sm
          text-gray-600
        "
      />
      <button
        onClick={handleSearch}
        className="bg-amber-500 text-white rounded-full p-2 mx-2 hover:bg-amber-600 transition-colors"
      >
        <FaSearch size={18} />
      </button>
    </div>
  );
}
