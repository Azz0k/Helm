import {
  columnFilteringFeature,
  columnVisibilityFeature,
  createFilteredRowModel,
  createPaginatedRowModel,
  createSortedRowModel,
  filterFn_includesString,
  globalFilteringFeature,
  rowPaginationFeature,
  rowSelectionFeature,
  rowSortingFeature,
  sortFn_alphanumeric,
  sortFn_text,
  tableFeatures
} from "@tanstack/vue-table";

export const features = tableFeatures({
  columnVisibilityFeature,
  rowSortingFeature,
  rowSelectionFeature,
  rowPaginationFeature,
  columnFilteringFeature,
  globalFilteringFeature,
  sortedRowModel: createSortedRowModel(),
  paginatedRowModel: createPaginatedRowModel(),
  filteredRowModel: createFilteredRowModel(),
  sortFns: {
    alphanumeric: sortFn_alphanumeric,
    text: sortFn_text,
  },
  filterFns: {
    includesString: filterFn_includesString,
  },
});

export interface User {
  Id:number
  Login:string
  Name:string
  Enabled:boolean
  Roles:string[]
}