import router from '@/router'

const getDefaultState = function () {
  return {
    team: {},
    quest: {},
    currentGroup: {},
    isQuestDone: false,
    techInfo: {},
    teamStatus: "initial",
    isTimerAlertShown: false
  }
}

const state = getDefaultState()

const actions = {
  REFRESH({ commit }, payload) {
    commit('REFRESH', payload)
  },

  SET_QUEST_STATE({ commit }, teamStatus) {
    commit('SET_QUEST_STATE', teamStatus)
  },

  SET_CONTACT_INFO({ commit }, techInfo) {
    commit('SET_CONTACT_INFO', techInfo)
  },

  SET_TIMER_ALERT_SHOWN({ commit }, isTimerAlertShown) {
    commit('SET_TIMER_ALERT_SHOWN', isTimerAlertShown)
  },

  RESET({ commit }) {
    commit('RESET')
  }
}

const mutations = {
  REFRESH({ commit }, payload) {
    state.team = payload.team
    state.quest = payload.quest
    state.currentGroup = payload.currentGroup || {}

    if (payload.statusCode) {
      state.teamStatus = payload.statusCode
    }

    var primaryDone = state.quest && state.quest.isQuestDone
    var secondaryDone = !state.currentGroup

    state.isQuestDone = primaryDone || secondaryDone
  },
  SET_QUEST_STATE({ commit }, teamStatus) {
    state.teamStatus = teamStatus

    if (teamStatus == "finished" && router.currentRoute.path != '/main/finish') {
      router.push("/main/finish")
    }
  },
  SET_CONTACT_INFO({ commit }, techInfo) {
    state.techInfo = techInfo
  },
  SET_TIMER_ALERT_SHOWN({ commit }, isTimerAlertShown) {
    state.isTimerAlertShown = isTimerAlertShown
  },
  RESET(state) {
    state = getDefaultState();
  }
}

const getters = {
  team(state) {
    return state.team;
  },

  quest(state) {
    return state.quest
  },

  currentGroup(state) {
    return state.currentGroup
  },

  teamStatus(state) {
    return state.teamStatus
  },

  isQuestDone(state) {
    return state.isQuestDone || state.teamStatus == "finished"
  },

  contactInfo(state) {
    return state.techInfo.contactInfo || ""
  },

  isTimerAlertShown(state) {
    return state.isTimerAlertShown
  }
}

export default {
  state: state,
  mutations: mutations,
  actions: actions,
  getters: getters
}
