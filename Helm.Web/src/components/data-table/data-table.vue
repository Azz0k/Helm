<script setup lang="ts" generic="T extends RowData">
import {
  type ColumnDef, type RowData,
  type TableFeatures,
  useTable, FlexRender,
} from '@tanstack/vue-table';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import DataTableLoading from './table-loading.vue';
import DataTablePagination from './table-pagination.vue';
import NoResultFound from "@/components/no-result-found.vue";

const props = defineProps<{
  features: TableFeatures,
  columns: ColumnDef<TableFeatures, T>[],
  data: T[],
}>();

const loading = false;
const table = useTable(
    {
      debugTable: true,
      features: props.features,
      columns:props.columns,
      data:props.data,
    },
)
</script>

<template>
  <div class="space-y-4">
    <slot name="toolbar" />

    <div class="border rounded-md">
      <Table>
        <TableHeader>
          <TableRow v-for="headerGroup in table.getHeaderGroups()" :key="headerGroup.id">
            <TableHead
                v-for="header in headerGroup.headers"
                :key="header.id"
            >
              <FlexRender v-if="!header.isPlaceholder" :render="header.column.columnDef.header" :props="header.getContext()" />
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody v-if="!loading">
          <template v-if="table.getRowModel().rows?.length">
            <TableRow
                v-for="row in table.getRowModel().rows"
                :key="row.id"
                :data-state="row.getIsSelected() && 'selected'"
            >
              <TableCell
                  v-for="cell in row.getVisibleCells()"
                  :key="cell.id"
              >
                <FlexRender :render="cell.column.columnDef.cell" :props="cell.getContext()" />
              </TableCell>
            </TableRow>
          </template>

          <TableRow v-else>
            <TableCell
                :colspan="columns.length"
                class="h-24 text-center"
            >
              <NoResultFound />
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
      <DataTableLoading v-if="loading" />
    </div>

    <DataTablePagination />
  </div>
</template>
