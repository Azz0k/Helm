import {observer} from "mobx-react";
import {rootStore} from "@/store/root-store.ts";

export const HomeContent = observer(() => {
  return (
    <div className="button-div">
      {rootStore.userName}
    </div>
  );
});