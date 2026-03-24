import {makeAutoObservable} from "mobx";

//const themeKey = "vite-ui-theme";
class RootStore
{
  constructor()
  {
    makeAutoObservable(this);
  }
  isLoggedIn:boolean = false;
  userName:string = "";
  accessToken:string = "";
  handleLogon = (name: string, token:string) => {
    this.isLoggedIn = true;
    this.userName = name;
    this.accessToken = token;
  }
  handleLogout = () =>{
    this.isLoggedIn = false;
    this.userName = "";
    this.accessToken = "";
  }
}

export const rootStore= new RootStore();