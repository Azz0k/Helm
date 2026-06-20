import {makeAutoObservable} from "mobx";
import {router} from "@/routes/router.tsx";
import {
  mainMenu,
  type MenuGroupItems,
  UserRolesMenuItem,
  UsersMenuItem
} from "@/store/main-menu.ts";
import ThemeStore from "@/store/theme-store.ts";
import SearchStore from "@/store/search-store.tsx";
import {Authenticate} from "@/services/Authenticate.api.ts";

type CurrentUserRoles = {
  roles : string[];
}

class RootStore
{
  constructor()
  {
    makeAutoObservable(this);
    router.subscribe('onResolved', (evt)=>{
      this.pathName = evt.toLocation.pathname;
    });
  }
  themeStore = new ThemeStore();
  searchStore = new SearchStore();
  pathName!: string;
  isLoggedIn:boolean = false;
  userName:string = "";
  accessToken:string = "";
  idToken:string = "";
  currentUserRoles: string [] = [];
  mainMenu: MenuGroupItems = [];
  get mainMenuTitle() {

    return this.mainMenu.find(e=>e.url===this.pathName)?.title;
  }
  generateMainMenu = () => {
    const defaultMenu = mainMenu.navDefault;
    const adminMenu = mainMenu.navAdmin
    const userMangerRoleName = import.meta.env.VITE_USER_MANAGER_ROLE_NAME;
    const userRoleMangerRoleName = import.meta.env.VITE_USER_ROLE_MANAGER_ROLE_NAME;
    adminMenu.items = [];
    if (this.currentUserRoles.includes(userMangerRoleName)){
      adminMenu.items.push(UsersMenuItem)
    }
    if (this.currentUserRoles.includes(userRoleMangerRoleName)){
      adminMenu.items.push(UserRolesMenuItem)
    }
    const result :MenuGroupItems  = []
    result.push(defaultMenu)
    if (adminMenu.items.length > 0){
      result.push(adminMenu);
    }
    return result;
  }

  handleLogon = async (name: string, accessToken:string, idToken:string) => {
    this.isLoggedIn = true;
    this.userName = name;
    this.accessToken = accessToken;
    this.idToken = idToken;
    const res: CurrentUserRoles = await  Authenticate();
    this.currentUserRoles = res.roles;
    this.mainMenu = this.generateMainMenu();
  }
  handleLogout = () =>{
    this.isLoggedIn = false;
    this.userName = "";
    this.accessToken = "";
    this.idToken = "";
    this.currentUserRoles = [];
    this.mainMenu = [];
  }

}

export const rootStore= new RootStore();