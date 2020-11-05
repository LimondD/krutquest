import service from './service'

const _getAll = function () {
  return service.get('api/team/getAll');
}

const _getById = function (teamId) {
  return service.get('api/team/getById?teamId=' + teamId)
}

const _create = function (team) {
  return service.post('api/team/create', team)
}

const _edit = function (team) {
  return service.post('api/team/edit', team)
}

const _delete = function (teamId) {
  return service.get('api/team/delete?teamId=' + teamId)
}

export default {
  getAll: _getAll,
  getById: _getById,
  create: _create,
  edit: _edit,
  delete: _delete
}
