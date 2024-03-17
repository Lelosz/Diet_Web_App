<script setup>
    import { ref, onMounted } from 'vue'
    import { useRouter } from 'vue-router'
    import store from '@/store/index.js'

    const items = ref([])
    const authTokenValue = ref(0)
    const PostList = ref([])
    const li = ref([])
    const isAdmin = ref(0)

    const page = ref(1);
    const pageLength = ref(5);
    const pageNumber = ref(1);


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
        let postCreationTime;
        for (const i in PostList.value) {
            postCreationTime = parseDate(PostList.value[i].created);
            if (PostList.value[i].archivized) {
                adminPostList.push({ id: PostList.value[i].id, title: PostList.value[i].title, content: PostList.value[i].postContent.slice(0, 201) + ' ...', dateAdded: postCreationTime});
            } else {
                userPostList.push({ id: PostList.value[i].id, title: PostList.value[i].title, content: PostList.value[i].postContent.slice(0, 201) + ' ...', dateAdded: postCreationTime });
                adminPostList.push({ id: PostList.value[i].id, title: PostList.value[i].title, content: PostList.value[i].postContent.slice(0, 201) + ' ...', dateAdded: postCreationTime });
            }
        }
        if (isAdmin.value) { li.value = adminPostList; } else { li.value = userPostList; }
        
        pageNumber.value = Math.ceil(li.value.length / 5);
        console.log('po przetworzeniu', li.value)

        return "Dane logowania są niepoprawne";
    });

    function parseDate(value) {
        let parsedDate = new Date(value);
        let day = parsedDate.getDate();
        if (day < 10) { day = '0' + day }
        let month = parsedDate.getMonth() + 1;
        if (month < 10) { month = '0' + month }
        let year = parsedDate.getFullYear();

        let hour = parsedDate.getHours();
        if (hour < 10) { hour = '0' + hour }
        let minutes = parsedDate.getMinutes();
        if (minutes < 10) { minutes = '0' + minutes }

        return year + '-' + month + '-' + day + ', ' + hour + ':' + minutes;
    }

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

                    <div class="text-center ma-6">
                        <router-link to="/Blog/CreateBlogPost" v-if="isAdmin">
                            <v-btn class="font-weight-bold" variant="outlined" color="orange">
                                Stwórz wpis
                            </v-btn>
                        </router-link>
                    </div>
                </div>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-divider></v-divider>
                <v-container>
                    <v-row>
                        <v-col v-for="item in li.slice((page - 1) * pageLength,Math.min((page - 1) * pageLength + pageLength - 1, li.length - 1)+1)"
                               :key="i"
                               cols="12">
                            <v-card class="mx-auto"
                                    variant="outlined">
                                <v-card-item>
                                    <div class="ma-2">
                                        <div class="text-h5 mb-1">
                                            {{item.title}}
                                        </div>
                                        <div class="text-subtitle-1 mb-4">
                                            {{item.dateAdded}}
                                        </div>
                                        <div v-html="item.content" style="white-space: pre-wrap;">
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
                    <div class="mt-6">
                        <v-pagination v-model="page"
                                      :length="pageNumber"></v-pagination>
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