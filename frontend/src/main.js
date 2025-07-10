import { createApp } from 'vue'
import { createRouter, createWebHistory } from "vue-router";
import apiPlugin from './plugins/api';
import HomePage from "./components/VendingMachine.vue";

import '@/assets/css/VendingMachine.css'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", name: "HomePage", component: HomePage },
  ],
});

const app = createApp(HomePage);
app.use(router);
app.use(apiPlugin);
app.mount("#app");