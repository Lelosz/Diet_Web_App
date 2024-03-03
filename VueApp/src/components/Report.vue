<script setup>
    import { onMounted, ref, onBeforeMount } from 'vue'
    import { useRouter } from 'vue-router'
    import { useField, useForm } from 'vee-validate'
    import store from '@/store/index.js'
    import Chart from 'chart.js/auto'

    
    const calculatorNames =  ['BMI','WHR','PPM i CPM', 'Planer wagi', 'Aktywność fizyczna','Kalorie z przekąsek i na pojów']
    const calculators = ref([{ name: 'BMI', value: 0 }, { name: 'WHR', value: 1 }, { name: 'PPM', value: 2 }, { name: 'CPM', value: 3 }, { name: 'Spalone kalorie', value: 4 }, { name: 'Spożyte kalorie', value: 5 }])
    const calculatorType = useField('calculatorType')

    const Months = ref([])
    const selectedMonth = useField('selectedMonth')

    const authTokenValue = ref(0)
    const userHistory = ref([])
    const userData = ref([])
    const history = ref(0)

    const labels = ref([])
    const chartData = ref([])

    const BMIResults = ref([])
    const WHRResults = ref([])
    const PPMResults = ref([])
    const CPMResults = ref([])
    const WeightPlannerResults = ref([[,[]]])
    const PhysicalActivityResults = ref([])
    const FoodCaloriesResults = ref([])
    const cleanedHistory = ref([])

    
    onMounted(async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value

        //console.log(store.state.userId)
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
            
           

            


            const BMIregex = /\d{1,2}.\d{1,2}/;
            const PPMCPMregex = /\d{1,4}/g;
            const PhysicalActivityregex = /\d{1,4}.\d{1,2}/;


            for (const historyItem in userHistory.value) {
                let calcType = userHistory.value[historyItem].calculatorType;
                let creationDate = userHistory.value[historyItem].created.slice(0, 10)
                //let creationDate = userData[historyItem].created.slice(0, 10).split('-')
                switch (calcType) {
                    case 0:
                        const BMImatch = userHistory.value[historyItem].result.match(BMIregex);
                        BMIResults.value.push([creationDate,BMImatch]);
                        break;
                    case 1:
                        const WHRmatch = userHistory.value[historyItem].result.match(BMIregex);
                        WHRResults.value.push([creationDate, WHRmatch]);
                        break;
                    case 2:
                        const PPMCPMmatch = userHistory.value[historyItem].result.match(PPMCPMregex);
                        PPMResults.value.push([creationDate, PPMCPMmatch[0]]);
                        CPMResults.value.push([creationDate, PPMCPMmatch[1]]);
                        break;
                    case 3:
                        if (!WeightPlannerResults.value[1]) {
                            WeightPlannerResults.value.push([creationDate, userHistory.value[historyItem].result.match(PPMCPMregex)]);
                            WeightPlannerResults.value[0] = [creationDate, userHistory.value[historyItem].result.match(PPMCPMregex)];
                        }
                        break;
                    case 4:
                        const PhysicalActivitymatch = userHistory.value[historyItem].result.match(PhysicalActivityregex);
                        PhysicalActivityResults.value.push([creationDate, PhysicalActivitymatch]);
                        break;
                    case 5:
                        const FoodCaloriesmatch = userHistory.value[historyItem].result.match(PhysicalActivityregex);
                        FoodCaloriesResults.value.push([creationDate, FoodCaloriesmatch]);
                        break;
                }

                const regex = /\=([^,]+)/;
                const match = userHistory.value[historyItem].result.match(regex);
                if (match) { chartData.value.push(match[1]); }

                const dateValue = userHistory.value[historyItem].created;
                const dayValue = parseDate(dateValue);
                if (dayValue) {labels.value.push(dayValue); }
                //console.log('PRZYKLAD ' + match[1]);

                let calculatorName = calculatorNames[userHistory.value[historyItem].calculatorType];
                let saveDate = parseDate(userHistory.value[historyItem].created) + ', ' + parseHour(userHistory.value[historyItem].created);
                let result = userHistory.value[historyItem].result;
                cleanedHistory.value.push({ 'Data zapisania': saveDate, 'Kalkulator': calculatorName, 'Wynik': result });
            }

            console.log(BMIResults.value);
            console.log(WHRResults.value);
            console.log(PPMResults.value);
            console.log(CPMResults.value);
            console.log(WeightPlannerResults.value);
            console.log(PhysicalActivityResults.value);
            console.log(FoodCaloriesResults.value);
            //console.log(labels);
            let defaultLabels = []
            let defaultData = []
            var currentTime = new Date();
            var year = currentTime.getFullYear();
            var month = currentTime.getMonth() + 1;
            if (month < 10) { month = '0' + month; }
            for (const i in BMIResults.value) {
                if (year + '-' + month == BMIResults.value[i][0].slice(0, 7)) { defaultLabels.unshift(BMIResults.value[i][0]); defaultData.unshift(BMIResults.value[i][1][0]); }
            }
            
            const data = {
                labels: defaultLabels,
                datasets: [{
                    label: 'Kalkulator BMI',
                    backgroundColor: 'rgb(255,99,132)',
                    borderColor: 'rgb(255,99,132)',
                    data: defaultData,
                }]
            };
            const config = {
                type: 'line',
                data: data,
                options: {}
            };
            myChart = new Chart(
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

    function updateMonths() {
        
        let uniqueItem = 1;
        Months.value = [];
        switch (calculatorType.value.value) {
            case 0:
                for (const i in BMIResults.value){
                    uniqueItem = BMIResults.value[i][0].slice(0, 7);
                    if (!Months.value.includes(uniqueItem)) { Months.value.push(uniqueItem); }
                }
                break;
            case 1:
                for (const i in WHRResults.value) {
                    uniqueItem = WHRResults.value[i][0].slice(0, 7);
                    if (!Months.value.includes(uniqueItem)) { Months.value.push(uniqueItem); }
                }
                break;
            case 2:
                for (const i in PPMResults.value) {
                    uniqueItem = PPMResults.value[i][0].slice(0, 7);
                    if (!Months.value.includes(uniqueItem)) { Months.value.push(uniqueItem); }
                }
                break;
            case 3:
                for (const i in CPMResults.value) {
                    uniqueItem = CPMResults.value[i][0].slice(0, 7);
                    if (!Months.value.includes(uniqueItem)) { Months.value.push(uniqueItem); }
                }
                break;
            case 4:
                for (const i in PhysicalActivityResults.value) {
                    uniqueItem = PhysicalActivityResults.value[i][0].slice(0, 7);
                    if (!Months.value.includes(uniqueItem)) { Months.value.push(uniqueItem); }
                }
                break;
            case 5:
                for (const i in FoodCaloriesResults.value) {
                    uniqueItem = FoodCaloriesResults.value[i][0].slice(0, 7);
                    if (!Months.value.includes(uniqueItem)) { Months.value.push(uniqueItem); }
                }
                break;
        }
    }

    function updateChart() {
        console.log('dane wykresu',myChart);
        let newLabels = []
        let newData = []

        switch (calculatorType.value.value) {
            case 0:
                for (const i in BMIResults.value) {
                    if (selectedMonth.value.value == BMIResults.value[i][0].slice(0, 7)) { newLabels.unshift(BMIResults.value[i][0]); newData.unshift(BMIResults.value[i][1][0]); }
                }
                break;
            case 1:
                for (const i in WHRResults.value) {
                    if (selectedMonth.value.value == WHRResults.value[i][0].slice(0, 7)) { newLabels.unshift(WHRResults.value[i][0]); newData.unshift(WHRResults.value[i][1][0]); }
                }
                break;
            case 2:
                for (const i in PPMResults.value) {
                    if (selectedMonth.value.value == PPMResults.value[i][0].slice(0, 7)) { newLabels.unshift(PPMResults.value[i][0]); newData.unshift(PPMResults.value[i][1]); }
                }
                break;
            case 3:
                for (const i in CPMResults.value) {
                    if (selectedMonth.value.value == CPMResults.value[i][0].slice(0, 7)) { newLabels.unshift(CPMResults.value[i][0]); newData.unshift(CPMResults.value[i][1]); }
                }
                break;
            case 4:
                for (const i in PhysicalActivityResults.value) {
                    if (selectedMonth.value.value == PhysicalActivityResults.value[i][0].slice(0, 7)) { newLabels.unshift(PhysicalActivityResults.value[i][0]); newData.unshift(PhysicalActivityResults.value[i][1][0]); }
                }
                break;
            case 5:
                for (const i in FoodCaloriesResults.value) {
                    if (selectedMonth.value.value == FoodCaloriesResults.value[i][0].slice(0, 7)) { newLabels.unshift(FoodCaloriesResults.value[i][0]); newData.unshift(FoodCaloriesResults.value[i][1][0]); }
                }
                break;
        }

        console.log(newLabels, newData);
        function getCalculatorName(calculator) {
            return calculator.value === calculatorType.value.value;
        }
        myChart.config.data.datasets[0].data = newData;
        //myChart.config.data.datasets.label = calculators.value.find(getCalculatorName).name;
        myChart.config.data.labels = newLabels;
        myChart.config.data.datasets[0].label = calculators.value.find(getCalculatorName).name;
        myChart.update();
        
    }
    

</script>

<template>
    
    <v-card class="mx-auto my-16 w-50" min-width="400" max-width="1000">
        <v-card-title class="text-center text-h4 ma-4 font-weight-bold">
            Raport
        </v-card-title>
        <v-card-text>
            <div class="text-h5 ma-4 font-weight-bold" align="left">Wizualizacja</div>
            <div class="d-flex justify-space-between ma-2 ">
                        <v-select v-model="calculatorType.value.value"
                                  :items="calculators"
                                  item-title="name"
                                  item-value="value"
                                  variant="outlined"
                                  @update:modelValue="updateMonths"
                                  label="BMI"
                                  ></v-select>
                   
                        <v-select v-model="selectedMonth.value.value"
                                  :items="Months"
                                  variant="outlined"
                                  ></v-select>
                    
                        <v-btn @click="updateChart"
                               color="success"
                               height="55px">
                            Zaktualizuj
                        </v-btn>
            </div>
                <canvas id="myChart"></canvas>


            <div class="text-h5 ma-4 font-weight-bold" align="left">Długoterminowy cel</div>

            
            <v-card variant="outlined">
                <div class="ma-4">
                    <div class="text-h6 mb-1">
                        Na podstawie ostatniego zapisanego wyniku należy spożywać:
                    </div>
                        {{WeightPlannerResults[0][1][3]}} kcal aby osiągnąć docelowe {{WeightPlannerResults[0][1][2]}} kg. <br />
                        {{WeightPlannerResults[0][1][4]}} kcal aby utrzymać docelowe {{WeightPlannerResults[0][1][2]}} kg. <br />
                        {{WeightPlannerResults[0][1][1]}} kcal aby utrzymać obecną wagę {{WeightPlannerResults[0][1][0]}} kg.
                </div>
            </v-card>
               

            <div class="text-h5 ma-4 font-weight-bold" align="left">Historia</div>

            <v-data-table :items="cleanedHistory"></v-data-table>

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