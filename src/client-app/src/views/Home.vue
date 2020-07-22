<template>
  <div>
    <h1>
      Welcome to StackPosts_
      <button v-b-modal.addPostModal class="btn btn-primary mt-2 float-right">
        <i class="fas fa-plus" /> Start a post
      </button>
    </h1>
    <ul class="list-group post-previews mt-4">
      <post-preview
        v-for="post in posts"
        :key="post.id"
        :post="post"
        class="list-group-item list-group-item-action mb-3"
      />
    </ul>
    <add-post-modal @post-added="onPostAdded" />
  </div>
</template>

<script>
import PostPreview from "@/components/post-preview";
import AddPostModal from "@/components/add-post-modal";
//import axios from "axios";
import PostsDataService from "../services/PostsDataService";

export default {
  components: {
    PostPreview,
    AddPostModal
  },
  data() {
    return {
      posts: []
    };
  },
  created() {
    this.fetchPosts();
    // axios
    //   .get("/api/posts")
    //   .then(res => {
    //     this.posts = res.data;
    //     console.log(this.data);
    //   })
    //   .catch(error => {
    //       console.log(error);
    //     });
  },
  methods: {
    fetchPosts() {
      PostsDataService.getAll()
        .then(res => {
          this.posts = res.data;
          console.log(res.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    onPostAdded(post) {
      this.posts = [post, ...this.posts];
    }
  }
};
</script>

<style>
.post-previews .list-group-item {
  cursor: pointer;
}
</style>
