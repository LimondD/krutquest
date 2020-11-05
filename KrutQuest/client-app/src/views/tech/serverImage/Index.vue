<template>
  <div class="server-image-index-view border border-top-0 p-3">
    <h2>Изображения</h2>

    <p>
      <b-link @click="openUploadForm">Загрузить изображение</b-link>
    </p>

    <b-table-simple>
      <b-thead>
        <b-tr>
          <b-th>Имя</b-th>
          <b-th></b-th>
        </b-tr>
      </b-thead>
      <b-tbody>
        <b-tr v-for="item in items" v-bind:key="item.id">
          <b-td>{{ item.name }}</b-td>
          <b-td>
            <b-link :href="imageUrl(item)" target="_blank">Просмотр</b-link> |
            <b-link @click="onDelete(item.id)">Удалить</b-link>
          </b-td>
        </b-tr>
      </b-tbody>
    </b-table-simple>

    <b-modal id="uploadModal" ref="uploadModal" ok-title="Загрузить" cancel-title="Отмена" title="Загрузить изображение" centered @ok="uploadImage">
      <b-form>
        <b-form-group id="imageGroup" label="Изображение:" label-for="imagePicture">
          <b-form-file id="imagePicture" accept="image/jpeg, image/png" placeholder="Не выбрано" browse-text="Обзор" v-model="uploadForm.image">
          </b-form-file>
        </b-form-group>
      </b-form>
    </b-modal>
  </div>
</template>

<script>
  import api from '@/api'

  export default {
    name: 'ServerImageIndex',
    data: function () {
      return {
        items: [],
        uploadForm: {
          image: null
        }
      };
    },
    methods: {
      imageUrl: function (image) {
        return (image && image.virtualFilePath)
          ? window.location.protocol + "//" + window.location.host + "/" + image.virtualFilePath
          : ""
      },
      openUploadForm: function () {
        this.$refs['uploadModal'].show()
      },
      uploadImage: function () {
        var vm = this
        this.$bus.$emit('ui-block-show')

        api.serverImage.upload(this.uploadForm.image)
          .then(() => {
            return vm.refresh()
          })
          .catch(error => console.log(error))
          .finally(() => this.$bus.$emit('ui-block-hide'))
      },
      onDelete: function (teamId) {
        var vm = this
        this.$bus.$emit('ui-block-show')

        api.serverImage.delete(teamId)
          .then(() => {
            return vm.refresh()
          })
          .catch(error => console.log(error))
          .finally(() => this.$bus.$emit('ui-block-hide'))
      },
      refresh: function () {
        var vm = this;

        return new Promise((resolve, reject) => {
          api.serverImage.getAll()
            .then(result => {
              vm.items = result;
              resolve()
            }, reject)
        })
      }
    },
    mounted: function () {
      this.$bus.$emit('ui-block-show')

      this.refresh()
        .catch(error => console.log(error))
        .finally(() => this.$bus.$emit('ui-block-hide'))
    }
  }
</script>

<style scoped>

</style>
