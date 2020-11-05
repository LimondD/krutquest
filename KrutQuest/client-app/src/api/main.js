import service from './service'

const _getViewModel = function () {
  return service.get('api/main/getViewModel')
}

const _sendAnswer = function (questionId, answer) {
  return service.post('api/main/sendAnswer?questionId=' + questionId + '&answer=' + answer)
}

const _getQuestionHint = function (questionId) {
  return service.post('api/main/getQuestionHint?questionId=' + questionId)
}

const _finishGroup = function (groupId) {
  return service.post('api/main/finishGroup?groupId=' + groupId)
}

const _startQuest = function () {
  return service.post('api/main/startQuest')
}

const _getTeamStatus = function () {
  return service.get('api/main/getTeamStatus')
}

const _getQuest = function () {
  return service.get('/api/main/getQuest')
}

export default {
  getViewModel: _getViewModel,
  sendAnswer: _sendAnswer,
  getQuestionHint: _getQuestionHint,
  finishGroup: _finishGroup,
  startQuest: _startQuest,
  getTeamStatus: _getTeamStatus,
  getQuest: _getQuest
}
