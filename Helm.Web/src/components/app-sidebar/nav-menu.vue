<script setup lang="ts">
import type { NavGroup } from './app-sidebar.types.ts'
import NavMenuCollapsible from './nav-menu-collapsible.vue'

const { navMain } = defineProps<{
  navMain: NavGroup[]
}>();

</script>

<template>
  <NavMenuCollapsible :nav-main="navMain" />
</template>

<style scoped>

</style>