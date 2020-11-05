<template>
  <div class="team-edit-view border border-top-0 p-3">
    <h2>Изменить</h2>
    <br />

    <b-form @submit="onSubmit">
      <b-form-group id="nameGroup" label="Название:" label-for="name">
        <b-form-input id="name" v-model="form.name" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="loginGroup" label="Логин:" label-for="login">
        <b-form-input id="login" v-model="form.login" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="passwordGroup" label="Пароль:" label-for="password">
        <b-form-input id="password" v-model="form.password" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="questIdGroup" label="Квест:" label-for="questId">
        <b-form-select id="questId" v-model="form.questId" :options="questsList" required>
        </b-form-select>
      </b-form-group>

      <b-form-group id="questionGroupsOrderGroup" label="Порядок групп вопросов:" label-for="questionGroupsOrder">
        <b-form-input id="questionGroupsOrder" v-model="form.questionGroupsOrder" required>
        </b-form-input>
      </b-form-group>

      <b-button type="submit" variant="primary">Сохранить</b-button>
    </b-form>

    <LinkBack></LinkBack>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'TeamEdit',
    data: function () {
      return {
        form: {
          name: "",
          login: "",
          password: "",
          questId: null,
          questionGroupsOrder: ""
        },
        questsList: []
      };
    },
    methods: {
      onSubmit: function (evt) {
        evt.preventDefault()

        var vm = this
        this.$bus.$emit('ui-block-show')

        var team = {
          id: this.$route.params.id,
          login: this.form.login,
          password: this.form.password,
          questId: this.form.questId,
          name: this.form.name,
          questionGroupsOrder: this.form.questionGroupsOrder
        }

        api.team.edit(team)
          .then(() => {
            this.$bus.$emit('ui-block-hide')

            vm.$router.push('/tech/team/index')
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.log(error)
          })
      },
      loadLookups: function () {
        var vm = this

        var questPromise =
          api.quest.getAll()
            .then(result => {
              vm.questsList = result.map(function (item) {
                return {
                  value: item.id,
                  text: item.name
                }
              })
            })

        return Promise.all([questPromise])
      }
    },
    mounted: function () {
      var vm = this
      this.$bus.$emit('ui-block-show')

      var teamId = this.$route.params.id

      this.loadLookups().then()
        .then(() => {
          return api.team.getById(teamId)
        })      
        .then((team) => {          
          vm.form.login = team.login
          vm.form.password = team.password
          vm.form.questId = team.questId
          vm.form.name = team.name
          vm.form.questionGroupsOrder = team.questionGroupsOrder
        })
        .catch(error => console.log(error))
        .finally(() => {
          this.$bus.$emit('ui-block-hide')
        })
    }
  }
</script>

<style scoped>

</style>
