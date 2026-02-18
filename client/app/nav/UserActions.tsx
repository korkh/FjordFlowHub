"use client";

import {
  Dropdown,
  DropdownDivider,
  DropdownHeader,
  DropdownItem,
} from "flowbite-react";
import { User } from "next-auth";
import { signOut } from "next-auth/react";
import Link from "next/link";
import { usePathname, useRouter } from "next/navigation";
import { AiFillTrophy } from "react-icons/ai";
import { HiCode, HiCog, HiLogout, HiTruck, HiUser } from "react-icons/hi";
import { useParamsStore } from "../hooks/useParamsStore";

type UserActionsProps = {
  user: User;
};

export default function UserActions({ user }: UserActionsProps) {
  const router = useRouter();
  const pathname = usePathname();
  const setParams = useParamsStore((state) => state.setParams);

  function setWinner() {
    setParams({ winner: user.username, seller: undefined });
    if (pathname !== "/") router.push("/");
  }

  function setSeller() {
    setParams({ seller: user.username, winner: undefined });
    if (pathname !== "/") router.push("/");
  }

  return (
    <Dropdown label={`Welcome ${user.name}`} inline color="gray">
      <DropdownHeader>
        <span className="block text-sm">Signed in as</span>
        <span className="block truncate text-sm font-medium">{user.name}</span>
      </DropdownHeader>

      <DropdownItem icon={HiUser} onClick={setSeller}>
        My Tenders
      </DropdownItem>

      <DropdownItem icon={AiFillTrophy} onClick={setWinner}>
        Tenders won
      </DropdownItem>

      <DropdownItem icon={HiTruck}>
        <Link href="/freights/create">Send my cargo</Link>
      </DropdownItem>

      {/* This is only visible in development */}
      {process.env.NODE_ENV === "development" && (
        <DropdownItem icon={HiCode}>
          <Link href="/session">My Session (Dev Only)</Link>
        </DropdownItem>
      )}

      <DropdownItem icon={HiCog}>
        <Link href="/settings">Settings</Link>
      </DropdownItem>

      <DropdownDivider />

      <DropdownItem
        icon={HiLogout}
        onClick={() => signOut({ redirectTo: "/" })}
      >
        Sign out
      </DropdownItem>
    </Dropdown>
  );
}
