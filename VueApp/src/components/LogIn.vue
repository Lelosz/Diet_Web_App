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
            password(value) {
                if (value?.length > 3) return true

                return 'Hasło powinno zawierać co najmniej 3 znaki.'
            }
        },
    })

    const login = useField('login')
    const password = useField('password')


    const router = useRouter()

    const submit = handleSubmit(values => {
        onSubmit();
    })

    const onSubmit = async () => {
        
        await fetch('https://localhost:7011/api/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify({
                Username: login.value.value,
                Password: password.value.value
            })
        }).then((response) => {
            if (response.ok) {
                router.push('/');
            }
            return Promise.reject(response);
            })
            .then((result) => {
                console.log(result);
            })
            .catch((error) => {
                console.log('Something went wrong.', error);
            });



      
        return "Dane logowania są niepoprawne"
    }
   
</script>

<template>
    <v-card class="mx-auto my-8" max-width="600">
        <v-card-title>
            Zaloguj się
        </v-card-title>
        <v-card-text>
            <v-form @submit.prevent="submit">
                <v-text-field v-model="login.value.value"
                              label="Nazwa użytkownika"
                              :error-messages="login.errorMessage.value"
                              ></v-text-field>

                <v-text-field v-model="password.value.value"
                              type="password"
                              label="Hasło"
                              :error-messages="password.errorMessage.value"
                              ></v-text-field>

                
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