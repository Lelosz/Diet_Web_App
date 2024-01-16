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
            height(value) {
                if (value <= 250 && value >= 50) return true

                return 'Podaj prawidłowy zakres wzrostu.'
            },
            age(value) {
                if (value <= 120 && value >= 1) return true

                return 'Podaj prawidłowy zakres wieku.'
            },
            height(value) {
                if (value <= 250 && value >= 50) return true

                return 'Podaj prawidłowy zakres wzrostu.'
            },
            gender(value) {
                if (value) return true

                return 'Wybierz płeć.'
            },
            activitylevel(value) {
                if (value) return true

                return 'Wybierz poziom swojej aktywności.'
            },

        },
    })

    const genders = ref([
        'Mężczyzna',
        'Kobieta',
    ])

    const activityLevels = [
        { name: 'Brak aktywności, chory, leżący', pal: 1.2},
        { name: 'Lekka aktywność ok. 140min tygodniowo', pal: 1.4},
        { name: 'Średnia aktywność ok. 280 minut tygodniowo', pal: 1.6},
        { name: 'Wysoka aktywność ok. 420 minut tygodniowo', pal: 1.8},
        { name: 'Bardzo wysoka aktywność ok. 560 minut tygodniowo', pal: 2},
    ]

    

    const gender = useField('gender')
    const age = useField('age')
    const weight = useField('weight')
    const height = useField('height')
    const activitylevel = useField('activitylevel')

    let resultPPM = ref(0)
    let resultCPM = ref(0)


    const verifyResultPPM = value => {
        if (value == 'Mężczyzna')
            return (9.99 * weight.value.value + 6.25 * height.value.value - 4.92 * age.value.value + 5).toFixed(0);
        else
            return (9.99 * weight.value.value + 6.25 * height.value.value - 4.92 * age.value.value - 161).toFixed(0);

    }
    const verifyResultCPM = value => {
        return (value * resultPPM.value) || 0;
    }
    //const verifyResultCPM = value => {
    //    if (isNaN(value * resultPPM.value)) { return 0; }
    //    return (value * resultPPM.value).toFixed(0);
    //}

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
        type.value = verifyResultPPM(gender.value.value)
        res.value = 'PPM = ' + type.value + ', CPM = ' + verifyResultCPM(activitylevel.value.value).toFixed(0)


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/Calculator', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    Result: res.value,
                    CalculatorType: 2,
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



</script>
<template>
    <v-card class="mx-auto my-16 w-25" min-width="400" max-width="600">
        <v-card-title class="mb-6 font-weight-bold">
            Kalkulator PPM i CPM
        </v-card-title>
        <v-card-text>
            <v-form ref="form" @submit.prevent="submit">

                <v-select v-model="gender.value.value"
                          :items="genders"
                          :error-messages="gender.errorMessage.value"
                          label="Płeć"></v-select>

                <v-select v-model="activitylevel.value.value"
                          :items="activityLevels"
                          :error-messages="activitylevel.errorMessage.value"
                          item-title="name"
                          item-value="pal"
                          label="Aktywność fizyczna"></v-select>

                <v-text-field v-model="age.value.value"
                              :error-messages="age.errorMessage.value"
                              label="Wiek"
                              v-validate:required="true"></v-text-field>

                <v-text-field v-model="weight.value.value"
                              :error-messages="weight.errorMessage.value"
                              label="Masa ciała w kg"
                              v-validate:required="true"></v-text-field>

                <v-text-field v-model="height.value.value"
                              :error-messages="height.errorMessage.value"
                              label="Wzrost w cm"
                              v-validate:required="true"></v-text-field>


                <v-row>
                    <v-col>
                        <v-btn class="me-4"
                               type="submit"
                               @click="resultPPM = verifyResultPPM(gender.value.value)">
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
            <v-row class="ma-6">
                <div class="font-weight-bold">
                    Twój wskaźnik PPM: {{resultPPM}} kcal dziennie
                    <br />
                    Twój wskaźnik CPM: {{verifyResultCPM(activitylevel.value.value).toFixed(0)}} kcal dziennie
                </div>
                <!-- v-if Zapisz wynik- button, opis wyniku -->
            </v-row>
            <v-row>
                <v-col>
                    <v-btn class="font-weight-bold" color="red" @click="$router.back()">
                        Wstecz
                    </v-btn>
                </v-col>
                <v-col class="text-right">
                    <v-btn class="font-weight-bold" color="green" @click="saveResult">
                        Zapisz wynik
                    </v-btn>
                </v-col>
            </v-row>
        </v-card-item>
    </v-card>
</template>