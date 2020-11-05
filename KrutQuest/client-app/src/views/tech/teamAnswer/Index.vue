<template>
  <div class="team-answer-index-view border border-top-0 p-3">
    <h2>Ответы команд</h2>

    <b-table :items="items" :fields="fields" :primary-key="id">
      <template v-slot:cell(func)="data">
        <b-link :to="{ path: '/tech/teamAnswer/details/' + data.item.id }">Перейти</b-link>
      </template>
    </b-table>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'TeamAnswerIndex',
    data: function () {
      return {
        items: [],
        fields: [
          {
            key: "name",
            label: "Команда",
            headerTitle: "Команда"
          },
          {
            key: "questName",
            label: "Квест",
            headerTitle: "Квест"
          },
          {
            key: "score",
            label: "Общий балл",
            headerTitle: "Общий балл",
            sortable: true
          },
          {
            key: "time",
            label: "Время (мин)",
            headerTitle: "Время (мин)",
            sortable: true
          },
          {
            key: "func",
            label: "",
            headerTitle: ""
          }
        ]
      };
    },
    methods: {
      refresh: function () {
        var vm = this;
        this.$bus.$emit('ui-block-show')

        api.teamAnswer.getAll()
          .then((result) => {
            this.$bus.$emit('ui-block-hide')

            vm.items = result
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.error(error)
          })        
      }
    },
    mounted: function () {
      this.refresh()
    }
  }
</script>

<style scoped>

</style>
