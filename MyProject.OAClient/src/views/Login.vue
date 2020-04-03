<template>
  <div>
    <h1>登录</h1>
    <input type="text"  v-model="form.username"/>
    <input type="text" v-model="form.password"/>
    <button @click="login">登录</button>
  </div>
</template>
<script>
import axios from 'axios';
export default {
  data() {
    return {
      form: {
        username: "",
        password: ""
      }
    };
  },
  methods: {
      login(){
        axios.get('/api/login/userlogin',{
            params:this.form
        }).then(res=>{
            if(res.data.success==true){
                this.$router.push('/admin/user')
            }else{
                alert("账号或密码有误")
            }
        })

      }
  }
};
</script>