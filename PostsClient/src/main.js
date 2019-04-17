import Vue from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import '@fortawesome/fontawesome-free/css/all.css'
import PostHub from './PostHub'

Vue.config.productionTip = false

// Setup axios
axios.defaults.baseURL = 'https://localhost:5001'
Vue.prototype.$http = axios

// Install Vue extensions
Vue.use(BootstrapVue)
Vue.use(PostHub)

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
