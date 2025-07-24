import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

export default function Dashboard() {
  const { user, token, logout } = useAuth();
  const navigate = useNavigate();

  console.log("Kullanıcı:", user);
  console.log("Token:", token);

  useEffect(() => {
    if (!token) {
      navigate("/login");
    }
  }, [token, navigate]);

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  if (!user) {
    return null; // veya yükleniyor gösterebilirsin
  }

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100 p-4">
      <h1 className="text-3xl font-bold mb-4">Hoşgeldin, {user.email}</h1>
      <button
        onClick={handleLogout}
        className="rounded bg-red-600 px-4 py-2 text-white hover:bg-red-700"
      >
        Çıkış Yap
      </button>
    </div>
  );
}
