<script>
import contacts from '@/data/contacts.json'

export default {
  data(){
    return { contacts }
  },

  render(createElement) {
    return createElement("div", {}, [
      createElement("h4", {}, "Your contacts"),
      createElement("ul", {},
      this.contacts.map(contact => {
        return createElement("li", {}, `${contact.name.first}`)
      }))
    ])
  }
}
</script>