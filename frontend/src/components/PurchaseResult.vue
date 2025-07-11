<template>
  <h2 class="text-center mb-4">Resumen de compra</h2>
  <div v-if="total > 0">
    <div class="card shadow-sm mx-auto w-100" style="max-width: 400px;">
      <div class="card-body" style="background-color: #FEB2B4;">
        <p>Total a pagar: <strong>₡{{ total }}</strong></p>
        <p>Dinero ingresado: <strong>₡{{ userMoney }}</strong></p>

        <div v-if="userMoney >= total" class="mt-3">
          <p>Vuelto: <strong>₡{{ userMoney - total }}</strong></p>
        </div>

        <div v-else class="text-danger mt-3">
          Dinero insuficiente.
        </div>
      </div>
    </div>

    <button
      class="btn btn-primary mx-auto d-block"
      style="background-color: #EE8187; border-color: #d65a60; color: black;
      margin-top: 15px; max-width: 400px; width: 100%;"
      @click="$emit('purchase')">
      Confirmar compra
    </button>
  </div>
</template>

<script>
export default {
  props: {
    total: Number,
    userMoney: Number,
    selectedQuantities: Object
  }
}
</script>
