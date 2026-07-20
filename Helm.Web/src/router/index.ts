import { createRouter, createWebHistory } from 'vue-router';
import { handleHotUpdate, routes } from 'vue-router/auto-routes'
import Welcome from '@/pages/welcome/index.vue';
import Default from '@/layouts/default.vue';

const routes = [
  { path: '/welcome',
    component: Default,
    children: [
      {
        path: '',
        components: Welcome,
      }
    ],
  },
]
const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() {
    return { left: 0, top: 0 , behavior: 'smooth' };
  }
});