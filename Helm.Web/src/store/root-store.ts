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
  idToken:string = "";
  handleLogon = (name: string, accessToken:string, idToken:string) => {
    this.isLoggedIn = true;
    this.userName = name;
    this.accessToken = accessToken;
    this.idToken = idToken;
  }
  handleLogout = () =>{
    this.isLoggedIn = false;
    this.userName = "";
    this.accessToken = "";
    this.idToken = "";
  }
}

export const rootStore= new RootStore();