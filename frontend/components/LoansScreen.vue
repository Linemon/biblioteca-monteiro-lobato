<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <div class="row mb-4">
        <div class="col-12 d-flex justify-content-between align-items-center">
          <div class="d-flex align-items-center">
            <button class="btn btn-outline-secondary btn-sm rounded-custom me-3" @click="$emit('navigate', 'dashboard')">
              <i class="bi bi-arrow-left me-1"></i> Voltar
            </button>
            <h1 class="text-dark h3 mb-0">Empréstimos</h1>
          </div>
          <button class="btn btn-primary-custom rounded-custom" @click="$emit('navigate', 'loan-form')">
            <i class="bi bi-journal-plus me-2"></i> Registrar Empréstimo
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
                    <th class="text-muted border-0 py-3">Livro</th>
                    <th class="text-muted border-0 py-3">Leitor</th>
                    <th class="text-muted border-0 py-3">Data Empréstimo</th>
                    <th class="text-muted border-0 py-3">Status</th>
                    <th class="text-muted border-0 py-3">Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="loan in mockLoans" :key="loan.id">
                    <td class="py-3">{{ loan.book }}</td>
                    <td class="py-3">{{ loan.reader }}</td>
                    <td class="py-3">{{ loan.loanDate }}</td>
                    <td class="py-3">
                      <span class="badge bg-primary rounded-custom">{{ loan.status }}</span>
                    </td>
                    <td class="py-3">
                      <button v-if="loan.status === 'Ativo'" class="btn btn-success btn-sm rounded-custom">
                        Registrar Devolução
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
  name: 'LoansScreen',
  emits: ['navigate'],
  setup() {
    const mockLoans = ref([
      { id: 1, book: "Dom Casmurro", reader: "João Silva", loanDate: "15/01/2025", status: "Ativo" },
      { id: 2, book: "1984", reader: "Maria Santos", loanDate: "10/01/2025", status: "Ativo" }
    ])
    
    return { mockLoans }
  }
}
</script>