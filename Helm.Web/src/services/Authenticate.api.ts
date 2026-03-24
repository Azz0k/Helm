import {AddData} from "./DataService.api.ts";

const authApiUrl = import.meta.env.VITE_AUTH_API_URL;
export const Authenticate = async () => {
  const res = await AddData(authApiUrl, "");
  if (res.status !== 200) {
    throw res.status;
  }
  return res.json();
};