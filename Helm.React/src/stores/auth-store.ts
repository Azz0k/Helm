import  {makeAutoObservable} from "mobx";
import type {AccountInfo} from '@azure/msal-browser';

export class AuthStore {
  constructor() {
    makeAutoObservable(this);
  }
  isRedirectDone:boolean=false;
  user: AccountInfo|null = null;
  get isAuthenticated(){
    return this.user!==null;
  }
}