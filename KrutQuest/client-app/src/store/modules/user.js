const state = {
  accessToken: null,
  userType: null
}

const actions = {
  LOGIN({ commit }, { accessToken, userType }) {
    commit('LOGIN', { accessToken, userType })
  },
  LOGOUT({ commit }) {
    commit('LOGOUT')
    commit('RESET')
  }  
}

const mutations = {
  LOGIN(state, { accessToken, userType }) {
    state.accessToken = accessToken
    state.userType = userType

    window.localStorage.setItem('accessToken', accessToken)
    window.localStorage.setItem('userType', userType)
  },
  LOGOUT(state) {
    window.localStorage.removeItem('accessToken')
    window.localStorage.removeItem('userType')

    state.accessToken = null
    state.userType = null
  }
}

const getters = {
  accessToken(state) {
    return window.localStorage.getItem('accessToken') || state.accessToken
  },

  userType(state) {
    return window.localStorage.getItem('userType') || state.userType
  }
}

export default {
  state: state,
  mutations: mutations,
  actions: actions,
  getters: getters
}
