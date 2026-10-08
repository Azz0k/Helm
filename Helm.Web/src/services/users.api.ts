import {AddData, DeleteData, FetchData, UpdateData} from "./DataService.api.ts";
import {userListSchema} from "@/pages/users/data/schema.ts";

const usersApiUrl = import.meta.env.VITE_USERS_API_URL;
const getAccessToken = async () => {
  return "remove this mock";
}
export const addUser = async (body:string)=>{
  const accessToken = await getAccessToken();
  const res = await AddData(usersApiUrl, body, accessToken);
  if (res.status !== 201) {
    throw res.status;
  }
  return res.json();
}
export const updateUser = async (body:string)=>{
  const accessToken = await getAccessToken();
  const res = await UpdateData(usersApiUrl, body, accessToken);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
};
export const deleteUser = async (id?:number)=>{
  const accessToken = await getAccessToken();
  const res = await DeleteData(`${usersApiUrl}/${id}`, accessToken);
  if (res.status !== 204) {
    throw res.status;
  }
}
export const updateUserStatus = async (id:number, body:string)=>{
  const accessToken = await getAccessToken();
  const res = await UpdateData(`${usersApiUrl}/${id}/status`, body, accessToken);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
}
export const replaceRoleToUser = async (body:string)=>{
  const accessToken = await getAccessToken();
  const res = await UpdateData(`${usersApiUrl}/role`, body, accessToken);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
}

export const loadAllUsers = async (accessToken:string) => {
  const response = await FetchData(usersApiUrl, accessToken);
  if (response.status !== 200) {
    throw response.status;
  }
  return userListSchema.parse(await response.json());
};