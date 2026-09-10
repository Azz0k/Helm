<script setup lang="ts">
import { cn } from '@/lib/utils'
import {SidebarInset, SidebarProvider, SidebarSeparator, SidebarTrigger} from "@/components/ui/sidebar";
import AppSidebar from "@/components/app-sidebar/app-sidebar.vue";
import {useThemeStore} from "@/stores/theme-store.ts";
import {storeToRefs} from "pinia";
import SearchPanel from "@/components/search-panel/search-panel.vue";
import ToggleTheme from "@/components/toggle-theme.vue";

const themeStore = useThemeStore()
const { contentLayout } = storeToRefs(themeStore)
</script>

<template>
  <SidebarProvider>
    <AppSidebar />
      <SidebarInset
          class="w-full max-w-full peer-data-[state=collapsed]:w-[calc(100%-var(--sidebar-width-icon)-1rem)] peer-data-[state=expanded]:w-[calc(100%-var(--sidebar-width))]"
      >
        <header
            class="flex items-center gap-3 sm:gap-4 h-16 p-4 shrink-0 transition-[width,height] ease-linear border-b"
        >
          <SidebarTrigger  class="-ml-1" />
          <SidebarSeparator orientation="vertical" class="h-6"  />
          <SearchPanel />
          <div class="flex-1" />
          <ToggleTheme />
        </header>
        <main
            :class="cn(
              'p-4 grow',
              contentLayout === 'centered' ? 'container mx-auto ' : '',
            )"
        >
          <router-view />
        </main>
    </SidebarInset>
  </SidebarProvider>
</template>

<style scoped>

</style>