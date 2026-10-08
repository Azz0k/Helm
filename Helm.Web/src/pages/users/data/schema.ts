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
import {z as zod} from "zod";

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


export const UserSchema = zod.object({
  id:zod.number(),
  login:zod.string(),
  name:zod.string(),
  enabled:zod.boolean(),
  roles:zod.array(zod.number()),
});

export type User = zod.infer<typeof UserSchema>;

export const userListSchema = zod.array(UserSchema);