<script>
export default {
  props: ['tag'],

  render(createElement) {
    return createElement(this.tag, {
      attrs: {
        class: "btn btn-primary mr-2"
      },
      on: {
        click: () => alert(`clicked with tag is ${this.tag}!`)
      }
    }, "Hello world")
  }

}
</script>