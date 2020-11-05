<template>
  <div class="map-view p-3 bg-pl-4">
    <div class="row my-1">
      <div class="col text-center">
        <h3>Карта</h3>
      </div>
    </div>
    <div class="row mt-2">
      <div class="col text-center">
        <ImageBox :image="currentGroup.mapPicture"></ImageBox>
      </div>
    </div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'

  import ImageBox from '@/components/main/ImageBox'

  export default {
    name: 'Map',
    components: {
      'ImageBox': ImageBox
    },
    computed: {
      ...mapGetters(['currentGroup'])
    }
  }
</script>

<style scoped>
 
</style>
