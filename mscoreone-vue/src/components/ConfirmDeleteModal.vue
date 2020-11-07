<template>
  <modal-dialog :show="show" @close="cancel">
    <div class="modal-header">
      <h5 class="modal-title">Are you sure?</h5>
      <button type="button" class="close" data-dismiss="modal" aria-label="Close">
        <span aria-hidden="true">&times;</span>
      </button>
    </div>
    <div class="modal-body">
      <p>Deleting your account cannot be undone.</p>
    </div>
    <div class="modal-footer">
        <button type="button" @click="cancel" class="btn btn-grey">Cancel</button>
        <button type="button" @click="confirmDelete" class="btn btn-danger" data-dismiss="modal">Delete it</button>
      </div>
  </modal-dialog>
</template>

<script>
import ModalDialog from './ModalDialog'

export default {
  props: ['show', 'accountId'],

  components: {
    ModalDialog
  },

  methods: {
    cancel(){
      this.$emit('close');
    },

    confirmDelete(){
      console.log(`Delete account ${this.accountId}`);
      this.$emit('close');
    }
  }

}
</script>

<style>

</style>