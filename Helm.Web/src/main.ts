import { createApp } from 'vue';
import { createPinia } from 'pinia';
import './style.css'
import App from './App.vue'
import {msalInstance} from '@/config/msalConfig.ts';

try{
  await msalInstance.initialize();
}
catch(error){
  console.error('MSAL initialization error: ', error);
}
const pinia = createPinia();
const app = createApp(App);
app.use(pinia);
app.mount('#app');
