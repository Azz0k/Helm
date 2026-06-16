import {userRoleColumns} from "@/content/user-roles/user-roles-columns.tsx";
import {DataTable} from "@/components/data-table.tsx";
import {observer} from "mobx-react";
import {userRoleStore} from "@/content/user-roles/user-role-store.ts";
import {rootStore} from "@/store/root-store.ts";
import {useEffect} from "react";
import {reaction} from "mobx";

export const UserRolesContent = observer(() => {
  useEffect(()=>{
    return   reaction(
      ()=>rootStore.isLoggedIn,
      ()=>{
        if (rootStore.isLoggedIn){
          userRoleStore.LoadAllUserRoles().then();
        }
      },
      { fireImmediately: true }
    );
  },[]);
  return (
    <section className="container mx-auto py-10">
      <DataTable columns={userRoleColumns} data={userRoleStore.userRolesData}/>
    </section>
  );
});