import { useNavigate } from "react-router-dom";

export default function NotFound() {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100 text-center">
      <h1 className="text-6xl font-bold text-indigo-600 mb-4">404</h1>
      <p className="text-xl mb-6">Üzgünüz, sayfa bulunamadı.</p>
      <button
        onClick={() => navigate("/")}
        className="text-indigo-600 hover:underline text-lg"
      >
        Ana sayfaya dön
      </button>
    </div>
  );
}
