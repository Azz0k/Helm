<script setup lang="ts">
import {
  Sidebar,
  SidebarContent, SidebarFooter, SidebarHeader, SidebarRail,
} from '@/components/ui/sidebar'
import {useSidebar} from "@/composables/use-sidebar.ts";
import type {SidebarData, User} from "./app-sidebar.types.ts";
import NavMenu from "@/components/app-sidebar/nav-menu.vue";
import AppBrand from "@/components/app-sidebar/app-brand.vue";
import NavFooter from "@/components/app-sidebar/nav-footer.vue";

const user: User = {
  name: 'shadcn',
  email: 'm@example.com',
};
const { navData } = useSidebar()
const sidebarData: SidebarData = {
  user,
  navMain: navData.value!,
}
</script>

<template>
  <Sidebar collapsible="icon" class="z-50">
    <SidebarHeader>
      <AppBrand />
    </SidebarHeader>
    <SidebarContent  class = "border-b">
      <NavMenu :nav-main="sidebarData.navMain" />
    </SidebarContent>
    <SidebarFooter>
      <NavFooter :user="user" />
    </SidebarFooter>
    <SidebarRail />
  </Sidebar>
</template>
