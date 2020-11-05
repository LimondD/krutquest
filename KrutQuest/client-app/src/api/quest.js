import service from './service'

const _getAll = function () {
  return service.get('api/quest/getAll');
}

const _getById = function (questId) {
  return service.get('api/quest/getById?questId=' + questId)
}

const _create = function (quest) {
  return service.post('api/quest/create', quest)
}

const _edit = function (quest) {
  return service.post('api/quest/edit', quest)
}

const _delete = function (questId) {
  return service.get('api/quest/delete?questId=' + questId)
}

export default {
  getAll: _getAll,
  getById: _getById,
  create: _create,
  edit: _edit,
  delete: _delete
}
