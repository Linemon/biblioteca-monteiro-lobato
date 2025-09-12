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
                <div class="row mb-3">
                  <div class="col-md-6">
                    <label class="form-label">Nome Completo</label>
                    <input type="text" class="form-control rounded-custom" v-model="form.name" required />
                  </div>
                  <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <input type="email" class="form-control rounded-custom" v-model="form.email" required />
                  </div>
                </div>
                <div class="d-flex justify-content-end gap-3">
                  <button type="button" class="btn btn-outline-secondary rounded-custom" @click="$emit('navigate', 'readers')">Cancelar</button>
                  <button type="submit" class="btn btn-primary-custom rounded-custom">{{ isEditing ? 'Salvar' : 'Cadastrar' }}</button>
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
import { ref, computed } from 'vue'

export default {
  name: 'ReaderFormScreen',
  props: { readerId: Number },
  emits: ['navigate'],
  setup(props, { emit }) {
    const form = ref({ name: '', email: '' })
    const isEditing = computed(() => props.readerId !== undefined)
    
    const handleSubmit = () => emit('navigate', 'readers')
    
    return { form, isEditing, handleSubmit }
  }
}
</script>