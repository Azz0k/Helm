<script setup lang="ts">
import type {Row, TableFeatures} from '@tanstack/vue-table'

import { EllipsisIcon } from '@lucide/vue'
import { type User} from '../data/schema'
import {Button} from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger
} from "@/components/ui/dropdown-menu";

interface DataTableRowActionsProps {
  row: Row<TableFeatures, User>
}
const props = defineProps<DataTableRowActionsProps>()



</script>

<template>
    <DropdownMenu>
      <DropdownMenuTrigger as-child>
        <Button
            variant="ghost"
            class="flex h-8 w-8 p-0 data-[state=open]:bg-muted"
        >
          <EllipsisIcon class="size-4" />
          <span class="sr-only">Open menu</span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end" class="w-40">
        <DropdownMenuItem >
          Edit
        </DropdownMenuItem>

        <DropdownMenuItem>
          Delete
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
</template>
