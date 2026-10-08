import {
  createColumnHelper, type TableFeatures,
} from "@tanstack/vue-table";
import { h } from "vue";
import { type User } from "../data/schema.ts";
import Badge from "@/components/ui/badge/Badge.vue"
import UsersDataTableRowActions from "./users-data-table-row-actions.vue";
import type {FacetedFilterOption} from "@/components/data-table/types.ts";

const statuses = new Map();
statuses.set(true, "Active");
statuses.set(false, "Disabled");
const callTypes: (FacetedFilterOption & { style: string })[] = [
  {
    label: "Active",
    value: "Active",
    style: "bg-teal-100/30 text-teal-900 dark:text-teal-200 border-teal-200",
  },
  {
    label: "Disabled",
    value: "Disabled",
    style: "bg-neutral-300/40 border-neutral-300",
  },
];

const errorCallType: (FacetedFilterOption & {  style: string}) ={
  label: "Error",
  value: "Error",
  style: "bg-destructive/10 dark:bg-destructive/50 text-destructive dark:text-primary border-destructive/10",
}

//const columnHelper = createColumnHelper<typeof features, User>();
const columnHelper = createColumnHelper<TableFeatures, User>();
export const columns = columnHelper.columns([
  columnHelper.accessor("login", {
    header: "Login",
    cell: (info)=>info.getValue(),
  }),
  columnHelper.accessor("name", {
    header: "Name",
    cell: (info)=>info.getValue(),
  }),
  columnHelper.accessor("enabled", {
    header: "Status",
    cell: (info)=>{
      const text = statuses.get(info.getValue());
      const callType = callTypes.find(c=>c.value===text) ?? errorCallType;
      return h(Badge, { class: `${callType.style || ""}`, variant: "outline" }, () => callType.label);
    },
  }),
  columnHelper.accessor("roles", {
    header: "Roles",
    cell: (info)=>{
      return info.getValue().join(",");
    },
  }),
  columnHelper.display({
    id:"actions",
    cell: ( props ) => h(UsersDataTableRowActions,  props ),
  })
]);
