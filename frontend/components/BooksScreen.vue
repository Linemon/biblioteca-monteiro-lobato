<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <!-- Header -->
      <div class="row mb-4">
        <div class="col-12 d-flex justify-content-between align-items-center">
          <div class="d-flex align-items-center">
            <button 
              class="btn btn-outline-secondary btn-sm rounded-custom me-3"
              @click="$emit('navigate', 'dashboard')"
            >
              <i class="bi bi-arrow-left me-1"></i>
              Voltar
            </button>
            <h1 class="text-dark h3 mb-0">Listagem de Livros</h1>
          </div>
          <button 
            class="btn btn-primary-custom rounded-custom"
            @click="$emit('navigate', 'book-form')"
          >
            <i class="bi bi-plus-circle me-2"></i>
            Adicionar Livro
          </button>
        </div>
      </div>

      <!-- Search Bar -->
      <div class="row mb-4">
        <div class="col-12">
          <div class="position-relative">
            <i class="bi bi-search position-absolute top-50 start-0 translate-middle-y ms-3 text-muted"></i>
            <input
              type="text"
              class="form-control rounded-custom ps-5"
              placeholder="Buscar livros por título ou autor..."
              v-model="searchQuery"
            />
          </div>
        </div>
      </div>

      <!-- Books Table -->
      <div class="row">
        <div class="col-12">
          <div class="card shadow-custom rounded-custom">
            <div class="table-responsive">
              <table class="table table-hover mb-0">
                <thead class="bg-light">
                  <tr>
                    <th class="text-muted border-0 py-3">Título</th>
                    <th class="text-muted border-0 py-3">Autor</th>
                    <th class="text-muted border-0 py-3">Ano</th>
                    <th class="text-muted border-0 py-3">Status</th>
                    <th class="text-muted border-0 py-3" style="width: 150px;">Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="book in mockBooks" :key="book.id" class="border-bottom">
                    <td class="py-3">
                      <div class="text-dark">{{ book.title }}</div>
                    </td>
                    <td class="text-muted py-3">{{ book.author }}</td>
                    <td class="text-muted py-3">{{ book.year }}</td>
                    <td class="py-3">
                      <span 
                        class="badge rounded-custom"
                        :class="book.status === 'Disponível' ? 'bg-success text-white' : 'bg-warning text-dark'"
                      >
                        {{ book.status }}
                      </span>
                    </td>
                    <td class="py-3">
                      <div class="d-flex gap-2">
                        <button
                          class="btn btn-outline-secondary btn-sm rounded-custom"
                          @click="$emit('navigate', 'book-form', book.id)"
                        >
                          <i class="bi bi-pencil"></i>
                        </button>
                        <button
                          class="btn btn-outline-danger btn-sm rounded-custom"
                        >
                          <i class="bi bi-trash"></i>
                        </button>
                      </div>
                    </td>
                  </tr>
                </tbody>
              </table>
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
  name: 'BooksScreen',
  emits: ['navigate'],
  setup() {
    const searchQuery = ref('')
    
    const mockBooks = [
      { id: 1, title: "Dom Casmurro", author: "Machado de Assis", year: 1899, status: "Disponível" },
      { id: 2, title: "1984", author: "George Orwell", year: 1949, status: "Emprestado" },
      { id: 3, title: "O Alquimista", author: "Paulo Coelho", year: 1988, status: "Disponível" },
      { id: 4, title: "Grande Sertão: Veredas", author: "Guimarães Rosa", year: 1956, status: "Disponível" },
      { id: 5, title: "A Hora da Estrela", author: "Clarice Lispector", year: 1977, status: "Emprestado" },
      { id: 6, title: "Memórias Póstumas de Brás Cubas", author: "Machado de Assis", year: 1881, status: "Disponível" }
    ]

    return {
      searchQuery,
      mockBooks
    }
  }
}
</script>