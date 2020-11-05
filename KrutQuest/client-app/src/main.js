// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import store from "./store"
import BootstrapVue from 'bootstrap-vue'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import {
  ButtonPlugin, NavbarPlugin, ModalPlugin, FormInputPlugin, InputGroupPlugin,
  LayoutPlugin, FormPlugin, FormSelectPlugin, SpinnerPlugin, TablePlugin, LinkPlugin,
  FormFilePlugin, AlertPlugin, ButtonGroupPlugin, ImagePlugin
} from 'bootstrap-vue'

import LinkBack from '@/components/tech/LinkBack'
import EntityLookup from '@/components/tech/EntityLookup'
import ImageLookup from '@/components/tech/ImageLookup'

Vue.config.productionTip = false
Vue.use(BootstrapVue);

Vue.use(ButtonPlugin)
Vue.use(NavbarPlugin)
Vue.use(ModalPlugin)
Vue.use(FormInputPlugin)
Vue.use(InputGroupPlugin)
Vue.use(LayoutPlugin)
Vue.use(FormPlugin)
Vue.use(FormSelectPlugin)
Vue.use(SpinnerPlugin)
Vue.use(TablePlugin)
Vue.use(LinkPlugin)
Vue.use(FormFilePlugin)
Vue.use(AlertPlugin)
Vue.use(ButtonGroupPlugin)
Vue.use(ImagePlugin)

Vue.component("LinkBack", LinkBack)
Vue.component("EntityLookup", EntityLookup)
Vue.component("ImageLookup", ImageLookup)

Object.defineProperty(Vue.prototype, "$bus", {
  get: function () {
    return this.$root.bus;
  }
});

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>',
  data: {
    bus: new Vue({})
  }
})
