import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../components/HomePage.vue'; 
import Register from '../components/RegisterPage.vue';
import Login from '../components/LoginPage.vue';
import Dashboard from '../components/DashboardPage.vue';

const routes = [
  { path: '/', component: HomePage }, 
  { path: '/register', component: Register },
  { path: '/login', component: Login },
  { path: '/dashboard', component: Dashboard, meta: { requiresAuth: true } },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth && !localStorage.getItem('token')) {
    next('/login'); 
  } else {
    next(); 
  }
});

router.beforeEach((to, from, next) => {
  if ((to.path === '/login' || to.path === '/register') && localStorage.getItem('token')) {
    next('/dashboard');
  } else {
    next();
  }
});

export default router;
