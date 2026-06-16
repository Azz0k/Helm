import {makeAutoObservable} from "mobx";
import type {ChangeEvent} from "react";

export type DraftUserRole = {
  description: string;
  name: string,

};
export const defaultDraftUserRole = {
  description: "",
  name: "",
};

export default class UserRoleMutationStore{
  constructor() {
    makeAutoObservable(this);
    const name_max_length = import.meta.env.VITE_USER_ROLES_NAME_MAX_LENGTH;
    if (name_max_length && name_max_length > 0){
      this.NAME_MAX_LENGTH = name_max_length;
    }
  }
  NAME_MAX_LENGTH = 50;
  error: string | null = null;
  draft:DraftUserRole = defaultDraftUserRole;
  createNewDraft =(userRole:DraftUserRole)=>{
    this.draft = userRole;
  }
  clear = () =>{
    this.error = null;
    this.draft = defaultDraftUserRole;
  }
  validateDraft = ()=>{
    this.draft.name = this.draft.name.trim();
    this.draft.description = this.draft.description.trim();
    if (this.draft.name.length===0) {
      this.error = "Название роли не должно быть пустым";
      return false;
    }
    if (this.draft.name.length>this.NAME_MAX_LENGTH) {
      this.error = "Название роли слишком длинное";
      return false;
    }
    return true;
  }
  handleDraftNameChangeValue = (event: ChangeEvent<HTMLInputElement>) => {
    this.draft.name = event.target.value;
  }
  handleDraftDescriptionChangeValue = (event: ChangeEvent<HTMLInputElement>) => {
    this.draft.description = event.target.value;
  }
}
