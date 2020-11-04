import Vue from 'vue'
import Vuetify from 'vuetify/lib'

Vue.use(Vuetify)

const opts = {
  theme: {
    dark: false
  },
  options: {
    customProperties: true
  },
  icons: {
    iconfont: "mdi"
  }
}

export default new Vuetify(opts)