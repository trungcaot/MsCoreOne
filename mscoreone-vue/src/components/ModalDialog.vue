<template>
  <div class="modal fade show modal-active" v-show="show">
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['show'],

  methods: {
    dismiss(){
      this.$emit('close');
    }
  }

}
</script>

<style lang="scss" scoped>
.modal-active {
  display: block;
}
</style>