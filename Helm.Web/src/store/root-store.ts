import {makeAutoObservable} from "mobx";
import {router} from "@/routes/router.tsx";
import {mainMenu} from "@/store/main-menu.ts";

//const themeKey = "vite-ui-theme";

class RootStore
{
  constructor()
  {
    makeAutoObservable(this);
    router.subscribe('onResolved', (evt)=>{
      this.pathName = evt.toLocation.pathname;
    });
  }
  searchValue: string = "";
  pathName!: string;
  themeSwitchValue = true;
  isLoggedIn:boolean = false;
  userName:string = "";
  accessToken:string = "";
  idToken:string = "";
  get mainMenuTitle() {
    return this.mainMenu.find(e=>e.url===this.pathName)?.title;
  }
  get mainMenu() {
    return mainMenu.navAdmin;
  }
  get theme() {
    return this.themeSwitchValue? "dark" : "light";
  }
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