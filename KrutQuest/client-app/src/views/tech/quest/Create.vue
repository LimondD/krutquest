<template>
  <div class="quest-create-view border border-top-0 p-3">
    <h2>Создать</h2>
    <br />

    <b-form @submit="onSubmit">
      <b-form-group id="nameGroup" label="Название:" label-for="name">
        <b-form-input id="name" v-model="form.name" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="durationGroup" label="Длительность:" label-for="duration">
        <b-form-input id="duration" v-model="form.duration" type="number" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="rulesTextGroup" label="Текст правил:" label-for="rulesText">
        <b-form-textarea id="rulesText" v-model="form.rulesText" rows="5" no-resize required>
        </b-form-textarea>
      </b-form-group>

      <b-form-group id="finishTextGroup" label="Текст финиша:" label-for="finishText">
        <b-form-textarea id="finishText" v-model="form.finishText" rows="5" no-resize required>
        </b-form-textarea>
      </b-form-group>

      <b-form-group id="finishPictureIdGroup" label="Картинка для финиша:" label-for="finishPictureId">
        <b-form-select id="finishPictureId" v-model="form.finishPictureId" :options="picturesList">
        </b-form-select>
      </b-form-group>

      <b-button type="submit" variant="primary">Создать</b-button>
    </b-form>

    <LinkBack></LinkBack>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'QuestCreate',
    data: function () {
      return {
        form: {
          name: "",
          duration: 0,
          rulesText: "",
          finishText: "",
          finishPicture: null
        },
        picturesList: []
      };
    },
    methods: {
      onSubmit: function (evt) {
        evt.preventDefault()

        var vm = this
        this.$bus.$emit('ui-block-show')

        var quest = {
          name: this.form.name,
          duration: this.form.duration,
          rulesText: this.form.rulesText,
          finishText: this.form.finishText,
          finishPictureId: this.form.finishPictureId
        }

        api.quest.create(quest)
          .then(() => {
            this.$bus.$emit('ui-block-hide')

            vm.$router.push('/tech/quest/index')
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.log(error)
          })
      }
    },
    mounted: function () {
      var vm = this
      this.$bus.$emit('ui-block-show')

      api.serverImage.getAll()
        .then(result => {
          vm.picturesList = result.map(function (item) {
            return {
              value: item.id,
              text: item.name
            }
          })
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
