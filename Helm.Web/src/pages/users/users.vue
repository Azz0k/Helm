<script setup lang="ts">

import BasicPage from '@/components/global-layout/basic-page.vue'

import UsersTable from './components/users-table.vue'
import UserCreate from './components/user-create.vue'
import type {User} from "@/pages/users/data/schema.ts";
import {ref} from "vue";

const data:User[] = [
  {
    Id:1,
    Login: "Test",
    Name: "test",
    Enabled: true,
    Roles: ["admin"],
  }
];
const loading = ref<boolean>(false);

</script>

<template>
  <BasicPage
      title="Users"
      description="Users description"
      sticky
  >
    <template #actions>
      <UserCreate />
    </template>
    <div class="overflow-x-auto">
      <UsersTable :loading :data />
    </div>
  </BasicPage>
</template>
