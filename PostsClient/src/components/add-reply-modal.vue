<template>
  <b-modal id="addReplyModal" ref="addReplyModal">
    <b-form @submit.prevent="onSubmit" @reset.prevent="onCancel">
      <b-form-group label="Your Reply" label-for="replyInput">
        <b-form-textarea
          id="replyInput"
          v-model="form.body"
          placeholder="Enter your reply here..."
          :rows="6"
          :max-rows="10">
        </b-form-textarea>
      </b-form-group>
    </b-form>

    <button class="btn btn-primary float-right ml-2" type="submit">Submit</button>
    <button class="btn btn-secondary float-right" type="reset">Cancel</button>
  </b-modal>
</template>

<script>
export default {

}
</script>

<style>
</style>
