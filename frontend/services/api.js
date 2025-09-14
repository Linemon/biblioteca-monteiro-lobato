const API_BASE_URL = 'http://localhost:5088/api/v1'

class ApiService {
  constructor() {
    this.baseURL = API_BASE_URL
    this.token = localStorage.getItem('accessToken')
  }

  setToken(token) {
    this.token = token
    localStorage.setItem('accessToken', token)
  }

  clearToken() {
    this.token = null
    localStorage.removeItem('accessToken')
  }

  async request(endpoint, options = {}) {
    const url = `${this.baseURL}${endpoint}`
    const config = {
      headers: {
        'Content-Type': 'application/json',
        ...options.headers
      },
      ...options
    }

    if (this.token) {
      config.headers.Authorization = `Bearer ${this.token}`
    }

    try {
      const response = await fetch(url, config)
      
      if (!response.ok) {
        if (response.status === 401) {
          this.clearToken()
          const errorData = await response.json().catch(() => ({}))
          throw new Error(errorData.message || 'Credenciais inválidas.')
        }
        const errorData = await response.json().catch(() => ({}))
        throw new Error(errorData.message || `Erro ${response.status}: ${response.statusText}`)
      }

      return await response.json()
    } catch (error) {
      console.error('Erro na requisição:', error)
      throw error
    }
  }

  // Auth endpoints
  async login(email, password) {
    return this.request('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ email, password })
    })
  }

  async refreshToken(refreshToken) {
    return this.request('/auth/refresh', {
      method: 'POST',
      body: JSON.stringify({ refreshToken })
    })
  }

  async logout() {
    try {
      await this.request('/auth/logout', { method: 'POST' })
    } finally {
      this.clearToken()
    }
  }

  async getCurrentUser() {
    return this.request('/auth/me')
  }

  // Readers endpoints
  async getReaders() {
    return this.request('/readers')
  }

  async getReaderById(id) {
    return this.request(`/readers/${id}`)
  }

  async createReader(readerData) {
    return this.request('/readers', {
      method: 'POST',
      body: JSON.stringify(readerData)
    })
  }

  async updateReader(id, readerData) {
    return this.request(`/readers/${id}`, {
      method: 'PUT',
      body: JSON.stringify(readerData)
    })
  }

  async deleteReader(id) {
    return this.request(`/readers/${id}`, {
      method: 'DELETE'
    })
  }
}

export default new ApiService()
