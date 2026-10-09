import type {NavItem} from "@/components/app-sidebar/app-sidebar.types.ts";
import {SidebarMenuItem} from "@/components/ui/sidebar.tsx";
import {MenuButton} from "@/components/app-sidebar/menu-button.tsx";

type NavMenuItemProps = {
  navItems: NavItem[];
}
export const NavMenuItem = ({
     navItems,
                            }:NavMenuItemProps)=>{
  const elements = navItems.map(item=>{
    return (
      <div key={item.title}>
        {
          !item.items && (
            <SidebarMenuItem>
              <MenuButton
                isActive={false}
                tooltip={item.title}
                isExternalUrl={false}
                menu={item as NavItem}
              />
            </SidebarMenuItem>
          )
        }
      </div>
    );
  })
  return (
    <>
      {elements}
    </>
  )
};