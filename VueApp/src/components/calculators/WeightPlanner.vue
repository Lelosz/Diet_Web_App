﻿<script setup>
    import { ref, computed } from 'vue'
    import { useField, useForm } from 'vee-validate'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'

    console.log(store.state.userId)
    const physicalActivities = [
        { name: 'leżenie w łóżku', mets: 1 },
        { name: 'chodzenie', mets: 2 },
        { name: 'naprawy domowe', mets: 3 },
        { name: 'siatkówka', mets: 4 },
        { name: 'kajakarstwo', mets: 5 },
    ]

    const genders = ref([
        'Mężczyzna',
        'Kobieta',
    ])

    const actionPhysicalChange = ref([
        'Dodaj',
        'Odejmij',
    ])

    const amountofActivity = ref([
        'Dziennie',
        'Tygodniowo',
    ])
    //const amountofActivity = [
    //    { name: 'Dziennie', value: 1 },
    //    { name: 'Tygodniowo', value: 7 },
    //]

    const activityLevels = [
        { name: 'Brak aktywności, chory, leżący', pal: 1.2 },
        { name: 'Lekka aktywność ok. 140min tygodniowo', pal: 1.4 },
        { name: 'Średnia aktywność ok. 280 minut tygodniowo', pal: 1.6 },
        { name: 'Wysoka aktywność ok. 420 minut tygodniowo', pal: 1.8 },
        { name: 'Bardzo wysoka aktywność ok. 560 minut tygodniowo', pal: 2 },
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

    const weight = useField('weight')
    const gender = useField('gender')
    const age = useField('age')
    const height = useField('height')
    const activitylevel = useField('activitylevel')
    const goalWeight = useField('goalWeight')

    const selectedDate = ref(null)
    const date = ref(new Date())
    date.value.setDate(date.value.getDate() + 1)

    const currentDate = new Date()
    currentDate.setDate(currentDate.getDate() + 2)
    const minDate = ref(currentDate.toISOString().split('T')[0])

    const daysUntilGoal = ref(1);

    const activityChange = useField('activityChange')
    activityChange.value.value = 0
    const type = useField('type')
    const intensity = useField('intensity')
    const minutes = useField('minutes')
    const times = useField('times')
    const per = useField('per')
    const overlay = ref(false)

    const items = ref([
        
    ])

    const goal = ref(0)
    const maintainGoal = ref(0)
    const maintainWeight = ref(0)

    const dayToReachGoal = ref(1)


    const activityLevelChange = ref(1)
    const totalActivityLevel= ref(1)
    const CPM = value => {
        let activityLevel = activitylevel.value.value;
        if (value == 'Mężczyzna')
            return ((9.99 * weight.value.value + 6.25 * height.value.value - 4.92 * age.value.value + 5) * activityLevel).toFixed(0);
        else
            return ((9.99 * weight.value.value + 6.25 * height.value.value - 4.92 * age.value.value - 161) * activityLevel).toFixed(0);

    }
    
    const CPMGoal = value => {
        let newActivity = activityChange.value.value / 7;
        let activityLevel = activitylevel.value.value + (activitylevel.value.value * newActivity /100 );
        console.log(activityLevel)
        if (value == 'Mężczyzna')
            return ((9.99 * goalWeight.value.value + 6.25 * height.value.value - 4.92 * age.value.value + 5) * activityLevel).toFixed(0);
        else
            return ((9.99 * goalWeight.value.value + 6.25 * height.value.value - 4.92 * age.value.value - 161) * activityLevel).toFixed(0);

    }

    
    function CPMtoGoal(value, days) {
        //let activityLevel = activitylevel.value.value * activityChange.value.value / 100;
        let cpm;
        let newActivity= activityChange.value.value / 7;
        let activityLevel = activitylevel.value.value + (activitylevel.value.value * newActivity / 100);
        if (value == 'Mężczyzna')
            cpm = ((9.99 * weight.value.value + 6.25 * height.value.value - 4.92 * age.value.value + 5) * activityLevel);
        else
            cpm = ((9.99 * weight.value.value + 6.25 * height.value.value - 4.92 * age.value.value - 161) * activityLevel);
        return (cpm - ((weight.value.value - goalWeight.value.value) * 7700 / days)).toFixed(0);
    }


    function daysToGoal(value) {
        let Difference_In_Time = value.getTime() - new Date().getTime();
        daysUntilGoal.value = Difference_In_Time;
        return Math.round(Difference_In_Time / (1000 * 3600 * 24));
    }

    const verifyResult = value => {
        return value * portionSize.value.value / 100 || 0;
    }

    const verifyActivityResult = value => {
        return value * 60 / (activity.value.value * weight.value.value) || 0;
    }

    const id = ref(0)
    const addToTable = value => {
        items.value.push({ id: id.value, type: type.value.value, intensity: intensity.value.value, minutes: minutes.value.value, times: times.value.value, per: per.value.value });
        let finalChange = 1;
        let pal = activitylevel.value.value; //start pal value
        let allItems = Object.values(items)[3];
        let totalChange = 0;
        for (let i = 0; i < allItems.length; i++) {
            let change = 0;
            let METminutes = Number(allItems[i].times) * Number(allItems[i].minutes) * allItems[i].intensity;
            if (allItems[i].type == 'Dodaj') {
                change = change + METminutes;
            } else {
                change = change - METminutes;
            }

            if (allItems[i].per == 'Dziennie') {
                totalChange = totalChange + (change * 7 / 1440);
            } else {
                totalChange = totalChange + (change / 1440);
            }
        }
        
        //o = allItems[allItems.length - 1].id;
        //console.log(o);


        console.log(Object.values(items)[3]);
        finalChange = ((((totalChange + pal) / pal) * 100)-100).toFixed(0);
        console.log(finalChange);
        activityChange.value.value = finalChange;
        id.value++;
    }


    function removeItem(index) {
        items.value.splice(index, 1);
        let finalChange = 1;
        let pal = activitylevel.value.value; //start pal value
        let allItems = Object.values(items)[3];
        let totalChange = 0;
        for (let i = 0; i < allItems.length; i++) {
            let change = 0;
            let METminutes = Number(allItems[i].times) * Number(allItems[i].minutes) * allItems[i].intensity;
            if (allItems[i].type == 'Dodaj') {
                change = change + METminutes;
            } else {
                change = change - METminutes;
            }

            if (allItems[i].per == 'Dziennie') {
                totalChange = totalChange + (change * 7 / 1440);
            } else {
                totalChange = totalChange + (change * 7 / 10080);
            }
        }

        //o = allItems[allItems.length - 1].id;
        //console.log(o);


        console.log(Object.values(items)[3]);
        finalChange = Math.round(((totalChange + pal) / pal) * 100);
        console.log(finalChange);
        activityChange.value.value = finalChange;
    }


    const router = useRouter()
    const res = ref(0)
    const authTokenValue = ref(0)

    async function saveResult() {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value
        res.value = 'Aby utrzymać obecną wagę ' + weight.value.value + 'kg = ' + CPM(gender.value.value) + ', aby osiągnąć cel ' + goalWeight.value.value +'kg = ' + CPMtoGoal(gender.value.value, daysUntilGoal.value) + ', aby utrzymać cel = ' + CPMGoal(gender.value.value)


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/Calculator', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    Result: res.value,
                    CalculatorType: 3,
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
    <v-container class="mx-auto my-16 w-50">
        <v-stepper alt-labels :items="['Podstawowe informacje', 'Cel', 'Aktywność', 'Wynik']">
            <template v-slot:item.1>
                <v-card title="Wprowadź podstawowe informacje" flat>

                    <v-text-field v-model="weight.value.value"
                                  :error-messages="weight.errorMessage.value"
                                  label="Twoja obecna waga w kg"></v-text-field>

                    <v-select v-model="gender.value.value"
                              :items="genders"
                              :error-messages="gender.errorMessage.value"
                              label="Płeć"></v-select>

                    <v-text-field v-model="age.value.value"
                                  :error-messages="age.errorMessage.value"
                                  label="Wiek"></v-text-field>

                    <v-text-field v-model="height.value.value"
                                  :error-messages="height.errorMessage.value"
                                  label="Wzrost w cm"></v-text-field>

                    <v-select v-model="activitylevel.value.value"
                              :items="activityLevels"
                              :error-messages="activitylevel.errorMessage.value"
                              item-title="name"
                              item-value="pal"
                              label="Aktywność fizyczna"></v-select>

                </v-card>
            </template>

            <template v-slot:item.2>
                <v-card title="Podaj wagę docelową" flat>

                    <v-text-field v-model="goalWeight.value.value"
                                  :error-messages="goalWeight.errorMessage.value"
                                  label="Waga docelowa w kg"></v-text-field>

                    <v-container>
                        <v-row justify="space-around">
                            <v-date-picker
                                v-model="date"
                                title="Chcę osiągnąć mój cel do:"
                                border="rounded-pill"
                                color="success"
                                bg-color="rgba(128, 237, 128, 0.8)"
                                :min="minDate"
                            ></v-date-picker>
                        </v-row>
                    </v-container>

                </v-card>
            </template>

            <template v-slot:item.3>
                <v-card title="Zmiana poziomu aktywności fizycznej (Opcjonalne)" flat>
                    <v-text-field v-model="activityChange.value.value"
                                  :error-messages="activityChange.errorMessage.value"
                                  label="Zmiana w poziomie aktywności fizycznej wyrażona w %"></v-text-field>
                    <span class="">Jeżeli nie wiesz jaką wartość powinieneś wpisać użyj opcji poniżej.</span>
                    <v-spacer></v-spacer>
                    <v-btn class="font-weight-bold my-6" variant="outlined" color="green" @click="overlay = !overlay">
                        Dodaj nową aktywność
                        <v-overlay activator="parent"
                                   location-strategy="connected"
                                   scroll-strategy="block">
                            <v-card class="pa-2">
                                <v-form @submit="submit">

                                    <v-select v-model="type.value.value"
                                              :items="actionPhysicalChange"
                                              :error-messages="type.errorMessage.value"
                                              label="Rodzaj"></v-select>

                                    <v-select v-model="intensity.value.value"
                                              :items="physicalActivities"
                                              item-title="name"
                                              item-value="mets"
                                              :error-messages="intensity.errorMessage.value"
                                              label="Intensywność"></v-select>

                                    <v-text-field v-model="minutes.value.value"
                                                  :error-messages="minutes.errorMessage.value"
                                                  label="Czas trwania w minutach"></v-text-field>

                                    <v-text-field v-model="times.value.value"
                                                  :error-messages="times.errorMessage.value"
                                                  label="Razy"></v-text-field>

                                    <v-select v-model="per.value.value"
                                              :items="amountofActivity"
                                              item-title="name"
                                              item-value="value"
                                              :error-messages="per.errorMessage.value"
                                              label="W ciągu"></v-select>

                                    <v-btn @click="addToTable()" variant="outlined" color="green">Dodaj</v-btn>
                                </v-form>
                            </v-card>
                        </v-overlay>
                    </v-btn>

                    <v-data-table v-if="items.length>0?true:false">
                        <template v-slot:default>
                            <thead>
                                <tr>
                                    <th class="text-left">
                                        ID
                                    </th>
                                    <th class="text-left">
                                        Akcja
                                    </th>
                                    <th class="text-left">
                                        Aktywność
                                    </th>
                                    <th class="text-left">
                                        Czas trwania
                                    </th>
                                    <th class="text-left">
                                        Ilość wystąpień
                                    </th>
                                    <th class="text-left">
                                        Częstotliwość wystąpień
                                    </th>
                                    <th>Usuń</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item, index) in items"
                                    :key="item.id">
                                    <td>{{ item.id }}</td>
                                    <td>{{ item.type }}</td>
                                    <td>{{ item.intensity }}</td>
                                    <td>{{ item.minutes }}</td>
                                    <td>{{ item.times }}</td>
                                    <td>{{ item.per }}</td>
                                    <td>
                                        <v-btn icon @click="removeItem(index)">
                                            <v-icon>mdi-delete</v-icon>
                                        </v-btn>
                                    </td>
                                </tr>
                            </tbody>
                        </template>
                        <template v-slot:bottom>
                            <div class="text-center pt-2">
                                <v-pagination v-model="page"></v-pagination>
                            </div>
                        </template>
                    </v-data-table>
                </v-card>
            </template>

            <template v-slot:item.4>
                <v-card title="Wynik" flat>
                    <div class="my-6">
                        <v-card class="my-4" variant="outlined">
                            <div class="ma-4">
                                <div class="text-h6 mb-1">
                                    {{CPM(gender.value.value)}} kcal
                                </div>
                                <div class="text-caption">Tyle powinieneś spożywać aby utrzymać obecną wagę {{weight.value.value}}kg.</div>
                            </div>
                        </v-card>
                        <v-card class="my-4" variant="outlined">
                            <div class="ma-4">
                                <div class="text-h6 mb-1">
                                    {{CPMtoGoal(gender.value.value, daysToGoal(date))}} kcal
                                </div>
                                <div class="text-caption">Tyle powinieneś spożywać aby osiągnąć swój cel:{{goalWeight.value.value}} kg w czasie {{daysToGoal(date)}} dni.</div>
                            </div>
                        </v-card>
                        <v-card class="my-4" variant="outlined">
                            <div class="ma-4">
                                <div class="text-h6 mb-1">
                                    {{CPMGoal(gender.value.value)}} kcal
                                </div>
                                <div class="text-caption">Tyle powinieneś spożywać aby utrzymać swoją docelową wagę {{goalWeight.value.value}} kg.</div>
                            </div>
                        </v-card>


                    </div>
                </v-card>
                <v-row>
                    
                    <v-col class="text-right">
                        <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3" @click="saveResult">
                            Zapisz wynik
                        </v-btn>
                    </v-col>
                </v-row>
            </template>

        </v-stepper>

    </v-container>
</template>