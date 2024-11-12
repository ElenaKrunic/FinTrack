import { createRouter, createWebHistory } from 'vue-router';
import AuthForm from './components/AuthForm.vue';
import Home from './components/Home.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: AuthForm,
    props: { isLogin: true }
  },
  {
    path: '/register',
    name: 'Register',
    component: AuthForm,
    props: { isLogin: false }
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
