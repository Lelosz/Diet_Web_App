<script setup>
    import { onMounted, ref } from 'vue'
    import store from '@/store/index.js'
    import VueCookies from 'vue-cookies'
   
    const message = ref('')
    
    const authTokenValue = ref(0)
    onMounted(async () => {
            
        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }

        if (authTokenValue.value) {
            console.log(authTokenValue.value)
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
                store.commit('userId', userData.id)
                store.commit('userRole', userData.role)
                store.commit('userName', userData.username)
                console.log(userData.username)
            } else {
                store.commit('loginStatus', false)
            }

        }
        
    });

</script>

<template>
    <!--<v-img
           aspect-ratio="16/9"
           cover
           height="400"
           src="/src/assets/homepage_banner.jpg">

    </v-img>-->

    <v-locale-provider locale="pl">
        
        <v-parallax src="/src/assets/homepage_banner.jpg" >
            <div class="mt-12 pa-12 flex-column fill-height justify-center align-center text-white">
                <v-card class="mx-auto align-center justify-center" max-width="1000" color="rgb(0, 0, 0, 0.3)">
                    <div class="text-h3 ma-6 text-center">
                        Zapoznaj się z naszą ofertą!
                    </div>
                </v-card>
                <v-card class="mx-auto fill-height align-center justify-center "
                        max-width="1000"
                        :color="green"
                        :variant="primary">
                    <!--<v-card-item>
                        <div class="text-h4 ma-6">
                            Zapoznaj się z naszą ofertą!
                        </div>
                    </v-card-item>-->
                    <v-divider></v-divider>
                    <v-divider></v-divider>
                    <v-card-item>
                        <div class="text-h5 ma-6">
                            Wypróbuj nasz zbiór kalkulatorów
                        </div>
                        <div class=" ma-6">
                            Znajdziesz u nas takie kalkulatory jak:

                            <v-list>
                                <v-list-item prepend-icon="mdi-circle-small">
                                    <v-list-item-content>
                                        <v-list-item-title>BMI (Body mass index)</v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                                <v-list-item prepend-icon="mdi-circle-small">
                                    <v-list-item-content>
                                        <v-list-item-title>WHR (Waist-hip ratio)</v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                                <v-list-item prepend-icon="mdi-circle-small">
                                    <v-list-item-content>
                                        <v-list-item-title>PPM (Podstawowej przemiany materii)</v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                                <v-list-item prepend-icon="mdi-circle-small">
                                    <v-list-item-content>
                                        <v-list-item-title>CPM (Całkowitej przemiany materii)</v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                                <v-list-item prepend-icon="mdi-circle-small">
                                    <v-list-item-content>
                                        <v-list-item-title>Kalorii spalonych z wykorzystaniem wybranych ćwiczeń</v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                                <v-list-item prepend-icon="mdi-circle-small">
                                    <v-list-item-content>
                                        <v-list-item-title>Ilości wysiłku, który musisz włożyć aby spalic kalorie z wybranego jedzenia czy napojów</v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                                <v-list-item prepend-icon="mdi-circle-small">
                                    <v-list-item-content>
                                        <v-list-item-title>A także złożony kalkulator do planowania długoterminowego zrzucania wagi</v-list-item-title>
                                    </v-list-item-content>
                                </v-list-item>
                            </v-list>
                            <div class="text-center">
                                <p class="ma-5 text-h6">Jeśli zostałeś zaciekawiony kliknij w przycisk poniżej!</p>
                                <router-link style="text-decoration: none; color: inherit;" to="/CalculatorList">
                                    <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3">
                                        Sprawdź kalkulatory
                                    </v-btn>
                                </router-link>
                            </div>

                        </div>
                    </v-card-item>
                    <v-divider></v-divider>

                    <v-card-item>
                        <div class="text-h5 ma-6">
                            Sprawdz wyniki w raporcie
                        </div>
                        <div class="ma-6">
                            Jeśli dokonałeś obliczeń i zdążyłeś juz zapisać pare wyników sprawdź koniecznie funkcjonalność raportu. Wystarczy że zalogujesz się i przejdziesz z paska nawigacyjnego do swojego raportu.
                        </div>
                    </v-card-item>
                    <v-divider></v-divider>
                    <v-card-item>
                        <div class="text-h5 ma-6">
                            Jeśli szukasz dodatkowych informacji obczaj koniecznie naszego bloga
                        </div>
                        <div class="ma-6">
                            Poruszamy na nim wiele ciekawych tematów związanych ze zdrowiem i odżywianiem.
                            <div class="text-center">
                                <p class="ma-5 text-h6">Zaciekawiony? Kliknij w przycisk poniżej!</p>
                                <router-link style="text-decoration: none; color: inherit;" to="/CalculatorList">
                                    <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3">
                                        Sprawdź bloga
                                    </v-btn>
                                </router-link>
                            </div>
                        </div>
                    </v-card-item>
                    <v-divider></v-divider>
                    <v-card-item>
                        <div class="text-h5 ma-6">
                            Stwórz konto, jeśli jeszcze tego nie zrobiłeś
                        </div>
                        <div class="ma-6">
                            Umożliwi ci to zapisanie swoich wyników do raportu i ich późniejszą analize, a także komentowanie na blogu.
                            <div class="text-center">
                                <p class="ma-5 text-h6">Nie trać czasu i kliknij w przycisk poniżej!</p>
                                <router-link style="text-decoration: none; color: inherit;" to="/SignUp">
                                    <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3">
                                        Zarejestruj się
                                    </v-btn>
                                </router-link>
                            </div>
                        </div>
                    </v-card-item>

                    <v-divider></v-divider>
                    <v-divider></v-divider>
                </v-card>
            </div>
        </v-parallax>
            
    </v-locale-provider>
</template>


