<template>
  <div class="library-container bg-white d-flex align-items-center justify-content-center p-4">
    <div class="container-fluid">
      <div class="row justify-content-center">
        <div class="col-md-4">
          <div class="card shadow-custom rounded-custom">
            <div class="card-header bg-white border-0 text-center py-4">
              <h2 class="text-dark mb-1">Recuperar Senha</h2>
              <p class="text-muted mb-0">Digite seu email para receber as instruções</p>
            </div>
            <div class="card-body px-4 pb-4">
              <form @submit.prevent="handleSubmit">
                <div class="mb-4">
                  <label for="email" class="form-label text-dark">Email</label>
                  <input
                    type="email"
                    class="form-control rounded-custom"
                    id="email"
                    v-model="email"
                    placeholder="Digite seu email cadastrado"
                    required
                  />
                </div>
                
                <button
                  type="submit"
                  class="btn btn-primary-custom w-100 py-2 rounded-custom mb-3"
                  :disabled="isLoading"
                >
                  <span v-if="isLoading" class="spinner-border spinner-border-sm me-2"></span>
                  {{ isLoading ? 'Enviando...' : 'Enviar Instruções' }}
                </button>
                
                <div class="text-center">
                  <button
                    type="button"
                    class="btn btn-link text-primary-custom p-0"
                    @click="$emit('navigate', 'login')"
                  >
                    Voltar ao Login
                  </button>
                </div>
              </form>

              <div v-if="showSuccess" class="alert alert-success rounded-custom mt-3">
                <i class="bi bi-check-circle me-2"></i>
                Instruções enviadas! Verifique seu email.
              </div>
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
  name: 'ForgotPasswordScreen',
  emits: ['navigate'],
  setup(props, { emit }) {
    const email = ref('')
    const isLoading = ref(false)
    const showSuccess = ref(false)

    const handleSubmit = async () => {
      if (!email.value) return

      isLoading.value = true
      
      setTimeout(() => {
        isLoading.value = false
        showSuccess.value = true
        
        setTimeout(() => {
          emit('navigate', 'login')
        }, 3000)
      }, 2000)
    }

    return {
      email,
      isLoading,
      showSuccess,
      handleSubmit
    }
  }
}
</script>