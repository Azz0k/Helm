import {queryClient} from "../main.tsx";
import {AddData, DeleteData, FetchData, UpdateData} from "./DataService.api.ts";

const usersApiUrl = import.meta.env.VITE_USERS_API_URL;

export const addUser = async (body?:string)=>{
  const res = await AddData(usersApiUrl, body);
  if (res.status !== 201) {
    throw res.status;
  }
  return res.json();
}
export const updateUser = async (body?:string)=>{
  const res = await UpdateData(usersApiUrl, body);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
};
export const deleteUser = async (id?:number)=>{
  const res = await DeleteData(`${usersApiUrl}/${id}`);
  if (res.status !== 204) {
    throw res.status;
  }
}
export const updateUserStatus = async (id?:number, body?:string)=>{
  const res = await UpdateData(`${usersApiUrl}/${id}/status`, body);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
}
export const replaceRoleToUser = async (body?:string)=>{
  const res = await UpdateData(`${usersApiUrl}/role`, body);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
}

export const loadAllUsers = async () => {
  return await queryClient.fetchQuery({
    queryKey: ["users", "get"],
    queryFn:()=> FetchData(usersApiUrl)
      .then(res => {
        if (res.status !== 200) {
          throw res.status;
        }
        return res.json();
      }),
    staleTime: 60_000,
  });
};
