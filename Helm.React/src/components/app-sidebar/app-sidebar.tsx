import {useSidebar} from "@/composables/use-sidebar.ts";
import {Sidebar, SidebarContent, SidebarFooter, SidebarHeader, SidebarRail} from "@/components/ui/sidebar.tsx";
import {AppBrand} from "@/components/app-sidebar/app-brand.tsx";
import {NavMenu} from "@/components/app-sidebar/nav-menu.tsx";
import {NavFooter} from "@/components/app-sidebar/nav-footer.tsx";

export const AppSidebar = () => {
  const {navData} = useSidebar();
  return (
    <Sidebar collapsible="icon" className="z-50">
      <SidebarHeader>
        <AppBrand />
      </SidebarHeader>
      <SidebarContent className="border-b">
        <NavMenu navMain={navData}/>
      </SidebarContent>
      <SidebarFooter>
        <NavFooter />
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
};