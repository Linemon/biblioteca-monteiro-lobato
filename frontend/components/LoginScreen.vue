<template>
  <div class="library-container bg-white d-flex align-items-center justify-content-center p-4">
    <div class="container-fluid">
      <div class="row justify-content-center">
        <div class="col-md-4">
          <div class="card shadow-custom rounded-custom">
            <div class="card-header bg-white border-0 text-center py-4">
              <h2 class="text-dark mb-1">Biblioteca Pública Municipal</h2>
              <h3 class="text-dark mb-3">"Monteiro Lobato"</h3>
              <p class="text-muted mb-0">Faça login para acessar o sistema</p>
            </div>
            <div class="card-body px-4 pb-4">
              <form @submit.prevent="handleSubmit">
                <div class="mb-3">
                  <label for="email" class="form-label text-dark">Email</label>
                  <input
                    type="email"
                    class="form-control rounded-custom"
                    id="email"
                    v-model="email"
                    placeholder="Digite seu email"
                    required
                  />
                </div>
                
                <div class="mb-4">
                  <label for="password" class="form-label text-dark">Senha</label>
                  <input
                    type="password"
                    class="form-control rounded-custom"
                    id="password"
                    v-model="password"
                    placeholder="Digite sua senha"
                    required
                  />
                </div>
                
                <button
                  type="submit"
                  class="btn btn-primary-custom w-100 py-2 rounded-custom mb-3"
                >
                  Entrar
                </button>
                
                <div class="text-center">
                  <button
                    type="button"
                    class="btn btn-link text-primary-custom p-0"
                    @click="$emit('navigate', 'forgot-password')"
                  >
                    Esqueci minha senha
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'

export default {
  name: 'LoginScreen',
  emits: ['login', 'navigate'],
  setup(props, { emit }) {
    const email = ref('')
    const password = ref('')

    const handleSubmit = () => {
      if (email.value && password.value) {
        emit('login')
      }
    }

    return {
      email,
      password,
      handleSubmit
    }
  }
}
</script>

<style scoped>
.btn-link:hover {
  text-decoration: underline !important;
}
</style>