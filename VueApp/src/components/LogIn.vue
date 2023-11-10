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
    }
</script>

<template>
    <v-sheet width="500" class="mx-auto">
        <v-form @submit.prevent="submit">
            <v-text-field
                v-model="login"
                label="Nazwa użytkownika"
                :rules="loginRules"
            ></v-text-field>

            <v-text-field
                v-model="password"
                type="password"
                label="Hasło"
                :rules="passwordRules"
            ></v-text-field>

            <v-btn type="submit">Zaloguj się</v-btn>
            <router-link to="/SignUp">
                <v-btn>Zarejestruj się</v-btn>
            </router-link>
</v-form>
    </v-sheet>
</template>