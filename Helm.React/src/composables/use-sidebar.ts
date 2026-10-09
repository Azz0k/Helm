import type {NavGroup} from "@/components/app-sidebar/app-sidebar.types.ts";
import { BadgeHelpIcon, BoxesIcon, LayoutDashboardIcon, UsersIcon } from "lucide-react";


export function useSidebar() {
  const navData:NavGroup[] = [
    {
      title: "General",
      items: [
        { title: "Dashboard", url: "/", icon: LayoutDashboardIcon },
        { title: "Help Center", url: "/help-center", icon: BadgeHelpIcon },
        { title: "Apps", url: "/apps", icon: BoxesIcon },
        { title: "Users", url: "/users", icon: UsersIcon },

      ],
    }];
  return {
    navData,
  };
}