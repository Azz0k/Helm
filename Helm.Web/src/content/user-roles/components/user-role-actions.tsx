import {observer} from "mobx-react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {Button} from "@/components/ui/button.tsx";
import {MoreHorizontal} from "lucide-react";
import React from "react";
import {userRoleStore} from "@/content/user-roles/user-role-store.ts";
import {ActionConfirmationDialog} from "@/components/action-confirmation-dialog.tsx";
import {EditUserRoleDialog} from "@/content/user-roles/components/edit-user-role-dialog.tsx";
import type {UserRole} from "@/content/user-roles/user-roles-columns.tsx";

type UserRoleActionsProps = {
  userRole:UserRole;
}
export const UserRoleActions = observer(({userRole}:UserRoleActionsProps)=>{
  const [open, SetOpen] = React.useState(false);
  const cancelAction  = ()=>{
    userRoleStore.handleCancelAction();
    SetOpen(false);
  };
  return (
    <DropdownMenu open={open} onOpenChange={SetOpen}>
      <DropdownMenuTrigger asChild>
        <Button variant="ghost" className="h-8 w-8 p-0">
          <span className="sr-only">Open menu</span>
          <MoreHorizontal className="h-4 w-4" />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end">
        <ActionConfirmationDialog
          onCancel={cancelAction}
          onConfirm={()=>userRoleStore.handleAddUserRole()}
          error={userRoleStore.error}
          menuItemText="Добавить роль"
          loading={userRoleStore.loading}
          description="Введите данные новой роли"
          title={`Новая роль`}
        >
          <EditUserRoleDialog />
        </ActionConfirmationDialog>
        <ActionConfirmationDialog
          onCancel={cancelAction}
          onConfirm={()=>userRoleStore.handleUpdateUserRole(userRole.id)}
          error={userRoleStore.error}
          menuItemText="Редактировать роль"
          loading={userRoleStore.loading}
          description="Введите новые данные для роли"
          title={`Редактируем ${userRole.name}`}
        >
          <EditUserRoleDialog userRole={userRole}/>
        </ActionConfirmationDialog>
        <ActionConfirmationDialog
          onCancel={cancelAction}
          onConfirm={()=>userRoleStore.handleDeleteUserRole(userRole.id)}
          error={userRoleStore.error}
          menuItemText="Удалить роль"
          loading={userRoleStore.loading}
          description="Это действие нельзя отменить."
          title="Вы уверены?"
        >
        </ActionConfirmationDialog>

      </DropdownMenuContent>
    </DropdownMenu>
  )
});