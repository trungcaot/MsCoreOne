<template>
  <form @submit.prevent="submit">
    <h1>Your Settings</h1>
    <v-text-field v-model="email" label="E-mail Address"></v-text-field>
    <toggle-input v-model="receiveNewsletter"></toggle-input>
    <v-btn type="submit" class="btn btn-primary float-right">Update Settings</v-btn>
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
      console.log({
        email: this.email,
        receiveNewsletter: this.receiveNewsletter
      });
    }
  }
}
</script>
