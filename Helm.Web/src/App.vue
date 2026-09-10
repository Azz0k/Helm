<script setup lang="ts">
import { onBeforeMount } from 'vue';
import { useAuthStore } from "@/stores/auth-store.ts";
import { useAuth } from "@/hooks/useAuth.ts";
import DefaultLayout from "@/layouts/default-layout.vue";

const authStore = useAuthStore();
const {login, handleRedirect } = useAuth();
const handleLogin = async () => {
  await login();
};
onBeforeMount(async () =>{
  await handleRedirect();
});

</script>

<template>
  <div v-if="authStore.isRedirectDone">
    <div v-if="authStore.isAuthenticated">
      <DefaultLayout />
    </div>
    <div v-else>
      <button @click="handleLogin">Login</button>/
    </div>
  </div>
</template>
