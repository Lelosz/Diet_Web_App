<script setup>
    import { ref, onMounted } from 'vue'
    const items = ref(
        [
            { name: 'Health effects of dietary risks in 195 countries, 1990–2017', description: 'Suboptimal diet is an important preventable risk factor for non-communicable diseases (NCDs); however, its impact on the burden of NCDs has not been systematically evaluated. This study aimed to evaluate the consumption of major foods and nutrients across 195 countries and to quantify the impact of their...', link: 'BMI' },
            { name: 'Dementia prevention, intervention, and care', description: 'Executive summary The number of older people, including those living with dementia, is rising, as younger age mortality declines. However, the age-specific incidence of dementia has fallen in many countries, probably because of improve...', link: 'WHR' },
            { name: 'Social Relationships and Mortality Risk', description: 'Social relationships, or the relative lack thereof, constitute a major risk factor for health—rivaling the effect of well established health risk factors such as cigarette smoking, blood pressure, blood lipids, obesity and physical...', link: 'PPMCPM' },
            { name: 'Ultra-Processed Diets Cause Excess Calorie Intake and Weight Gain', description: 'We investigated whether ultra-processed foods affect energy intake in 20 weight-stable adults, aged (mean ± SE) 31.2 ± 1.6 years and BMI = 27 ± 1.5 kg/m2. Subjects were admitted to the NIH Clinical Center and randomized to receive either ultra-processed or unprocessed diets for 2 weeks...', link: 'WeightPlanner' },
            { name: 'Dietary carbohydrate intake and mortality', description: 'Summary Background Low carbohydrate diets, which restrict carbohydrate in favour of increased protein or fat intake, or both, are a popular weight-loss strategy. However, the long-term effect of carbohydrate restriction on mortality is...', link: 'PhisicalActivity' },
            { name: 'Artificial sweeteners induce glucose intolerance by altering the gut microbiota', description: 'Non-caloric artificial sweeteners (NAS) are among the most widely used food additives worldwide, regularly consumed by lean and obese individuals alike. NAS consumption is considered safe and beneficial owing to their low caloric content, yet supporting scientific data remain sparse and...', link: 'FoodCalories' }
        ]
    )


</script>

<template>
    <v-img aspect-ratio="16/9"
           cover
           src="/src/assets/blog_banner2.jpg">
       
        <v-card class="mx-auto align-center justify-center" max-width="1000" color="rgb(0, 0, 0, 0.2)">
            <div class="text-h2 ma-6 text-center ">
                Blog
            </div>
            <div class="text-h3 ma-6 text-center ">
                Przeglądaj spośród wielu artykułów
            </div>
        </v-card>
        <v-card class="mx-auto align-center justify-center" max-width="1000">
            <v-card-item>
                <div class="text-h4 ma-6">
                    <v-form width="600">
                        <v-text-field label="Wpisz nazwę interesującego cię artykułu"></v-text-field>
                        <div class="text-center">
                            <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3">Szukaj</v-btn>

                        </div>
                    </v-form>
                </div>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-container>
                    <v-row>
                        <v-col v-for="item in items"
                               :key="i"
                               cols="12">
                            <v-card class="mx-auto"
                                    variant="outlined">
                                <v-card-item>
                                    <div class="ma-2">
                                        {{ name }}
                                        <div class="text-h6 mb-1">
                                            {{item.name}}
                                        </div>
                                        <div>
                                            {{item.description}}
                                        </div>
                                        <div class="mt-3">
                                            <router-link :to="'/CalculatorList/' + item.link">
                                                <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3">
                                                    Czytaj dalej
                                                </v-btn>
                                            </router-link>
                                        </div>
                                    </div>
                                </v-card-item>
                            </v-card>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card-item>

        </v-card>
    </v-img>
    <v-locale-provider locale="pl">
        
    </v-locale-provider>
</template>

<style>
   

</style>