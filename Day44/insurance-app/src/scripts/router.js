import HelloWorld from "@/components/HelloWorld.vue";
import LoginComponent from "@/components/LoginComponent.vue";
import PoliciesComponent from "@/components/PoliciesComponent.vue";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
    {path: '/', component: HelloWorld},
    {path: '/login', component: LoginComponent},
    {path: '/policies', component: PoliciesComponent},
]

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;