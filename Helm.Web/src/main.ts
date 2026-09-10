import { createApp } from 'vue';
import '@/assets/index.css';
import '@/assets/scrollbar.css';
import '@/assets/themes.css';
import '@/assets/chart-theme.css';
import './style.css';
import App from './App.vue';
import { setupPlugins } from '@/plugins/plugins.ts';

const app = createApp(App);
await setupPlugins(app);
app.mount(  '#app');
