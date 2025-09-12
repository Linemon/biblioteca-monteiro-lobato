<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <div class="row mb-4">
        <div class="col-12 d-flex justify-content-between align-items-center">
          <div class="d-flex align-items-center">
            <button class="btn btn-outline-secondary btn-sm rounded-custom me-3" @click="$emit('navigate', 'dashboard')">
              <i class="bi bi-arrow-left me-1"></i> Voltar
            </button>
            <h1 class="text-dark h3 mb-0">Leitores Cadastrados</h1>
          </div>
          <button class="btn btn-primary-custom rounded-custom" @click="$emit('navigate', 'reader-form')">
            <i class="bi bi-person-plus me-2"></i> Cadastrar Leitor
          </button>
        </div>
      </div>
      
      <div class="row">
        <div class="col-12">
          <div class="card shadow-custom rounded-custom">
            <div class="table-responsive">
              <table class="table table-hover mb-0">
                <thead class="bg-light">
                  <tr>
                    <th class="text-muted border-0 py-3">Nome</th>
                    <th class="text-muted border-0 py-3">Email</th>
                    <th class="text-muted border-0 py-3">Status</th>
                    <th class="text-muted border-0 py-3">Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="reader in mockReaders" :key="reader.id">
                    <td class="py-3">{{ reader.name }}</td>
                    <td class="py-3">{{ reader.email }}</td>
                    <td class="py-3">
                      <span class="badge bg-success rounded-custom">{{ reader.status }}</span>
                    </td>
                    <td class="py-3">
                      <button class="btn btn-outline-secondary btn-sm rounded-custom" @click="$emit('navigate', 'reader-form', undefined, reader.id)">
                        <i class="bi bi-pencil"></i>
                      </button>
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
  name: 'ReadersScreen',
  emits: ['navigate'],
  setup() {
    const mockReaders = ref([
      { id: 1, name: "João Silva", email: "joao@email.com", status: "Ativo" },
      { id: 2, name: "Maria Santos", email: "maria@email.com", status: "Ativo" }
    ])
    
    return { mockReaders }
  }
}
</script>