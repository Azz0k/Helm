import { setupMSAL } from '@/plugins/msal/setup-msal.ts';
import { setupNProgress } from '@/plugins/nprogress/setup-nprogress.ts';

export const setupPlugins = async () => {
  await setupMSAL();
  setupNProgress();
}