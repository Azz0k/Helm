<script setup lang="ts">

import BasicPage from '@/components/global-layout/basic-page.vue';
import UsersTable from './components/users-table.vue';
import EditUserDialog from './components/edit-user-dialog.vue';
import {useUsersStore} from "@/pages/users/components/user-store.ts";
const store = useUsersStore();
</script>

<template>
  <BasicPage
      title="Users"
      description="Users description"
      sticky
  >
    <template #actions>
      <EditUserDialog />
    </template>
    <div v-if="store.isError">
      {{store.error}}
    </div>
    <div class="overflow-x-auto">
      <UsersTable :loading="store.isPending" :data="store.data ?? []" />
    </div>
  </BasicPage>
</template>
