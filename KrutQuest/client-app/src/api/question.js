import service from './service'

const _getAll = function () {
  return service.get('api/question/getAll');
}

const _getById = function (questionId) {
  return service.get('api/question/getById?questionId=' + questionId)
}

const _create = function (question) {
  return service.post('api/question/create', question)
}

const _edit = function (question) {
  return service.post('api/question/edit', question)
}

const _delete = function (questionId) {
  return service.get('api/question/delete?questionId=' + questionId)
}

export default {
  getAll: _getAll,
  getById: _getById,
  create: _create,
  edit: _edit,
  delete: _delete
}
