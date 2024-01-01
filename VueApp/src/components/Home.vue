<script setup>
    import { onMounted, ref } from 'vue'
    import store from '@/store/index.js'
    import VueCookies from 'vue-cookies'
   
    const message = ref('')
    
    const authTokenValue = ref('')
    onMounted(async () => {
            
        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value
        let response = await fetch('https://localhost:7011/api/user/user', {
            method: 'GET',
            mode: 'cors',
            headers: { 'Content-Type': 'application/json', 'Authorization': token },
            credentials: 'include'
        });
        let userData = await response.json()
        console.log(userData)
        if (userData) {
            store.commit('loginStatus', true)
        } else {
            store.commit('loginStatus', false)
        }
        
    });

</script>

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


