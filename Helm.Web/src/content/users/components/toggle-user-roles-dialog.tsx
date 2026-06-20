import type {User} from "@/content/users/user-columns.tsx";
import {
  ToggleGroup,
  ToggleGroupItem,
} from "@/components/ui/toggle-group"
import {userStore} from "@/content/users/user-store.ts";
import {useEffect} from "react";
import {observer} from "mobx-react";

type ToggleUserRolesDialogProps = {
  user?: User;
};

export const ToggleUserRolesDialog = observer(({ user }: ToggleUserRolesDialogProps) => {
  useEffect(() => {
    userStore.userMutationStore.roles = user?.roles.map(r=>r.toString()) ?? [];
  }, []);
  const roles = userStore.roleData.map(r=>{
    return (
      <ToggleGroupItem
        key={r.id}
        value={r.id.toString()}
      >
        {r.name}
      </ToggleGroupItem>);
  })
  return (
    <ToggleGroup
      type="multiple"
      orientation="vertical"
      spacing={1}
      value={userStore.userMutationStore.roles}
      onValueChange={value => userStore.userMutationStore.handleToggleRoles(value)}
      >
      {roles}
    </ToggleGroup>
  );
});