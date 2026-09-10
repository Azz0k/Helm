<script setup lang="ts">
import {useAuthStore} from "@/stores/auth-store.ts";

const authStore = useAuthStore();
</script>

<template>
  <div>
    Welcome, {{ authStore.user?.username }}!
  </div>
</template>

<style scoped>

</style>