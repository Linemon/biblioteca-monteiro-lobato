<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <div class="row mb-4">
        <div class="col-12 d-flex align-items-center">
          <button class="btn btn-outline-secondary btn-sm rounded-custom me-3" @click="$emit('navigate', 'books')">
            <i class="bi bi-arrow-left me-1"></i> Voltar
          </button>
          <h1 class="text-dark h3 mb-0">{{ isEditing ? 'Editar Livro' : 'Adicionar Novo Livro' }}</h1>
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-lg-8">
          <div class="card shadow-custom rounded-custom">
            <div class="card-body p-4">
              <form @submit.prevent="handleSubmit">
                <div class="row mb-3">
                  <div class="col-md-6">
                    <label class="form-label">Título</label>
                    <input type="text" class="form-control rounded-custom" v-model="form.title" required />
                  </div>
                  <div class="col-md-6">
                    <label class="form-label">Autor</label>
                    <input type="text" class="form-control rounded-custom" v-model="form.author" required />
                  </div>
                </div>
                <div class="d-flex justify-content-end gap-3">
                  <button type="button" class="btn btn-outline-secondary rounded-custom" @click="$emit('navigate', 'books')">Cancelar</button>
                  <button type="submit" class="btn btn-primary-custom rounded-custom">{{ isEditing ? 'Salvar' : 'Adicionar' }}</button>
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
  name: 'BookFormScreen',
  props: { bookId: Number },
  emits: ['navigate'],
  setup(props, { emit }) {
    const form = ref({ title: '', author: '' })
    const isEditing = computed(() => props.bookId !== undefined)
    
    const handleSubmit = () => emit('navigate', 'books')
    
    return { form, isEditing, handleSubmit }
  }
}
</script>