<template>
  <div>
    <template v-if="!!image">
      <b-link :href="linkRef" target="_blank">{{ linkText }}</b-link>
    </template>    
  </div>
</template>

<script>
  export default {
    name: 'ImageLookup',
    props: {
      image: Object
    },
    computed: {
      linkRef: function () {        
        return (this.image && this.image.virtualFilePath)
          ? window.location.protocol + "//" + window.location.host + "/" + this.image.virtualFilePath
          : ""
      },
      linkText: function () {
        return (this.image && this.image.name) ? this.image.name : "(изображение)"
      }
    }
  }
</script>

<style scoped>
  
</style>
