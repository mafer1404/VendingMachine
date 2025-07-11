<template>
  <div>
    <h2 class="text-center mb-4">Refrescos disponibles</h2>
    <div class="row gx-3 gy-4 justify-content-center">
      <div
        v-for="drink in drinks"
        :key="drink.name"
        class="col-12 col-md-5 d-flex justify-content-center">
        <div
          class="card shadow-sm w-100"
          style="max-width: 200px; background-color: #FFEAA2; min-height: 135px;">
          <div class="card-body d-flex flex-column justify-content-center text-center">
            <h5 class="card-title fw-bold" style="color: #FEB2B4;">{{ drink.name }}</h5>
            <p class="card-text mb-1">Precio: ₡{{ drink.price }}</p>
            <p class="card-text">Disponibles: {{ drink.quantity }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['drinks']
}
</script>
