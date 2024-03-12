<script setup>
    import { ref, onMounted } from 'vue'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'

    const items = ref([])
    const authTokenValue = ref(0)
    const PostList = ref([])
    const li = ref([])
    const isAdmin = ref(0)

    if (store.state.userRole == 'Administrator') { isAdmin.value = 1; }

    onMounted(async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value;


        if (store.state.userId != null) {
            let response = await fetch('https://localhost:7011/api/BlogPost/PostList', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include'

            });
            let resp = await response.json()
            PostList.value = resp.slice().reverse()

        }
        console.log("lista wpisow", PostList.value);
        let userPostList = [];
        let adminPostList = [];
        for (const i in PostList.value) {
            if (PostList.value[i].archivized) {
                adminPostList.push({id: PostList.value[i].id, title: PostList.value[i].title, content: PostList.value[i].postContent.slice(0, 201) });
            } else {
                userPostList.push({ id: PostList.value[i].id, title: PostList.value[i].title, content: PostList.value[i].postContent.slice(0, 201) });
                adminPostList.push({ id: PostList.value[i].id, title: PostList.value[i].title, content: PostList.value[i].postContent.slice(0, 201) });
            }
        }
        if (isAdmin.value) { li.value = adminPostList; } else { li.value = userPostList; }
        

        console.log('po przetworzeniu', li.value)

        return "Dane logowania są niepoprawne";
    });

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
                    <router-link to="/Blog/CreateBlogPost" v-if="isAdmin">
                        <v-btn class="font-weight-bold" variant="outlined" color="orange">
                            Stwórz wpis
                        </v-btn>
                    </router-link>
                </div>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-container>
                    <v-row>
                        <v-col v-for="item in li"
                               :key="i"
                               cols="12">
                            <v-card class="mx-auto"
                                    variant="outlined">
                                <v-card-item>
                                    <div class="ma-2">
                                        <div class="text-h6 mb-1">
                                            {{item.title}}
                                        </div>
                                        <div>
                                            {{item.content}} ...
                                        </div>
                                        <div class="mt-3">
                                            <router-link :to="{ name: 'BlogPost', params: { postId: item.id }}">
                                                <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3">
                                                    Czytaj dalej
                                                </v-btn>
                                            </router-link>
                                            <router-link :to="{ name: 'EditBlogPost', params: { postId: item.id }}" v-if="isAdmin">
                                                <v-btn class="font-weight-bold" variant="outlined" color="orange">
                                                    Edytuj wpis
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