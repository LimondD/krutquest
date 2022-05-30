<template>
  <div class="questions-view p-3 bg-pl-4">
    <div class="row my-1">
      <div class="col text-center">
        <h3>{{ currentGroup.name }}</h3>
      </div>
    </div>
    
    <template v-for="item in currentGroup.questions">
      <question :question="item" :key="item.id"></question>
      <br />
    </template>
    <b-button variant="secondary" @click="finishGroup">Пропустить блок</b-button>
    <br />
  </div>
</template>

<script>
  import api from '@/api'
  import Question from '@/components/main/Question.vue'
  import { mapGetters } from 'vuex'

  export default {
    name: 'Questions',
    data: function () {
      return {
      };
    },
    methods: {
      refreshData: function () {
        var vm = this

        vm.$bus.$emit('ui-block-show')

        api.main.getViewModel()
          .then(function (viewModel) {
            vm.$store.dispatch('REFRESH', viewModel)

            if (!viewModel) {
              vm.$router.push('/main/finish')
            }
          })
          .catch(error => console.log)
          .finally(function () {
            vm.$bus.$emit('ui-block-hide')
          })
      },
      alertInterval: function (alertTimeout) {
        this.alertTimeout = alertTimeout
      },
      finishGroup: function () {        
        var vm = this

        vm.$bvModal.msgBoxConfirm('Вы уверены, что хотите пропустить блок?', {
          title: 'Подтверждение',
          size: 'lg',
          buttonSize: 'lg',
          centered: true,
          okTitle: 'Да',
          okVariant: 'danger',
          cancelTitle: 'Нет',
          hideHeaderClose: false
        })
          .then(result => {
            if (result) {
              vm.$bus.$emit('ui-block-show')

              api.main.finishGroup(this.currentGroup.id)
                .then(function () {
                  vm.refreshData()
                }, console.log)
                .finally(function () {
                  vm.$bus.$emit('ui-block-hide')
                })
            }
          })
      }
    },
    components: {
      'question': Question
    },
    computed: {
      ...mapGetters(['currentGroup'])
    },
    mounted: function () {
      var vm = this

      this._questionsRefresh = function () {
        vm.refreshData()
      }

      vm.$bus.$on("questions-refresh", this._questionsRefresh)
      
      this.refreshData()
    },
    beforeDestroy: function () {
      var vm = this

      if (this._questionsRefresh) {
        vm.$bus.$off("questions-refresh", this._questionsRefresh)
      }
    }
  }
</script>

<style scoped>
 
</style>
