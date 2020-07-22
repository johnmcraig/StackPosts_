import Vue from 'vue';
import App from './App.vue';
import router from './router';
import axios from "axios";
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import '@fortawesome/fontawesome-free/css/all.css';
//import PostHub from './post-hub';
import store from './store';

Vue.config.productionTip = false;

// Setup axios
axios.defaults.baseURL = 'http://localhost:5000/api';
Vue.prototype.$http = axios;

// Install Vue extensions
Vue.use(BootstrapVue);
//Vue.use(PostHub);

new Vue({
  store,
  router,
  render: (h) => h(App),
}).$mount('#app');
