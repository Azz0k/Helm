import {SidebarMenu, SidebarMenuButton} from "@/components/ui/sidebar.tsx";
import {GalleryVerticalEndIcon} from "lucide-react";
export const AppBrand = () => {
  const name = "Energo";
  const plan = "Enterprise";
  return (
    <SidebarMenu className="border-b h-14">
      <SidebarMenuButton
        size="lg"
        className="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
      >
        <div
          className="flex items-center justify-center rounded-lg aspect-square size-8 bg-sidebar-primary text-sidebar-primary-foreground"
        >
          <GalleryVerticalEndIcon className="size-4"/>
        </div>
        <div className="grid flex-1 text-sm leading-tight text-left">
          <span className="font-semibold truncate">{name}</span>
          <span className="text-xs truncate">{plan}</span>
        </div>
      </SidebarMenuButton>
    </SidebarMenu>
  )
};