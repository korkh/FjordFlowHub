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
import { AiFillTrophy } from "react-icons/ai";
import { HiCode, HiCog, HiLogout, HiTruck } from "react-icons/hi";

type UserActionsProps = {
  user: User;
};

export default function UserActions({ user }: UserActionsProps) {
  return (
    <Dropdown label={`Welcome ${user.name}`} inline color="gray">
      <DropdownHeader>
        <span className="block text-sm">Signed in as</span>
        <span className="block truncate text-sm font-medium">{user.name}</span>
      </DropdownHeader>

      <DropdownItem icon={AiFillTrophy}>
        <Link href="/freights/mytenders">Tenders won</Link>
      </DropdownItem>

      <DropdownItem icon={HiTruck}>
        <Link href="/freights/mycargo">Send my cargo</Link>
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
