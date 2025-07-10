import axios from "axios";

export default {
  install(app) {
    const apiBaseURL = "https://localhost:7295/api";

    app.config.globalProperties.$apiBaseURL = apiBaseURL;

    app.config.globalProperties.$api = {

      getAvailableDrinks() {
        return axios.get(`${apiBaseURL}/vendingmachine/drinks`);
      },
    };
  },
};