import '@tanstack/react-table';
import {type ColumnDef} from "@tanstack/react-table";
import {BadgeCheck, BadgeXIcon,} from "lucide-react"
import {Badge} from "@/components/ui/badge"
import {UserActions} from "@/content/users/components/user-actions.tsx";
import {userStore} from "@/content/users/user-store.ts";

export type User = {
  id: number;
  login: string;
  name: string;
  roles: number[];
  enabled: boolean;
};

export const userColumns: ColumnDef<User>[] = [
  {
    accessorKey: "login",
    header: "Логин",
    meta: {headerClassName: "w-1/7",},
  },
  {
    accessorKey: "name",
    header: "ФИО",
    meta: {headerClassName: "w-1/7",},
  },
  {
    header: "Назначенные роли",
    meta: {headerClassName: "w-3/7",},
    accessorKey: "roles",
    cell: ({row}) => {
      const user = row.original;
      const textRoles = user.roles.map(userRoleId=>userStore.roleData.find(role=>role.id === userRoleId)?.name);
      return (
        <>
          {textRoles.join(", ")}
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

