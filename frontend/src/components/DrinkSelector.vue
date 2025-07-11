<template>
  <div>
    <h2 class="text-center mb-4">Seleccionar refrescos</h2>
    <div class="row gx-3 gy-4 justify-content-center">
      <div
        v-for="drink in drinks"
        :key="drink.name"
        class="col-12 col-md-5"
      >
        <div
          class="card w-100 shadow-sm"
          style="max-width: 200px; background-color: #FFEAA2;"
        >
          <div class="card-body text-center">
            <h5 class="card-title fw-bold" style="color: #FEB2B4;">{{ drink.name }}</h5>
            <p class="card-text mb-1">₡{{ drink.price }}</p>
            <input
              type="number"
              min="0"
              :max="drink.quantity"
              v-model.number="localQuantities[drink.name]"
              class="form-control mt-2"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    drinks: Array,
    modelValue: Object
  },
  data() {
    return {
      localQuantities: {}
    }
  },
  watch: {
    modelValue: {
      immediate: true,
      handler(newVal) {
        if (JSON.stringify(newVal) !== JSON.stringify(this.localQuantities)) {
          this.localQuantities = { ...newVal }
        }
      }
    },
    localQuantities: {
      deep: true,
      handler(val) {
        if (JSON.stringify(val) !== JSON.stringify(this.modelValue)) {
          this.$emit('update:modelValue', val)
        }
      }
    }
  },
  computed: {
    total() {
      return this.drinks.reduce((sum, drink) => {
        const qty = this.localQuantities[drink.name] || 0
        return sum + qty * drink.price
      }, 0)
    }
  }
}
</script>
