<script setup>
    import { onMounted, ref } from 'vue'

    const hasSaved = ref(false)
    const isEditing = ref(null)

    const username = ref('')
    const oldPassword = ref('')
    const newPassword = ref('')
    const email = ref('')

    const authTokenValue = ref(0)

    function save() {
        isEditing.value = !isEditing.value
        hasSaved.value = true
    }

    onMounted(async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }

        if (authTokenValue.value) {
            console.log(authTokenValue.value)
            const token = 'Bearer ' + authTokenValue.value
            let response = await fetch('https://localhost:7011/api/user/user', {
                method: 'GET',
                mode: 'cors',
                headers: { 'Content-Type': 'application/json', 'Authorization': token },
                credentials: 'include'
            });
            let userData = await response.json()
            console.log(userData)
            if (userData) {
                username.value = userData.username
                email.value = userData.emailAdress
            }

        }

    });

</script>

<template>
    <v-card class="mx-auto my-16"
            color="teal-lighten-1"
            max-width="500">
        <v-toolbar flat color="teal-darken-1">
            <v-btn icon="mdi-account"></v-btn>

            <v-toolbar-title class="font-weight-light">
                Profil użytkownika
            </v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn icon
                   @click="isEditing = !isEditing">
                <v-fade-transition leave-absolute>
                    <v-icon v-if="isEditing">mdi-close</v-icon>

                    <v-icon v-else>mdi-pencil</v-icon>
                </v-fade-transition>
            </v-btn>
        </v-toolbar>

        <v-card-text>
            <v-text-field :disabled="!isEditing"
                          base-color="white"
                          v-model="username"
                          label="Nazwa użytkownika"></v-text-field>


            <v-text-field :disabled="!isEditing"
                          base-color="white"
                          v-model="email"
                          label="E-mail"></v-text-field>

            <v-text-field :disabled="!isEditing"
                          v-show="!isEditing"
                          model-value="******"
                          type="password"
                          base-color="white"
                          label="Hasło"></v-text-field>

            <v-text-field :disabled="!isEditing"
                          v-show="isEditing"
                          v-model="oldPassword"
                          type="password"
                          base-color="white"
                          label="Stare hasło"></v-text-field>

            <v-text-field :disabled="!isEditing"
                          v-show="isEditing"
                          v-model="newPassword"
                          type="password"
                          base-color="white"
                          label="Nowe hasło"></v-text-field>

            <v-text-field :disabled="!isEditing"
                          v-show="isEditing"
                          type="password"
                          base-color="white"
                          label="Powtórz nowe hasło"></v-text-field>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
            <v-spacer></v-spacer>

            <v-btn :disabled="!isEditing"
                   @click="save">
                Zapisz
            </v-btn>
        </v-card-actions>

        <v-snackbar v-model="hasSaved"
                    :timeout="2000"
                    attach
                    position="absolute"
                    location="bottom left">
            Your profile has been updated
        </v-snackbar>
    </v-card>
</template>