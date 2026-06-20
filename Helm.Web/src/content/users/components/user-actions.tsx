import {observer} from "mobx-react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {Button} from "@/components/ui/button.tsx";
import {MoreHorizontal} from "lucide-react";
import React from "react";
import {userStore} from "@/content/users/user-store.ts";
import {ActionConfirmationDialog} from "@/components/action-confirmation-dialog.tsx";
import type {User} from "@/content/users/user-columns.tsx";
import {EditUserDialog} from "@/content/users/components/edit-user-dialog.tsx";
import {ToggleUserRolesDialog} from "@/content/users/components/toggle-user-roles-dialog.tsx";

type UserActionsProps = {
  user:User;
}
export const UserActions = observer(({user}:UserActionsProps)=>{
  const [open, SetOpen] = React.useState(false);
  const cancelAction  = ()=>{
    userStore.handleCancelAction();
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
          onConfirm={()=>userStore.handleAddUser()}
          error={userStore.error}
          menuItemText="Добавить пользователя"
          loading={userStore.loading}
          description="Введите данные нового пользователя"
          title={`Новый пользователь`}
        >
          <EditUserDialog />
        </ActionConfirmationDialog>
        <ActionConfirmationDialog
          onCancel={cancelAction}
          onConfirm={()=>userStore.handleUpdateUser(user.id)}
          error={userStore.error}
          menuItemText="Редактировать пользователя"
          loading={userStore.loading}
          description="Введите новые данные пользователя"
          title={`Редактируем ${user.login}`}
        >
          <EditUserDialog user={user}/>
        </ActionConfirmationDialog>
        <ActionConfirmationDialog
          onCancel={cancelAction}
          onConfirm={()=>userStore.handleChangeUserStatus(user.id)}
          error={userStore.error}
          menuItemText={`${userStore.changeUserStatusText(user.id)} пользователя`}
          loading={userStore.loading}
          description=""
          title="Вы уверены?"
        >
        </ActionConfirmationDialog>
        <ActionConfirmationDialog
          onCancel={cancelAction}
          onConfirm={()=>userStore.handleUpdateUserStatus(user.id)}
          error={userStore.error}
          menuItemText="Назначить роли пользователю"
          loading={userStore.loading}
          description="Выберите роли пользователя"
          title={`Редактируем ${user.login}`}
        >
          <ToggleUserRolesDialog user={user}/>
        </ActionConfirmationDialog>
        <ActionConfirmationDialog
          onCancel={cancelAction}
          onConfirm={()=>userStore.handleDeleteUser(user.id)}
          error={userStore.error}
          menuItemText="Удалить пользователя"
          loading={userStore.loading}
          description="Это действие нельзя отменить."
          title="Вы уверены?"
        >
        </ActionConfirmationDialog>
      </DropdownMenuContent>
    </DropdownMenu>
  )
});