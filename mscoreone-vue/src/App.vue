<template>
  <div class="min-h-screen bg-grey-darker p-8">
    <div class="max-w-sm mx-auto">
      <user-settings-form></user-settings-form>
    </div>
  </div>
</template>

<script>
import UserSettingsForm from './components/UserSettingsForm.vue'

export default {
  name: 'App',
  components: {
    UserSettingsForm
  }
}
</script>

<style src="./assets/css/app.css" />
