import  {makeAutoObservable} from "mobx";
import {AuthStore} from "@/stores/auth-store.ts";

class RootStore {
  constructor() {
    makeAutoObservable(this);
  }
  authStore = new AuthStore();
}

export const rootStore = new RootStore();