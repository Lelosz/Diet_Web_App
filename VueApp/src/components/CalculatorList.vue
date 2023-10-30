
<template>
  <v-container>
    <v-row>
      <v-col
        v-for="(name, i) in names"
        :key="i"
        cols="4"
      >
        <v-card
          class="mx-auto"
          :variant="variant">
          <v-card-item>
            <div>
              <div class="text-overline mb-1">
                {{ name }}
              </div>
              <div class="text-h6 mb-1">
                {{ descriptions[i] }}
              </div>
              <div class="text-caption"> {{ contents[i] }} </div>
            </div>
          </v-card-item>
          <v-card-actions>
            <v-btn>
              Skorzystaj
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
  const names = ['bmi', 'asd', 'dsad', 'sdad', 'sads', 'sadsad', 'ased22']
  const descriptions = ['Desc1', 'Desc2', 'Desc3', 'Desc4', 'Desc5', 'Desc6', 'Desc7']
  const contents = ['Content1', 'Content2', 'Content3', 'Content4', 'Content5', 'Content6', 'Content7']
</script>
<style scoped>
      div {
        font-weight: bold;
      }
</style>
