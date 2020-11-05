<template>
  <div class="question-group-edit-view border border-top-0 p-3">
    <h2>Изменить</h2>
    <br />

    <b-form @submit="onSubmit">
      <b-form-group id="nameGroup" label="Название:" label-for="name">
        <b-form-input id="name" v-model="form.name" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="questIdGroup" label="Квест:" label-for="questId">
        <b-form-select id="questId" v-model="form.questId" :options="questsList" required>
        </b-form-select>
      </b-form-group>

      <b-form-group id="mapPictureIdGroup" label="Карта для группы:" label-for="mapPictureId">
        <b-form-select id="mapPictureId" v-model="form.mapPictureId" :options="picturesList">
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
          questId: null,
          mapPictureId: null
        },
        questsList: [],
        picturesList: []
      };
    },
    methods: {
      onSubmit: function (evt) {
        evt.preventDefault()

        var vm = this
        this.$bus.$emit('ui-block-show')

        var questionGroup = {
          id: this.$route.params.id,
          name: this.form.name,
          questId: this.form.questId,
          mapPictureId: this.form.mapPictureId
        }

        api.questionGroup.edit(questionGroup)
          .then(() => {
            this.$bus.$emit('ui-block-hide')

            vm.$router.push('/tech/questionGroup/index')
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

        var picturesPromise = api.serverImage.getAll()
          .then(result => {
            vm.picturesList = result.map(function (item) {
              return {
                value: item.id,
                text: item.name
              }
            })
          })

        return Promise.all([questPromise, picturesPromise])
      }
    },
    mounted: function () {
      var vm = this
      var questionGroupId = this.$route.params.id

      this.$bus.$emit('ui-block-show')
      this.loadLookups()
        .then(() => {
          return api.questionGroup.getById(questionGroupId);
        })
        .then(questionGroup => {          
          vm.form.name = questionGroup.name
          vm.form.questId = questionGroup.questId
          vm.form.mapPictureId = questionGroup.mapPictureId
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
