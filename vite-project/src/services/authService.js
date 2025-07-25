// Auth Service - .NET Backend ile entegrasyon
import { ApiErrorHandler, DebugHelper } from '../utils/errorHandler.js';

const API_BASE_URL = window.location.hostname === 'localhost' 
  ? 'http://localhost:5000/api'  // Development veya Docker localhost için (Port 5000'e güncellendi)
  : '/api';  // Production nginx proxy için

// Debug helper function
const logRequest = (endpoint, payload) => {
  DebugHelper.logApiCall('POST', `${API_BASE_URL}${endpoint}`, payload);
};

// Common request function with better error handling
const makeRequest = async (endpoint, options = {}) => {
  const startTime = new Date();
  
  try {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      ...options,
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        ...options.headers
      }
    });

    let data;
    try {
      data = await response.json();
    } catch (jsonError) {
      DebugHelper.logError('JSON Parse', jsonError);
      throw new Error(`Invalid JSON response from server (${response.status})`);
    }

    DebugHelper.logApiResponse(`${API_BASE_URL}${endpoint}`, response.status, data);

    if (!response.ok) {
      // 400 Bad Request için özel işlem
      if (response.status === 400) {
        throw new Error(ApiErrorHandler.handle400Error(data.message));
      }
      // Diğer HTTP hataları için genel işlem
      throw new Error(ApiErrorHandler.handleHttpError(response.status, data.message));
    }

    return { success: true, data, response };
  } catch (error) {
    DebugHelper.logError(`API Request ${endpoint}`, error);
    
    // Network hataları için özel işlem
    if (error.name === 'TypeError' || error.message.includes('fetch')) {
      throw new Error(ApiErrorHandler.handleNetworkError(error));
    }
    
    throw error;
  }
};

export const authService = {
  // Kullanıcı kayıt
  async register(userData) {
    try {
      const payload = {
        email: userData.email,
        username: userData.username,
        phoneNumber: userData.phoneNumber,
        password: userData.password,
        confirmPassword: userData.confirmPassword
      };
      
      logRequest('/auth/register', payload);
      
      const { data } = await makeRequest('/auth/register', {
        method: 'POST',
        body: JSON.stringify(payload)
      });

      return {
        success: true,
        data: data.data,
        message: data.message || 'Kayıt başarılı'
      };
    } catch (error) {
      return {
        success: false,
        message: error.message || 'Bir hata oluştu'
      };
    }
  },

  // Kullanıcı giriş
  async login(credentials) {
    try {
      const payload = {
        email: credentials.email,
        password: credentials.password
      };
      
      logRequest('/auth/login', payload);
      
      const { data } = await makeRequest('/auth/login', {
        method: 'POST',
        body: JSON.stringify(payload)
      });

      return {
        success: true,
        token: data.token,
        user: data.user,
        message: data.message || 'Giriş başarılı'
      };
    } catch (error) {
      return {
        success: false,
        message: error.message || 'Bir hata oluştu'
      };
    }
  },

  // Şifre sıfırlama token isteme
  async forgotPassword(email) {
    try {
      const payload = { email };
      
      logRequest('/auth/forgot-password', payload);
      
      const { data } = await makeRequest('/auth/forgot-password', {
        method: 'POST',
        body: JSON.stringify(payload)
      });

      return {
        success: true,
        data: data.resetToken, // Backend'den gelen resetToken'ı al
        message: data.message || 'Token gönderildi'
      };
    } catch (error) {
      return {
        success: false,
        message: error.message || 'Bir hata oluştu'
      };
    }
  },

  // Şifre sıfırlama
  async resetPassword({ email, token, newPassword, confirmPassword }) {
    try {
      // 1. Payload Validasyonu ve Tip Kontrolü
      const payload = {
        email: String(email || '').toLowerCase().trim(),
        token: String(token || '').trim(),
        newPassword: String(newPassword || '').trim(),
        confirmPassword: String(confirmPassword || '').trim()
      };

      // 2. Zorunlu Alan Kontrolü
      const requiredFields = ['email', 'token', 'newPassword', 'confirmPassword'];
      for (const field of requiredFields) {
        if (!payload[field]) {
          throw new Error(`${field} is required and cannot be empty`);
        }
      }

      // 3. Email Format Kontrolü
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(payload.email)) {
        throw new Error('Invalid email format');
      }

      // 4. Şifre Eşleştirme Kontrolü
      if (payload.newPassword !== payload.confirmPassword) {
        throw new Error('New password and confirm password do not match');
      }

      // 5. Debug Log (Development için)
      logRequest('/auth/reset-password', {
        email: payload.email,
        token: payload.token.substring(0, 10) + '...',
        newPasswordLength: payload.newPassword.length,
        confirmPasswordLength: payload.confirmPassword.length
      });

      // 6. API İsteği (makeRequest helper kullanarak)
      const { data } = await makeRequest('/auth/reset-password', {
        method: 'POST',
        body: JSON.stringify(payload)
      });

      return {
        success: true,
        data: data.data,
        message: data.message || 'Şifre başarıyla sıfırlandı'
      };
    } catch (error) {
      return {
        success: false,
        message: error.message || 'Bir hata oluştu'
      };
    }
  },

  // Token doğrulama
  async verifyToken(token) {
    try {
      const { response } = await makeRequest('/auth/verify', {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
        }
      });

      return response.ok;
    } catch (error) {
      return false;
    }
  }
};
