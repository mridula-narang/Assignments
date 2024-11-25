<script>
import { RegisterUser } from '@/scripts/RegisterService';
import { useRouter } from 'vue-router';

export default{
    name: 'RegisterComponent',
    data(){
        return{
            user:{
                username:'',
                password:'',
                email:'',
                phone:'',
                role:1,
            }
        }
    },
    setup(){
        const router = useRouter();
        return { router };
    },
    methods:{
        async register(){
            try {
                const response = await RegisterUser(this.user);
                console.log(response.data.username + ' registered successfully.');
                this.router.push('/login');
            } catch (error) {
                console.log('Registration failed.'+ error);
            }
        }
    }
}
</script>

<template>
    <div>Register</div>
    <div>
        <form>
            <div>
                <label for="username">Username</label>
                <input type="text" id="username" v-model="user.username">
            </div>
            <div>
                <label for="password">Password</label>
                <input type="password" id="password" v-model="user.password">
            </div>
            <div>
                <label for="email">Email</label>
                <input type="email" id="email" v-model="user.email">
            </div>
            <div>
                <label for="phone">Phone</label>
                <input type="text" id="phone" v-model="user.phone">
            </div>
            <div>
                <button @click.prevent="register">Register</button>
            </div>
        </form>
    </div>
</template>

<style>
</style>