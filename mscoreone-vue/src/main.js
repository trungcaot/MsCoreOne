import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'

Vue.config.productionTip = false

const app = new Vue({
  vuetify,
  render: h => h(App),
}).$mount('#app')

export default app;