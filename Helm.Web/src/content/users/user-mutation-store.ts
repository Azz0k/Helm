import {makeAutoObservable} from "mobx";
import type {ChangeEvent} from "react";

export type DraftUser = {
  login: string;
  name: string,
  enabled: boolean,
};
export const defaultDraftUser = {
  login: "",
  name: "",
  enabled: true,
};

class UserMutationStore{
  constructor() {
    makeAutoObservable(this);
  }
  error: string | null = null;
  draft:DraftUser = defaultDraftUser;
  roles: string[] = [];
  createNewDraft =(user:DraftUser)=>{
    this.draft = user;
  }
  clear = () =>{
    this.error = null;
    this.draft = defaultDraftUser;
    this.roles = [];
  }

  validateDraft = ()=>{
    this.draft.name = this.draft.name.trim();
    this.draft.login = this.draft.login.trim();
    if (this.draft.name.length===0) {
      this.error = "ФИО не должно быть пустым";
      return false;
    }
    if (this.draft.login.length===0) {
      this.error = "Логин не должен быть пустым";
      return false;
    }
    return true;
  }
  handleDraftUserNameChangeValue = (event: ChangeEvent<HTMLInputElement>) => {
    this.draft.login = event.target.value;
  }
  handleDraftFullNameChangeValue = (event: ChangeEvent<HTMLInputElement>) => {
    this.draft.name = event.target.value;
  }
  handleToggleRoles = (value:string[]) => {
    this.roles = value;
  }
}

export const userMutationStore = new UserMutationStore();