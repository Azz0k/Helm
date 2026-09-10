import { createRouter, createWebHistory } from 'vue-router';
import Welcome from '@/pages/welcome/welcome.vue';
import nprogress from 'nprogress';

const routes = [
  { path: '/', component: Welcome },
  { path: '/dashboard', component: Welcome },
  { path: '/help-center', component: Welcome },
  { path: '/apps', component: Welcome },
  { path: '/users', component: Welcome },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() {
    return { left: 0, top: 0 , behavior: 'smooth' };
  }
});
router.beforeEach(()=>{
  nprogress.start();
});
router.afterEach(() => {
  nprogress.done()
});

export { router };
