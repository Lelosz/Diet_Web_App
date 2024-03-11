<script setup>
    import { ref, onMounted } from 'vue'

    const postTitle = ref('');
    const postContent = ref('');



</script>

<template>
   

        <v-card class="mx-auto align-center justify-center" max-width="1000" color="rgb(0, 0, 0, 0)" height="10vh">
            
        </v-card>
        <v-card class="mx-auto align-center justify-center" max-width="1000">
            <v-card-item>
                <div class="text-h4 ma-6 text-center">
                    Stwórz nowy wpis
                </div>
                <div class="text-h4 ma-6">
                    <v-form width="600">
                        <v-text-field v-model="postTitle" variant="outlined" label="Tytuł"></v-text-field>
                        <v-textarea v-model="postContent" variant="outlined" rows="10" auto-grow label="Treść"></v-textarea>

                        <v-row>
                            <v-col>
                                <v-btn class="font-weight-bold" variant="outlined" color="red" @click="$router.back()">
                                    Wstecz
                                </v-btn>
                            </v-col>
                            <v-col class="text-right">
                                <v-btn class="font-weight-bold" variant="outlined" color="green-accent-3" @click="">
                                    Opublikuj
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