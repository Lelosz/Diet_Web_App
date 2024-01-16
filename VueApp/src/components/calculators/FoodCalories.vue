<script setup>
    import { ref } from 'vue'
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'

    const snacks = [
        { name: 'Baton Milky Way', kcal: 448},
        { name: 'Baton Twix', kcal: 483},
        { name: 'Napoleonka', kcal: 350},
        { name: 'Chipsy', kcal: 537}
    ]

    const physicalActivities = [
        { name: 'leżenie w łóżku', mets: 1 },
        { name: 'chodzenie', mets: 2 },
        { name: 'naprawy domowe', mets: 3 },
        { name: 'siatkówka', mets: 4 },
        { name: 'kajakarstwo', mets: 5 },
    ]
    

    const { handleSubmit, handleReset } = useForm({
        validationSchema: {
            portionSize(value) {
                if (value <= 2000 && value >= 1) return true

                return 'Podałeś nieprawidłowy zakres wagi.'
            },
            snack(value) {
                if (value) return true

                return 'Wybierz przekąskę.'
            },
        },
    })

    const snack = useField('snack')
    const portionSize = useField('portionSize')
    const weight = useField('weight')
    const activity = useField('activity')

    let result = ref(0)


    const verifyResult = value => {
        return value * portionSize.value.value / 100 || 0;
    }

    const verifyActivityResult = value => {
        return value *60 / (activity.value.value * weight.value.value) || 0;
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
        type.value = verifyResult(snack.value.value).toFixed(2)
        res.value = 'Całkowita ilość kcal = ' + type.value + ', aby spalić kalorie z wybranego produktu musisz wydatkować energię z intensywnością wybranej aktywności przez = ' + verifyActivityResult(type.value).toFixed(0) + ' minut.'


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/Calculator', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    Result: res.value,
                    CalculatorType: 5,
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
            Kalkulator kalorii z napojów i przekąsek
        </v-card-title>
        <v-card-text>
            <v-form ref="form" @submit.prevent="submit">

                <v-autocomplete v-model="snack.value.value"
                                :items="snacks"
                                :error-messages="snack.errorMessage.value"
                                item-title="name"
                                item-value="kcal"
                                label="Wybierz produkt"></v-autocomplete>

                <v-text-field v-model="snack.value.value"
                              label="Kcal w 100 gramach"
                              variant="solo"
                              readonly
                              v-validate:required="true"></v-text-field>

                <v-text-field v-model="portionSize.value.value"
                              label="Całkowita waga produktu w gramach"
                              :error-messages="portionSize.errorMessage.value"></v-text-field>

                <v-text-field v-model="weight.value.value"
                              :error-messages="weight.errorMessage.value"
                              label="Twoja waga w kg"></v-text-field>

                <v-autocomplete v-model="activity.value.value"
                                :items="physicalActivities"
                                :error-messages="activity.errorMessage.value"
                                item-title="name"
                                item-value="mets"
                                label="Wybierz aktywność"></v-autocomplete>
                <v-row>
                    <v-col>
                        <v-btn class="me-4"
                               type="submit"
                               @click="result = verifyResult(snack.value.value).toFixed(2)">
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
                    Calkowita ilość kalorii: {{result}}
                    <v-spacer></v-spacer>
                    Aby spalić kalorie z wybranego produktu musisz wydatkować energię z intensywnością wybranej aktywności przez {{verifyActivityResult(result).toFixed(0)}} minut.
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