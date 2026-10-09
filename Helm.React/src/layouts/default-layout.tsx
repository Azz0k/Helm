import {SidebarInset, SidebarProvider, SidebarSeparator, SidebarTrigger} from "@/components/ui/sidebar.tsx";
import {AppSidebar} from "@/components/app-sidebar/app-sidebar.tsx";
import {Outlet, } from "@tanstack/react-router";
import {SearchPanel} from "@/components/search-panel/search-panel.tsx";
import {ToggleTheme} from "@/components/toggle-theme/toggle-theme.tsx";


export const DefaultLayout = () => {
  return (
    <SidebarProvider>
      <AppSidebar />
      <SidebarInset
        className="w-full max-w-full peer-data-[state=collapsed]:w-[calc(100%-var(--sidebar-width-icon)-1rem)] peer-data-[state=expanded]:w-[calc(100%-var(--sidebar-width))]"
      >
        <header
          className="flex items-center gap-3 sm:gap-4 h-16 p-4 shrink-0 transition-[width,height] ease-linear border-b"
        >
          <SidebarTrigger  className="-ml-1" />
          <SidebarSeparator orientation="vertical" className="h-6"  />
          <SearchPanel />
          <div className="flex-1" />
          <ToggleTheme />
        </header>
        <main
         className="cn('p-4 grow',contentLayout === 'centered' ? 'container mx-auto ' : '',)"
        >
          <Outlet />

      </main>
    </SidebarInset>

  </SidebarProvider>
  );
}