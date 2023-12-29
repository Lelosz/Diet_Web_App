<script>
    export default {
        data: () => ({
            login: '',
            loginRules: [
                value => {
                    if (value?.length > 3) return true

                    return 'Nazwa użytkownika powinna miec conajmniej 4 znaki'
                },
            ],
            password: '',
            passwordRules: [
                value => {
                    if (value?.length > 7) return true

                    return 'Hasło powinno miec conajmniej 8 znaków'
                },
            ],
        }),
        methods: {
            submit() {
                console.log(this.login)
            }
        }
    }
</script>

<template>
    <v-card class="mx-auto my-8" max-width="600">
        <v-card-title>
            Zaloguj się
        </v-card-title>
        <v-card-text>
            <v-form @submit.prevent="submit">
                <v-text-field v-model="login"
                              label="Nazwa użytkownika"
                              :rules="loginRules"></v-text-field>

                <v-text-field v-model="password"
                              type="password"
                              label="Hasło"
                              :rules="passwordRules"></v-text-field>

                
                <v-row>
                    <v-col align-self="start">
                        <v-btn class="success mx-0 mt-3" type="submit">Zaloguj się</v-btn>
                    </v-col>

                    <v-col align-self="center">Nie masz konta?</v-col>
                    <v-col align-self="end">
                        <router-link to="/SignUp">
                            <v-btn class="success mx-0 mt-3">Zarejestruj się!</v-btn>
                        </router-link>

                    </v-col>
                </v-row>
            </v-form>
        </v-card-text>
    </v-card>
</template>