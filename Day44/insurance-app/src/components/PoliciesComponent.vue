<script>
import { GetPolicy } from '@/scripts/AddPolicy';
import PolicyComponent from './PolicyComponent.vue';

export default {
    name: 'PoliciesComponent',
    data(){
        return{
            policies: []
        }
    },
    components:{
        PolicyComponent
    },
    created(){
        GetPolicy().then(response => {
            console.log("hi");
            this.policies = response.data;
            console.log(this.policies);
        }
        )
    }
}
</script>

<template>
    <div>
        <h1>Policies</h1>
        <div v-for="policy in policies" :key="policy.id">
            <PolicyComponent :policy="policy"/> 
        </div>
    </div>
</template>

<style>
</style>