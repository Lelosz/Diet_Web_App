<script setup>
    import { ref } from 'vue'
    import { useField, useForm } from 'vee-validate'

    const { handleSubmit, handleReset } = useForm({
        validationSchema: {
            weight(value) {
                if (value <= 250 && value >= 30) return true

                return 'Waga nie może przekraczac 100kg.'
            },
            height(value) {
                if (value <= 250 && value >= 50) return true

                return 'Podaj prawidłowy zakres.'
            }
        },
    })

    const weight = useField('weight')
    const height = useField('height')


    let result = ref(0)

    

    const submit = handleSubmit(values => {
        alert(JSON.stringify(values, null, 2))
    })

    
    
    
</script>

<template>
    <v-form v-model="form" @submit.prevent="submit">

        <v-text-field v-model="weight.value.value"
                      :error-messages="weight.errorMessage.value"
                      label="Waga"></v-text-field>

        <v-text-field v-model="height.value.value"
                      :error-messages="height.errorMessage.value"
                      label="Wzrost"></v-text-field>

        <v-btn class="me-4"
               type="submit"
               @click="result = (weight.value.value/((height.value.value/100)*(height.value.value/100))).toFixed(2)">
            Zatwierdź
        </v-btn>

        <v-btn @click="handleReset">
            Wyczyść
        </v-btn>

    </v-form>

    <div>
        wynik = {{result}}
    </div>
</template>