import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/Login'

// import Layui from 'layui-src'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Login',
      component: Login
    }
  ]
})
