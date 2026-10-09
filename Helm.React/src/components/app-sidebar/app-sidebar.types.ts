import type { LucideIcon } from "lucide-react";

interface BaseNavItem {
  title: string
  icon?: LucideIcon
}
export type NavItem
  = | BaseNavItem & {
  items: (BaseNavItem & { url?: string })[]
  url?: never
  isActive?: boolean
} | BaseNavItem & {
  url: string
  items?: never
}

export interface NavGroup {
  title: string
  items: NavItem[]
}

export interface User {
  name: string | undefined
  email: string | undefined
}

export interface SidebarData {
  user: User
  navMain: NavGroup[]
}

