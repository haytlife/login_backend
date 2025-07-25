import React from "react";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import "./App.css";
import Login from "./comp/Login/Login.jsx";
import NotFound from "./comp/Login/NotFound.jsx";
import { useAuth } from "./context/AuthContext.jsx";

// Dashboard olmadan basit bir component
const SimpleDashboard = () => {
  const { user, logout } = useAuth();

  const handleLogout = () => {
    logout();
    window.location.href = '/login';
  };

  return (
    <div style={{ 
      padding: '40px', 
      textAlign: 'center', 
      fontFamily: 'Arial, sans-serif',
      backgroundColor: '#f0f8ff',
      minHeight: '100vh'
    }}>
      <h1 style={{ color: '#333', marginBottom: '30px' }}>🎉 Başarılı Giriş!</h1>
      <div style={{
        backgroundColor: 'white',
        padding: '40px',
        borderRadius: '15px',
        boxShadow: '0 4px 15px rgba(0,0,0,0.1)',
        maxWidth: '600px',
        margin: '0 auto'
      }}>
        <h2 style={{ color: '#28a745', marginBottom: '20px' }}>Hoş geldiniz!</h2>
        <p style={{ fontSize: '18px', color: '#666', marginBottom: '30px' }}>
          {user?.email ? `${user.email} olarak giriş yaptınız` : 'Başarıyla giriş yaptınız'}
        </p>
        
        <div style={{ marginBottom: '30px' }}>
          <p><strong>👤 Kullanıcı:</strong> {user?.firstName || user?.email || 'Kullanıcı'}</p>
          <p><strong>🔒 Rol:</strong> {user?.role || 'User'}</p>
          <p><strong>✅ Durum:</strong> <span style={{ color: '#28a745' }}>Aktif</span></p>
        </div>

        <button 
          onClick={handleLogout}
          style={{
            backgroundColor: '#dc3545',
            color: 'white',
            border: 'none',
            padding: '15px 30px',
            borderRadius: '8px',
            cursor: 'pointer',
            fontSize: '16px',
            fontWeight: 'bold'
          }}
        >
          🚪 Çıkış Yap
        </button>
      </div>
    </div>
  );
};

function App() {
  const { user } = useAuth();

  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={user ? <Navigate to="/dashboard" /> : <Navigate to="/login" />}
        />
        <Route
          path="/dashboard"
          element={user ? <SimpleDashboard /> : <Navigate to="/login" />}
        />
        <Route path="/login" element={<Login />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
