import { createApp } from 'vue'
import App from './App.vue'

// Vuetify
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import router from './router'
import { pl } from 'vuetify/locale'

const myCustomLightTheme = {
    dark: false,
    colors: {
        background: '#AAAAAA',
        surface: '#FFFFFF',
        primary: '#6200EE',
        'primary-darken-1': '#3700B3',
        secondary: '#03DAC6',
        'secondary-darken-1': '#018786',
        error: '#B00020',
        info: '#2196F3',
        success: '#4CAF50',
        warning: '#FB8C00',
    },
}

const vuetify = createVuetify({
    components,
    directives,
    theme: {
        defaultTheme: 'dark',
        themes: {
            myCustomLightTheme,
        },
    },
    locale: {
        locale: 'pl',
        fallback: 'pl',
        messages: {pl},
    },
})

createApp(App).use(router).use(vuetify).mount('#app')



