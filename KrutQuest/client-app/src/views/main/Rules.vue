<template>
  <div class="rules-view pt-3 px-0 bg-pl-4">
    <div class="row my-1">
      <div class="col text-center">
        <h3>Правила</h3>
      </div>
    </div>
    
    <b-form-textarea class="rules-text border-0 bg-pl-4" no-resize readonly rows="12" v-model="quest.rulesText">
    </b-form-textarea>

    <div class="rules-buttons row mt-2">
      <div class="col text-center">
        <b-button v-if="!team.hasBegun" variant="outline-dark" class="w-25" @click="startQuest">Начать</b-button>
      </div>
    </div>
    <br />

  </div>
</template>

<script>
  import api from '@/api'

  import axios from 'axios'
  import { mapGetters } from 'vuex'

  export default {
    name: 'Rules',
    data: function () {
      return {
        rulesText: "",
        hasBegun: true
      }
    },
    methods: {
      startQuest: function () {
        var vm = this

        this.$bus.$emit('ui-block-show')

        api.main.startQuest()
          .then(function () {
            vm.$router.push('/main/questions')
          })
          .catch(error => console.log)
          .finally(function () {
            vm.$bus.$emit('ui-block-hide')
          })
      }
    },
    mounted: function () {
    },
    computed: {
      ...mapGetters(['quest', 'team'])
    }
  }
</script>

<style scoped>
 
</style>
