<script setup>
    import { ref } from 'vue'
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router'

    const { handleSubmit, handleReset } = useForm({
        validationSchema: {
            login(value) {
                if (value?.length > 3) return true

                return 'Nazwa użytkownika powinna zawierać co najmniej 3 znaki.'
            },
            email(value) {
                if (value?.length > 3) return true

                return 'E-mail powinien zawierać co najmniej 3 znaki.'
            },
            password(value) {
                if (value?.length > 3 && value == repeatPassword.value.value) return true

                return 'Hasło powinno zawierać co najmniej 3 znaki.'
            },
            repeatPassword(value) {
                if (value?.length > 3 && value == password.value.value) return true

                return 'Hasło powinno zawierać co najmniej 3 znaki.'
            },
        },
    })

    const login = useField('login')
    const email = useField('email')
    const password = useField('password')
    const repeatPassword = useField('repeatPassword')

    const router = useRouter()

    const submit = handleSubmit(values => {
        onSubmit();
    })

    const onSubmit = async() => {
        if (password.value.value == repeatPassword.value.value) {
            await fetch('https://localhost:7011/api/user/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Username: login.value.value,
                    Password: password.value.value,
                    EmailAdress: email.value.value,
                    Role: "User"
                })
            }).then((response) => {
                if (response.ok) {
                    router.push('/LogIn');
                }
                return Promise.reject(response);
                })
                .then((result) => {
                    console.log(result);
                })
                .catch((error) => {
                    console.log('Something went wrong.', error);
                });

            

            return
        }
        return "hasla sie nie zgadzaja"
    }

</script>

<template>
    <v-card class="mx-auto my-16" max-width="600">
        <v-card-title class="text-center text-h4 ma-6 font-weight-bold">
            Zarejestruj się
        </v-card-title>
        <v-card-text>
            <v-form @submit.prevent="submit">
                <v-text-field v-model="login.value.value"
                              label="Nazwa użytkownika"
                              :error-messages="login.errorMessage.value"></v-text-field>

                <v-text-field v-model="email.value.value"
                              label="E-mail"
                              :error-messages="email.errorMessage.value"></v-text-field>

                <v-text-field v-model="password.value.value"
                              type="password"
                              label="Hasło"
                              :error-messages="password.errorMessage.value"></v-text-field>

                <v-text-field v-model="repeatPassword.value.value"
                              type="password"
                              label="Powtórz hasło"
                              :error-messages="repeatPassword.errorMessage.value"></v-text-field>

                <v-btn class="success mx-0 mt-3" variant="outlined" color="green-accent-3" type="submit">Zarejestruj się</v-btn>
            </v-form>
        </v-card-text>
    </v-card>
    <!--<v-sheet width="500" class="mx-auto">
        <v-form @submit.prevent="submit">
            <v-text-field v-model="login"
                          label="Nazwa użytkownika"
                          :rules="loginRules"></v-text-field>

            <v-text-field v-model="password"
                          type="password"
                          label="Hasło"
                          :rules="passwordRules"></v-text-field>

            <v-text-field v-model="repeatPassword"
                          type="password"
                          label="Powtórz hasło"
                          :rules="passwordRules"></v-text-field>

            <v-btn type="submit">Zarejestruj się</v-btn>
        </v-form>
    </v-sheet>-->
</template>