import {House, type LucideIcon, UserCog} from "lucide-react";

export type MenuItem = {
  title: string,
  url: string,
}
export type MenuGroupItem = {
  title: string,
  url: string,
  icon: LucideIcon,
  isActive: boolean,
  items: MenuItem[],
}
export type MenuGroupItems = MenuGroupItem[];
const navHome: MenuGroupItem =
  {
    title: "Главная",
    url: "/",
    icon: House,
    isActive: true,
    items:[
      {
        title: "Документация",
        url: "/documents",
      },
      {
        title: "Часто задаваемые вопросы",
        url: "/faq",
      },
      {
        title: "Снять пароль docx, xlsx",
        url: "/removepassword",
      }
    ]
  };
const navAdmin: MenuGroupItem =
  {
    title: "Администрирование",
    icon: UserCog,
    url: "#",
    isActive: true,
    items: [
    ]
  }
;
export const mainMenu = {
  navDefault: navHome,
  navAdmin,
}


export const UsersMenuItem : MenuItem = {
  title: "Пользователи",
    url: "/users",
};
export const UserRolesMenuItem : MenuItem = {
  title: "Роли",
    url: "/userroles",
};