import type { ColumnDef, TableFeatures } from '@tanstack/vue-table'
import type {Component} from "vue";

export interface FacetedFilterOption {
  label: string
  value: string
  icon?: Component
}

export interface ServerPagination {
  page: number
  pageSize: number
  total: number
  onPageChange: (page: number) => void
  onPageSizeChange: (pageSize: number) => void
}

export interface DataTableProps<T extends TableFeatures> {
  loading?: boolean
  columns: ColumnDef<T, any>[]
  data: T[]
  serverPagination?: ServerPagination
}
