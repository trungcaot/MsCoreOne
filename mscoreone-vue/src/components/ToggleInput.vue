<template>
    <v-switch 
      :v-model="test"
      label="Send you occasional news and updates."
    ></v-switch>
</template>

<script>
export default {
  props: ['value'],

  methods: {
    toggle() {
      this.$emit('input', !this.value)
    }
  }
}
</script>
