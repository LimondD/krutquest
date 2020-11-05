<template>
  <div class="login-view p-3">

    <div class="row">
      <div class="col-sm text-center">
        <h3>KrutQuest</h3>
      </div>
    </div>
    
    <b-form @submit="onSubmit">
      <b-form-group id="login-group"
                    label="Логин:"
                    label-for="login-input">
        <b-form-input id="login-input"
                      type="text"
                      required
                      placeholder="Введите логин"
                      v-model="form.login"></b-form-input>
      </b-form-group>

      <b-form-group id="password-group"
                    label="Пароль:"
                    label-for="password-input">
        <b-form-input id="password-input"
                      type="password"
                      required
                      placeholder="Введите пароль"
                      v-model="form.password"></b-form-input>
      </b-form-group>

      <div class="row mt-1">
        <div class="col text-center">
          <b-button type="submit" variant="primary" class="w-25">Войти</b-button>
        </div>
      </div>      
    </b-form>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'Admin',
    data: function () {
      return {
        form: {
          login: "",
          password: ""
        }
      }
    },
    methods: {
      onSubmit: function (evt) {
        evt.preventDefault()

        this.$bus.$emit('ui-block-show')

        api.auth.loginAdmin({ login: this.form.login, password: this.form.password })
          .then(result => {
            this.$bus.$emit('ui-block-hide')

            this.$store.dispatch('LOGIN', { accessToken: result.accessToken, userType: result.userType })

            this.$router.push('/tech/home')
          }, error => {
            this.$bus.$emit('ui-block-hide')

            vm.$bvModal.msgBoxOk(error, {
              title: 'Ошибка',
              centered: true,
              size: 'lg',
              buttonSize: 'lg'
            })

            console.error(error)
          })
      }
    }    
  }
</script>

<style scoped>
 
</style>
