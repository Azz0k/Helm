import {observer} from "mobx-react";
import {rootStore} from "@/stores/root-store.ts";

export const WelcomePage = observer(() => {
  return (
    <div>
      Welcome, {rootStore.authStore.user?.username}
    </div>
  );
});