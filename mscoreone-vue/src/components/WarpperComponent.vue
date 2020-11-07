<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <div class="row p-2">
          <button type="button" @click="showAnnouncement = true" class="btn btn-outline-primary">Open Announcement Modal</button>
          <announcement-modal :show="showAnnouncement" @close="showAnnouncement = false"></announcement-modal>
        </div>
        <div class="row p-2">
          <button type="button" @click="showConfirmDelete = true" class="btn btn-outline-danger">Delete Account</button>
          <confirm-delete-modal :show="showConfirmDelete" :accountId="1" @close="showConfirmDelete = false"></confirm-delete-modal>
        </div>
      </div>
      <div class="col">
        COL B
      </div>
    </div>
  </div>
</template>

<script>
import AnnouncementModal from './AnnouncementModal'
import ConfirmDeleteModal from './ConfirmDeleteModal'

export default {
  components: {
    AnnouncementModal,
    ConfirmDeleteModal
  },

  data(){
    return {
      showConfirmDelete: false,
      showAnnouncement: false,
    }
  }
}
</script>
