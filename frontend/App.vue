<template>
  <div class="min-vh-100 bg-light">
    <component 
      :is="currentComponent" 
      :book-id="selectedBookId"
      :reader-id="selectedReaderId"
      @navigate="handleNavigation"
      @login="handleLogin"
    />
  </div>
</template>

<script>
import { ref, computed } from 'vue'
import LoginScreen from './components/LoginScreen.vue'
import ForgotPasswordScreen from './components/ForgotPasswordScreen.vue'
import Dashboard from './components/Dashboard.vue'
import BooksScreen from './components/BooksScreen.vue'
import BookFormScreen from './components/BookFormScreen.vue'
import ReadersScreen from './components/ReadersScreen.vue'
import ReaderFormScreen from './components/ReaderFormScreen.vue'
import LoansScreen from './components/LoansScreen.vue'
import LoanFormScreen from './components/LoanFormScreen.vue'
import NotificationsScreen from './components/NotificationsScreen.vue'

export default {
  name: 'App',
  components: {
    LoginScreen,
    ForgotPasswordScreen,
    Dashboard,
    BooksScreen,
    BookFormScreen,
    ReadersScreen,
    ReaderFormScreen,
    LoansScreen,
    LoanFormScreen,
    NotificationsScreen
  },
  setup() {
    const currentScreen = ref('login')
    const selectedBookId = ref(undefined)
    const selectedReaderId = ref(undefined)
    const currentUser = ref(null)

    const handleNavigation = (screen, bookId, readerId) => {
      currentScreen.value = screen
      selectedBookId.value = bookId
      selectedReaderId.value = readerId
    }

    const handleLogin = (user) => {
      currentUser.value = user
      currentScreen.value = 'dashboard'
    }

    const currentComponent = computed(() => {
      const components = {
        'login': 'LoginScreen',
        'forgot-password': 'ForgotPasswordScreen',
        'dashboard': 'Dashboard',
        'books': 'BooksScreen',
        'book-form': 'BookFormScreen',
        'readers': 'ReadersScreen',
        'reader-form': 'ReaderFormScreen',
        'loans': 'LoansScreen',
        'loan-form': 'LoanFormScreen',
        'notifications': 'NotificationsScreen'
      }
      return components[currentScreen.value] || 'LoginScreen'
    })

    return {
      currentComponent,
      selectedBookId,
      selectedReaderId,
      currentUser,
      handleNavigation,
      handleLogin
    }
  }
}
</script>

<style>
@import 'bootstrap/dist/css/bootstrap.min.css';
@import 'bootstrap-icons/font/bootstrap-icons.css';

:root {
  --primary-blue: #2D8CFF;
  --primary-blue-hover: #1F7CE8;
}

body {
  font-family: 'Roboto', sans-serif;
}

.btn-primary-custom {
  background-color: var(--primary-blue);
  border-color: var(--primary-blue);
  color: white;
}

.btn-primary-custom:hover {
  background-color: var(--primary-blue-hover);
  border-color: var(--primary-blue-hover);
  color: white;
}

.text-primary-custom {
  color: var(--primary-blue) !important;
}

.bg-primary-custom {
  background-color: var(--primary-blue) !important;
}

.border-primary-custom {
  border-color: var(--primary-blue) !important;
}

.library-container {
  width: 1920px;
  height: 1080px;
  margin: 0 auto;
}

.shadow-custom {
  box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075) !important;
}

.rounded-custom {
  border-radius: 0.375rem !important;
}
</style>