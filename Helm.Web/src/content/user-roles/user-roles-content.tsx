import {userRoleColumns} from "@/content/user-roles/user-roles-columns.tsx";
import {DataTable} from "@/components/data-table.tsx";
import {observer} from "mobx-react";
import {userRoleStore} from "@/content/user-roles/user-role-store.ts";
import {rootStore} from "@/store/root-store.ts";
import {useEffect} from "react";
import {reaction} from "mobx";
import {Forbidden} from "@/pages/Forbidden.tsx";

export const UserRolesContent = observer(() => {
  useEffect(()=>{
    return   reaction(
      ()=>rootStore.isLoggedIn,
      ()=>{
        if (rootStore.isLoggedIn){
          userRoleStore.handleFetchUserRoles().then(()=>{
            rootStore.searchStore.enableSearch();
          });
        }
      },
      { fireImmediately: true }
    );
  },[]);
  return (
    <section className="container mx-auto py-10">
      {
        userRoleStore.forbidden ? (
          <Forbidden />
        ) : (
          <DataTable columns={userRoleColumns} data={userRoleStore.userRolesData}/>
        )
      }
    </section>
  );
});