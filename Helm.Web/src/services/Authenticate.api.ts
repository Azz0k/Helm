import {AddData} from "./DataService.api.ts";

const authApiUrl = import.meta.env.VITE_AUTH_API_URL;
export const Authenticate = async ( accessToken:string) => {
  const res = await AddData(authApiUrl, "", accessToken);
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
};