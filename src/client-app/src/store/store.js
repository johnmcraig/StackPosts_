import Vue from 'vue';
import Vuex from 'vuex';
import Axios from 'axios';

Vue.use(Vuex);

const baseUrl = 'api';

const postsUrl = `${baseUrl}/posts`;

export default Vuex.Store({
  strict: true,
  state: {
    posts: [],
    replies: [],
  },
  mutations: {
    setPosts(state, posts) {
      state.posts = posts;
    },
  },
  actions: {
    async getPosts(context) {
      context.commit('setPosts', await Axios.get(postsUrl));
    },
  },
});
