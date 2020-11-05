import service from './service'

const _getAll = function () {
  return service.get('api/questionGroup/getAll');
}

const _getById = function (questionGroupId) {
  return service.get('api/questionGroup/getById?questionGroupId=' + questionGroupId)
}

const _create = function (questionGroup) {
  return service.post('api/questionGroup/create', questionGroup)
}

const _edit = function (questionGroup) {
  return service.post('api/questionGroup/edit', questionGroup)
}

const _delete = function (questionGroupId) {
  return service.get('api/questionGroup/delete?questionGroupId=' + questionGroupId)
}

export default {
  getAll: _getAll,
  getById: _getById,
  create: _create,
  edit: _edit,
  delete: _delete
}
