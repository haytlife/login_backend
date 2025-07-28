import React from "react";
import { useNavigate } from "react-router-dom";
import "./Dashboard.css";

export default function Dashboard({ user, onLogout }) {
  const navigate = useNavigate();

  const handleLogout = () => {
    onLogout();
    navigate("/login");
  };

  return (
    <div className="dashboard-container">
      <div className="dashboard-box">
        <h1 className="welcome-title">🎉 Hoşgeldiniz</h1>
        <h2 className="user-name">{user.firstName}</h2>

        <p className="welcome-text">Giriş başarıyla gerçekleştirildi.</p>

        <div className="user-info-column">
          <div className="info-card">
            <span className="label">Ad Soyad</span>
            <span className="value">{user.firstName}</span>
          </div>
          <div className="info-card">
            <span className="label">Email</span>
            <span className="value">{user.email}</span>
          </div>
          <div className="info-card">
            <span className="label">Telefon</span>
            <span className="value">{user.phoneNumber || "-"}</span>
          </div>
        </div>

        <button className="logout-btn" onClick={handleLogout}>
          Çıkış Yap
        </button>
      </div>
    </div>
  );
}
