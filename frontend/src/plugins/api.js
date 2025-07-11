import axios from "axios";

export default {
  install(app) {
    const apiBaseURL = "https://localhost:7295/";

    app.config.globalProperties.$apiBaseURL = apiBaseURL;

    app.config.globalProperties.$api = {

      getAvailableDrinks() {
        return axios.get(`${apiBaseURL}VendingMachine/Drinks`);
      },

      buyDrink(purchase) {
        return axios.post(`${apiBaseURL}VendingMachine/Buy`, purchase)
    }
    };
  },
};