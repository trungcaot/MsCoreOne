<template>
  <form @submit.prevent="submit">
    <h1>Your Settings</h1>
    <div class="form-group">
      <label for="exampleInputEmail1">Email Address</label>
      <input type="email" v-model="email" class="form-control" placeholder="Enter email">
    </div>
    <div class="form-group">
      <label for="exampleInputEmail1">Newsletter</label>
      <toggle-input v-model="receiveNewsletter"></toggle-input>
    </div>
    <button type="submit" class="btn btn-primary float-right">Update Settings</button>
  </form>
</template>

<script>
import ToggleInput from "./ToggleInput.vue"

export default {
  components: {
    ToggleInput
  },
  data() {
    return {
      email: "jane@example.com",
      receiveNewsletter: false
    }
  },
  methods: {
    submit() {
      // console.log({
      //   email: this.email,
      //   receiveNewsletter: this.receiveNewsletter
      // });
    }
  }
}
</script>
