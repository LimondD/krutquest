<template>
  <div id="app">
    <UIBlocker></UIBlocker>
    <router-view />
  </div>
</template>

<script>
  import api from '@/api'

  import { mapGetters } from 'vuex';

  import DefaultLayout from '@/layouts/DefaultLayout.vue'
  import TeamLayout from '@/layouts/TeamLayout.vue'  
  import TechLayout from '@/layouts/TechLayout.vue'

  import UIBlocker from '@/components/UIBlocker.vue'

  export default {
    name: 'App',
    data: function () {
      return {
        
      }
    },
    components: {
      'DefaultLayout': DefaultLayout,
      'TeamLayout': TeamLayout,
      'TechLayout': TechLayout,
      'UIBlocker': UIBlocker
    },
    computed: {
      ...mapGetters(['userType'])
    },
    mounted: function () {
      //api.common.whoAmI()
    }
  }
</script>

<style>
  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
  }

  .cs-btn-1 {
    border-color: #E7302A !important;
    color: #F8F8F8 !important;
    background-color: #E7302A !important;
  }

  .cs-btn-outline-1 {
    border-color: #E7302A !important;
    color: #E7302A !important;
    background-color: #F8F8F8 !important;
  }

  .bg-pl-1 {
    border-color: #000 !important;
    background-color: #E7302A !important;
    color: #000 !important;
  }

  .bg-pl-2 {
    border-color: #fff !important;
    background-color: #202020 !important;
    color: #fff !important;
  }

  .bg-pl-3 {
    border-color: #000 !important;
    background-color: #808080 !important;
    color: #000 !important;
  }

  .bg-pl-4 {
    border-color: #000 !important;
    background-color: #F0F0F0 !important;
    color: #000 !important;
  }

  .bg-pl-5 {
    border-color: #000 !important;
    background-color: #F8F8F8 !important;
    color: #000 !important;
  }
</style>
