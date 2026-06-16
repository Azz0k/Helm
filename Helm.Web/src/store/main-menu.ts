import {House, UserCog } from "lucide-react";

export const mainMenu = {
  navAdmin: [
    {
      title: "",
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
    },
    {
      title: "Администрирование",
      icon: UserCog,
      url: "#",
      isActive: true,
      items: [
        {
          title: "Пользователи",
          url: "/users",
        },
        {
          title: "Роли",
          url: "/userroles",
        }
      ]
    }
  ],
}