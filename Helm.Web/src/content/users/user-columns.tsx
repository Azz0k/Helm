import '@tanstack/react-table';
import {type ColumnDef} from "@tanstack/react-table";
import {BadgeCheck, BadgeXIcon,} from "lucide-react"
import {Badge} from "@/components/ui/badge"
import {UserActions} from "@/content/users/components/user-actions.tsx";

export type User = {
  id: number;
  Login: string;
  Name: string;
  isAdmin: boolean;
  enabled: boolean;
};


export const userColumns: ColumnDef<User>[] = [
  {
    accessorKey: "login",
    header: "Логин",
    meta: {headerClassName: "w-1/7",},
  },
  {
    accessorKey: "adLogin",
    header: "Логин в АД",
    meta: {headerClassName: "w-1/7",},
  },
  {
    accessorKey: "name",
    header: "ФИО",
    meta: {headerClassName: "w-2/7",},
  },

  {
    header: "Администратор",
    meta: {headerClassName: "w-1/7",},
    cell: ({row}) => {
      const user = row.original;
      return (
        <>
          <Badge variant="secondary">
            {user.isAdmin ? <BadgeCheck data-icon="inline-start"/> : <BadgeXIcon data-icon="inline-start"/>}
            {user.isAdmin ? "Да" : "Нет"}
          </Badge>
        </>
      );
    }
  },
  {
    header: "Включен",
    meta: {headerClassName: "w-1/7",},
    cell: ({row}) => {
      const user = row.original;
      return (
        <>
          <Badge variant="secondary">
            {user.enabled ? <BadgeCheck data-icon="inline-start"/> : <BadgeXIcon data-icon="inline-start"/>}
            {user.enabled ? "Да" : "Нет"}
          </Badge>
        </>
      );
    }
  },
  {
    id: "actions",
    header: "Действия",
    cell: ({row}) => {
      const user = row.original;
      return (
        <UserActions user={user}/>
      )
    },
    meta: {headerClassName: "w-1/7",}
  },
];

