<script setup lang="ts">
import {Field, FieldGroup} from "@/components/ui/field";
import {Label} from "@/components/ui/label";
import {Input} from "@/components/ui/input";
import {Button} from "@/components/ui/button";
import {DialogClose} from "@/components/ui/dialog";
import {useUserEditStore} from "@/pages/users/components/user-edit-store.ts";
import {ref} from "vue";

const props = defineProps<{
  id: number;
  login: string;
  name: string;
}>();
const login = ref(props.login);
const name = ref(props.name);
const store = useUserEditStore();
</script>

<template>
  <FieldGroup>
    <Field>
      <Label htmlFor="login">Логин</Label>
      <Input
          id="userName"
          type="text"
          v-model="login"
      />
    </Field>
    <Field>
      <Label htmlFor="fullName">ФИО</Label>
      <Input
          id="fullName"
          type="text"
          v-model="name"
      />
    </Field>
    <Field orientation="horizontal">
      <Button
          type="submit"
          @click="()=>store.handleEditSubmit(props.id, login, name)"
      >
        Submit
      </Button>
      <DialogClose as-child>
        <Button
            variant="outline" type="button">
          Cancel
        </Button>
      </DialogClose>
    </Field>
  </FieldGroup>
</template>

<style scoped>

</style>