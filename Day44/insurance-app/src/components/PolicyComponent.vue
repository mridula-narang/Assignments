<script>
export default {
    name: 'PolicyComponent',
    props:{
        policy:{
            policyName : '',
            policyType : ''
        }
    }
}   
</script>

<template>
    <div>
        <h1>Policies</h1>
        <div>
            <h2>{{ policy.policyName }}</h2>
            <p>{{ policy.policyType }}</p>
        </div>
    </div>
</template>