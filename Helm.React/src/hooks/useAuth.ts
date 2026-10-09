import  { msalInstance } from "../config/msalConfig.ts";
import {rootStore} from "@/stores/root-store.ts";

export const useAuth = ()=> {
  const login = async () => {
    try {
      await msalInstance.loginRedirect();
    }
    catch (error) {
      console.log("login error: ", error);
    }
  }
  const logout = async () => {
    try {
      await msalInstance.logoutRedirect({
        account: msalInstance.getActiveAccount()
      });
    }
    catch (error) {
      console.log("logout error: ", error);
    }
  }
  const handleRedirect = async () => {
    rootStore.authStore.isRedirectDone = false;
    try {
      const result = await msalInstance.handleRedirectPromise();
      if (result) {
        msalInstance.setActiveAccount(result.account);
        rootStore.authStore.user = result.account;
        return;
      }
      const activeAccount = msalInstance.getActiveAccount();
      if (activeAccount) {
        rootStore.authStore.user = activeAccount;
        return;
      }
      const currentAccounts = msalInstance.getAllAccounts();
      if (currentAccounts.length > 0) {
        msalInstance.setActiveAccount(currentAccounts[0]);
        rootStore.authStore.user = currentAccounts[0];
        return;
      }
      await login();
    }
    catch (error) {
      console.log("handleRedirect error: ", error);
    }
    finally {
      rootStore.authStore.isRedirectDone = true;
    }
  };
  const getAccessToken = async () => {
    const account =  msalInstance.getActiveAccount();
    const accessTokenRequest = {
      scopes: ["openid"],
      account: account ?? undefined,
    };
    let token: string;
    try {
      const response = await msalInstance.acquireTokenSilent(accessTokenRequest);
      token = response.accessToken;
    }
    catch {
      const response = await msalInstance.acquireTokenSilent(accessTokenRequest);
      token = response.accessToken;
    }
    return token;
  };
  return {  login, logout, handleRedirect, getAccessToken };
};
