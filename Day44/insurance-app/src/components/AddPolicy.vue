<script>
import { AddPolicy } from '@/scripts/AddPolicy.js'
export default{
    name:'AddPolicyComponent',
    data(){
        return {
            policy:{
                policyName:'',
                policyType:'',
                userName:''
            },
            addPolicy:()=>{
                event.preventDefault();
                const addPol = async()=>{
                    var data = await AddPolicy(this.policy.policyName, this.policy.policyType, this.policy.userName);
                    console.log(data);
                    alert("Policy added successfully");
                }
                addPolicy();
            }
        }
    }
}
</script>

<template>
    <div>
        <form>
            <div>
                <label class="form-control" for="polname">Policy Name</label>
                <input class="form-control" type="text" id="polname" v-model="policy.policyName">
            </div>
            <div>
                <label class="form-control" for="poltype">Policy Type</label>
                <input class="form-control" type="text" id="poltype" v-model="policy.policyType">
            </div>
            <div>
                <label class="form-control" for="username">User Name</label>
                <input class="form-control" type="text" id="username" v-model="policy.userName">
            </div>
            <button class="btn btn-success" @click="addPolicy()" type="submit">Submit
            </button>
        </form>
    </div>
</template>