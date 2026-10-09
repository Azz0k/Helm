import type {NavGroup} from "./app-sidebar.types.ts";
import {SidebarGroup, SidebarGroupLabel, SidebarMenu} from "@/components/ui/sidebar.tsx";
import {NavMenuItem} from "@/components/app-sidebar/nav-menu-item.tsx";

type NavMenuProps = {
  navMain:NavGroup[];
}
export const NavMenu = ({
  navMain,
                        }:NavMenuProps) => {
  const elements = navMain.map((item: NavGroup) => {
    return (
      <SidebarGroup key={item.title}>
        <SidebarGroupLabel>{item.title}</SidebarGroupLabel>
        <SidebarMenu>
          <NavMenuItem navItems={item.items}/>
        </SidebarMenu>
      </SidebarGroup>
    )
  })
  return (
    <>
      {elements}
    </>
  );
}