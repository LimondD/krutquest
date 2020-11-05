<template>
  <div class="question-index-view border border-top-0 p-3">
    <h2>Вопросы</h2>

    <p>
      <b-link to="./create">Создать новую запись</b-link>
    </p>

    <b-table-simple>
      <b-thead>
        <b-tr>
          <b-th>Группа</b-th>
          <b-th>Текст</b-th>
          <b-th>Ответы</b-th>
          <b-th>Балл</b-th>
          <b-th>Картинка для вопроса</b-th>
          <b-th></b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="item in items" v-bind:key="item.id">
          <b-td><EntityLookup :entity="item.group"></EntityLookup></b-td>
          <b-td>{{ item.text }}</b-td>
          <b-td>{{ item.answers }}</b-td>
          <b-td>{{ item.score }}</b-td>
          <b-td><ImageLookup :image="item.picture"></ImageLookup></b-td>
          <b-td>
            <b-link :to="{ path: '/tech/question/edit/' + item.id }">Изменить</b-link> |
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
    name: 'QuestionIndex',
    data: function () {
      return {
        items: []
      };
    },
    methods: {
      refresh: function () {
        var vm = this;
        this.$bus.$emit('ui-block-show')

        api.question.getAll()
          .then((result) => {
            this.$bus.$emit('ui-block-hide')
            vm.items = result;
            //vm.forceUpdate()
          }, error => {
            this.$bus.$emit('ui-block-hide')

            console.error(error)
          })        
      },
      onDelete: function (questionId) {
        var vm = this
        this.$bus.$emit('ui-block-show')

        api.question.delete(questionId)
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
