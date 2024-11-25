import { createApp } from 'vue'
import App from './App.vue'
// import './index.css'
import router from './scripts/router'
import { createPinia } from 'pinia'
// import './assets/styles.css'

const pinia = createPinia();

createApp(App).use(pinia).use(router).mount('#app')
