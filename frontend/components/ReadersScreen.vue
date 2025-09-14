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
          <div v-if="error" class="alert alert-danger rounded-custom mb-3">
            {{ error }}
          </div>
          
          <div class="card shadow-custom rounded-custom">
            <div class="table-responsive">
              <table class="table table-hover mb-0">
                <thead class="bg-light">
                  <tr>
                    <th class="text-muted border-0 py-3">Nome</th>
                    <th class="text-muted border-0 py-3">Email</th>
                    <th class="text-muted border-0 py-3">Telefone</th>
                    <th class="text-muted border-0 py-3">Status</th>
                    <th class="text-muted border-0 py-3">Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-if="loading">
                    <td colspan="5" class="text-center py-4">
                      <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Carregando...</span>
                      </div>
                    </td>
                  </tr>
                  <tr v-else-if="readers.length === 0">
                    <td colspan="5" class="text-center py-4 text-muted">
                      Nenhum leitor cadastrado
                    </td>
                  </tr>
                  <tr v-else v-for="reader in readers" :key="reader.id">
                    <td class="py-3">{{ reader.name }}</td>
                    <td class="py-3">{{ reader.email }}</td>
                    <td class="py-3">{{ reader.phone }}</td>
                    <td class="py-3">
                      <span :class="reader.status === 'Ativo' ? 'badge bg-success' : 'badge bg-secondary'" class="rounded-custom">
                        {{ reader.status }}
                      </span>
                    </td>
                    <td class="py-3">
                      <div class="btn-group" role="group">
                        <button 
                          class="btn btn-outline-primary btn-sm rounded-custom" 
                          @click="$emit('navigate', 'reader-form', undefined, reader.id)"
                          title="Editar"
                        >
                          <i class="bi bi-pencil"></i>
                        </button>
                        <button 
                          class="btn btn-outline-danger btn-sm rounded-custom" 
                          @click="handleDelete(reader.id)"
                          title="Inativar"
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
import { ref, onMounted } from 'vue'
import apiService from '../services/api.js'

export default {
  name: 'ReadersScreen',
  emits: ['navigate'],
  setup() {
    const readers = ref([])
    const loading = ref(false)
    const error = ref('')

    const loadReaders = async () => {
      loading.value = true
      error.value = ''
      
      try {
        // Primeiro, buscar a lista de IDs dos leitores
        const readersList = await apiService.getReaders()
        
        // Para cada ID, fazer um GET individual para obter os dados completos
        const readersPromises = readersList.map(async (readerItem) => {
          try {
            const readerDetails = await apiService.getReaderById(readerItem.id)
            return {
              id: readerItem.id,
              ...readerDetails,
              status: readerDetails.status === 0 ? 'Ativo' : 'Inativo'
            }
          } catch (err) {
            console.error(`Erro ao carregar detalhes do leitor ${readerItem.id}:`, err)
            // Retornar dados básicos em caso de erro
            return {
              id: readerItem.id,
              name: 'Erro ao carregar',
              email: 'N/A',
              phone: 'N/A',
              status: 'Erro'
            }
          }
        })
        
        // Aguardar todas as requisições individuais
        const readersData = await Promise.all(readersPromises)
        readers.value = readersData
      } catch (err) {
        error.value = err.message || 'Erro ao carregar leitores.'
      } finally {
        loading.value = false
      }
    }

    const handleDelete = async (readerId) => {
      if (!confirm('Tem certeza que deseja inativar este leitor?')) {
        return
      }

      try {
        await apiService.deleteReader(readerId)
        await loadReaders() // Recarregar lista
      } catch (err) {
        error.value = err.message || 'Erro ao inativar leitor.'
      }
    }

    onMounted(() => {
      loadReaders()
    })

    return { 
      readers, 
      loading, 
      error, 
      handleDelete 
    }
  }
}
</script>