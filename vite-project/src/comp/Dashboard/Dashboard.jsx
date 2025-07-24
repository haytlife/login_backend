import React from 'react';
import { useAuth } from '../../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import './Dashboard.css';

const Dashboard = () => {
  const { logout, user } = useAuth();
  const navigate = useNavigate();

  const handleLogout = async () => {
    await logout();
    navigate('/');
  };

  return (
    <div className="dashboard-container">
      {/* Header */}
      <header className="dashboard-header">
        <div className="header-content">
          <h1 className="logo">Dashboard</h1>
          <div className="user-section">
            <span className="welcome-text">Hoş geldiniz, {user?.userName || user?.email || 'Kullanıcı'}!</span>
            <button onClick={handleLogout} className="logout-btn">
              <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/>
                <polyline points="16,17 21,12 16,7"/>
                <line x1="21" y1="12" x2="9" y2="12"/>
              </svg>
              Çıkış Yap
            </button>
          </div>
        </div>
      </header>

      {/* Main Content */}
      <main className="dashboard-main">
        <div className="dashboard-content">
          {/* Welcome Card */}
          <div className="welcome-card">
            <div className="card-header">
              <h2>Hoş Geldiniz! 🎉</h2>
              <p>Başarıyla giriş yaptınız. Panel üzerinden işlemlerinizi gerçekleştirebilirsiniz.</p>
            </div>
          </div>

          {/* Stats Cards */}
          <div className="stats-grid">
            <div className="stat-card">
              <div className="stat-icon blue">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                  <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
                  <circle cx="12" cy="7" r="4"/>
                </svg>
              </div>
              <div className="stat-info">
                <h3>Profil</h3>
                <p>Hesap bilgilerinizi görüntüleyin</p>
              </div>
            </div>

            <div className="stat-card">
              <div className="stat-icon green">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                  <rect x="2" y="3" width="20" height="14" rx="2" ry="2"/>
                  <line x1="8" y1="21" x2="16" y2="21"/>
                  <line x1="12" y1="17" x2="12" y2="21"/>
                </svg>
              </div>
              <div className="stat-info">
                <h3>Projeler</h3>
                <p>Aktif projelerinizi yönetin</p>
              </div>
            </div>

            <div className="stat-card">
              <div className="stat-icon purple">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                  <line x1="12" y1="1" x2="12" y2="23"/>
                  <path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/>
                </svg>
              </div>
              <div className="stat-info">
                <h3>Finans</h3>
                <p>Mali durumunuzu kontrol edin</p>
              </div>
            </div>

            <div className="stat-card">
              <div className="stat-icon orange">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                  <path d="M9 19c-5 0-8-3-8-8s3-8 8-8 8 3 8 8-3 8-8 8"/>
                  <path d="M17.7 7.7l-4.6 7.6"/>
                  <path d="M21 12h-7"/>
                </svg>
              </div>
              <div className="stat-info">
                <h3>Ayarlar</h3>
                <p>Sistem ayarlarını düzenleyin</p>
              </div>
            </div>
          </div>

          {/* User Info Section */}
          <div className="user-info-section">
            <h3>Hesap Bilgileri</h3>
            <div className="user-info-grid">
              <div className="info-item">
                <label>Email:</label>
                <span>{user?.email || 'Bulunamadı'}</span>
              </div>
              <div className="info-item">
                <label>Kullanıcı Adı:</label>
                <span>{user?.userName || 'Bulunamadı'}</span>
              </div>
              <div className="info-item">
                <label>Telefon:</label>
                <span>{user?.phoneNumber || 'Bulunamadı'}</span>
              </div>
              <div className="info-item">
                <label>Giriş Durumu:</label>
                <span className="status-active">Aktif</span>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
};

export default Dashboard;
