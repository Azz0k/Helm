import { defineStore } from 'pinia';
import type {User} from "@/pages/users/data/schema.ts";
import {ref} from "vue";

const defaultUser: User = {
  id:0,
  login: "",
  name: "",
  enabled: true,
  roles: []
}
export const useUserEditStore = defineStore('user-edit', () => {
 const user = ref(defaultUser);
 const handleEditSubmit = (id: number, login:string, name:string)=>{
   console.log(id, login, name);
 };
 return {user, handleEditSubmit}
});