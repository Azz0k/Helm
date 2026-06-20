import {makeAutoObservable} from "mobx";
import type {User} from "@/content/users/user-columns.tsx";
import {
  addUser,
  replaceRoleToUser,
  deleteUser,
  loadAllUsers,
  updateUser,
  updateUserStatus
} from "@/services/users.api.ts";
import {rootStore} from "@/store/root-store.ts";
import {userMutationStore} from "@/content/users/user-mutation-store.ts";
import type {UserRoles} from "@/content/user-roles/user-role-store.ts";
import {loadAllUserRoles} from "@/services/user-roles.api.ts";

type wrappedFunc = ( props : wrapperFuncProps) =>  Promise<boolean>;
type wrapperFuncProps = {
  userId?: number;
  body?: string;
  roleId?: number;
}
type Users = User[];
class UserStore {

  constructor(){
    makeAutoObservable(this);
  }
  usersData: Users = [];
  roleData : UserRoles = [];
  loading: boolean = false;
  error: string | null = null;
  userMutationStore = userMutationStore;
  forbidden: boolean = false;

  changeUserStatusText =  (id : number) => {
    if (this.usersData.find(user => user.id === id)?.enabled === true){
      return "Отключить";
    }
    return "Включить";
  }
  handleCancelAction = () =>{
    this.userMutationStore.clear();
    this.error = null;
  }
  handleAddUser = async () => {
    this.error = null;
    if (!this.userMutationStore.validateDraft()){
      this.error = this.userMutationStore.error;
      return false;
    }
    this.loading = true;
    return this.wrapRequest(this.addUser, {
      body: JSON.stringify(this.userMutationStore.draft)
    });
  }
  handleUpdateUser = async (id:number) => {
    this.error = null;
    if (!this.userMutationStore.validateDraft()){
      this.error = this.userMutationStore.error;
      return false;
    }
    this.loading = true;
    return  this.wrapRequest(this.updateUser, {
      body: JSON.stringify({...this.userMutationStore.draft, id})
    });
  }
  handleUpdateUserStatus = async (id:number) => {
    this.error = null;
    this.loading = true;
    const currentUser = this.usersData.find(user => user.id === id);
    if (currentUser === undefined) {
      this.error = "Обновите страницу";
      return false;
    }
    const newRoles = this.userMutationStore.roles.map(r=>+r);
    return this.wrapRequest(this.replaceRoleToUser,{
      body: JSON.stringify({
        Roles: newRoles,
        UserId: currentUser.id
      }),
    });
  }
  handleDeleteUser = async (id:number) => {
    this.error = null;
    this.loading = true;
    return this.wrapRequest(this.deleteUser, {userId: id});
  }
  handleChangeUserStatus = async (id:number) => {
    this.error = null;
    this.loading = true;
    const currentUser: User | undefined = this.usersData.find(u=>u.id===id);
    if (currentUser === undefined){
      this.error = "Обновите страницу"
      return  false;
    }
    return this.wrapRequest(this.updateUserStatus, {
      userId: id,
      body: JSON.stringify({...currentUser, enabled: !currentUser.enabled})
    });
  }
  handleFetchUsers = async () => {
    this.error = null;
    this.loading = true;
    return this.wrapRequest(this.loadAllUsers, {});
  }
  loadAllUsers = async () => {
    this.roleData = await loadAllUserRoles() as UserRoles;
    this.usersData = await loadAllUsers() as Users;
    return true;
  }
  addUser = async ({body}:wrapperFuncProps)  => {
    const res: User = await addUser(body);
    if (res) {
      this.usersData = [...this.usersData, res];
      return true;
    }
    this.error = 'Неизвестная ошибка';
    return false;
  }
  updateUser = async ({body}:wrapperFuncProps) => {
    const res :User = await updateUser(body);
    if (res){
      this.usersData = this.usersData.map(element =>{
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

  replaceRoleToUser = async ({body}:wrapperFuncProps) => {
    const res :User = await replaceRoleToUser(body);
    if (res){
      this.usersData = this.usersData.map(element =>{
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


  updateUserStatus = async ({userId, body}:wrapperFuncProps) => {
    const res :User = await updateUserStatus(userId, body);
    if (res){
      this.usersData = this.usersData.map(element =>{
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

  deleteUser = async ({userId}:wrapperFuncProps) => {
    await deleteUser(userId);
    this.usersData = this.usersData.filter(user => user.id !== userId);
    return true;
  }
  wrapRequest  =  async (func: wrappedFunc, props :wrapperFuncProps)  => {
    this.error = null;
    try {
      const result =  await func(props);
      if (result){
        this.error = null;
        this.userMutationStore.clear();
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
          this.usersData = [];
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

export const userStore = new UserStore();