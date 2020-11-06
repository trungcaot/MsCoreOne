<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <button type="button" @click="modalOpen = true" class="btn btn-outline-primary">Open Modal</button>
        <announcement-modal :show="modalOpen" @close="modalOpen = false"></announcement-modal>
      </div>
      <div class="col">
        COL B
      </div>
    </div>
  </div>
</template>

<script>
import AnnouncementModal from './AnnouncementModal'

export default {
  components: {
    AnnouncementModal
  },

  data(){
    return {
      modalOpen: false
    }
  }
}
</script>
