import Vue from 'vue'
import Router from 'vue-router'
import HomePage from '@/views/Home.vue'
import PostPage from '@/views/Post.vue'
import Login from '@/views/Login.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Home',
      component: HomePage
    },
    {
      path: '/post/:id',
      name: 'Post',
      component: PostPage
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    }
  ]
})
