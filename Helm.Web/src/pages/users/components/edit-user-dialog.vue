<script setup lang="ts">
import {ref} from "vue";
import {Dialog, DialogContent, DialogTitle, DialogTrigger} from "@/components/ui/dialog";
import {Button} from "@/components/ui/button";
import { UserRoundPlusIcon } from '@lucide/vue'
import EditUserFields from "./edit-user-fields.vue";

const isOpen = ref(false);
const id = 0;
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
      <EditUserFields
          :id
          login=""
          name=""
      />
    </DialogContent>
  </Dialog>
</template>

<style scoped>

</style>