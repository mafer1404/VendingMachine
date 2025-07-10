<template>
  <div class="drinks-container">
    <h2>Refrescos disponibles</h2>

    <div v-if="drinks.length === 0" class="text-muted">No hay refrescos disponibles.</div>
    <div v-else class="cards-wrapper">
      <div v-for="drink in drinks" :key="drink.name" class="card drink-card">
        <div class="card-body">
          <h5 class="card-title">{{ drink.name }}</h5>
          <p class="card-text">Precio: ₡{{ drink.price }}</p>
          <p class="card-text">Disponibles: {{ drink.quantity }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
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
        const response = await this.$api.getAvailableDrinks();
        this.drinks = response.data;
        this.errorMessage = '';
      } catch (error) {
        this.errorMessage = 'Error al cargar refrescos: ' + (error.message || 'Error desconocido');
      }
    }
  }
}
</script>

<style src="@/assets/css/AvailableDrinks.css"></style>
