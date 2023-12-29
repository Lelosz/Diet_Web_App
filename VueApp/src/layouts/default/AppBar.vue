<template>
  <v-app-bar flat color="teal-darken-4">
    <template v-slot:prepend>
      <router-link style="text-decoration: none; color: inherit;" to="/" >
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
    <v-btn icon>
      <v-icon>mdi-post</v-icon>
    </v-btn>
    <router-link style="text-decoration: none; color: inherit;" to="/LogIn">
        <v-btn class="font-weight-bold">
            Zaloguj się
        </v-btn>
    </router-link>
    <v-btn icon @click="changeTheme">
        <v-icon :icon="darkTheme ?
                'mdi-weather-sunny' :
                'mdi-weather-night'"></v-icon>
    </v-btn>
  </v-app-bar>
</template>

<script setup>
    import { ref } from 'vue'
    import { useTheme } from 'vuetify';

    const darkTheme = ref(false);
    const theme = useTheme();

    function changeTheme() {
        darkTheme.value = !darkTheme.value;
        theme.global.name.value = darkTheme.value ? 'light' : 'dark';
    }

</script>

<style>
  
  .router-link-active{
    color: white;
  }

</style>
