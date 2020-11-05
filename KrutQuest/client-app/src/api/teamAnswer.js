import service from './service'

const _getAll = function () {
  return service.get('api/teamAnswer/getAll');
}

const _getById = function (teamAnswerId) {
  return service.get('api/teamAnswer/getById?teamId=' + teamAnswerId)
}

const _save = function (data) {
  return service.post('api/teamAnswer/save', data)
}

export default {
  getAll: _getAll,
  getById: _getById,
  save: _save
}
