<template>
  <div id="app">
    <Navbar />
    <main role="main" class="container mt-4">
      <router-view/>
    </main>
  </div>
</template>

<script>
import Navbar from '@/layout/Navbar'

export default {
  name: 'App',
  components: {
    Navbar
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: left;
  color: #2c3e50;
}
</style>
