import Vuex from 'vuex';
import createPersistedState from "vuex-persistedstate";

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
    },
    actions: {
        loadFromLocalStorage({ commit }) {
            const storedState = localStorage.getItem('app-state');
            if (storedState) {
                commit('setState', JSON.parse(storedState));
            }
        },
        saveToLocalStorage({ state }) {
            localStorage.setItem('app-state', JSON.stringify(state));
        },
    }, plugins: [
        createPersistedState({
            storage: {
                getItem: key => localStorage.getItem(key),
                setItem: (key, value) => localStorage.setItem(key, value),
                removeItem: key => localStorage.removeItem(key),
            },
        }),
    ],
});

export default store