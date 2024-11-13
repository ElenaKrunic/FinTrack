<template>
  <div class="auth-form">
    <h2>{{ isLogin ? 'Login' : 'Register' }}</h2>
    <form @submit.prevent="handleSubmit">
      <input 
        v-if="!isLogin" 
        v-model="username" 
        placeholder="Username" 
        required 
      />
      <input 
        v-model="email" 
        type="email" 
        placeholder="Email" 
        required 
      />
      <input 
        v-model="password" 
        type="password" 
        placeholder="Password" 
        required 
      />
      <button type="submit">{{ isLogin ? 'Login' : 'Register' }}</button>
    </form>
    <p v-if="message">{{ message }}</p>
    <button @click="toggleMode">
      <router-link :to="isLogin ? '/register' : '/login'">
        {{ isLogin ? 'Need an account? Register' : 'Already have an account? Login' }}
      </router-link>
    </button>
  </div>
</template>

<script>
import authService from '@/services/authService'; 

export default {
  props: {
    isLogin: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      username: '',
      email: '',
      password: '',
      message: ''
    };
  },
methods: {
  async handleSubmit() {
    try {
      let response;
      if (this.isLogin) {
        const payload = {
          email: this.email,
          password: this.password,
        };
        response = await authService.login(payload);
        this.message = 'Login successful!';
        
        if (response.token) {
          localStorage.setItem('token', response.token);
        } else {
          this.message = 'Token not found in response';
        }
      } else {
        const payload = {
          username: this.username,
          email: this.email,
          password: this.password,
        };
        response = await authService.register(payload);
        this.message = 'Registration successful!';
      }
    } catch (error) {
      this.message = 'Something went wrong!';
      console.error(error);
    }
  }
}
};
</script>

<style scoped>
.auth-form {
  max-width: 400px;
  margin: 0 auto;
  text-align: center;
}
input {
  display: block;
  margin: 10px 0;
  padding: 10px;
  width: 100%;
}
button {
  padding: 10px;
  background-color: #42b983;
  color: white;
  border: none;
  cursor: pointer;
}
</style>
