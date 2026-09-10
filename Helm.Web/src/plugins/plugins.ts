import { setupPinia } from '@/plugins/pinia/setup-pinia.ts';
import type { App } from 'vue';
import { setupMSAL } from '@/plugins/msal/setup-msal.ts';
import { setupNProgress } from '@/plugins/nprogress/setup-nprogress.ts';
import {setupRouter} from "@/plugins/router/setup-router.ts";

export const setupPlugins = async (app: App) => {
  await setupMSAL();
  setupNProgress();
  setupPinia(app);
  setupRouter(app);
}