<template>
  <div class="row">
    <div class="col-md-6 offset-md-3">
      <h3>Login</h3>
      <!-- <div class="alert alert-danger" v-if="error">{{ error }} </div> -->
      <form novalidate @submit.prevent="onSubmit">
        <div class="form-group">
          <label for="username">Username</label>
          <input
            type="text"
            class="form-control"
            name="username"
            v-model="credentials.username"
            v-validate="'required'"
          />
          <!-- <span class="text-danger" v-if="error.has('username')">* Required </span>  -->
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <input
            type="password"
            class="form-control"
            name="password"
            v-model="credentials.password"
            v-validate="'required'"
          />
          <!-- <span class="text-danger" v-if="error.has('password')">* Required </span> -->
        </div>
        <div class="form-group">
          <input type="submit" class="btn btn-success" value="Login" />
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      credentials: {},
    };
  },
  methods: {
    noSubmit() {
      this.$validator.validateAll().then(result => {
        if (result) {
          alert(this.credentials.username);
        }
      });
    },
    error() {

    }
  }
};
</script>