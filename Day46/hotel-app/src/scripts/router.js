import HelloWorld from "@/components/HelloWorld.vue";
import LoginComponent from "@/components/Login/LoginComponent.vue";
import RoomsComponent from "@/components/RoomsComponent.vue";
import HotelsComponent from "@/components/HotelsComponent.vue";
import RegisterComponent from "@/components/Register/RegisterComponent.vue";
import UserBookingsComponent from "@/components/UserBookingsComponent.vue";
import { createRouter, createWebHistory } from 'vue-router';
import Paymentcomponent from "@/components/Paymentcomponent.vue";

const routes=[
    {path: '/', component: HelloWorld},
    {path: '/login', component: LoginComponent},
    {path: '/rooms', component: RoomsComponent},
    {path: '/hotels', component: HotelsComponent},
    {path: '/register', component: RegisterComponent},
    {path: '/room/:id', component: RoomsComponent, props: true},
    {path: '/bookings', component: UserBookingsComponent},
    {path: '/payment/:bookingid', component: Paymentcomponent}
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;