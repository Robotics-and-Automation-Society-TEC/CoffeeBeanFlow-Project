import axios from 'axios'

const API_BASE_URL = 'http://localhost:5176/api' // Ajusta el puerto de tu API .NET

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000
})

// Interceptor para manejar errores globalmente
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('Error en API:', error)
    return Promise.reject(error)
  }
)

const apiService = {
  // Métodos genéricos - solo pasas el endpoint
  async crear(endpoint, data) {
    try {
      const response = await apiClient.post(`/${endpoint}`, data)
      return response.data
    } catch (error) {
      throw error.response?.data || error.message
    }
  },

  async obtenerTodos(endpoint) {
    try {
      const response = await apiClient.get(`/${endpoint}`)
      return response.data
    } catch (error) {
      throw error.response?.data || error.message
    }
  },

  async obtenerPorId(endpoint, id) {
    try {
      const response = await apiClient.get(`/${endpoint}/${id}`)
      return response.data
    } catch (error) {
      throw error.response?.data || error.message
    }
  },

  async actualizar(endpoint, id, data) {
    try {
      const response = await apiClient.put(`/${endpoint}/${id}`, data)
      return response.data
    } catch (error) {
      throw error.response?.data || error.message
    }
  },

  async eliminar(endpoint, id) {
    try {
      const response = await apiClient.delete(`/${endpoint}/${id}`)
      return response.data
    } catch (error) {
      throw error.response?.data || error.message
    }
  }
}

export default apiService