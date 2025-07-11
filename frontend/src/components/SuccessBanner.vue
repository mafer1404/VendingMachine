<template>
  <div v-if="message" class="alert w-100" style="background-color: #FEB2B4; margin-top: 10px;">
    {{ message }}
  </div>
</template>

<script>
export default {
  props: {
    message: {
      type: String,
      default: ''
    }
  }
}
</script>
