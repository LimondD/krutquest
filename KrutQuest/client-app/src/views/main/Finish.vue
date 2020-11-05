<template>
  <div class="finish-view p-3 bg-pl-4">
    <div class="row my-1">
      <div class="col text-center">
        <h3>Финиш</h3>
      </div>
    </div>

    <div class="row mt-2">
      <div class="col text-center">
        <ImageBox :image="quest.finishPicture"></ImageBox>
      </div>
    </div>

    <b-form-textarea class="border-0 bg-pl-4" no-resize readonly rows="12" v-model="finishText"></b-form-textarea>
    <br />

    <div class="row mt-2">
      <div class="col text-center">
        <b-button variant="outline-dark" class="w-25" @click="logOut">Выйти</b-button>
      </div>
    </div>
    <br />

  </div>
</template>

<script>
  import { mapGetters } from 'vuex'

  import ImageBox from '@/components/main/ImageBox'

  export default {
    name: 'Finish',
    components: {
      'ImageBox': ImageBox
    },
    computed: {
      ...mapGetters(['quest', 'team']),
      finishText: function () {
        return this.quest.finishText + "\n\nВаш результат:" + this.team.totalScore
      }
    },
    methods: {
      logOut: function () {
        var vm = this

        vm.$store.dispatch('LOGOUT')
        vm.$router.push('/login')
      }
    }
  }
</script>

<style scoped>
 
</style>
