import { createRouter, createWebHistory } from 'vue-router';
import MainTitle from "/src/views/MainTitle.vue";
import Authorization from "/src/views/Authorization.vue";
import Startups from "/src/views/Startups.vue";
import Projects from "/src/views/Projects.vue";
import Commands from "/src/views/Commands.vue";


const routes = [
    {
        path: '/',
        name: 'Authorization',
        component: Authorization
    },
    {
        path: '/main-title',
        name: 'MainTitle',
        component: MainTitle,
        meta: { requiresAuth: true }
    },
    {
        path: '/command',
        name: 'Commands',
        component: Commands,
    },

];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
