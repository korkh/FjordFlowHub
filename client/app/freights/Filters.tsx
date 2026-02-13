"use client";

import { Button, ButtonGroup } from "flowbite-react";
import { AiOutlineClockCircle, AiOutlineSortAscending } from "react-icons/ai";
import { BsFillStopCircleFill, BsStopwatchFill } from "react-icons/bs";
import { GiFinishLine, GiWeight } from "react-icons/gi"; // Добавила иконку веса
import { useParamsStore } from "../hooks/useParamsStore";

const pageSizeButtons = [4, 8, 12];

const orderData = [
  { label: "Auction End", value: "auctionEnd", icon: AiOutlineClockCircle },
  { label: "Recently added", value: "new", icon: BsFillStopCircleFill },
  { label: "Budget", value: "price", icon: AiOutlineSortAscending },
  { label: "Weight", value: "weight", icon: GiWeight },
];

const filterData = [
  { label: "Live", value: "live", icon: BsStopwatchFill },
  { label: "Ending Soon", value: "endingSoon", icon: GiFinishLine },
  { label: "Finished", value: "finished", icon: BsFillStopCircleFill },
  { label: "All", value: "all", icon: BsStopwatchFill },
];

export default function Filters() {
  const pageSize = useParamsStore((state) => state.pageSize);
  const setParams = useParamsStore((state) => state.setParams);
  const orderBy = useParamsStore((state) => state.orderBy);
  const filterBy = useParamsStore((state) => state.filterBy);

  return (
    <div className="flex justify-between items-center mb-4">
      <div>
        <span className="uppercase text-sm text-gray-500 mr-2">Filter by</span>
        <ButtonGroup outline>
          {filterData.map(({ label, value, icon: Icon }) => (
            <Button
              key={value}
              onClick={() => setParams({ filterBy: value })}
              color={`${filterBy === value ? "yellow" : "gray"}`}
              size="sm"
            >
              <Icon className="mr-2 h-4 w-4" />
              {label}
            </Button>
          ))}
        </ButtonGroup>
      </div>

      <div>
        <span className="uppercase text-sm text-gray-500 mr-2">Order by</span>
        <ButtonGroup outline>
          {orderData.map(({ label, value, icon: Icon }) => (
            <Button
              key={value}
              onClick={() => setParams({ orderBy: value })}
              color={`${orderBy === value ? "yellow" : "gray"}`}
              size="sm"
            >
              <Icon className="mr-2 h-4 w-4" />
              {label}
            </Button>
          ))}
        </ButtonGroup>
      </div>

      <div>
        <span className="uppercase text-sm text-gray-500 mr-2">Page size</span>
        <ButtonGroup outline>
          {pageSizeButtons.map((value, index) => (
            <Button
              key={index}
              onClick={() => setParams({ pageSize: value })}
              color={`${value === pageSize ? "yellow" : "gray"}`}
              className="focus:ring-0"
              size="sm"
            >
              {value}
            </Button>
          ))}
        </ButtonGroup>
      </div>
    </div>
  );
}
