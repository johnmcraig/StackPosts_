<template>
  <b-modal
    id="addReplyModal"
    ref="addReplyModal"
    hide-footer
    title="Add a Reply"
    @hidden="onHidden"
  >
    <b-form @submit.prevent="onSubmit" @reset.prevent="onCancel">
      <b-form-group label="Your Reply:" label-for="replyInput">
        <b-form-textarea
          id="replyInput"
          v-model="form.body"
          placeholder="Enter your reply here..."
          :rows="6"
          :max-rows="10"
        ></b-form-textarea>
      </b-form-group>

      <button class="btn btn-primary float-right ml-2" type="submit">Submit</button>
      <button class="btn btn-secondary float-right" type="reset">Cancel</button>
    </b-form>
  </b-modal>
</template>

<script>
import axios from 'axios'

export default {
  props: {
    postId: {
      type: Number,
      required: true
    }
  },
  data () {
    return {
      form: {
        title: '',
        body: ''
      }
    }
  },
  methods: {
    onSubmit () {
      axios.post(`/posts/${this.postId}/reply`, this.form).then(res => {
        this.$emit('reply-added', res.data)
        this.$refs.addReplyModal.hide()
      })
    },
    onCancel () {
      this.$refs.addReplyModal.hide()
    },
    onHidden () {
      Object.assign(this.form, {
        title: '',
        body: ''
      })
    }
  }
}
</script>

<style>
</style>
