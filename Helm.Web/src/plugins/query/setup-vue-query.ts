import type {App} from "vue";
import { VueQueryPlugin } from '@tanstack/vue-query';

export const setupVue = (app: App) => {
  app.use(VueQueryPlugin);
};