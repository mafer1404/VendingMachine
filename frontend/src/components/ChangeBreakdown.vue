<template>
  <div v-if="breakdown && Object.keys(breakdown).length" class="alert w-100" style="background-color: #FEB2B4;">
    <h5>Desglose:</h5>
    <ul>
      <li v-for="(qty, coin) in breakdown" :key="coin">
        {{ qty }} de {{ coin }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  props: {
    breakdown: Object
  }
}
</script>
