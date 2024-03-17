<script setup>
    import { onMounted, ref } from 'vue'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'

    
    const router = useRouter();

    const hasSaved = ref(false)
    const isEditing = ref(null)

    const username = ref('')
    const oldPassword = ref('')
    const newPassword = ref('')
    const newPasswordRepeat = ref('')
    const email = ref('')

    const authTokenValue = ref(0)

    console.log('test', store.state.userId);

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
    async function submitPassword() {
        

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value


        if (store.state.userId != null && newPassword.value != '') {

            await fetch('https://localhost:7011/api/User/' + store.state.userId, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    "id": store.state.userId,
                    "username": store.state.userName,
                    "password": newPassword.value,
                    "emailAdress": email.value,
                    "role": store.state.userRole
                })
            }).then((response) => {
                if (response.ok) {
                    logout();
                    router.push({ path: '/LogIn' });
                }
                return Promise.reject(response);
            })
                .then((result) => {
                    console.log(result);
                })
                .catch((error) => {
                    console.log('Something went wrong.', error);
                });

        }


        return "Niepoprawne hasło"
        
    }

    const logout = async () => {
        await fetch('https://localhost:7011/api/user/logout', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
        store.commit('loginStatus', false)
        store.commit('userId', null)
        store.commit('userRole', null)
        store.commit('userName', null)
        console.log("wylogowany")
    }
</script>

<template>
    <v-card class="mx-auto my-16 w-50" min-width="500" max-width="500">
        <v-card-title class="text-center text-h4 ma-4 font-weight-bold">
            Moje konto
        </v-card-title>
        <v-card-text>

            <v-text-field :disabled="true"
                          base-color="white"
                          v-model="username"
                          label="Nazwa użytkownika"></v-text-field>


            <v-text-field :disabled="true"
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
                          v-model="newPasswordRepeat"
                          type="password"
                          base-color="white"
                          label="Powtórz nowe hasło"></v-text-field>

        </v-card-text>

        <v-card-item>

            <v-row>
                <v-col>
                    <v-btn v-if="!isEditing" class="font-weight-bold" variant="outlined" color="red" width="130" @click="$router.back()">
                        Wstecz
                    </v-btn>
                    <v-btn v-if="isEditing" class="font-weight-bold" variant="outlined" color="red" width="130" @click="isEditing = !isEditing">
                        Anuluj
                    </v-btn>
                </v-col>
                <v-col class="text-right">
                    <v-btn v-if="!isEditing" class="font-weight-bold " variant="outlined" color="success" width="130" @click="isEditing = !isEditing">
                        Zmień hasło
                    </v-btn>
                    <v-btn v-if="isEditing" class="font-weight-bold " variant="outlined" color="success" width="130" @click="submitPassword">
                        Zapisz
                    </v-btn>
                </v-col>

            </v-row>
        </v-card-item>
    </v-card>

</template>