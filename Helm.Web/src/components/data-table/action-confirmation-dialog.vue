<script setup lang="ts">
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

import {DropdownMenuItem} from "@/components/ui/dropdown-menu";

type ActionConfirmationDialogProps = {
  onCancel: ()=>void;
  onConfirm: () => Promise<boolean>;
  error: string | null;
  menuItemText :string;
  loading: boolean;
  description:string;
  title:string;
}
const errorTitle = "Ошибка";
const props = defineProps<ActionConfirmationDialogProps>();
const open  = defineModel<boolean>({
  set(value){
    if (!value) props.onCancel();
    return value;
  }
});
/*
const handleOpenChange = (value: boolean): void => {
  open.value = value;
  if (!value) props.onCancel();
}
const handleSubmit = () => {
  props.onConfirm().then(result => {
    if (result) {
      open.value = false;
      props.onCancel();
    }
  });
}*/
</script>

<template>
  <Dialog v-model:open="open">
    <form>
      <DialogTrigger as-child>
        <DropdownMenuItem @select.prevent="open=true">
          {{props.menuItemText}}
        </DropdownMenuItem>
      </DialogTrigger>
      <DialogContent class="sm:max-w-sm">
        <DialogHeader>
          <DialogTitle>
            {{props.error===null?props.title:errorTitle}}
          </DialogTitle>
          <DialogDescription>
            {{error===null?description:error}}
          </DialogDescription>
        </DialogHeader>
        <slot/>
        <DialogFooter>

        </DialogFooter>
      </DialogContent>
    </form>
  </Dialog>
</template>

<style scoped>

</style>