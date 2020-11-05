<template>
    <v-switch 
      :v-model="toggled"
      @click="toggle"
      label="Send you occasional news and updates."
    ></v-switch>
</template>

<script>
export default {
  model: {
    prop: 'toggled',
    event: 'toggle'
  },

  props: ['toggled'],
  
  methods: {
    toggle() {
      this.$emit('toggle', !this.toggled)
    }
  }
}
</script>
