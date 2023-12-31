import Vuex from 'vuex';


const store = new Vuex.Store({
    state: {
        loginStatus: false
    },
    mutations: {
        loginStatus(state, val) {
            state.loginStatus = val
        }
    }
});

export default store