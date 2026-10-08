<script setup lang="ts">
import {ref} from "vue";
import {Dialog, DialogContent, DialogTitle, DialogTrigger} from "@/components/ui/dialog";
import {Button} from "@/components/ui/button";
import { UserRoundPlusIcon } from '@lucide/vue'

const isOpen = ref(false);
</script>

<template>
  <Dialog v-model:open="isOpen">
    <DialogTrigger as-child>
      <Button>
        <UserRoundPlusIcon />
        Create User
      </Button>
    </DialogTrigger>
    <DialogContent>
      <DialogTitle>
        Титле
      </DialogTitle>
      Калтент
    </DialogContent>
  </Dialog>
</template>

<style scoped>

</style>