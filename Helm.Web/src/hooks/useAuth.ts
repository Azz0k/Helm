import  { msalInstance } from '../config/msalConfig.ts';
import {useAuthStore} from "@/stores/auth.ts";

export function useAuth() {
  const authStore = useAuthStore();
  const login = async () => {
    try {
      await msalInstance.loginRedirect();
    }
    catch (error) {
      console.log('login error: ', error);
    }
  }
  const logout = async () => {
    try {
      await msalInstance.logoutRedirect({
        account: msalInstance.getActiveAccount()
      });
    }
    catch (error) {
      console.log('logout error: ', error);
    }
  }
  const handleRedirect = async () => {
    authStore.isRedirectDone = false;
    try {
      const result = await msalInstance.handleRedirectPromise();
      if (result) {
        msalInstance.setActiveAccount(result.account);
        authStore.user = result.account;
        return;
      }
      const activeAccount = msalInstance.getActiveAccount();
      if (activeAccount) {
        authStore.user = activeAccount;
        return;
      }
      const currentAccounts = msalInstance.getAllAccounts();
      if (currentAccounts.length > 0) {
        msalInstance.setActiveAccount(currentAccounts[0]);
        authStore.user = currentAccounts[0];
        return;
      }
      await login();
    }
    catch (error) {
      console.log('handleRedirect error: ', error);
    }
    finally {
      authStore.isRedirectDone = true;
    }
  }
  return {  login, logout, handleRedirect };
}
