<template>
  <div>
    <b-img alt="Изображение" :src="imgUrl" fluid />
  </div>
</template>

<script>
  export default {
    name: 'ImageBox',
    props: {
      image: Object
    },
    computed: {
      imgUrl: function () {
        return (this.image && this.image.virtualFilePath)
          ? window.location.protocol + "//" + window.location.host + "/" + this.image.virtualFilePath
          : ""
      }
    }
  }
</script>

<style scoped>
  
</style>
