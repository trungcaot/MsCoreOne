<template>
  <span class="toggle m-2" role="checkbox" tabindex="0"
    @click="toggle"
    @keydown.space.prevent="toggle"
    :aria-checked="toggled.toString()"
  ></span>
</template>

<script>
export default {
  model: {
    prop: 'toggled',
    event: 'toggle'
  },

  props: ['toggled'],
  
  methods: {
    toggle() {
      this.$emit('toggle', !this.toggled)
    }
  }
}
</script>

<style lang="scss" scoped>
.toggle {
  position: relative;
  display: inline-block;
  -ms-flex-negative: 0;
  flex-shrink: 0;
  border-radius: 9999px;
  cursor: pointer;
  height: 1.5rem;
  width: 3rem;
}
.toggle:focus {
  outline: 0;
  -webkit-box-shadow: 0 0 0 4px rgba(52, 144, 220, 0.5);
  box-shadow: 0 0 0 4px rgba(52, 144, 220, 0.5);
}
.toggle:before {
  display: inline-block;
  border-radius: 9999px;
  height: 100%;
  width: 100%;
  background-color: #dae1e7;
  content: "";
  -webkit-box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.1);
  box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.1);
  -webkit-transition: background-color 0.2s ease;
  transition: background-color 0.2s ease;
}
.toggle[aria-checked="true"]:before {
  background-color: #3490dc;
}
.toggle:after {
  position: absolute;
  top: 0;
  left: 0;
  border-radius: 9999px;
  height: 1.5rem;
  width: 1.5rem;
  background-color: #fff;
  border-width: 1px;
  border-color: #dae1e7;
  -webkit-box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.1);
  box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.1);
  content: "";
  -webkit-transition: -webkit-transform 0.2s ease;
  transition: -webkit-transform 0.2s ease;
  transition: transform 0.2s ease;
  transition: transform 0.2s ease, -webkit-transform 0.2s ease;
  -webkit-transform: translateX(0);
  transform: translateX(0);
}
.toggle[aria-checked="true"]:after {
  -webkit-transform: translateX(1.5rem);
  transform: translateX(1.5rem);
}
</style>