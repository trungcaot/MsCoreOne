<template>
  <div class="container">
    <v-app>
      <user-settings-form></user-settings-form>
    </v-app>
  </div>
</template>

<script>
import UserSettingsForm from './components/UserSettingsForm'

export default {
  name: 'App',
  components: {
    UserSettingsForm
  }
}
</script>


