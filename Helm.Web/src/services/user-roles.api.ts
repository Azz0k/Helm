import {queryClient} from "../main.tsx";
import {AddData, DeleteData, FetchData, UpdateData} from "./DataService.api.ts";

const userRolesApiUrl = import.meta.env.VITE_USER_ROLES_API_URL;

export const addUserRole = async (body:string)=>{
  const res = await AddData(userRolesApiUrl, body);
  if (res.status !== 201) {
    throw res.status;
  }
  return res.json();
}
export const updateUserRole = async (body:string)=>{
  const res = await UpdateData(userRolesApiUrl, body);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
};
export const deleteUserRole = async (id:string)=>{
  const res = await DeleteData(`${userRolesApiUrl}/${id}`);
  if (res.status !== 204) {
    throw res.status;
  }
}
export const loadAllUserRoles = async () => {
  return await queryClient.fetchQuery({
    queryKey: ["user-roles", "get"],
    queryFn:()=> FetchData(userRolesApiUrl)
      .then(res => {
        if (res.status !== 200) {
          throw res.status;
        }
        return res.json();
      }),
    staleTime: 60_000,
  });
};
