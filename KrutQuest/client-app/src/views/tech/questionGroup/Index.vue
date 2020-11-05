<template>
  <div class="question-group-index-view border border-top-0 p-3">
    <h2>Группы вопросов</h2>

    <p>
      <b-link to="./create">Создать новую запись</b-link>
    </p>

    <b-table-simple>
      <b-thead>
        <b-tr>
          <b-th>Название</b-th>
          <b-th>Индекс</b-th>
          <b-th>Квест</b-th>
          <b-th>Карта для группы</b-th>
          <b-th></b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="item in items" v-bind:key="item.id">
          <b-td>{{ item.name }}</b-td>
          <b-td>{{ item.index }}</b-td>
          <b-td><EntityLookup :entity="item.quest"></EntityLookup></b-td>
          <b-td><ImageLookup :image="item.mapPicture"></ImageLookup></b-td>
          <b-td>
            <b-link :to="{ path: '/tech/questionGroup/edit/' + item.id }">Изменить</b-link> |
            <b-link @click="onDelete(item.id)">Удалить</b-link>
          </b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'QuestGroupIndex',
    data: function () {
      return {
        items: []
      };
    },
    methods: {
      refresh: function () {
        var vm = this;
        this.$bus.$emit('ui-block-show')

        api.questionGroup.getAll()
          .then((result) => {
            this.$bus.$emit('ui-block-hide')

            vm.items = result;
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.error(error)
          })        
      },
      onDelete: function (questionGroupId) {
        var vm = this
        this.$bus.$emit('ui-block-show')

        api.questionGroup.delete(questionGroupId)
          .then(() => {
            vm.refresh()
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.log(error)
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
