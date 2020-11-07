<template>
  <modal-dialog :show="show" @close="dismiss">
    <div class="modal-body">
      <h3 class="text-center text-2xl font-bold mb-4">
        Exciting new features are here!
      </h3>
      <p class="text-center">Lorem ipsum dolor sit amet, consectetur adipisicing elit. At ut eligendi quod tempore totam explicabo sit consectetur architecto? Tempora, repellat est rem ut esse ab officia saepe ratione tempore. Obcaecati.</p>
    </div>
    <div class="modal-footer">
      <button @click="dismiss" type="button" class="btn btn-primary">Dismiss</button>
    </div>
  </modal-dialog>
</template>

<script>
import ModalDialog from './ModalDialog'

export default {
  props: ['show'],

  components:{
    ModalDialog
  },

  methods: {
    dismiss(){
      this.$emit('close');
    }
  }
}
</script>

