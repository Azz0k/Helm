import type {NavItem} from "@/components/app-sidebar/app-sidebar.types.ts";
import {SidebarMenuButton} from "@/components/ui/sidebar.tsx";
import {ExternalLinkIcon} from "lucide-react";
import {Link} from "@tanstack/react-router";

type MenuButtonProps = {
  isActive: boolean;
  tooltip?: string;
  isExternalUrl?: boolean;
  menu: NavItem;
}
export const MenuButton = (
  {
    isActive,
    tooltip,
    isExternalUrl,
    menu
  }: MenuButtonProps
) =>{
 return (
   <SidebarMenuButton
     isActive={isActive}
     tooltip={tooltip}
   >
     {
       isExternalUrl ?
         (
           <a href={menu.url} target="_blank" rel="noopener noreferrer" className="flex items-center gap-2">
             {menu.icon && <menu.icon />}
             <span>{ menu.title }</span>
             <ExternalLinkIcon className="w-4 h-4 ml-auto" />
           </a>
         ) : (
           <Link to={menu.url} className="flex items-center gap-2">
             {menu.icon && <menu.icon />}
             <span>{ menu.title }</span>
           </Link>
         )
     }
   </SidebarMenuButton>
 );
}