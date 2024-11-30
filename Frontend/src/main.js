import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import './assets/tailwind.css';
import "@fortawesome/fontawesome-free/css/all.css";
import "@fortawesome/fontawesome-free/js/all.js";

createApp(App)
  .use(router)
  .mount('#app');
