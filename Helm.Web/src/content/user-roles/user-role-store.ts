import {makeAutoObservable} from "mobx";
import {addUserRole, deleteUserRole, loadAllUserRoles, updateUserRole} from "@/services/user-roles.api.ts";
import {rootStore} from "@/store/root-store.ts";
import type {UserRole} from "@/content/user-roles/user-roles-columns.tsx";
import UserRoleMutationStore from "@/content/user-roles/user-role-mutation-store.ts";

type wrappedFunc =  wrappedUpdate | wrappedLoading
type wrappedUpdate = (body: string) =>  Promise<boolean>;
type wrappedLoading = () =>  Promise<boolean>;
export type UserRoles = UserRole[];
class UserRoleStore {
  constructor(){
    makeAutoObservable(this);
  }

  forbidden: boolean = false;
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
    return this.wrapRequest(this.addUserRole, JSON.stringify(this.userRoleMutationStore.draft));
  }
  handleUpdateUserRole = async (id:number) => {
    this.error = null;
    if (!this.userRoleMutationStore.validateDraft()){
      this.error = this.userRoleMutationStore.error;
      return false;
    }
    this.loading = true;
    return  this.wrapRequest(this.updateUserRole, JSON.stringify({...this.userRoleMutationStore.draft, id}));
  }
  handleDeleteUserRole = async (id:number) => {
    this.error = null;
    this.loading = true;
    return this.wrapRequest(this.deleteUserRole, `${id}`);
  }
  handleFetchUserRoles = async () => {
    this.error = null;
    this.loading = true;
    return this.wrapRequest(this.loadAllUserRoles, "");
  }
  loadAllUserRoles = async () => {
    this.userRolesData = await loadAllUserRoles() as UserRoles;
    return true;
  }
  addUserRole = async (body:string)  => {
    const res: UserRole = await addUserRole(body);
    if (res) {
      this.userRolesData = [...this.userRolesData, res];
      return true;
    }
    this.error = 'Неизвестная ошибка';
    return false;
  }
  updateUserRole = async (body:string) => {
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
  deleteUserRole = async (id:string) => {
    await deleteUserRole(id);
    this.userRolesData = this.userRolesData.filter(user => user.id !== +id);
    return true;
  }
  wrapRequest  =  async (func: wrappedFunc, body:string)  => {
    this.error = null;
    try {
      const result =  await func(body);
      if (result){
        this.error = null;
        this.userRoleMutationStore.clear();
      }
      return result;
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
          this.forbidden = true;
          break;
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
    finally {
      this.loading = false;
    }
  }
}

export const userRoleStore = new UserRoleStore();
