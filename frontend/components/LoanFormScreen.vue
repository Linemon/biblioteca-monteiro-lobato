<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <div class="row mb-4">
        <div class="col-12 d-flex align-items-center">
          <button class="btn btn-outline-secondary btn-sm rounded-custom me-3" @click="$emit('navigate', 'loans')">
            <i class="bi bi-arrow-left me-1"></i> Voltar
          </button>
          <h1 class="text-dark h3 mb-0">Registrar Empréstimo</h1>
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-lg-8">
          <div class="card shadow-custom rounded-custom">
            <div class="card-body p-4">
              <form @submit.prevent="handleSubmit">
                <div class="row mb-3">
                  <div class="col-md-6">
                    <label class="form-label">Livro</label>
                    <select class="form-select rounded-custom" v-model="form.book" required>
                      <option value="">Selecione um livro</option>
                      <option value="1">Dom Casmurro</option>
                      <option value="2">O Alquimista</option>
                    </select>
                  </div>
                  <div class="col-md-6">
                    <label class="form-label">Leitor</label>
                    <select class="form-select rounded-custom" v-model="form.reader" required>
                      <option value="">Selecione um leitor</option>
                      <option value="1">João Silva</option>
                      <option value="2">Maria Santos</option>
                    </select>
                  </div>
                </div>
                <div class="d-flex justify-content-end gap-3">
                  <button type="button" class="btn btn-outline-secondary rounded-custom" @click="$emit('navigate', 'loans')">Cancelar</button>
                  <button type="submit" class="btn btn-primary-custom rounded-custom">Confirmar Empréstimo</button>
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
  name: 'LoanFormScreen',
  emits: ['navigate'],
  setup(props, { emit }) {
    const form = ref({ book: '', reader: '' })
    
    const handleSubmit = () => emit('navigate', 'loans')
    
    return { form, handleSubmit }
  }
}
</script>