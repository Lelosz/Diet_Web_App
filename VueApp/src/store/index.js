import Vuex from 'vuex';


const store = new Vuex.Store({
    state: {
        loginStatus: false,
        userId: null
    },
    mutations: {
        loginStatus(state, val) {
            state.loginStatus = val
        },
        userId(state, val) {
            state.userId = val
        }
    }
});

export default store