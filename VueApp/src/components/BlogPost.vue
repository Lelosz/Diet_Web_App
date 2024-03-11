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
    const allComments = ref(
        [
            { userName: 'Maciek1', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
            { userName: 'Magda2', comment: 'Bzdury gadasz.', date: '5.03.2023' },
            { userName: 'Piotr3', comment: 'Nie no, ma racje', date: '1.01.2024' },
            { userName: 'Maciek4', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
            { userName: 'Magda5', comment: 'Bzdury gadasz.', date: '5.03.2023' },
            { userName: 'Piotr6', comment: 'Nie no, ma racje', date: '1.01.2024' },
            { userName: 'Maciek7', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
            { userName: 'Magda8', comment: 'Bzdury gadasz.', date: '5.03.2023' },
            { userName: 'Piotr9', comment: 'Nie no, ma racje', date: '1.01.2024' },
            { userName: 'Maciek10', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
            { userName: 'Magda11', comment: 'Bzdury gadasz.', date: '5.03.2023' },
            { userName: 'Piotr12', comment: 'Nie no, ma racje', date: '1.01.2024' },
            { userName: 'Maciek13', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
            { userName: 'Magda14', comment: 'Bzdury gadasz.', date: '5.03.2023' },
            { userName: 'Piotr15', comment: 'Nie no, ma racje', date: '1.01.2024' },
            { userName: 'Maciek16', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
            { userName: 'Magda17', comment: 'Bzdury gadasz.', date: '5.03.2023' },
            { userName: 'Piotr18', comment: 'Nie no, ma racje', date: '1.01.2024' },
        ]
    )
    const page = ref(1);
    const pageLength = ref(Math.ceil(allComments.value.length / 5));
    const commentsSection = ref([{ userName: 'Maciek', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },])
    const postContent = "Suboptimal diet is an important preventable risk factor for non-communicable diseases (NCDs); however, its impact on the burden of NCDs has not been systematically evaluated. This study aimed to evaluate the consumption of major foods and nutrients across 195 countries and to quantify the impact of their"
</script>

<template>
    <v-img aspect-ratio="16/9"
           cover
           src="/src/assets/blog_banner2.jpg">

        <v-card class="mx-auto align-center justify-center" max-width="1000" color="rgb(0, 0, 0, 0)" height="10vh">
            
        </v-card>
        <v-card class="mx-auto align-center justify-center" max-width="1000">
            <v-card-item>
                <div class="text-h3 ma-6 text-center">
                    Health effects of dietary risks in 195 countries {{$route.params.postId}}
                </div>
                <div class="text-h7 ma-6 text-center">
                    Autor: Jacek Sezamowski, Dnia: 20.1.2024
                    <br /><br />


                </div>    
                <div>
                    <v-container>
                        <v-row>
                            <v-col cols="12">
                                <v-card class="mx-auto"
                                        variant="outlined"
                                        >
                                    <v-card-item>
                                        <div class="ma-2">
                                            {{postContent}}
                                        </div>
                                    </v-card-item>
                                </v-card>
                            </v-col>
                        </v-row>
                    </v-container>
                </div>
            </v-card-item>
            <v-card-item>
                <div class="text-h4 ma-6 text-center">
                    Komentarze
                </div>
                <div class="ma-6">
                    <v-textarea rows="5" auto-grow bg-color="grey-lighten-2" color="black" label="Wpisz swój komentarz"></v-textarea>
                    <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3" @click="">
                        Dodaj
                    </v-btn>
                </div>
                <v-container>
                    <v-row>
                        <!--rgb(0, 0, 0, 0.2)-->
                        <v-col v-for="comment in allComments.slice((page - 1) * (pageLength+1), Math.min((page - 1) * (pageLength+1) + (pageLength+1) - 1, allComments.length - 1)+1 )"
                               :key="i"
                               cols="12">
                            <v-card class="mx-auto"
                                    variant="outlined">
                                <v-card-item>
                                    <div class="ma-2">
                                        {{comment.userName}}, {{comment.date}}
                                        <div class="text-h6 mb-1">
                                            {{ comment.comment }}
                                        </div>

                                        <div class="mt-3">
                                            <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-thumb-up-outline">
                                                5
                                            </v-btn>
                                            <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-thumb-down-outline">
                                                2
                                            </v-btn>
                                        </div>
                                    </div>
                                </v-card-item>
                            </v-card>
                        </v-col>
                    </v-row>
                    <div class="mt-6">
                        <v-pagination v-model="page"
                                      :length="pageLength"></v-pagination>
                    </div>
                </v-container>
            </v-card-item>
        </v-card>
    </v-img>
    <v-locale-provider locale="pl">

    </v-locale-provider>
</template>

<style>
</style>