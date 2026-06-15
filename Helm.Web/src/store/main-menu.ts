import {House, UserCog } from "lucide-react";

export const mainMenu = {
  navAdmin: [
    {
      title: "Главная",
      url: "/",
      icon: House,
      isActive: true,
      items:[
        {
          title: "Снять пароль docx, xlsx",
          url: "/removepassword",
      }
      ]
    },
    {
      title: "Пользователи",
      url: "/users",
      icon: UserCog,
    }
  ],
}