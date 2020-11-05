<template>
  <div>
    <template v-if="!!entity">
      <span>{{ entityName }}</span>
    </template>    
  </div>
</template>

<script>
  export default {
    name: 'EntityLookup',
    props: {
      entity: Object
    },
    computed: {
      entityName: function () {
        return (this.entity && this.entity.name) ? this.entity.name : "(без имени)"
      }
    }
  }
</script>

<style scoped>
  
</style>
