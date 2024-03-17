<script setup>
    import { ref, onMounted } from 'vue'
    import { useRoute } from 'vue-router'
    import store from '@/store/index.js'


    const page = ref(1);
    const pageNumber = ref(1);
    const pageLength = ref(5);
    const route = useRoute();
    const authTokenValue = ref(0)
    const postData = ref('')
    const postPublishedDate = ref('')
    const postTitle = ref('')
    const postContent = ref('')
    const postAuthor = ref('')
    const commentContent = ref('')
    const commentsData = ref('')
    const allComments = ref([])
    const allReplies = ref([])
    //{ { $route.params.postId } }

     
    onMounted(async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value;


        if (store.state.userId != null) {
            let response = await fetch('https://localhost:7011/api/BlogPost/' +  route.params.postId  , {
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

        postPublishedDate.value = parseDate(postData.value.created);
        postTitle.value = postData.value.title;
        postContent.value = postData.value.postContent;
        postAuthor.value = postData.value.author
        console.log('title', postContent.value)

        getComments();

        return "Dane logowania są niepoprawne";
    });


    function parseDate (value) {
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

    const getComments = async () => {
        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }

        const token = 'Bearer ' + authTokenValue.value;
        let responseComments = await fetch('https://localhost:7011/api/BlogComment/' + route.params.postId + '/comments', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json', 'Authorization': token
            },
            credentials: 'include'

        });
        let respComments = await responseComments.json()
        commentsData.value = respComments

        
        let allCommentsList = [];
        for (const c in commentsData.value) {

            let date = parseDate(commentsData.value[c].created);
            let responseReply = await fetch('https://localhost:7011/api/BlogReply/' + commentsData.value[c].id + '/replies', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include'

            });
            let respReply = await responseReply.json();

            for (const r in respReply) {
                respReply[r].createdDate = parseDate(respReply[r].createdDate)
            }
            respReply.reverse();
            allCommentsList.push({ id: commentsData.value[c].id, author: commentsData.value[c].author, date: date, content: commentsData.value[c].commentContent, likes: commentsData.value[c].totalLikes, dislikes: commentsData.value[c].totalDislikes, replies: respReply, isReplying: false })
        }
        allComments.value = allCommentsList;
        allComments.value.reverse();
        pageNumber.value = Math.ceil(allComments.value.length / 5);
        console.log('dlugosc',pageLength.value)
    }


    async function likeComment(commentId) {
        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }

        const token = 'Bearer ' + authTokenValue.value;

        
        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/BlogComment/' + commentId + '/like', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({})
            }).then((result) => {
                console.log(result);
            }).catch((error) => {
                console.log('Something went wrong.', error);
            });
        }

        getComments();

    }


    async function dislikeComment(commentId) {
        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }

        const token = 'Bearer ' + authTokenValue.value;


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/BlogComment/' + commentId + '/dislike', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({})
            }).then((result) => {
                console.log(result);
            }).catch((error) => {
                console.log('Something went wrong.', error);
            });
        }

        getComments();
    }


    const postComment = async () => {

        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value
        

        if (store.state.userId != null && commentContent.value != '') {
            await fetch('https://localhost:7011/api/BlogComment/' + route.params.postId +'/comments' , {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    "author": store.state.userName,
                    "commentContent": commentContent.value,
                    "created": new Date(),
                    "totalLikes": 0,
                    "totalDislikes": 0,
                    "likedByUser": false,
                    "dislikedByUser": false,
                    "userId": store.state.userId
                })
            }).then((result) => {
                    console.log(result);
                })
                .catch((error) => {
                    console.log('Something went wrong.', error);
                });

            commentContent.value = '';
            getComments();
        }




        return "Komentarz nie może zostać opublikowany"
    }

    async function postReply(commentId, replyContent) {
        console.log('commentid', commentId)
        console.log('commentid', replyContent)
        try {
            authTokenValue.value = document.cookie.split(';').find(cookie => cookie.startsWith('token=')).split('=')[1];


        } catch (error) {
            console.log('brak tokenu')
        }
        const token = 'Bearer ' + authTokenValue.value


        if (store.state.userId != null) {
            await fetch('https://localhost:7011/api/BlogReply/' + commentId +'/replies', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json', 'Authorization': token
                },
                credentials: 'include',
                body: JSON.stringify({
                    "author": store.state.userName,
                    "replyContent": replyContent, 
                    "createdDate": new Date()
                })
            }).then((result) => {
                console.log(result);
            })
                .catch((error) => {
                    console.log('Something went wrong.', error);
                });

            
            getComments();
        }




        return "Komentarz nie może zostać opublikowany"
    }




    //const allComments = ref(
    //    [
    //        { userName: 'Maciek1', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
    //        { userName: 'Magda2', comment: 'Bzdury gadasz.', date: '5.03.2023' },
    //        { userName: 'Piotr3', comment: 'Nie no, ma racje', date: '1.01.2024' },
    //        { userName: 'Maciek4', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
    //        { userName: 'Magda5', comment: 'Bzdury gadasz.', date: '5.03.2023' },
    //        { userName: 'Piotr6', comment: 'Nie no, ma racje', date: '1.01.2024' },
    //        { userName: 'Maciek7', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
    //        { userName: 'Magda8', comment: 'Bzdury gadasz.', date: '5.03.2023' },
    //        { userName: 'Piotr9', comment: 'Nie no, ma racje', date: '1.01.2024' },
    //        { userName: 'Maciek10', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
    //        { userName: 'Magda11', comment: 'Bzdury gadasz.', date: '5.03.2023' },
    //        { userName: 'Piotr12', comment: 'Nie no, ma racje', date: '1.01.2024' },
    //        { userName: 'Maciek13', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
    //        { userName: 'Magda14', comment: 'Bzdury gadasz.', date: '5.03.2023' },
    //        { userName: 'Piotr15', comment: 'Nie no, ma racje', date: '1.01.2024' },
    //        { userName: 'Maciek16', comment: 'Zgadzam się, ale bym zmienil drugi paragraf', date: '7.03.2024' },
    //        { userName: 'Magda17', comment: 'Bzdury gadasz.', date: '5.03.2023' },
    //        { userName: 'Piotr18', comment: 'Nie no, ma racje', date: '1.01.2024' },
    //    ]
    //)
   
    

    
    //const postContent = "Suboptimal diet is an important preventable risk factor for non-communicable diseases (NCDs); however, its impact on the burden of NCDs has not been systematically evaluated. This study aimed to evaluate the consumption of major foods and nutrients across 195 countries and to quantify the impact of their"
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
                    {{postTitle}}
                </div>
                <div class="text-h7 ma-6 text-center">
                    Autor: {{postAuthor}}, Data: {{postPublishedDate}}
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
                                        <div v-html="postContent" class="ma-2" style="white-space: pre-wrap;">
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
                    <v-textarea v-model=commentContent rows="5" auto-grow bg-color="grey-lighten-2" color="black" label="Wpisz swój komentarz"></v-textarea>
                    <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3" @click="postComment">
                        Dodaj
                    </v-btn>
                </div>
                <v-container>
                    <v-expand-transition>
                        <v-row>
                        <!--rgb(0, 0, 0, 0.2)-->
                            <v-col v-for="(comment,i) in allComments.slice((page - 1) * pageLength,Math.min((page - 1) * pageLength + pageLength - 1, allComments.length - 1)+1)"
                                   :key="i"
                                   cols="12">
                                <v-card class="mx-auto"
                                        variant="outlined">
                                    <v-card-item>
                                        <div class="ma-2">
                                            <v-tooltip text="Komentarz wystawiony przez twórcę posta">
                                                <template v-slot:activator="{ props }">
                                                    <v-btn v-if="comment.author == postAuthor" v-bind="props" class="font-weight-bold" color="teal-darken-1">OP</v-btn>
                                                </template>
                                            </v-tooltip>
                                             {{comment.author}}, {{comment.date}}
                                        <div class="text-h6 mb-1">
                                            {{ comment.content }}
                                        </div>

                                            <div class="mt-3">
                                                <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-thumb-up-outline" @click="likeComment(comment.id)">
                                                    {{comment.likes}}
                                                </v-btn>
                                                <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-thumb-down-outline" @click="dislikeComment(comment.id)">
                                                    {{comment.dislikes}}
                                                </v-btn>
                                                <v-btn class="font-weight-bold text-h6" prepend-icon="mdi-reply-outline" @click="comment.isReplying = !comment.isReplying">
                                                    Odpowiedz
                                                </v-btn>
                                            </div>
                                            <div v-if="comment.isReplying" class="ma-6">
                                                <v-textarea v-model=allReplies[i] rows="3" auto-grow bg-color="grey-lighten-2" color="black" label="Wpisz odpowiedź"></v-textarea>
                                                <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3" @click="postReply(comment.id,allReplies[i]), allReplies[i]=''">
                                                    Dodaj
                                                </v-btn>
                                            </div>
                                        </div>
                                    </v-card-item>
                                </v-card>
                                <v-card>
                                    <v-card-item class="ml-5">
                                        <v-col v-for="reply in comment.replies"
                                               :key="i"
                                               cols="12">
                                            <v-card class="mx-auto"
                                                    variant="outlined">
                                                <v-card-item>
                                                    <div class="ma-2">
                                                        <v-tooltip text="Komentarz wystawiony przez twórcę posta">
                                                            <template v-slot:activator="{ props }">
                                                                <v-btn v-if="comment.author == postAuthor" v-bind="props" class="font-weight-bold" color="teal-darken-1">OP</v-btn>
                                                            </template>
                                                        </v-tooltip>
                                                        {{reply.author}}, {{reply.createdDate}}
                                                        <div class="text-h6 mb-1">
                                                            {{ reply.replyContent }}
                                                        </div>
                                                    </div>
                                                </v-card-item>

                                            </v-card>
                                        </v-col>
                                    </v-card-item>
                                </v-card>
                            </v-col>
                        </v-row>
                    </v-expand-transition>
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