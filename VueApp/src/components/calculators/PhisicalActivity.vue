<script setup>
    import { ref } from 'vue'
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'

    const { handleSubmit, handleReset } = useForm({
        validationSchema: {
            weight(value) {
                if (value <= 250 && value >= 30) return true

                return 'Podałeś nieprawidłowy zakres wagi.'
            },
            time(value) {
                if (value <= 1000 && value >= 1) return true

                return 'Podaj prawidłowy zakres czasu trwania aktywności.'
            },
            activity(value) {
                if (value) return true

                return 'Wybierz aktywność.'
            }
        },
    })

    const physicalActivities = [
        { name: 'leżenie w łóżku', mets: 1 },
        { name: 'chodzenie', mets: 2 },
        { name: 'naprawy domowe', mets: 3 },
        { name: 'siatkówka', mets: 4 },
        { name: 'kajakarstwo', mets: 5 },
    ]

   

    const weight = useField('weight')
    const time = useField('time')
    const activity = useField('activity')

    let result = ref(0)

    const verifyResult = value => {
        return value * weight.value.value * time.value.value / 60|| 0;
    }

    

    const router = useRouter()
    const type = ref('')
    const res = ref(0)
    const authTokenValue = ref(0)

    const saveResult = async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value
        res.value = 'Spaliłeś = ' + verifyResult(activity.value.value).toFixed(2) + ' kcal'


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/Calculator', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    Result: res.value,
                    CalculatorType: 4,
                    Created: new Date(),
                    UserId: store.state.userId
                })
            }).then((response) => {
                if (response.ok) {
                    router.back();
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




        return "Dane logowania są niepoprawne"
    }

    const submitted = ref(false)

    const submit = handleSubmit(values => {
        submitted.value = true
    })

</script>
<template>
    <v-card class="mx-auto my-16 w-25" min-width="400" max-width="600">
        <v-card-title class="text-center text-h5 ma-4 font-weight-bold">
            Kalkulator aktywności fizycznej
        </v-card-title>
        <v-card-text>
            <v-form @submit.prevent="submit">

                <v-text-field v-model="weight.value.value"
                              :error-messages="weight.errorMessage.value"
                              label="Waga"></v-text-field>

                <v-text-field v-model="time.value.value"
                              :error-messages="time.errorMessage.value"
                              label="Czas trwania aktywności"></v-text-field>

                <v-autocomplete v-model="activity.value.value"
                          :items="physicalActivities"
                          :error-messages="activity.errorMessage.value"
                                item-title="name"
                                item-value="mets"
                          label="Aktywność fizyczna"></v-autocomplete>
                <v-row>
                    <v-col>
                        <v-btn class="me-4"
                               type="submit"
                               color="green-accent-3"
                               variant="outlined"
                               @click="result = verifyResult(activity.value.value).toFixed(2)">
                            Oblicz
                        </v-btn>
                    </v-col>
                    <!--<v-col class="text-right">
            <v-btn @click="handleReset">
                Wyczyść
            </v-btn>
        </v-col>-->
                </v-row>
            </v-form>


        </v-card-text>
        <v-card-item>
            <v-row v-if="submitted">
                <v-col>
                    <v-card variant="outlined">
                        <div class="ma-4">
                            <div class="text-h5 mb-1">
                                Wynik
                            </div>
                            <div>Podczas wybranej aktywności spaliłeś: {{result}} kcal</div>
                        </div>
                    </v-card>
                    <!-- v-if Zapisz wynik- button, opis wyniku -->
                </v-col>
            </v-row>
            <v-row>
                <v-col>
                    <v-btn class="font-weight-bold" variant="outlined" color="red" @click="$router.back()">
                        Wstecz
                    </v-btn>
                </v-col>
                <v-col class="text-right">
                    <v-btn v-if="submitted" class="font-weight-bold" variant="outlined" color="green-accent-3" @click="saveResult">
                        Zapisz wynik
                    </v-btn>
                </v-col>
            </v-row>
        </v-card-item>
    </v-card>
</template>