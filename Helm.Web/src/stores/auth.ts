import {defineStore} from 'pinia';
import type {AccountInfo} from '@azure/msal-browser';

export const useAuthStore = defineStore('auth',{
  state: ()=>({
    isRedirectDone: false,
    user: null as AccountInfo | null,
  }),
  getters: {
    isAuthenticated: (state) => state.user !== null,
  }
});