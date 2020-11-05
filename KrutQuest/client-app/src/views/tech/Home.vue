<template>
  <div class="home-view p-3 border">
    <h3>Главная</h3>

    <b-form @submit="onSubmit">
      <b-form-group id="infoGroup" label="Контактная информация:" label-for="info">
        <b-form-input id="info" v-model="form.info" required>
        </b-form-input>
      </b-form-group>

      <b-button type="submit" variant="primary">Сохранить</b-button>
    </b-form>

  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'Home',
    data: function () {
      return {
        form: {
          info: ""
        }
      }
    },
    methods: {
      refresh: function () {
        var vm = this

        return new Promise((resolve, reject) => {
          api.common.getContactInfo()
            .then(techUser => {
              vm.form.info = techUser.contactInfo
              resolve()
            })
            .catch(error => { reject(error) })
        })
      },
      onSubmit: function (evt) {
        evt.preventDefault()

        var vm = this
        vm.$bus.$emit('ui-block-show')

        api.common.setContactInfo(vm.form.info)
          .then(() => { return vm.refresh })
          .catch(error => console.log)
          .finally(() => { vm.$bus.$emit('ui-block-hide') })
      }
    },
    mounted: function () {
      var vm = this
      vm.$bus.$emit('ui-block-show')
      vm.refresh()
        .catch(error => console.log)
        .finally(() => { vm.$bus.$emit('ui-block-hide') })
    }
  }
</script>

<style scoped>
 
</style>
