<script setup lang="ts">
import { SearchIcon } from '@lucide/vue'
import {InputGroup, InputGroupAddon, InputGroupInput} from "@/components/ui/input-group";
</script>

<template>
  <div>
    <div
        class="text-sm items-center justify-between text-muted-foreground bg-muted/5 px-4 py-2 rounded-md md:min-w-[220px] cursor-pointer hidden md:flex"
    >
      <InputGroup>
        <InputGroupInput placeholder="Search..." />
        <InputGroupAddon>
          <SearchIcon />
        </InputGroupAddon>
      </InputGroup>

    </div>
  </div>
</template>

<style scoped>

</style>