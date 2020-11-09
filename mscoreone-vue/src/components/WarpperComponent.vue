<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <div class="card">
          <div class="card-header">
            Extending Components Using Composition
          </div>
          <div class="card-body">
            <div class="row">
              <div class="col">
                <button type="button" @click="showAnnouncement = true" class="btn btn-outline-primary">Open Announcement Modal</button>
                <announcement-modal :show="showAnnouncement" @close="showAnnouncement = false"></announcement-modal>
              </div>
              <div class="col">
                <button type="button" @click="showConfirmDelete = true" class="btn btn-outline-danger">Delete Account</button>
                <confirm-delete-modal :show="showConfirmDelete" :accountId="1" @close="showConfirmDelete = false"></confirm-delete-modal>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col">
        <div class="card">
          <div class="card-header">
            Render function
          </div>
          <div class="card-body">
            <div class="row">
              <div class="col">
                <hello-world tag="a"></hello-world>
                <hello-world tag="span"></hello-world>
              </div>
              <div class="col">
                <func-toggle-input></func-toggle-input>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AnnouncementModal from './AnnouncementModal'
import ConfirmDeleteModal from './ConfirmDeleteModal'
import HelloWorld from './HelloWorld'
import FuncToggleInput from "./FuncToggleInput";

export default {
  components: {
    AnnouncementModal,
    ConfirmDeleteModal,
    HelloWorld,
    FuncToggleInput
  },

  data(){
    return {
      showConfirmDelete: false,
      showAnnouncement: false,
    }
  }
}
</script>
