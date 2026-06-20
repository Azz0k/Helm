import {userColumns} from "@/content/users/user-columns.tsx";
import {DataTable} from "@/components/data-table.tsx";
import {observer} from "mobx-react";
import {userStore} from "@/content/users/user-store.ts";
import {rootStore} from "@/store/root-store.ts";
import {useEffect} from "react";
import {reaction} from "mobx";
import {Forbidden} from "@/pages/Forbidden.tsx";

export const UsersContent = observer(() => {
  useEffect(()=>{
    return   reaction(
      ()=>rootStore.isLoggedIn,
      ()=>{
        if (rootStore.isLoggedIn){
          userStore.handleFetchUsers().then();
        }
      },
      { fireImmediately: true }
    );
  },[]);
  return (
    <section className="container mx-auto py-10">
      {
        userStore.forbidden ? ( <Forbidden />) :
          (<DataTable columns={userColumns} data={userStore.usersData}/>)
      }
    </section>
  );
});