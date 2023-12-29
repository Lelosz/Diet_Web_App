<script setup>
    import { ref, onMounted } from 'vue'
    const items = ref(
        [
            { name: 'BMI', description: 'Opis1', link: 'BMI' },
            { name: 'WHR', description: 'Opis2', link: 'WHR' },
            { name: 'PPM i CPM', description: 'Opis3', link: 'PPMCPM' },
            { name: 'Planer wagi', description: 'Opis4', link: 'WeightPlanner' },
            { name: 'Aktywność fizyczna', description: 'Opis5', link: 'PhisicalActivity' },
            { name: 'Kalorie z przekąsek i napojów', description: 'Opis6', link: 'FoodCalories' }
        ]
    )
</script>

<template>
  <v-container>
    <v-row>
      <v-col
        v-for="item in items"
        :key="i"
        cols="6"
      >
        <v-card
          class="mx-auto"
          :variant="variant">
          <v-card-item>
            <div>
              <!--<div class="text-overline mb-1">
                {{ name }}
              </div>-->
              <div class="text-h6 mb-1">
                  {{item.name}}
              </div>
              <div class="text-caption">
                {{item.description}}
              </div>
            </div>
          </v-card-item>
          <v-card-actions>
              <router-link :to="'/CalculatorList/' + item.link">
                <v-btn class="font-weight-bold" color="green">
                  Wypróbuj
                </v-btn>
              </router-link>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
      div {
        font-weight: bold;
      }
</style>
