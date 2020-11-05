<template>
  <div class="question-create-view border border-top-0 p-3">
    <h2>Создать</h2>
    <br />

    <b-form @submit="onSubmit">
      <b-form-group id="textGroup" label="Текст:" label-for="text">
        <b-form-input id="text" v-model="form.text" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="groupIdGroup" label="Группа:" label-for="groupId">
        <b-form-select id="groupId" v-model="form.groupId" :options="groupsList" required>
        </b-form-select>
      </b-form-group>

      <b-form-group id="answersGroup" label="Ответы:" label-for="answers">
        <b-form-input id="answers" v-model="form.answers" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="scoreGroup" label="Балл:" label-for="score">
        <b-form-input id="score" v-model="form.score" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="hintGroup" label="Подсказка:" label-for="hint">
        <b-form-input id="hint" v-model="form.hint" required>
        </b-form-input>
      </b-form-group>

      <b-form-group id="pictureIdGroup" label="Картинка для вопроса:" label-for="pictureId">
        <b-form-select id="pictureId" v-model="form.pictureId" :options="picturesList">
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
    name: 'QuestionCreate',
    data: function () {
      return {
        form: {
          text: "",          
          groupId: null,
          answers: "",
          score: 10,
          hint: "",
          pictureId: null
        },
        groupsList: [],
        picturesList: []
      };
    },
    methods: {
      onSubmit: function (evt) {
        evt.preventDefault()

        var vm = this
        this.$bus.$emit('ui-block-show')

        var question = {
          text: this.form.text,
          groupId: this.form.groupId,
          answers: this.form.answers,
          score: this.form.score,
          hint: this.form.hint,
          pictureId: this.form.pictureId
        }

        api.question.create(question)
          .then(() => {
            this.$bus.$emit('ui-block-hide')

            vm.$router.push('/tech/question/index')
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.log(error)
          })
      },
      loadLookups: function () {
        var vm = this

        var questionGroupPromise =
          api.questionGroup.getAll()
            .then(result => {
              vm.groupsList = result.map(function (item) {
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

        return Promise.all([questionGroupPromise, picturesPromise])
      }
    },
    mounted: function () {
      var vm = this
      this.$bus.$emit('ui-block-show')

      this.loadLookups()
        .then(() => { })
        .catch(error => console.log(error))
        .finally(() => { this.$bus.$emit('ui-block-hide') })      
    }
  }
</script>

<style scoped>

</style>
