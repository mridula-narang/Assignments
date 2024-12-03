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
    { path: '/admin/add-hotel', component: AddHotelComponent },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

// Combined global navigation guard
router.beforeEach((to, from, next) => {
    const token = sessionStorage.getItem('Token');
    const isAuthenticated = !!token;
    let isAdmin = false;

    if (token) {
        try {
            const decodedToken = jwtDecode(token);
            console.log("Decoded Token:", decodedToken);  // Log the decoded token
            // Access the custom role claim
            isAdmin = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === 'Admin';
        } catch (error) {
            console.error("Error decoding token:", error);
        }
    }

    console.log("Token:", token);
    console.log("Is Admin:", isAdmin);

    // If the route does not require authentication (e.g., login, register), allow access
    if (!isAuthenticated && (to.path === '/' || to.path === '/login' || to.path === '/register')) {
        next(); // Allow access to these routes
    }
    // If the user is not authenticated and tries to access a protected route
    else if (!isAuthenticated && to.path !== '/' && to.path !== '/login' && to.path !== '/register') {
        next({ path: '/login' }); // Redirect to login if not authenticated
    }
    // Restrict access to admin-only routes
    else if (to.path.startsWith('/admin') && !isAdmin) {
        alert("Access denied! Admins only.");
        next({ path: '/' }); // Redirect to home if not an admin
    } else {
        next(); // Allow access for authenticated users (both normal and admin)
    }
});



export default router;
