<script setup>
    import { ref, onMounted } from 'vue'
    import { useRouter, useRoute } from 'vue-router'
    import store from '@/store/index.js'

    const router = useRouter()
    const postTitle = ref('');
    const postContent = ref('');
    const authTokenValue = ref(0)
    const route = useRoute();
    const postData = ref('');
    const archivized = ref(false);

    onMounted(async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value;


        if (store.state.userId != null) {
            let response = await fetch('https://localhost:7011/api/BlogPost/' + route.params.postId, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include'

            });
            let resp = await response.json()
            postData.value = resp




        }
        console.log("wpisy", postData.value);

        
        postTitle.value = postData.value.title;
        postContent.value = postData.value.postContent;
        archivized.value = postData.value.archivized;
        
        return "Dane logowania są niepoprawne";
    });






    const saveResult = async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/BlogPost/' + route.params.postId , {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    "id": route.params.postId,
                    "author": store.state.userName,
                    "title": postTitle.value,
                    "postContent": postContent.value,
                    "created": new Date(),
                    "archivized": archivized.value,
                    "userId": store.state.userId
                })
            }).then((response) => {
                if (response.ok) {
                    router.back();
                }
                return Promise.reject(response);
            })
                .then((result) => {
                    console.log(result);
                })
                .catch((error) => {
                    console.log('Something went wrong.', error);
                });

        }




        return "Dane logowania są niepoprawne"
    }

</script>

<template>


    <v-card class="mx-auto align-center justify-center" max-width="1000" color="rgb(0, 0, 0, 0)" height="10vh">

    </v-card>
    <v-card class="mx-auto align-center justify-center" max-width="1000">
        <v-card-item>
            <div class="text-h4 ma-6 text-center">
                Edytuj wpis
            </div>
            <div class="text-h4 ma-6">
                <v-form width="600">
                    <v-text-field v-model="postTitle" variant="outlined" label="Tytuł"></v-text-field>
                    <v-textarea v-model="postContent" variant="outlined" rows="10" auto-grow label="Treść"></v-textarea>

                    <v-checkbox v-model="archivized" @click="archivized = !archivized">
                        <template v-slot:label>
                            <div>
                                Zarchiwizuj
                            </div>
                        </template>
                    </v-checkbox>
                    <v-row>
                        <v-col>
                            <v-btn class="font-weight-bold" variant="outlined" color="red" @click="$router.back()">
                                Wstecz
                            </v-btn>
                        </v-col>
                        <v-col class="text-right">
                            <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3" @click="saveResult">
                                Zapisz
                            </v-btn>
                        </v-col>
                    </v-row>


                </v-form>
            </div>


        </v-card-item>

    </v-card>
    <v-locale-provider locale="pl">

    </v-locale-provider>
</template>

<style>
</style>