<template>
  <div>
    <h2 class="text-center mb-4">Ingresar dinero</h2>

    <div class="row gx-3 gy-5 justify-content-center">
      <div
        v-for="(coin, key) in coins"
        :key="key"
        class="col-12 col-md-4">
        <div class="card w-100 shadow-sm" style="max-width: 200px; background-color: #FFEAA2;">
          <div class="card-body text-center">
            <h5 class="card-title fw-bold" style="color: #FEB2B4;">{{ coin.label }}</h5>
            <input
              type="number"
              min="0"
              v-model.number="localMoney[key]"
              class="form-control mt-2"/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    modelValue: Object
  },
  data() {
    return {
      localMoney: {
        money500: 0,
        money100: 0,
        money50: 0,
        money25: 0,
        money1000: 0
      },
      coins: {
        money500: { label: 'Monedas de ₡500' },
        money100: { label: 'Monedas de ₡100' },
        money50: { label: 'Monedas de ₡50' },
        money25: { label: 'Monedas de ₡25' },
        money1000: { label: 'Billetes de ₡1000' }
      }
    }
  },
  watch: {
    modelValue: {
      immediate: true,
      handler(newVal) {
        if (JSON.stringify(newVal) !== JSON.stringify(this.localMoney)) {
          this.localMoney = { ...newVal }
        }
      }
    },
    localMoney: {
      deep: true,
      handler(val) {
        if (JSON.stringify(val) !== JSON.stringify(this.modelValue)) {
          this.$emit('update:modelValue', val)
        }
      }
    }
  }
}
</script>
