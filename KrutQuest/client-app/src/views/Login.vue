<template>
  <div class="login-view p-3">

    <div class="row">
      <div class="col-sm vw-25 vh-25 text-center">
        <img src="../../static/KrutetsQuest-01.png" class="img-fluid" />
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
                      v-model="form.login"
                      class="bg-pl-5"></b-form-input>
      </b-form-group>

      <b-form-group id="password-group"
                    label="Пароль:"
                    label-for="password-input">
        <b-form-input id="password-input"
                      type="password"
                      required
                      placeholder="Введите пароль"
                      v-model="form.password"
                      class="bg-pl-5"></b-form-input>
      </b-form-group>

      <div class="row mt-1">
        <div class="col text-center">
          <b-button type="submit" variant="outline-dark" class="w-25">Войти</b-button>
        </div>
      </div>      
    </b-form>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'Login',
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

        var vm = this
        vm.$bus.$emit('ui-block-show')

        api.auth.login({ login: this.form.login, password: this.form.password })
          .then(result => {            
            vm.$store.dispatch('LOGIN', { accessToken: result.accessToken, userType: result.userType })
            setTimeout(() => { vm.$router.push('/main/rules') }, 1000)
          })
          .catch(error => {            
            vm.$bvModal.msgBoxOk(error, {
              title: 'Ошибка',
              centered: true,
              size: 'lg',
              buttonSize: 'lg'
            })
            
            console.error(error)
          })
          .finally(() => {
            vm.$bus.$emit('ui-block-hide')            
          })
      }
    }    
  }
</script>

<style scoped>
 
</style>
