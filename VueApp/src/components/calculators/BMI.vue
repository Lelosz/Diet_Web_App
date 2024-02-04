<script setup>
    import { ref, computed } from 'vue'
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'



    console.log(store.state.userId)

    const bmiRange = [
        { name: 'wygłodzenie', from: 10, to: 15.99 },
        { name: 'wychudzenie', from: 16, to: 16.99 },
        { name: 'niedowaga', from: 17, to: 18.49 },
        { name: 'waga prawidłowa', from: 18.5, to: 24.99 },
        { name: 'nadwaga', from: 25, to: 29.99 },
        { name: 'otyłość I stopnia', from: 30, to: 34.99 },
        { name: 'otyłość II stopnia', from: 35, to: 39.99 },
        { name: 'otyłość III stopnia', from: 40, to: 100 }
    ]

    const { handleSubmit, handleReset } = useForm({
        validationSchema: {
            weight(value) {
                if (value <= 250 && value >= 30) return true

                return 'Podałeś nieprawidłowy zakres wagi.'
            },
            height(value) {
                if (value <= 250 && value >= 50) return true

                return 'Podaj prawidłowy zakres wzrostu.'
            }
        },
    })

    const router = useRouter()
    const weight = useField('weight')
    const height = useField('height')

    

    const type = ref('')
    const result = ref(0)
    const res = ref(0)
    const authTokenValue = ref(0)

    const verifyResult = value => {
        return Object.values(bmiRange)
            .find(data => value >= data.from && value <= data.to)?.name || 'brak';
    }
    //const submit = handlesubmit(values => {
    //    alert(json.stringify(values, null, 2))
    //    result = (weight.value.value / ((height.value.value / 100) * (height.value.value / 100))).tofixed(2)
    //    console.log(result)
    //    return result
    //})
    //const submit = handleSubmit(values => {
    //    result = (weight.value.value / ((height.value.value / 100) * (height.value.value / 100))).toFixed(2)
    //    alert(JSON.stringify(result, null, 1))
    //})

    //max - width="400"

    const saveResult = async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value
        type.value = (weight.value.value / ((height.value.value / 100) * (height.value.value / 100))).toFixed(2)
        res.value = 'BMI = ' + type.value + ', Interpretacja: ' + verifyResult(type.value)


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/Calculator', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    Result: res.value,
                    CalculatorType: 0,
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
            Kalkulator BMI
        </v-card-title>
        <v-card-text>
            <v-form ref="form" @submit.prevent="submit">

                <v-text-field v-model="weight.value.value"
                              :error-messages="weight.errorMessage.value"
                              label="Waga"
                              v-validate:required="true"></v-text-field>

                <v-text-field v-model="height.value.value"
                              :error-messages="height.errorMessage.value"
                              label="Wzrost"
                              v-validate:required="true"></v-text-field>
                <v-row>
                    <v-col>
                        <v-btn class="me-4"
                               type="submit"
                               color="green-accent-3"
                               variant="outlined"
                               @click="result = (weight.value.value / ((height.value.value / 100) * (height.value.value / 100))).toFixed(2)">
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
                    <!--<div class="font-weight-bold">
                        Twój wskaźnik BMI: {{result}}
                        <br />
                        Interpretacja: {{verifyResult(result)}}

                    </div>-->
                    <v-card  variant="outlined">
                        <div class="ma-4">
                            <div class="text-h5 mb-1">
                                Wynik
                            </div>
                            <div>Twój wskaźnik BMI: {{result}}</div>
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

