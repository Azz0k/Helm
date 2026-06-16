import {makeAutoObservable} from "mobx";
import {addUserRole, deleteUserRole, loadAllUserRoles, updateUserRole} from "@/services/user-roles.api.ts";
import {rootStore} from "@/store/root-store.ts";
import type {UserRole} from "@/content/user-roles/user-roles-columns.tsx";
import UserRoleMutationStore, {type DraftUserRole} from "@/content/user-roles/user-role-mutation-store.ts";

type NewUserRole = {
  description: string;
  name: string,
}

type UserRoles = UserRole[];
class UserRoleStore {
  constructor(){
    makeAutoObservable(this);
  }

  userRolesData: UserRoles = [];
  loading: boolean = false;
  error: string | null = null;
  userRoleMutationStore = new UserRoleMutationStore();

  handleCancelAction = () =>{
    this.userRoleMutationStore.clear();
    this.error = null;
  }
  handleAddUserRole = async () => {
    this.error = null;
    if (!this.userRoleMutationStore.validateDraft()){
      this.error = this.userRoleMutationStore.error;
      return false;
    }
    this.loading = true;
    const result = await this.addUserRole(this.userRoleMutationStore.draft);
    if (result){
      this.error = null;
      this.userRoleMutationStore.clear();
    }
    this.loading = false;
    return result;
  }
  handleUpdateUserRole = async (id:number) => {
    this.error = null;
    if (!this.userRoleMutationStore.validateDraft()){
      this.error = this.userRoleMutationStore.error;
      return false;
    }
    this.loading = true;
    const result = await this.UpdateUserRole(id, this.userRoleMutationStore.draft);
    if (result){
      this.error = null;
      this.userRoleMutationStore.clear();
    }
    this.loading = false;
    return result;
  }

  handleDeleteUserRole = async (id:number) => {
    this.loading = true;
    const result = await this.DeleteUserRole(id);
    if(result){
      this.userRolesData = this.userRolesData.filter(user => user.id !== id);
      this.error = null;
    }
    else {
      this.error = "Не удалось удалить роль. Попробуйте обновить страницу."
    }
    this.loading = false;
    return result;
  }
  async LoadAllUserRoles(){
    this.loading = true;
    try{
      this.userRolesData = await loadAllUserRoles() as UserRoles;
    }
    catch(error:unknown){
      switch (error){
        case 403:
        case 401:
          this.userRolesData = [];
          rootStore.handleLogout();
          break;
        default:
          break;
      }
    }
    finally{
      this.loading = false;
    }
  }
  async addUserRole(newUserRole:NewUserRole) {
    this.error = null;
    try {
      const body = JSON.stringify(newUserRole);
      const res: UserRole = await addUserRole(body);
      if (res) {
        this.userRolesData = [...this.userRolesData, res];
        return true;
      }
      this.error = "Неизвестная ошибка";
      return false;
    }
    catch(error:unknown){
      switch (error){
        case 409:
          this.error = 'Роль с таким именем уже существует';
          break;
        case 400:
          this.error = 'Неверные параметры запроса';
          break;
        case 403:
        case 401:
          this.userRolesData = [];
          this.error = null;
          rootStore.handleLogout();
          break;
        default:
          this.error = 'Неизвестная ошибка';
          break;
      }
      return false;
    }
  }
  async UpdateUserRole (id:number,userRole:NewUserRole|DraftUserRole ) {
    this.error = null;
    try{
      const body = JSON.stringify({...userRole, id});
      const res :UserRole = await updateUserRole(body);
      if (res){
        this.userRolesData = this.userRolesData.map(element =>{
          if (element.id === res.id){
            return res;
          }
          return element;
        });
        return true;
      }
      this.error = "Неизвестная ошибка";
      return false;
    }
    catch(error:unknown){
      switch (error){
        case 409:
          this.error = 'Роль с таким именем уже существует';
          break;
        case 400:
          this.error = 'Неверные параметры запроса';
          break;
        case 403:
        case 401:
          this.userRolesData = [];
          this.error = null;
          rootStore.handleLogout();
          break;
        default:
          this.error = 'Неизвестная ошибка';
          break;
      }
      return false;
    }
  }
  async DeleteUserRole(id:number) {
    try {
      const code = await deleteUserRole(id);
      if (code === 401 || code === 403) {
        this.userRolesData = [];
        rootStore.handleLogout();
        return false;
      }
      return code===204;
    }
    catch  {
      return false;
    }
  }
}

export const userRoleStore = new UserRoleStore();
