import { Field, FieldGroup } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {observer} from "mobx-react";
import {userStore} from "@/content/users/user-store.ts";
import {useEffect} from "react";
import type {User} from "@/content/users/user-columns.tsx";
import {defaultDraftUser} from "@/content/users/user-mutation-store.ts";
type EditUserDialogProps = {
  user?: User;
}
export const EditUserDialog = observer(({user}:EditUserDialogProps)=>{
  useEffect(()=>{
    userStore.userMutationStore.createNewDraft({
    login:user?.login ?? defaultDraftUser.login,
    name:user?.name ?? defaultDraftUser.name,
    enabled:user?.enabled ?? defaultDraftUser.enabled,
    });
  },[])
  return (
    <FieldGroup>
      <Field>
        <Label htmlFor="userName">Логин</Label>
        <Input
          id="userName"
          type="text"
          value={userStore.userMutationStore.draft.login}
          onChange={userStore.userMutationStore.handleDraftUserNameChangeValue}
        />
      </Field>
      <Field>
        <Label htmlFor="fullName">ФИО</Label>
        <Input
          id="fullName"
          type="text"
          value={userStore.userMutationStore.draft.name}
          onChange={userStore.userMutationStore.handleDraftFullNameChangeValue}
        />
      </Field>
    </FieldGroup>
  );
});