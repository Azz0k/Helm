import type { App } from 'vue';
import {router} from "@/router/router.ts";


export const setupRouter= (app: App) => {
  app.use(router);
}