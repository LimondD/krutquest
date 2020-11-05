<template>
  <div class="team-answer-detail-view border border-top-0 p-3">
    <h2>{{ team.name }} ({{ team.questName }})</h2>
    <br />

    <b-link @click="onSave">Сохранить</b-link>
    <br />

    <b-table-simple>
      <b-thead>
        <b-tr>
          <b-th>Блок</b-th>
          <b-th>Вопрос</b-th>
          <b-th>Ответ</b-th>
          <b-th>Подсказка</b-th>
          <b-th>Балл</b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="item in team.details" v-bind:key="item.id">
          <b-td>{{ item.groupName }}</b-td>
          <b-td>{{ item.questionText }}</b-td>
          <b-td>{{ item.answer }}</b-td>
          <b-td>{{ item.isHintUsed ? 'Да' : 'Нет' }}</b-td>
          <b-td><b-form-input v-model="item.score"></b-form-input></b-td>
        </b-tr>
        <b-tr>
          <b-td>Итого</b-td>
          <b-td>--</b-td>
          <b-td>--</b-td>
          <b-td>--</b-td>
          <b-td>{{ totalScore }}</b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>

    <LinkBack></LinkBack>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'TeamAnswerDetails',
    data: function () {
      return {
        team: {}
      };
    },
    methods: {
      onSave: function (evt) {
        evt.preventDefault()

        var vm = this

        vm.$bus.$emit('ui-block-show')
        api.teamAnswer.save(vm.team)
          .then(() => {
            vm.$router.go(-1)
          })
          .catch(error => console.log)
          .finally(() => {
            vm.$bus.$emit('ui-block-hide')
          })
      }      
    },
    computed: {
      totalScore: function () {
        var total = 0;

        if (typeof this.team.details === "object" && this.team.details != null) {
          for (var i = 0; i < this.team.details.length; i++) {
            total += Number(this.team.details[i].score);
          }
        }

        if (isNaN(total)) {
          total = 0;
        }

        return total;
      }
    },
    mounted: function () {
      var vm = this

      var id = this.$route.params.id

      this.$bus.$emit('ui-block-show')
      api.teamAnswer.getById(id)
        .then(result => {
          vm.team = result
        })
        .catch(error => console.log(error))
        .finally(() => { this.$bus.$emit('ui-block-hide') })
    }
  }
</script>

<style scoped>

</style>
