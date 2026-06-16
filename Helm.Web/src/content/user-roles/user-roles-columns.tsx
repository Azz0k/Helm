import '@tanstack/react-table';
import {type ColumnDef} from "@tanstack/react-table";
import {UserRoleActions} from "@/content/user-roles/components/user-role-actions.tsx";


export type UserRole = {
  id: number;
  name: string;
  description: string;

};

export const userRoleColumns:ColumnDef<UserRole>[] = [
  {
    accessorKey: "name",
    header: "Название",
    meta: {headerClassName: "w-1/7", },
  },
  {
    accessorKey: "description",
    header: "Описание роли",
    meta: {headerClassName: "w-5/7", },
  },

  {
    id: "actions",
    header: "Действия",
    cell: ({ row }) => {
      const userRole = row.original;
      return (
        <UserRoleActions userRole={userRole}/>
      )
    },
    meta: {headerClassName: "w-1/7", }
  },
];

