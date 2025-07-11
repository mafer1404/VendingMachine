<template>
  <div v-if="message" class="alert alert-danger text-center">
    {{ message }}
  </div>
</template>

<script>
export default {
  props: ['message']
}
</script>
