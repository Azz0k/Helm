<script setup lang="ts">
import { onBeforeMount } from 'vue';
import { useAuthStore } from "@/store/authStore.ts";
import { useAuth } from "@/hooks/useAuth.ts";

const authStore = useAuthStore();
const {login, logout, handleRedirect } = useAuth();
const handleLogout = async () => {
  await logout();
};
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
      <div>
        Welcome, {{ authStore.user?.username }}!
        <button @click="handleLogout">Logout</button>/
      </div>
    </div>
    <div v-else>
      <button @click="handleLogin">Login</button>/
    </div>
  </div>
</template>
