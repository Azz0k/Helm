import { defineStore } from 'pinia';
import {useQuery} from "@tanstack/vue-query";
import {loadAllUsers} from "@/services/users.api.ts";
import {useAuth} from "@/hooks/useAuth.ts";
export const useUsersStore = defineStore('users', () => {
  const {getAccessToken} = useAuth();
  const { isPending, isError, data, error } = useQuery({
    queryKey: ['users'],
    queryFn: async ()=>loadAllUsers(await getAccessToken()),
  });

  return {isPending, isError, data, error}
});