<script setup lang="ts">
import { LogOutIcon } from '@lucide/vue';
import type {User} from "@/components/app-sidebar/app-sidebar.types.ts";
import {SidebarMenu, SidebarMenuButton, SidebarMenuItem} from "@/components/ui/sidebar";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger
} from "@/components/ui/dropdown-menu";
import {Avatar, AvatarFallback} from "@/components/ui/avatar";
import {useAuth} from "@/hooks/useAuth.ts";
import {useAuthStore} from "@/stores/auth-store.ts";


const {logout} = useAuth();
const authStore = useAuthStore();
console.log(authStore.user);
const user: User = {
  name: authStore.user?.name,
  email: authStore.user?.username
}
</script>

<template>
 <SidebarMenu >
   <SidebarMenuItem>
    <DropdownMenu>
      <DropdownMenuTrigger as-child>
        <SidebarMenuButton
          size="lg"
          class="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
        >
          <Avatar class="size-8 rounded-lg">
            <AvatarFallback class="rounded-lg" >
              CN
            </AvatarFallback>
          </Avatar>
          <div class="grid flex-1 text-sm leading-tight text-left">
            <span class="font-semibold truncate">{{ user.name }}</span>
            <span class="text-xs truncate">{{ user.email }}</span>
          </div>
        </SidebarMenuButton>
      </DropdownMenuTrigger>
      <DropdownMenuContent
          class="w-(--radix-dropdown-menu-trigger-width) min-w-56 rounded-lg"
          :side="'right'"
          align="start"
          :side-offset="4"
      >
        <DropdownMenuItem  @click="logout">
          <LogOutIcon />
          Logout
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
   </SidebarMenuItem>
 </SidebarMenu>
</template>

<style scoped>

</style>