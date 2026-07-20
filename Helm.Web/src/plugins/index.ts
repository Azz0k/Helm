import { setupPinia } from '@/plugins/pinia/setup.ts';
import type { App } from 'vue';
import { setupMSAL } from '@/plugins/msal/setup.ts';
import { setupNProgress } from '@/plugins/nprogress/setup.ts';

export const setupPlugins = async (app: App) => {
  await setupMSAL();
  setupNProgress();
  setupPinia(app);
}