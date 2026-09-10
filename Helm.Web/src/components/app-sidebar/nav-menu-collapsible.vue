<script setup lang="ts">
import {SidebarGroup, SidebarGroupLabel, SidebarMenu, SidebarMenuItem} from "@/components/ui/sidebar";
import type {NavGroup, NavItem} from "@/components/app-sidebar/app-sidebar.types.ts";
import MenuButton from "@/components/app-sidebar/menu-button.vue";
import { useRoute } from "vue-router";


  const { navMain } = defineProps<{
    navMain: NavGroup[]
  }>();
  const route = useRoute();
  const isActive = (menuItem: NavItem) => {
    if (menuItem.url) {
      return menuItem.url === route.path;
    }
    return false;
  }
</script>

<template>
 <SidebarGroup v-for="group in navMain" :key="group.title">
   <SidebarGroupLabel>{{ group.title }}</SidebarGroupLabel>
   <SidebarMenu>
     <template v-for="menu in group.items" :key="menu.title">
      <SidebarMenuItem v-if="!menu.items">
        <MenuButton
            :is-active="isActive(menu)"
            :tooltip="menu.title"
            :is-external-url="false"
            :menu="menu as NavItem"
        />
      </SidebarMenuItem>
     </template>
   </SidebarMenu>
 </SidebarGroup>
</template>

<style scoped>

</style>