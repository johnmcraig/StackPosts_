<template>
  <article class="container" v-if="post">
    <header class="row align-items-center">
      <post-score :post="post" class="col-1" />
      <h3 class="col-11">{{ post.title }}</h3>
    </header>
    <p class="row">
      <vue-markdown class="offset-1 col-11">{{ post.body }}</vue-markdown>
    </p>
    <ul class="list-unstyled row">
      <!-- v-if="hasReplies" -->
      <li v-for="reply in post.replies" :key="reply.id" class="offset-1 col-11 border-top py-2">
        <vue-markdown>{{ reply.body }}</vue-markdown>
      </li>
    </ul>
    <footer>
      <button class="btn btn-primary float-right" v-b-modal.addReplyModal>
        <i class="fas fa-edit" /> Post your Reply
      </button>
      <button class="btn btn-link float-right" @click="onReturnHome">Back to list</button>
    </footer>
    <add-reply-modal :post-id="this.post.id" @reply-added="onReplyAdded" />
  </article>
</template>

<script>
import VueMarkdown from "vue-markdown";
import PostScore from "@/components/post-score";
import AddReplyModal from "@/components/add-reply-modal";
import PostsDataService from '../services/PostsDataService';

export default {
  components: {
    VueMarkdown,
    PostScore,
    AddReplyModal
  },
  data() {
    return {
      post: null,
      replies: []
    };
  },
  computed: {
    hasReplies () {
      return this.post.replies.length >= 0
    }
  },
  created() {
    this.getPost()
    //this.$postHub.$on("reply-added", this.onReplyAdded);
    //this.$postHub.$on("score-changed", this.onScoreChanged);
  },
  // beforeDestroy() {
  //   this.$postHub.postClosed(this.postId);
  //   this.$postHub.$off("reply-added", this.onReplyAdded);
  // },
  methods: {
    getPost(id) {
      PostsDataService.get(id)
        .then(res => {
          this.post = res.data;
          console.log(res.data);
        })
        .catch(e => {
          console.log(e);
        });
    },
    onReturnHome() {
      this.$router.push({ name: "Home" });
    },
    onReplyAdded(reply) {
      if (this.post.id !== reply.postId) return;
      if (!this.post.replies.find(r => r.id === reply.id)) {
        this.post.replies.push(reply);
      }
    }
  },
  mounted() {
    this.getPost(this.$route.params.id)
  }
};
</script>

<style>
</style>
