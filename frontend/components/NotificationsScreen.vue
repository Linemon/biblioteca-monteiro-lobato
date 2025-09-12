<template>
  <div class="library-container bg-white p-4">
    <div class="container-fluid">
      <div class="row mb-4">
        <div class="col-12 d-flex justify-content-between align-items-center">
          <div class="d-flex align-items-center">
            <button class="btn btn-outline-secondary btn-sm rounded-custom me-3" @click="$emit('navigate', 'dashboard')">
              <i class="bi bi-arrow-left me-1"></i> Voltar
            </button>
            <h1 class="text-dark h3 mb-0">Notificações</h1>
            <span v-if="unreadCount > 0" class="badge bg-danger rounded-pill ms-3">{{ unreadCount }} não lidas</span>
          </div>
          <button class="btn btn-primary-custom rounded-custom">Marcar todas como lidas</button>
        </div>
      </div>
      
      <div class="row">
        <div class="col-12">
          <div v-for="notification in mockNotifications" :key="notification.id" class="card shadow-sm rounded-custom mb-3">
            <div class="card-body p-4">
              <div class="d-flex align-items-start">
                <div class="me-3 mt-1">
                  <i class="bi bi-exclamation-triangle-fill text-warning"></i>
                </div>
                <div class="flex-grow-1">
                  <h6 class="mb-2" :class="notification.unread ? 'text-dark fw-semibold' : 'text-muted'">
                    {{ notification.title }}
                  </h6>
                  <p class="mb-3">{{ notification.message }}</p>
                  <small class="text-muted">{{ notification.date }}</small>
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
import { ref, computed } from 'vue'

export default {
  name: 'NotificationsScreen',
  emits: ['navigate'],
  setup() {
    const mockNotifications = ref([
      { id: 1, title: "Empréstimo em atraso", message: "O livro '1984' está 5 dias em atraso.", date: "05/01/2025", unread: true },
      { id: 2, title: "Novo livro adicionado", message: "O livro 'Cem Anos de Solidão' foi adicionado.", date: "04/01/2025", unread: false }
    ])
    
    const unreadCount = computed(() => mockNotifications.value.filter(n => n.unread).length)
    
    return { mockNotifications, unreadCount }
  }
}
</script>