<template>
  <div class="tech-layout">
    <b-container>
      <div class="tech-layout-header">
        <b-nav tabs>
          <b-nav-item to="/tech/home" exact exact-active-class="active">Главная</b-nav-item>
          <b-nav-item to="/tech/quest/index" exact exact-active-class="active">Квесты</b-nav-item>
          <b-nav-item to="/tech/team/index" exact exact-active-class="active">Команды</b-nav-item>
          <b-nav-item to="/tech/questionGroup/index" exact exact-active-class="active">Группы вопросов</b-nav-item>
          <b-nav-item to="/tech/question/index" exact exact-active-class="active">Вопросы</b-nav-item>
          <b-nav-item to="/tech/teamAnswer/index" exact exact-active-class="active">Ответы команд</b-nav-item>
          <b-nav-item to="/tech/serverImage/index" exact exact-active-class="active">Изображения</b-nav-item>
        </b-nav>
      </div>

      <router-view />
    </b-container>
  </div>
</template>

<script>  
  export default {
    name: 'TechLayout'
  }
</script>

<style scoped>

</style>
