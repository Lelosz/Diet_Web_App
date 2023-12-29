<template>
    <v-img
           aspect-ratio="16/9"
           cover
           height="400"
           src="/src/assets/homepage_banner.jpg">

    </v-img>
    <v-container>
        <v-locale-provider locale="pl">

            <v-card class="mx-auto"
                    :color="green"
                    :variant="primary">
                <v-card-item>
                    <div class="text-h6 mb-1">
                        Wypróbuj nasz zbiór kalkulatorów!
                    </div>
                    <div class="text-caption">
                        Np BMI albo WHR
                    </div>
                </v-card-item>
                <v-card-item>
                    <div class="text-h6 mb-1">
                        Stwórz konto!
                    </div>
                    <div class="text-caption">
                        Aby zapisać swoje wyniki oraz udzielać się na blogu!
                    </div>
                </v-card-item>
                <v-card-item>
                    <div class="text-h6 mb-1">
                        Sprawdz wyniki w raporcie!
                    </div>
                    <div class="text-caption">
                        Wystarczy, że wejdziesz na swój profil
                    </div>
                </v-card-item>
                <v-card-item>
                    <div class="text-h6 mb-1">
                        Jeśli szukasz dodatkowych informacji obczaj naszego bloga
                    </div>
                    <div class="text-caption">
                        Poruszamy na nim ...
                    </div>
                </v-card-item>
            </v-card>
        </v-locale-provider>
    </v-container>
</template>

<script setup>


</script>
<!--<template>
  <HelloWorld />
</template>

<script setup>
  import HelloWorld from '@/components/HelloWorld.vue'
</script>-->
