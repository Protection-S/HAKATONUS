import { createApp } from 'vue';
import App from '/src/App.vue';
import router from '/src/router.js';
import { createVuetify } from 'vuetify';

const app = createApp(App);

app.use(router);

const vuetify = createVuetify({
    theme: {
        themes: {
            light: {
                primary: '#1D472A', 
                secondary: '#777777',
                accent: '#3C763D',
                success: '#5CB85C', 
                info: '#5BC0DE', 
                warning: '#F0AD4E', 
                error: '#D9534F', 
            },
        },
    },
});

app.use(vuetify);

app.mount('#app');
