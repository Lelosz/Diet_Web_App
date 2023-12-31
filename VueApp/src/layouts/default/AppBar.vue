<script setup>
    import { ref, computed } from 'vue'
    import { useTheme } from 'vuetify';
    import store from '@/store/index.js'

    const darkTheme = ref(false);
    const theme = useTheme();

    function changeTheme() {
        darkTheme.value = !darkTheme.value;
        theme.global.name.value = darkTheme.value ? 'light' : 'dark';
    }

    

    const logout = async () => {
        store.commit('loginStatus', false)
        await fetch('https://localhost:7011/api/user/logout', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
        
    }

</script>

<template>
    <v-app-bar flat color="teal-darken-4">
        <template v-slot:prepend>
            <router-link style="text-decoration: none; color: inherit;" to="/">
                <v-btn icon>
                    <v-icon>mdi-home-circle</v-icon>
                </v-btn>
            </router-link>
        </template>
        <v-app-bar-title>
            KalkApp
        </v-app-bar-title>
        <router-link style="text-decoration: none; color: inherit;" to="/CalculatorList">
            <v-btn icon>
                <v-icon>mdi-abacus</v-icon>
            </v-btn>
        </router-link>
        <router-link style="text-decoration: none; color: inherit;" to="/Report">
            <v-btn icon>
                <v-icon>mdi-post</v-icon>
            </v-btn>
        </router-link>
        <div v-if="!store.state.loginStatus">
            <router-link style="text-decoration: none; color: inherit;" to="/LogIn">
                <v-btn class="font-weight-bold">
                    Zaloguj się
                </v-btn>
            </router-link>
            <router-link style="text-decoration: none; color: inherit;" to="/SignUp">
                <v-btn class="font-weight-bold">
                    Zarejestruj się
                </v-btn>
            </router-link>
        </div>
        <div v-if="store.state.loginStatus">
            <router-link style="text-decoration: none; color: inherit;" to="/LogIn">
                <v-btn class="font-weight-bold" @click="logout">
                    Wyloguj się
                </v-btn>
            </router-link>
        </div>
            <v-btn icon @click="changeTheme">
                <v-icon :icon="darkTheme ?
                'mdi-weather-sunny' :
                'mdi-weather-night'"></v-icon>
            </v-btn>
    </v-app-bar>
</template>


<style>
  
  .router-link-active{
    color: white;
  }

</style>
