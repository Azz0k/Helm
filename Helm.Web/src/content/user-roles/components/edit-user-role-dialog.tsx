import { Field, FieldGroup } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {observer} from "mobx-react";
import {userRoleStore} from "@/content/user-roles/user-role-store.ts";
import {useEffect} from "react";
import {defaultDraftUserRole} from "@/content/user-roles/user-role-mutation-store.ts";
import type {UserRole} from "@/content/user-roles/user-roles-columns.tsx";
type EditUserRoleDialogProps = {
  userRole?: UserRole;
}
export const EditUserRoleDialog = observer(({userRole}:EditUserRoleDialogProps)=>{
  useEffect(()=>{
    userRoleStore.userRoleMutationStore.createNewDraft({
    name:userRole?.name ?? defaultDraftUserRole.name,
    description:userRole?.description ?? defaultDraftUserRole.description,
    });
  },[])
  return (
    <FieldGroup>
      <Field>
        <Label htmlFor="userName">Название роли</Label>
        <Input
          id="RoleName"
          type="text"
          value={userRoleStore.userRoleMutationStore.draft.name}
          onChange={userRoleStore.userRoleMutationStore.handleDraftNameChangeValue}
        />
      </Field>
      <Field>
        <Label htmlFor="fullName">Описание роли</Label>
        <Input
          id="RoleDescription"
          type="text"
          value={userRoleStore.userRoleMutationStore.draft.description}
          onChange={userRoleStore.userRoleMutationStore.handleDraftDescriptionChangeValue}
        />
      </Field>

    </FieldGroup>
  );
});