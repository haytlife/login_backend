// Error Handler Utility - 400 Bad Request ve diğer API hatalarını yönetir

export const ApiErrorHandler = {
  // 400 Bad Request için özel hata mesajları
  handle400Error: (errorMessage) => {
    const lowerError = errorMessage.toLowerCase();
    
    // Yaygın 400 hata durumları için özel mesajlar
    if (lowerError.includes('validation') || lowerError.includes('invalid')) {
      return 'Please check your input and try again. Some fields may be invalid.';
    }
    
    if (lowerError.includes('email')) {
      return 'Please check your email address format and try again.';
    }
    
    if (lowerError.includes('password')) {
      return 'Password requirements not met. Please ensure your password meets all criteria.';
    }
    
    if (lowerError.includes('token')) {
      return 'Invalid or expired reset token. Please request a new password reset.';
    }
    
    if (lowerError.includes('required')) {
      return 'All required fields must be filled out completely.';
    }
    
    // Varsayılan mesaj
    return errorMessage || 'Request failed. Please check your information and try again.';
  },
  
  // Genel HTTP hata yöneticisi
  handleHttpError: (status, message) => {
    switch (status) {
      case 400:
        return ApiErrorHandler.handle400Error(message);
      case 401:
        return 'Authentication failed. Please check your credentials.';
      case 403:
        return 'Access denied. You do not have permission to perform this action.';
      case 404:
        return 'Resource not found. Please check the URL and try again.';
      case 429:
        return 'Too many requests. Please wait a moment and try again.';
      case 500:
        return 'Server error. Please try again later.';
      case 503:
        return 'Service temporarily unavailable. Please try again later.';
      default:
        return message || `Request failed with status ${status}`;
    }
  },
  
  // Network hataları için
  handleNetworkError: (error) => {
    if (error.name === 'TypeError' && error.message.includes('fetch')) {
      return 'Network connection failed. Please check your internet connection.';
    }
    
    if (error.message.includes('timeout')) {
      return 'Request timed out. Please try again.';
    }
    
    return 'Network error occurred. Please check your connection and try again.';
  }
};

// Debug yardımcı fonksiyonları
export const DebugHelper = {
  logApiCall: (method, url, payload, timestamp = new Date()) => {
    if (process.env.NODE_ENV === 'development') {
      console.group(`🌐 API Call: ${method} ${url}`);
      console.log('📅 Timestamp:', timestamp.toISOString());
      console.log('📦 Payload:', payload);
      console.groupEnd();
    }
  },
  
  logApiResponse: (url, status, data, timestamp = new Date()) => {
    if (process.env.NODE_ENV === 'development') {
      console.group(`📡 API Response: ${status} ${url}`);
      console.log('📅 Timestamp:', timestamp.toISOString());
      console.log('📊 Status:', status);
      console.log('📦 Data:', data);
      console.groupEnd();
    }
  },
  
  logError: (context, error, timestamp = new Date()) => {
    if (process.env.NODE_ENV === 'development') {
      console.group(`❌ Error in ${context}`);
      console.log('📅 Timestamp:', timestamp.toISOString());
      console.log('🚫 Error:', error);
      console.log('📝 Stack:', error.stack);
      console.groupEnd();
    }
  }
};
