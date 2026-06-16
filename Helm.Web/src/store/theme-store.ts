import {makeAutoObservable} from "mobx";
const themeKey = "vite-ui-theme";
export  default class ThemeStore {
  constructor() {
    makeAutoObservable(this);
    const theme = localStorage.getItem(themeKey);
    if (theme) {
      if (theme === "light") {
        this.themeSwitchValue = false;
      }
    }
  }
  themeSwitchValue = true;
  get theme() {
    return this.themeSwitchValue? "dark" : "light";
  }
  handleCheckTheme = (value:boolean) => {
    this.themeSwitchValue = value;
    localStorage.setItem(themeKey, this.theme);
  }
}
