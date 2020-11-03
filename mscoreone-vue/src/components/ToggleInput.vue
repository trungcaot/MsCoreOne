<template>
  <div class="custom-control custom-checkbox">
    <input class="custom-control-input" id="customCheck1" type="checkbox" @click="toggle" :v-model="value.toString()">
    <label class="custom-control-label" for="customCheck1">
      Send you occasional news and updates.
    </label>
  </div>
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
