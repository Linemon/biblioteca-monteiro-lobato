<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <!-- Header -->
      <div class="row mb-4">
        <div class="col-12 d-flex justify-content-between align-items-center">
          <h1 class="text-dark h3 mb-0">Dashboard - Biblioteca Pública Municipal "Monteiro Lobato"</h1>
          <div class="d-flex gap-2">
            <button class="btn btn-outline-secondary rounded-custom me-2" @click="$emit('navigate', 'books')">
              Livros
            </button>
            <button class="btn btn-outline-secondary rounded-custom me-2" @click="$emit('navigate', 'loans')">
              Empréstimos
            </button>
            <button class="btn btn-outline-secondary rounded-custom me-2" @click="$emit('navigate', 'readers')">
              Leitores
            </button>
            <button class="btn btn-outline-secondary rounded-custom" @click="$emit('navigate', 'notifications')">
              Notificações
            </button>
          </div>
        </div>
      </div>

      <!-- Stats Cards -->
      <div class="row mb-4">
        <div class="col-md-4 mb-3">
          <div class="card shadow-custom rounded-custom">
            <div class="card-header bg-white border-0 pb-2">
              <h6 class="card-title text-muted mb-0">Livros Disponíveis</h6>
            </div>
            <div class="card-body pt-0">
              <div class="h2 text-primary-custom mb-1">1.247</div>
              <p class="text-muted small mb-0">Títulos em estoque</p>
            </div>
          </div>
        </div>

        <div class="col-md-4 mb-3">
          <div class="card shadow-custom rounded-custom">
            <div class="card-header bg-white border-0 pb-2">
              <h6 class="card-title text-muted mb-0">Empréstimos Ativos</h6>
            </div>
            <div class="card-body pt-0">
              <div class="h2 text-success mb-1">342</div>
              <p class="text-muted small mb-0">Livros emprestados</p>
            </div>
          </div>
        </div>

        <div class="col-md-4 mb-3">
          <div class="card shadow-custom rounded-custom">
            <div class="card-header bg-white border-0 pb-2">
              <h6 class="card-title text-muted mb-0">Empréstimos Atrasados</h6>
            </div>
            <div class="card-body pt-0">
              <div class="h2 text-danger mb-1">23</div>
              <p class="text-muted small mb-0">Necessitam atenção</p>
            </div>
          </div>
        </div>
      </div>

      <!-- Quick Actions & Recent Activities -->
      <div class="row">
        <div class="col-md-6">
          <div class="card shadow-custom rounded-custom">
            <div class="card-body p-4">
              <h5 class="card-title text-dark mb-4">Ações Rápidas</h5>
              <div class="d-grid gap-2">
                <button 
                  class="btn btn-primary-custom rounded-custom text-start"
                  @click="$emit('navigate', 'book-form')"
                >
                  <i class="bi bi-plus-circle me-2"></i>
                  Adicionar Novo Livro
                </button>
                <button 
                  class="btn btn-primary-custom rounded-custom text-start"
                  @click="$emit('navigate', 'loan-form')"
                >
                  <i class="bi bi-journal-plus me-2"></i>
                  Registrar Empréstimo
                </button>
                <button 
                  class="btn btn-primary-custom rounded-custom text-start"
                  @click="$emit('navigate', 'reader-form')"
                >
                  <i class="bi bi-person-plus me-2"></i>
                  Cadastrar Novo Leitor
                </button>
              </div>
            </div>
          </div>
        </div>

        <div class="col-md-6">
          <div class="card shadow-custom rounded-custom">
            <div class="card-body p-4">
              <h5 class="card-title text-dark mb-4">Atividades Recentes</h5>
              <div class="list-group list-group-flush">
                <div class="d-flex justify-content-between align-items-start mb-3">
                  <span class="text-dark small">Livro "Dom Casmurro" devolvido</span>
                  <span class="text-muted small">há 2h</span>
                </div>
                <div class="d-flex justify-content-between align-items-start mb-3">
                  <span class="text-dark small">Novo livro "1984" adicionado</span>
                  <span class="text-muted small">há 4h</span>
                </div>
                <div class="d-flex justify-content-between align-items-start mb-0">
                  <span class="text-dark small">Empréstimo de "O Alquimista" registrado</span>
                  <span class="text-muted small">há 6h</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Dashboard',
  emits: ['navigate']
}
</script>

<style scoped>
.gap-2 > * + * {
  margin-left: 0.5rem;
}

.d-grid.gap-2 > * + * {
  margin-left: 0;
  margin-top: 0.5rem;
}
</style>