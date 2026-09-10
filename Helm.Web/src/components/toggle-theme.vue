<script setup lang="ts">
import { useColorMode } from '@vueuse/core';
import {Button} from "@/components/ui/button";
import { MoonIcon, SunIcon } from '@lucide/vue';
const mode = useColorMode();
const handleClick = () => {
  if (mode.value === 'dark') {
    mode.value = 'light';
  }
  else {
    mode.value = 'dark';
  }
}
</script>

<template>
  <Button
      variant="outline"
      size="icon"
      aria-label="Toggle theme"
      title="Toggle theme"
      @click="handleClick"
  >
    <SunIcon class="rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0" />
    <MoonIcon class="absolute rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100" />
    <span class="sr-only">Toggle theme</span>
  </Button>
</template>

<style scoped>

</style>