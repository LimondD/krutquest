<template>
  <div class="quest-index-view border border-top-0 p-3">
    <h2>Квесты</h2>

    <p>
      <b-link to="./create">Создать новую запись</b-link>
    </p>

    <b-table-simple>
      <b-thead>
        <b-tr>
          <b-th>Название</b-th>
          <b-th>Длительность</b-th>
          <b-th>Текст правил</b-th>
          <b-th>Текст финиша</b-th>
          <b-th>Картинка для финиша</b-th>
          <b-th></b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="item in items" v-bind:key="item.id">
          <b-td>{{ item.name }}</b-td>
          <b-td>{{ item.duration }}</b-td>
          <b-td>{{ item.rulesText }}</b-td>
          <b-td>{{ item.finishText }}</b-td>
          <b-td><ImageLookup :image="item.finishPicture"></ImageLookup></b-td>
          <b-td>
            <b-link :to="{ path: '/tech/quest/edit/' + item.id }">Изменить</b-link> |
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
    name: 'QuestIndex',
    data: function () {
      return {
        items: []
      };
    },
    methods: {
      refresh: function () {
        var vm = this;
        this.$bus.$emit('ui-block-show')

        api.quest.getAll()
          .then((result) => {
            this.$bus.$emit('ui-block-hide')

            vm.items = result;
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.error(error)
          })        
      },
      onDelete: function (questId) {
        var vm = this
        this.$bus.$emit('ui-block-show')

        api.quest.delete(questId)
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
