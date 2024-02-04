<script setup>
    import { ref, onMounted } from 'vue'
    const items = ref(
        [
            { name: 'BMI', description: 'Wskaźnik BMI inaczej Body Mass Index jest najprostszą i najpowszechniejszą stosowaną metodą oceny stopnia otyłości.', link: 'BMI' },
            { name: 'WHR', description: 'Wskaźnik WHR inaczej Waist-Hip Ratio pozwala na rozróżnienie rodzaju otyłości pomiędzy otyłością pośladkowo-udową (typu gruszka), a brzuszną (typu jabłko).', link: 'WHR' },
            { name: 'PPM i CPM', description: 'Podstawowe i całkowite zapotrzebowanie energetyczne służy do określenia ile kalori spalamy kolejno nie biorąc pod uwagę aktywności fizycznej i biorąc ją pod uwagę w przeciągu doby.', link: 'PPMCPM' },
            { name: 'Planer wagi', description: 'Na podstawie danych o budowie ciała w połączeniu z poziomem aktywności fizycznej pozwala na planowanie i rozeznanie się w zapotrzebowaniu kalorycznym.', link: 'WeightPlanner' },
            { name: 'Aktywność fizyczna', description: 'Pozwala określić ilość spalonych kalori wskutek aktywności fizycznej odbytej przez użytkownika w określonym przedziale czasu.', link: 'PhisicalActivity' },
            { name: 'Kalorie z przekąsek i napojów', description: 'Pozwala określić ile przyjęliśmy kalorii z jedzenia lub picia i porównuje je do czasu trwania danej aktywności w celu spalenia tych kalorii.', link: 'FoodCalories' }
        ]
    )
</script>

<template>
    <div class="ma-12 pa-4 ">
        <v-card class="mx-auto" variant="outlined" >
            <div class="text-center ma-6">
                <h1>Lista kalkulatorów</h1>
            </div> 
            <v-container>
                <v-row>
                    <v-col v-for="item in items"
                           :key="i"
                           cols="6">
                        <v-card class="mx-auto"
                                variant="outlined">
                            <v-card-item>
                                <div class="ma-2">
                                    <!--<div class="text-overline mb-1">
                                {{ name }}
                            </div>-->
                                    <div class="text-h6 mb-1">
                                        {{item.name}}
                                    </div>
                                    <div>
                                        {{item.description}}
                                    </div>
                                    <div class="mt-3">
                                        <router-link :to="'/CalculatorList/' + item.link">
                                            <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3">
                                                Wypróbuj
                                            </v-btn>
                                        </router-link>
                                    </div>
                                </div>
                            </v-card-item>
                            <!--<v-card-actions>
                                <router-link :to="'/CalculatorList/' + item.link">
                                    <v-btn class="font-weight-bold" color="success">
                                        Wypróbuj
                                    </v-btn>
                                </router-link>
                            </v-card-actions>-->
                        </v-card>
                    </v-col>
                </v-row>
            </v-container>
        </v-card>
    </div>
</template>

<style scoped>
      div {
        font-weight: bold;
      }
</style>
