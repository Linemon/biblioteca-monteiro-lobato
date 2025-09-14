<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <div class="row mb-4">
        <div class="col-12 d-flex align-items-center">
          <button class="btn btn-outline-secondary btn-sm rounded-custom me-3" @click="$emit('navigate', 'readers')">
            <i class="bi bi-arrow-left me-1"></i> Voltar
          </button>
          <h1 class="text-dark h3 mb-0">{{ isEditing ? 'Editar Leitor' : 'Cadastrar Novo Leitor' }}</h1>
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-lg-8">
          <div class="card shadow-custom rounded-custom">
            <div class="card-body p-4">
              <form @submit.prevent="handleSubmit">
                <div v-if="error" class="alert alert-danger rounded-custom mb-3">
                  {{ error }}
                </div>
                
                <div class="row mb-3">
                  <div class="col-md-6">
                    <label class="form-label">Nome Completo</label>
                    <input 
                      type="text" 
                      class="form-control rounded-custom" 
                      v-model="form.name" 
                      :disabled="loading"
                      required 
                    />
                  </div>
                  <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <input 
                      type="email" 
                      class="form-control rounded-custom" 
                      v-model="form.email" 
                      :disabled="loading"
                      required 
                    />
                  </div>
                </div>
                
                <div class="row mb-3">
                  <div class="col-md-6">
                    <label class="form-label">Telefone</label>
                    <input 
                      type="tel" 
                      class="form-control rounded-custom" 
                      v-model="form.phone" 
                      :disabled="loading"
                      required 
                    />
                  </div>
                </div>
                
                <div class="d-flex justify-content-end gap-3">
                  <button 
                    type="button" 
                    class="btn btn-outline-secondary rounded-custom" 
                    @click="$emit('navigate', 'readers')"
                    :disabled="loading"
                  >
                    Cancelar
                  </button>
                  <button 
                    type="submit" 
                    class="btn btn-primary-custom rounded-custom"
                    :disabled="loading"
                  >
                    <span v-if="loading" class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                    {{ loading ? 'Salvando...' : (isEditing ? 'Salvar' : 'Cadastrar') }}
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
import { ref, computed, onMounted } from 'vue'
import apiService from '../services/api.js'

export default {
  name: 'ReaderFormScreen',
  props: { readerId: String },
  emits: ['navigate'],
  setup(props, { emit }) {
    const form = ref({ name: '', email: '', phone: '' })
    const loading = ref(false)
    const error = ref('')
    const isEditing = computed(() => props.readerId !== undefined)

    const loadReader = async () => {
      if (!isEditing.value) return

      loading.value = true
      error.value = ''

      try {
        const reader = await apiService.getReaderById(props.readerId)
        form.value = {
          name: reader.name,
          email: reader.email,
          phone: reader.phone
        }
      } catch (err) {
        error.value = err.message || 'Erro ao carregar dados do leitor.'
      } finally {
        loading.value = false
      }
    }

    const handleSubmit = async () => {
      if (!form.value.name || !form.value.email || !form.value.phone) {
        error.value = 'Por favor, preencha todos os campos.'
        return
      }

      loading.value = true
      error.value = ''

      try {
        if (isEditing.value) {
          await apiService.updateReader(props.readerId, form.value)
        } else {
          await apiService.createReader(form.value)
        }
        emit('navigate', 'readers')
      } catch (err) {
        error.value = err.message || 'Erro ao salvar leitor.'
      } finally {
        loading.value = false
      }
    }

    onMounted(() => {
      loadReader()
    })

    return { 
      form, 
      loading, 
      error, 
      isEditing, 
      handleSubmit 
    }
  }
}
</script>