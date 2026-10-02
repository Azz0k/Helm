<script setup lang="ts">
import {features, type User} from "../data/schema.ts";
import  DataTable  from '@/components/data-table/data-table.vue'
import  { columns} from "@/pages/users/components/columns.ts";

const props = defineProps<{
  loading: boolean;
  data: User[]
}>();


</script>

<template>
  <DataTable :features :data="props.data" :loading="props.loading" :columns />
</template>