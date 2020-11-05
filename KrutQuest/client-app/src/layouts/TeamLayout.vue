<template>
  <div class="team-layout">
    <b-container class="team-layout-content bg-pl-4">
      <div class="row p-2 bg-pl-2">
        <div class="col cs-title">#КрутЕцкийКвест</div>
      </div>
      <div class="team-layout-header row">
        <b-col>
          <template v-if="teamStatus == 'started'">
            <div class="d-flex flex-row">
              <router-link to="/main/rules" class="px-2 pb-1 flex-fill cs-nav-item" exact exact-active-class="cs-nav-item-active" tag="div">Правила</router-link>
              <router-link to="/main/questions" class="px-2 pb-1 flex-fill cs-nav-item" exact exact-active-class="cs-nav-item-active" tag="div">Задания</router-link>
              <router-link to="/main/map" class="px-2 pb-1 flex-fill cs-nav-item" exact exact-active-class="cs-nav-item-active" tag="div">Карта</router-link>              
            </div>
          </template>
          <template v-else>
            &nbsp;
          </template>
        </b-col>          
      </div>

      <div class="row">
        <div class="col">
          <div v-if="teamStatus == 'started'" class="d-flex flex-row justify-content-between p-2">
            <span>Набрано баллов: {{ score }}</span>
            <span>Времени осталось: {{ timeRemaining }} мин</span>
          </div>
          <div v-else>&nbsp;</div>
        </div>
      </div>

      <router-view />

      <div class="team-layout-footer row bg-pl-2">
        <div class="col text-center align-middle">
          <span>{{ contactInfo }}</span>
        </div>
      </div>
    </b-container>
  </div>
</template>

<script>
  import api from '@/api' 
  import { mapGetters } from 'vuex';

  export default {
    name: 'TeamLayout',
    data: function () {
      return {
        score: 0,
        timeRemaining: 0,
        code: "initial"
      }
    },
    computed: {
      ...mapGetters(['teamStatus', 'contactInfo'])
    },
    mounted: function () {
      var vm = this

      vm.$bus.$emit('ui-block-show')

      var handler = function () {
        api.main.getTeamStatus()
          .then(function (result) {
            vm.score = result ? result.score : 0
            vm.timeRemaining = result ? result.timer : 0
            vm.code = result ? result.code : "initial"

            if (vm.timeRemaining > 0 && vm.timeRemaining < 30 && !vm.$store.getters.isTimerAlertShown) {

              vm.$bvToast.toast("У вас осталось менее 30 минут!", {
                title: 'Внимание',
                variant: 'warning',
                autoHideDelay: 5000,
                toaster: "b-toaster-bottom-center"
              })

              vm.$store.dispatch('SET_TIMER_ALERT_SHOWN', true)
            }

            vm.$store.dispatch('SET_QUEST_STATE', vm.code)
          }, console.log)
      }

      handler()
      this._updateStatusTimer = setInterval(handler, 5000)

      api.common.getContactInfo()
        .then(contactInfo => {
          vm.$store.dispatch('SET_CONTACT_INFO', contactInfo)
          return api.main.getViewModel()
        })
        .then(viewModel => {
          vm.$store.dispatch('REFRESH', viewModel)
        })
        .catch(error => console.log(error))
        .finally(() => {
          vm.$bus.$emit('ui-block-hide')
        })

    },
    beforeDestroy: function () {
      if (this._updateStatusTimer) {
        clearTimeout(this._updateStatusTimer)
      }
    }
  }
</script>

<style scoped>
  @font-face {
    font-family: RodchenkoCTT;
    src: url(/../static/rodchenkoctt.ttf);
  }

  .team-layout-footer {

  }

  .cs-title {
    font-family: RodchenkoCTT, Arial;
    color: #fff;
    font-size: 2rem;
  }

  .cs-nav-item {
    border-bottom: 3px solid #202020;
    cursor: pointer;
    text-transform: uppercase;
    font-weight: bold;
  }

    .cs-nav-item:hover {
      border-color: #E7302A;
    }

  .cs-nav-item-active {
    border-bottom: 3px solid #E7302A;
    cursor: pointer;
  }
</style>
