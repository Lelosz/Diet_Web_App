import Vuex from 'vuex';


const store = new Vuex.Store({
    state: {
        loginStatus: false,
        userId: null,
        userName: null,
        userRole: null
    },
    mutations: {
        loginStatus(state, val) {
            state.loginStatus = val
        },
        userId(state, val) {
            state.userId = val
        },
        userName(state, val) {
            state.userName = val
        },
        userRole(state, val) {
            state.userRole = val
        }
    }
});

export default store