import HelloWorld from "@/components/HelloWorld.vue";
import LoginComponent from "@/components/Login/LoginComponent.vue";
import RoomsComponent from "@/components/RoomsComponent.vue";
import HotelsComponent from "@/components/HotelsComponent.vue";
import RegisterComponent from "@/components/Register/RegisterComponent.vue";
import UserBookingsComponent from "@/components/UserBookingsComponent.vue";
import { createRouter, createWebHistory } from 'vue-router';
import PaymentPageComponent from "@/components/Paymentcomponent.vue";
import GalleryComponent from "@/components/GalleryComponent.vue";
import NotFoundComponent from "@/components/NotFoundComponent.vue";
import { jwtDecode } from "jwt-decode";
import AddHotelComponent from "@/components/AddHotelComponent.vue";
import AddRoomComponent from "@/components/AddRoomComponent.vue";
import ForbiddenComponent from "@/components/ForbiddenComponent.vue";

const routes = [
    { path: '/', component: HelloWorld },
    { path: '/login', component: LoginComponent },
    { path: '/rooms', component: RoomsComponent },
    { path: '/hotels', component: HotelsComponent },
    { path: '/register', component: RegisterComponent },
    { path: '/room/:id', component: RoomsComponent, props: true },
    { path: '/bookings', component: UserBookingsComponent },
    { path: '/payment/:bookingid', component: PaymentPageComponent },
    { path: '/gallery', component: GalleryComponent },
    { path: '/:pathMatch(.*)*', component: NotFoundComponent },
    { path: '/admin/add-hotel', component: AddHotelComponent, meta: { requiresAdmin: true } },
    {path:'/admin/add-room', component: AddRoomComponent, meta: { requiresAdmin: true }},
    {path: '/forbidden', component: ForbiddenComponent}
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    const token = sessionStorage.getItem('Token');
    const isAuthenticated = !!token;
    let isAdmin = false;

    if (token) {
        try {
            const decodedToken = jwtDecode(token);
            isAdmin = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === 'Admin';
        } catch (error) {
            console.error("Error decoding token:", error);
        }
    }
    
    console.log("Is Admin:", isAdmin);
    if (!isAuthenticated && (to.path === '/' || to.path === '/login' || to.path === '/register')) {
        next(); 
    }
    else if (!isAuthenticated && to.path !== '/' && to.path !== '/login' && to.path !== '/register') {
        next({ path: '/login' }); 
    }
    else if (to.path.startsWith('/admin') && !isAdmin) {
        alert("Access denied! Admins only.");
        next({ path: '/forbidden'}); 
    } else {
        next(); 
    }

    if (to.meta.requiresAdmin && !isAdmin) {

        next({ name: "Forbidden" });
      } else {
        next(); 
      }
});



export default router;
