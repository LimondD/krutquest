<template>
  <b-card :class="questionStatusClass">
    <template v-if="question.picture">
      <b-card-img :src="imageUrl(question.picture)" alt="Изображение" top>
      </b-card-img>
    </template>

    <b-card-text no-body>
      <br />
      <p v-html="question.text">
      </p>
      <br />

      <template v-if="question.isHintUsed">
        <p>
          <i>Подсказка: {{ question.hint }}</i>
        </p>
        <br />
      </template>

      <span>Баллов за задание: {{ question.score }}</span>
    </b-card-text>

    <b-input-group prepend="Ответ" class="mt-3">
      <b-form-input class="bg-pl-5" v-model="answer" :disabled="question.isDone"></b-form-input>
      <b-input-group-append>
        <b-button variant="outline-dark" :disabled="question.isDone" @click="sendAnswer">ОК</b-button>
      </b-input-group-append>
    </b-input-group>

    <b-button variant="outline-success" class="mt-3" :disabled="question.isHintUsed || question.score <= 1" @click="getQuestionHint">Подсказка</b-button>
  </b-card>
</template>

<script>
  import api from '@/api'
  import { BCard, BCardTitle, BCardText, BCardImg } from 'bootstrap-vue'
  import ImageBox from '@/components/main/ImageBox'

  export default {
    name: 'Question',
    props: {
      question: Object
    },
    data() {
      return {
        answer: ""
      }
    },
    methods: {
      imageUrl: function (image) {
        return (image && image.virtualFilePath)
          ? window.location.protocol + "//" + window.location.host + "/" + image.virtualFilePath
          : ""
      },
      getQuestionHint: function () {
        var vm = this

        vm.$bvModal.msgBoxConfirm('Вы уверены, что хотите взять подсказку?', {
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

              api.main.getQuestionHint(this.question.id)
                .then(function () {
                  vm.$bus.$emit('questions-refresh')
                }, console.log)
                .finally(function () {
                  vm.$bus.$emit('ui-block-hide')
                })
            }
          })
      },
      sendAnswer: function () {
        var vm = this

        vm.$bvModal.msgBoxConfirm('Вы уверены в своем ответе?', {
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

              api.main.sendAnswer(this.question.id, this.answer)
                .then(function () {
                  vm.$bus.$emit('questions-refresh')
                }, console.log)
                .finally(function () {
                  vm.$bus.$emit('ui-block-hide')
                })
            }
          })
      }
    },
    components: {
      'b-card': BCard,
      'b-card-title': BCardTitle,
      'b-card-text': BCardText,
      'b-card-img': BCardImg,
      'ImageBox': ImageBox
    },
    computed: {
      questionStatusClass: function () {
        if (this.question.isDone) {
          return this.question.score > 0
            ? "border-success"
            : "border-danger"
        }
        else if (this.question.score < 10) {
          return "border-danger"
        }

        return "bg-pl-5";
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .card {
    border-width: 2px;
  }
</style>
