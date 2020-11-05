<template>
  <div>
    <b-link @click="goBack">Назад</b-link>
  </div>
</template>

<script>
  export default {
    name: 'LinkBack',
    methods: {
      goBack: function () {
        this.$router.go(-1)
      }
    }
  }
</script>

<style scoped>
  
</style>
