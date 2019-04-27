<template>
  <li class="card container" @click="onOpenPost">
    <div class="card-body row">
      <post-score :post="post" class="col-1"/>
      <div class="col-11">
        <h5 class="card-title">{{ post.title }}</h5>
        <p>
          <vue-markdown :source="post.body"/>
        </p>
        <a href="#" class="card-link">
          View post
          <span class="badge badge-success">{{ post.replyCount }}</span>
        </a>
      </div>
    </div>
  </li>
</template>

<script>
import VueMarkdown from 'vue-markdown'
import PostScore from '@/components/post-score'

export default {
  components: {
    VueMarkdown,
    PostScore
  },
  props: {
    post: {
      type: Object,
      required: true
    }
  },
  methods: {
    onOpenPost () {
      this.$router.push({ name: 'Post', params: { id: this.post.id } })
    },
    onReplyCountChanged ({ postId, replyCount }) {
      if (this.post.id !== postId) return
      Object.assign(this.post, { replyCount })
    }
  }
}
</script>

<style>

</style>
