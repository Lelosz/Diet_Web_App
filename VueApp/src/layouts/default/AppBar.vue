<script setup>
    import { ref, computed } from 'vue'
    import { useTheme } from 'vuetify';
    import store from '@/store/index.js'

    
    const loginState = computed(() => store.state.loginStatus);
    const darkTheme = ref(false);
    const theme = useTheme();

    function changeTheme() {
        darkTheme.value = !darkTheme.value;
        theme.global.name.value = darkTheme.value ? 'light' : 'dark';
    }

    

    const logout = async () => {
        await fetch('https://localhost:7011/api/user/logout', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
        store.commit('loginStatus', false)
        store.commit('userId', null)
        console.log("wylogowany")
    }

</script>

<template>
    <v-app-bar flat color="teal-darken-4">
        <template v-slot:prepend>
            <router-link style="text-decoration: none; color: inherit;" to="/">
                <v-btn icon class="font-weight-bold text-h6">
                    <v-icon>mdi-home-circle</v-icon>
                </v-btn>
            </router-link>
        </template>
        <v-app-bar-title>
            <p class="font-weight-bold text-h6">
                Kalkulatorownia
            </p>
        </v-app-bar-title>
        <router-link style="text-decoration: none; color: inherit;" to="/CalculatorList">
            <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-abacus">
                Kalkulatory
            </v-btn>
        </router-link>
        <div v-if="loginState">
            <router-link style="text-decoration: none; color: inherit;" to="/Report">
                <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-post">
                    Raport
                </v-btn>
            </router-link>
        </div>
        <router-link style="text-decoration: none; color: inherit;" to="/Blog">
            <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-abacus">
                Blog
            </v-btn>
        </router-link>
        <div v-if="!loginState">
            <router-link style="text-decoration: none; color: inherit;" to="/LogIn">
                <v-btn class="font-weight-bold text-h6">
                    Zaloguj się
                </v-btn>
            </router-link>
            <router-link style="text-decoration: none; color: inherit;" to="/SignUp">
                <v-btn class="font-weight-bold text-h6">
                    Zarejestruj się
                </v-btn>
            </router-link>
        </div>
        <div v-if="loginState">
            <router-link style="text-decoration: none; color: inherit;" to="/userProfile">
                <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-account">
                    Profil
                </v-btn>
            </router-link>
        </div>
        <div v-if="loginState">
            <router-link style="text-decoration: none; color: inherit;" to="/LogIn">
                <v-btn class="font-weight-bold text-h6" @click="logout">
                    Wyloguj się
                </v-btn>
            </router-link>
        </div>
        <v-btn icon @click="changeTheme" class="font-weight-bold text-h6">
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
