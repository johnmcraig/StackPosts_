<template>
  <div>
    <h1>
      This is StackPosts_ an imitation of Stack Overflow
      <button v-b-modal.addPostModal class="btn btn-primary mt-2 float-right">
        <i class="fas fa-plus"/> Start a post
      </button>
    </h1>
    <ul class="list-group post-previews mt-4">
      <post-preview
        v-for="post in posts"
        :key="post.id"
        :post="post"
        class="list-group-item list-group-item-action mb-3" />
    </ul>
    <add-post-modal @post-added="onPostAdded"/>
  </div>
</template>

<script>
import PostPreview from '@/components/post-preview'
import AddPostModal from '@/components/add-post-modal'
import axios from 'axios'

export default {
  components: {
    PostPreview,
    AddPostModal
  },
  data () {
    return {
      posts: []
    }
  },
  created () {
    axios.get('/posts').then(res => {
      this.posts = res.data
    })
  },
  methods: {
    onPostAdded (post) {
      this.posts = [post, ...this.posts]
    }
  }
}
</script>

<style>
.post-previews .list-group-item{
  cursor: pointer;
}
</style>
