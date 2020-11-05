<template>
  <div class="ui-blocker" v-show="visible">
    <div class="ui-blocker-spinner text-center">
      <b-spinner variant="primary"></b-spinner>
    </div>
  </div>
</template>

<script>  
  export default {
    name: 'UIBlocker',
    data: function () {
      return {
        visible: false
      }
    },
    mounted: function () {
      var vm = this;

      this._onShow = function () {
        vm.visible = true
      }

      this._onHide = function () {
        vm.visible = false
      }

      this.$bus.$on('ui-block-show', this._onShow)
      this.$bus.$on('ui-block-hide', this._onHide)
    },
    beforeDestroy: function () {
      if (this._onShow) {
        this.$bus.$off('ui-block-show', this._onShow)
      }

      if (this._onHide) {
        this.$bus.$off('ui-block-hide', this._onHide)
      }
    }
  }
</script>

<style scoped>
  .ui-blocker {
    position: fixed;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    background-color: #C0C0C0;
    opacity: 0.5;
    z-index: 1000;
  }

  .ui-blocker-spinner {
    position: fixed;
    top: 45%;
    left: 0;
    right: 0;
    margin: auto;
    z-index: 1100;
  }
</style>
