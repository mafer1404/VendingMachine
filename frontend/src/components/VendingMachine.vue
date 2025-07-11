<template>
  <div class="container py-4">
    <h1 class="text-center mb-4">Máquina expendedora</h1>

    <div class="row justify-content-center align-items-start g-4">

      <div v-if="!hasAvailableDrinks" class="alert alert-warning text-center w-100">
        Máquina fuera de servicio: no hay bebidas disponibles.
      </div>

      <div v-if="noMoreAvailableCoins" class="alert alert-warning text-center w-100">
        Máquina fuera de servicio: no hay monedas suficientes para el vuelto.
      </div>

      <div class="d-flex flex-wrap justify-content-center gap-4">

        <section
          v-if="hasAvailableDrinks && !noMoreAvailableCoins"
          class="d-flex flex-column align-items-center"
          style="min-width: 320px; max-width: 400px;">
          <AvailableDrinks :drinks="drinks" />
        </section>

        <section
          v-if="hasAvailableDrinks && !noMoreAvailableCoins"
          class="d-flex flex-column align-items-center"
          style="min-width: 320px; max-width: 400px;">
          <DrinkSelector :drinks="drinks" v-model="selectedQuantities" />
        </section>

        <section
          class="d-flex flex-column align-items-center"
          style="min-width: 320px; max-width: 400px;"
          v-if="hasAvailableDrinks && !noMoreAvailableCoins">
          <MoneyInput v-model="userMoney" />
        </section>

        <section
          class="d-flex flex-column align-items-center"
          style="min-width: 320px; max-width: 400px;"
          v-if="hasAvailableDrinks && !noMoreAvailableCoins">
          <ChangeResult
            :total="total"
            :userMoney="totalUserMoney"
            :selectedQuantities="selectedQuantities"
            @purchase="handlePurchase"
          />
        </section>

        <section
          class="d-flex flex-column align-items-center"
          style="min-width: 320px; max-width: 400px;"
          v-if="hasAvailableDrinks && !noMoreAvailableCoins">
          <h2 class="text-center mb-3">Estado de la compra</h2>
          <ErrorBanner :message="errorMessage" />
          <SuccessBanner :message="successMessage" />
          <ChangeBreakdown :breakdown="changeBreakdown" />
        </section>
      </div>

      <button
        v-if="successMessage || errorMessage"
        class="btn btn-primary mx-auto d-block"
        @click="resetState"
        style="max-width: 200px; width: 100%; background-color: #EE8187;
        border-color: #d65a60; color: black; margin-top: 40px;">
        Nueva compra
      </button>

    </div>
  </div>
</template>

<script>
import AvailableDrinks from '@/components/AvailableDrinks.vue'
import DrinkSelector from '@/components/DrinkSelector.vue'
import MoneyInput from '@/components/MoneyInput.vue'
import ChangeResult from '@/components/PurchaseResult.vue'
import ErrorBanner from '@/components/ErrorBanner.vue'
import SuccessBanner from '@/components/SuccessBanner.vue'
import ChangeBreakdown from './ChangeBreakdown.vue'

export default {
  components: {
    AvailableDrinks,
    DrinkSelector,
    MoneyInput,
    ChangeResult,
    ErrorBanner,
    SuccessBanner,
    ChangeBreakdown
  },
  data() {
    return {
      drinks: [],
      selectedQuantities: {}, 
      userMoney: {
        money500: 0,
        money100: 0,
        money50: 0,
        money25: 0,
        money1000: 0
      },
      errorMessage: '',
      successMessage: '',
      changeBreakdown: {},
      noMoreAvailableCoins: false
    }
  },

  computed: {
    total() {
      return this.drinks.reduce((sum, drink) => {
        const drinkQuantity = this.selectedQuantities[drink.name] || 0
        return sum + drinkQuantity * drink.price
      }, 0)
    },

    totalUserMoney() {
      const sumUserMoney = this.userMoney
      return (
        (sumUserMoney.money500 || 0) * 500 +
        (sumUserMoney.money100 || 0) * 100 +
        (sumUserMoney.money50 || 0) * 50 +
        (sumUserMoney.money25 || 0) * 25 +
        (sumUserMoney.money1000 || 0) * 1000
      )
    },

    hasAvailableDrinks() {
      return this.drinks.some(drink => drink.quantity > 0)
    }
  },

  mounted() {
    this.fetchDrinks()
  },

  methods: {
    async fetchDrinks() {
      try {
        const response = await this.$api.getAvailableDrinks()
        this.drinks = response.data.drinks
        this.noMoreAvailableCoins = response.data.noMoreAvailableCoins    
        this.selectedQuantities = {}
        this.drinks.forEach(d => (this.selectedQuantities[d.name] = 0))
        this.errorMessage = ''
        this.successMessage = ''
        this.changeBreakdown = {}
      } catch (e) {
        this.errorMessage = e.response?.data || 'Error al mostrar refrescos disponibles.'
      }
    },

    resetState() {
      this.fetchDrinks()
      this.selectedQuantities = {}
      this.drinks.forEach(d => (this.selectedQuantities[d.name] = 0))
      this.userMoney = {
        money500: 0,
        money100: 0,
        money50: 0,
        money25: 0,
        money1000: 0
      }
      this.successMessage = ''
      this.errorMessage = ''
      this.changeBreakdown = {}
    },

    async handlePurchase() {
      this.errorMessage = ''
      this.successMessage = ''
      const items = []
      for (const drinkName in this.selectedQuantities) {
        const drinkQuantity = this.selectedQuantities[drinkName]
        if (drinkQuantity > 0) {
          items.push({ name: drinkName, quantity: drinkQuantity })
        }
      }
      try {
        const response = await this.$api.buyDrink({
          items,
          amountPaid: this.totalUserMoney
        })
        this.successMessage = response.data.message
        this.changeBreakdown = response.data.desglose || {}
      } catch (e) {
        this.errorMessage = e.response?.data || 'Error al procesar la compra.'
      }
    }
  }
}
</script>
