<script setup>
    import { onMounted, ref, onBeforeMount } from 'vue'
    import { useRouter } from 'vue-router'
    import { useField, useForm } from 'vee-validate'
    import store from '@/store/index.js'
    import Chart from 'chart.js/auto'

    
    const calculatorNames =  ['BMI','WHR','PPM i CPM', 'Planer wagi', 'Aktywność fizyczna','Kalorie z przekąsek i na pojów']
    const calculators = [
        { name: 'BMI'},
        { name: 'WHR'},
        { name: 'PPM i CPM' },
        { name: 'Planer wagi'},
        { name: 'Aktywność fizyczna' },
        { name: 'Kalorie z przekąsek i na pojów'},
    ]
    const calculatorType = useField('calculatorType')

    const Months = [
        { name: '01.2024' },
        { name: '12.2023' },
        { name: '11.2023' },
        { name: '10.2023' },
        { name: '09.2023' },
        { name: '08.2023' },
    ]
    const selectedMonth = useField('selectedMonth')

    const authTokenValue = ref(0)
    const userHistory = ref({})
    const history = ref(0)

    const labels = []
    const chartData = []
    

    onBeforeMount(() => {
        const items = []
        const router = useRouter()
        const fetchResult = ref('')
        const userCalculatorData = ref('')
    }); 

    onMounted(async () => {


        
        
        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value

        console.log(store.state.userId)
        if (store.state.userId != null) {
            let response = await fetch('https://localhost:7011/api/Calculator/UserCalculatorList?userid=' + store.state.userId, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include'

            });
            let resp = await response.json()
            userHistory.value = resp.slice().reverse()
            
            //let userCalculatorData = await response.json()
            console.log(userHistory.value)
            for (const h in userHistory.value) {
                const regex = /\=([^,]+)/;
                const match = userHistory.value[h].result.match(regex);
                if (match) { chartData.push(match[1]); }

                const dateValue = userHistory.value[h].created;
                const dayValue = parseDate(dateValue);
                if (dayValue) {labels.push(dayValue); }
                console.log('PRZYKLAD ' + match[1]);
            }
            
            console.log(labels);

            const data = {
                labels: labels.reverse(),
                datasets: [{
                    label: 'Kalkulator BMI',
                    backgroundColor: 'rgb(255,99,132)',
                    borderColor: 'rgb(255,99,132)',
                    data: chartData.reverse(),
                }]
            };
            const config = {
                type: 'line',
                data: data,
                options: {}
            };

            const myChart = new Chart(
                document.getElementById('myChart'), config);
        }




        return "Dane logowania są niepoprawne"
    });

    const parseDate = value => {
        let parsedDate = new Date(value);
        let day = parsedDate.getDate();
        if (day < 10) { day = '0' + day }
        let month = parsedDate.getMonth() + 1;
        if (month < 10) { month = '0' + month }
        let year = parsedDate.getFullYear();
       
        return day + '-' + month + '-' + year;
    }

    const parseHour = value => {
        let parsedDate = new Date(value);
        let hour = parsedDate.getHours();
        if (hour < 10) { hour = '0' + hour }
        let minutes = parsedDate.getMinutes();
        if (minutes < 10) { minutes = '0' + minutes }
        return hour + ':' + minutes;
    }
    

</script>

<template>
    
    <v-card class="mx-auto my-16 w-50" min-width="400" max-width="800">
        <v-card-title class="text-center text-h5 ma-4 font-weight-bold">
            Raport
        </v-card-title>
        <v-card-text>
            <div class="text-h6 ma-4 font-weight-bold" align="left">Wizualizacja</div>
            <div>
                <v-row>
                    <v-col>
                        <v-select v-model="calculatorType.value.value"
                                  :items="calculators"
                                  item-title="name"
                                  item-value="name"
                                  return-object
                                  label="Rodzaj kalkulatora"></v-select>
                    </v-col>
                    <v-col>
                        <v-select v-model="selectedMonth.value.value"
                                  :items="Months"
                                  item-title="name"
                                  item-value="name"
                                  label="Miesiąc"></v-select>
                    </v-col>
                </v-row>
                
                <canvas id="myChart"></canvas>
            </div>

            <div class="text-h6 ma-4 font-weight-bold" align="left">Historia</div>
            <v-row>
                <v-col v-for="item in userHistory"
                       :key="i"
                       cols="12">
                    <v-card variant="outlined">
                        <div class="ma-4">
                            <div class="text-h6 mb-1">
                                Kalkulator: {{calculatorNames[item.calculatorType]}}
                            </div>
                            <div class="text-caption">Data: {{parseDate(item.created)}}, {{parseHour(item.created)}}</div>
                            <div class="text-caption">Wynik: {{item.result}}</div>
                        </div>
                    </v-card>
                </v-col>
            </v-row>






        </v-card-text>
        <v-card-item>

            <v-row>
                <v-col>
                    <v-btn class="font-weight-bold" variant="outlined" color="red" @click="$router.back()">
                        Wstecz
                    </v-btn>
                </v-col>

            </v-row>
        </v-card-item>
    </v-card>
</template>