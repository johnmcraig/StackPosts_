<template>
  <h3 class="text-center scoring">
    <button class="btn btn-link btn-lg p-0 d-block mx-auto" @click.stop="onUpvote">
      <i class="fas fa-sort-up"/>
    </button>
    <span class="d-block mx-auto">{{ post.score }}</span>
    <button class="btn btn-link btn-lg p-0 d-block mx-auto" @click.stop="onDownvote">
      <i class="fas fa-sort-down"/>
    </button>
  </h3>
</template>

<script>
export default {
  props: {
    post: {
      type: Object,
      required: true
    }
  },
  created () {
    // Listen to score changes coming from SignalR events
    this.$postHub.$on('score-changed', this.onScoreChanged)
  },
  beoreDestroy () {
    this.$postHub.$off('score-changed', this.onScoreChanged)
  },
  methods: {
    onUpvote () {
      this.$http.patch(`api/post/${this.post.id}/upvote`).then(res => {
        Object.assign(this.post, res.data)
      })
    },
    onDownvote () {
      this.$http.patch(`api/post/${this.post.id}/downvote`).then(res => {
        Object.assign(this.post, res.data)
      })
    },
    onScoreChanged ({ postId, score }) {
      if (this.post.id !== postId) return
      Object.assign(this.post, { score })
    }
  }
}
</script>

<style>
.scoring .btn-link {
  line-height: 1;
}
</style>
