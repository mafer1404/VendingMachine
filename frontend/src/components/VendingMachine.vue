<template>
  <div class="container py-4">
    <h1 class="text-center mb-4">Máquina expendedora</h1>

    <div class="row justify-content-center align-items-start">
      <div class="col-md-4 text-center">
        <img :src="imgSrc" alt="Máquina" class="img-fluid vending-machine-img"/>
      </div>

      <div class="col-md-7">
        <AvailableDrinks :drinks="drinks" />
        <ErrorBanner :message="errorMessage" />
      </div>
    </div>
  </div>
</template>

<script>
import vendingMachineImg from '@/assets/images/vendingMachine.png'
import AvailableDrinks from '@/components/AvailableDrinks.vue'
import ErrorBanner from '@/components/ErrorBanner.vue'

export default {
  components: { AvailableDrinks, ErrorBanner },
  data() {
    return {
      imgSrc: vendingMachineImg,
      drinks: [],
      errorMessage: ''
    }
  },
  mounted() {
    this.fetchDrinks()
  },
  methods: {
    async fetchDrinks() {
      try {
        const response = await this.$api.getAvailableDrinks()
        this.drinks = response.data
        this.selectedQuantities = {}
        this.drinks.forEach(d => this.selectedQuantities[d.name] = 0)
      } catch (e) {
        this.errorMessage = 'No se pudieron cargar los refrescos.'
      }
    }
  }
}
</script>
