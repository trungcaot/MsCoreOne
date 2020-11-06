<template>
  <div class="modal fade show modal-active" v-if="show" id="exampleModal">
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <h1 class="text-center text-2xl font-bold mb-4">
            Exciting new features are here!
          </h1>
          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. At ut eligendi quod tempore totam explicabo sit consectetur architecto? Tempora, repellat est rem ut esse ab officia saepe ratione tempore. Obcaecati.</p>
        </div>
        <div class="modal-footer">
          <button @click="dismiss" type="button" class="btn btn-primary">Dismiss</button>
        </div>
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