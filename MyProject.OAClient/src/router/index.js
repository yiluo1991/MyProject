import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)
import axios from 'axios';
const routes = [
  {
    path: '/login',
    name: 'login',
    component:()=> import('../views/Login.vue')
  },{
    path:'/admin',
    name:'admin',
    component:()=> import('../views/Admin.vue'),
    beforeEnter:(to, from, next) => {
      axios.get('/api/login/checklogin').then(res=>{
        if(res.data.success==true){
          next();
        }else{
          next('/login');
        }
      })
    },
    children:[
      {
        path:'user',
        name:'user',
        component:()=> import('../views/User.vue')
      },
      {
        path:'product',
        name:'product',
        component:()=> import('../views/Product.vue')
      }
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
