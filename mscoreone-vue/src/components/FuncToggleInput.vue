<script>
import ToggleInput from "./ToggleInput";

export default {
  props: [],

  data() {
    return {
      toggled: true
    }
  },

  components: {
    ToggleInput
  },

  render(createElement){
    return createElement('ToggleInput', {
        props: {
          toggled: this.toggled
        },
        on: {
          toggle: newValue => (this.toggled = newValue)
        }
      }
    )
  }
}
</script>

<style>

</style>