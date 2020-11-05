import axios from 'axios'
import service from './service'

const _getAll = function () {
  return service.get('api/serverImage/getAll');
}

const _delete = function (serverImageId) {
  return service.get('api/serverImage/delete?serverImageId=' + serverImageId)
}

const _upload = function (file) {
  var formData = new FormData();  
  formData.append("image", file);

  return service.upload('api/serverImage/upload', formData)
}

export default {
  getAll: _getAll,
  delete: _delete,
  upload: _upload
}
