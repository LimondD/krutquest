<template>
  <div class="quest-edit-view border border-top-0 p-3">
    <h2>Изменить</h2>
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

      <b-button type="submit" variant="primary">Сохранить</b-button>
    </b-form>

    <LinkBack></LinkBack>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'QuestEdit',
    data: function () {
      return {
        form: {
          name: "",
          duration: 0,
          rulesText: "",
          finishText: ""
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
          id: this.$route.params.id,
          name: this.form.name,
          duration: this.form.duration,
          rulesText: this.form.rulesText,
          finishText: this.form.finishText,
          finishPictureId: this.form.finishPictureId
        }

        api.quest.edit(quest)
          .then(() => {
            this.$bus.$emit('ui-block-hide')

            vm.$router.push('/tech/quest/index')
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.log(error)
          })
      },
      loadLookups: function () {
        var vm = this
        this.$bus.$emit('ui-block-show')

        var picturesPromise = api.serverImage.getAll()
          .then(result => {
            vm.picturesList = result.map(function (item) {
              return {
                value: item.id,
                text: item.name
              }
            })
          })

        return Promise.all([picturesPromise])
      }      
    },
    mounted: function () {
      var vm = this
      this.$bus.$emit('ui-block-show')

      var questId = this.$route.params.id
      
      this.loadLookups()
        .then(() => {
          return api.quest.getById(questId)
        })
        .then(quest => {
          vm.form.name = quest.name
          vm.form.duration = quest.duration
          vm.form.rulesText = quest.rulesText
          vm.form.finishText = quest.finishText
          vm.form.finishPictureId = quest.finishPictureId
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
