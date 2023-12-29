<script setup>
    import { ref } from 'vue'
    import { useField, useForm } from 'vee-validate'

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

    const submit = handleSubmit(values => {
        alert(JSON.stringify(values, null, 3))
    })

</script>
<template>
    <v-card class="mx-auto my-16 w-25" min-width="400" max-width="600">
        <v-card-title class="mb-6 font-weight-bold">
            Kalkulator Aktywności fizycznej
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
            <v-row class="ma-6">
                <div class="font-weight-bold">
                    Podczas wybranej aktywności spaliłeś: {{result}} kcal.
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
                    <v-btn class="font-weight-bold" color="green" @click="">
                        Zapisz wynik
                    </v-btn>
                </v-col>
            </v-row>
        </v-card-item>
    </v-card>
</template>