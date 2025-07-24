import React, { createContext, useState, useContext, useEffect } from "react";
import { authService } from "../services/authService";

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(() => {
    const storedUser = localStorage.getItem("user");
    return storedUser ? JSON.parse(storedUser) : null;
  });
  const [token, setToken] = useState(() => localStorage.getItem("token") || null);
  const [loading, setLoading] = useState(false);

  // Giriş işlemi
  const login = async (credentials) => {
    setLoading(true);
    try {
      const result = await authService.login(credentials);
      
      if (result.success) {
        setUser(result.user);
        setToken(result.token);
        localStorage.setItem("user", JSON.stringify(result.user));
        localStorage.setItem("token", result.token);
        return { success: true, message: result.message };
      } else {
        return { success: false, message: result.message };
      }
    } catch (error) {
      return { success: false, message: "Bir hata oluştu" };
    } finally {
      setLoading(false);
    }
  };

  // Kayıt işlemi
  const register = async (userData) => {
    setLoading(true);
    try {
      const result = await authService.register(userData);
      return result;
    } catch (error) {
      return { success: false, message: "Bir hata oluştu" };
    } finally {
      setLoading(false);
    }
  };

  // Çıkış işlemi
  const logout = () => {
    setUser(null);
    setToken(null);
    localStorage.removeItem("user");
    localStorage.removeItem("token");
  };

  // Şifre sıfırlama token isteme
  const forgotPassword = async (email) => {
    setLoading(true);
    try {
      const result = await authService.forgotPassword(email);
      return result;
    } catch (error) {
      return { success: false, message: "Bir hata oluştu" };
    } finally {
      setLoading(false);
    }
  };

  // Şifre sıfırlama
  const resetPassword = async ({ email, token, newPassword, confirmPassword }) => {
    setLoading(true);
    try {
      // Tip kontrolü ve temizleme
      const cleanData = {
        email: String(email || '').trim(),
        token: String(token || '').trim(), 
        newPassword: String(newPassword || '').trim(),
        confirmPassword: String(confirmPassword || '').trim()
      };
      
      // Debug log
      if (process.env.NODE_ENV === 'development') {
        console.log('AuthContext resetPassword cleanData:', cleanData);
      }
      
      const result = await authService.resetPassword(cleanData);
      
      // Debug log
      if (process.env.NODE_ENV === 'development') {
        console.log('AuthContext resetPassword result:', result);
      }
      
      return result;
    } catch (error) {
      console.error('AuthContext resetPassword error:', error);
      return { 
        success: false, 
        message: error.message || "Password reset failed. Please try again." 
      };
    } finally {
      setLoading(false);
    }
  };

  // Token doğrulama
  useEffect(() => {
    const verifyTokenOnLoad = async () => {
      if (token && !user) {
        const isValid = await authService.verifyToken(token);
        if (!isValid) {
          logout();
        }
      }
    };

    verifyTokenOnLoad();
  }, [token, user]);

  return (
    <AuthContext.Provider value={{ 
      user, 
      token, 
      loading,
      login, 
      register,
      logout,
      forgotPassword,
      resetPassword
    }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}
