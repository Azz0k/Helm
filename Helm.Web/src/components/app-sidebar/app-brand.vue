<script setup lang="ts">

import {SidebarMenu, SidebarMenuButton} from "@/components/ui/sidebar";
import {GalleryVerticalEndIcon} from "@lucide/vue";
  const name = "Energo";
  const plan = "Enterprise";
</script>

<template>
  <SidebarMenu class = "border-b h-14">
    <SidebarMenuButton
      size="lg"
      class="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
    >
      <div
          class="flex items-center justify-center rounded-lg aspect-square size-8 bg-sidebar-primary text-sidebar-primary-foreground"
      >
        <component :is="GalleryVerticalEndIcon" class="size-4" />
      </div>
      <div class="grid flex-1 text-sm leading-tight text-left">
        <span class="font-semibold truncate">{{ name }}</span>
        <span class="text-xs truncate">{{ plan }}</span>
      </div>
    </SidebarMenuButton>
  </SidebarMenu>
</template>

<style scoped>

</style>