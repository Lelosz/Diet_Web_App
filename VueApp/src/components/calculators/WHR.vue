<script setup>
    import { ref } from 'vue'
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'

    const whrRangeM = [
        { name: 'otyłość brzuszna', gender: 'male', from: 1 , to: 2},
        { name: 'otyłość udowo-pośladkowa', gender: 'male', from: 0.01, to: 0.99}
    ]
    const whrRangeF = [
        { name: 'otyłość brzuszna', gender: 'female', from: 0.8, to: 2 },
        { name: 'otyłość udowo-pośladkowa', gender: 'female', from: 0.01, to: 0.79 }
    ]

    const { handleSubmit, handleReset } = useForm({
        validationSchema: {
            waist(value) {
                if (value <= 250 && value >= 30) return true

                return 'Podałeś nieprawidłowy zakres.'
            },
            hips(value) {
                if (value <= 250 && value >= 50) return true

                return 'Podaj prawidłowy zakres.'
            },
            gender(value) {
                if (value) return true

                return 'Wybierz płeć.'
            },
        }
    })

    const genders = ref([
        'Mężczyzna',
        'Kobieta',
    ])

    const gender = useField('gender')
    const waist = useField('waist')
    const hips = useField('hips')
    let result = ref(0)

    const router = useRouter()
    const type = ref('')
    const res = ref(0)
    const authTokenValue = ref(0)

    const verifyResult = value => {
        if (gender.value.value == 'Mężczyzna')
            return Object.values(whrRangeM)
                .find(data => value >= data.from && value <= data.to)?.name || 'brak';
        else
            return Object.values(whrRangeF)
                .find(data => value >= data.from && value <= data.to)?.name || 'brak';

    }


    const saveResult = async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value
        type.value = (waist.value.value / hips.value.value).toFixed(2)
        res.value = 'WHR = ' + type.value + ', Interpretacja: ' + verifyResult(type.value)


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/Calculator', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    Result: res.value,
                    CalculatorType: 1,
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
            Kalkulator WHR
        </v-card-title>
        <v-card-text>
            <v-form ref="form" @submit.prevent="submit">

                <v-select v-model="gender.value.value"
                          :items="genders"
                          :error-messages="gender.errorMessage.value"
                          label="Płeć"></v-select>

                <v-text-field v-model="waist.value.value"
                              :error-messages="waist.errorMessage.value"
                              label="Obwód tali w cm"
                              v-validate:required="true"></v-text-field>

                <v-text-field v-model="hips.value.value"
                              :error-messages="hips.errorMessage.value"
                              label="Obwód bioder w cm"
                              v-validate:required="true"></v-text-field>
                <v-row>
                    <v-col>
                        <v-btn class="me-4"
                               type="submit"
                               color="green-accent-3"
                               variant="outlined"
                               @click="result = (waist.value.value/hips.value.value).toFixed(2)">
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
                            <div>Twój wskaźnik WHR: {{result}}</div>
                            <div>Interpretacja: {{verifyResult(result)}}</div>
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