import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import "./Login.css";
import "./Tail.css";

const Login = () => {
  const [isSignIn, setIsSignIn] = useState(true);
  const [showForgotPassword, setShowForgotPassword] = useState(false);
  const [forgotPasswordStep, setForgotPasswordStep] = useState(1);
  const [showTokenDisplay, setShowTokenDisplay] = useState(false);
  const [receivedToken, setReceivedToken] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const [showConfirmNewPassword, setShowConfirmNewPassword] = useState(false);
  const [loginSuccess, setLoginSuccess] = useState(false);
  const [formData, setFormData] = useState({
    email: '',
    username: '',
    phoneNumber: '',
    password: '',
    confirmPassword: ''
  });
  const [forgotPasswordData, setForgotPasswordData] = useState({
    email: '',
    token: '',
    newPassword: '',
    confirmPassword: ''
  });
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const { login, register, forgotPassword, resetPassword, loading } = useAuth();
  const navigate = useNavigate();

  // Form sıfırlama
  useEffect(() => {
    setFormData({
      email: '',
      username: '',
      phoneNumber: '',
      password: '',
      confirmPassword: ''
    });
    setForgotPasswordData({
      email: '',
      token: '',
      newPassword: '',
      confirmPassword: ''
    });
    setError('');
    setSuccess('');
    setForgotPasswordStep(1);
    setShowTokenDisplay(false);
    setReceivedToken('');
    setShowPassword(false);
    setShowConfirmPassword(false);
    setShowNewPassword(false);
    setShowConfirmNewPassword(false);
  }, [isSignIn, showForgotPassword]);

  const handleInputChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
    setError('');
    setSuccess('');
  };

  const handleForgotPasswordInputChange = (e) => {
    setForgotPasswordData({
      ...forgotPasswordData,
      [e.target.name]: e.target.value
    });
    setError('');
    setSuccess('');
  };

  const validatePassword = (password) => {
    const minLength = 8;
    const hasUpperCase = /[A-Z]/.test(password);
    const hasLowerCase = /[a-z]/.test(password);
    const hasNumbers = /\d/.test(password);
    const hasSpecialChar = /[@$!%*?&]/.test(password);

    if (password.length < minLength) {
      return 'Password must be at least 8 characters long';
    }
    if (!hasUpperCase) {
      return 'Password must contain at least 1 uppercase letter';
    }
    if (!hasLowerCase) {
      return 'Password must contain at least 1 lowercase letter';
    }
    if (!hasNumbers) {
      return 'Password must contain at least 1 number';
    }
    if (!hasSpecialChar) {
      return 'Password must contain at least 1 special character (@$!%*?&)';
    }
    return null;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    if (isSignIn) {
      if (!formData.email || !formData.password) {
        setError('Email and password are required');
        return;
      }

      try {
        const result = await login({
          email: formData.email,
          password: formData.password
        });

        if (result && result.success) {
          setSuccess(result.message || 'Login successful!');
          setLoginSuccess(true);
          setTimeout(() => {
            navigate('/dashboard');
          }, 2000);
        } else {
          setError(result?.message || 'Login failed');
        }
      } catch (err) {
        setError('An error occurred during login');
        console.error('Login error:', err);
      }
    } else {
      if (!formData.email || !formData.username || !formData.phoneNumber || !formData.password || !formData.confirmPassword) {
        setError('All fields are required');
        return;
      }

      if (formData.password !== formData.confirmPassword) {
        setError('Passwords do not match');
        return;
      }

      const passwordError = validatePassword(formData.password);
      if (passwordError) {
        setError(passwordError);
        return;
      }

      try {
        const result = await register(formData);

        if (result && result.success) {
          setSuccess(result.message + ' You can now sign in.');
          setIsSignIn(true);
          setFormData({
            email: '',
            username: '',
            phoneNumber: '',
            password: '',
            confirmPassword: ''
          });
        } else {
          setError(result?.message || 'Registration failed');
        }
      } catch (err) {
        setError('An error occurred during registration');
        console.error('Registration error:', err);
      }
    }
  };

  const handleForgotPasswordStep1Submit = async (e) => {
    e.preventDefault();
    setError('');

    if (!forgotPasswordData.email) {
      setError('Email is required');
      return;
    }

    try {
      const result = await forgotPassword(forgotPasswordData.email);

      if (result && result.success) {
        // Backend'den gelen token'ı al
        const token = result.data || result.token || result.resetToken;
        setReceivedToken(token || 'Token received');
        setShowTokenDisplay(true);
        setForgotPasswordStep(2);
      } else {
        setError(result?.message || 'Failed to send reset token');
      }
    } catch (err) {
      setError('An error occurred while sending reset token');
      console.error('Forgot password error:', err);
    }
  };

  const copyTokenToClipboard = () => {
    if (receivedToken) {
      navigator.clipboard.writeText(receivedToken);
      setSuccess('Token copied to clipboard!');
      setTimeout(() => setSuccess(''), 2000);
    }
  };

  const proceedToResetForm = () => {
    setShowTokenDisplay(false);
    setForgotPasswordStep(3);
  };

  const handleForgotPasswordStep2Submit = async (e) => {
    e.preventDefault();
    setError('');

    if (!forgotPasswordData.email || !forgotPasswordData.token || !forgotPasswordData.newPassword || !forgotPasswordData.confirmPassword) {
      setError('All fields are required');
      return;
    }

    if (forgotPasswordData.newPassword !== forgotPasswordData.confirmPassword) {
      setError('Passwords do not match');
      return;
    }

    const passwordError = validatePassword(forgotPasswordData.newPassword);
    if (passwordError) {
      setError(passwordError);
      return;
    }

    try {
      const result = await resetPassword(forgotPasswordData.token, forgotPasswordData.newPassword);

      if (result && result.success) {
        setSuccess(result.message || 'Password reset successful!');
        setTimeout(() => {
          setShowForgotPassword(false);
          setIsSignIn(true);
          setForgotPasswordStep(1);
          setForgotPasswordData({ email: '', token: '', newPassword: '', confirmPassword: '' });
          setError('');
          setSuccess('');
          setShowTokenDisplay(false);
          setReceivedToken('');
        }, 2000);
      } else {
        setError(result?.message || 'Password reset failed');
      }
    } catch (err) {
      setError('An error occurred during password reset');
      console.error('Reset password error:', err);
    }
  };

  return (
    <div className="login-container">
      {loginSuccess ? (
        // Küçük Hoş Geldiniz Ekranı - Şeffaf Arka Plan
        <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
          <div className="bg-white rounded-xl shadow-lg p-6 text-center max-w-xs mx-4 animate-pulse">
            <div className="mb-4">
              <div className="w-12 h-12 bg-green-500 rounded-full mx-auto mb-3 flex items-center justify-center">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="white" strokeWidth="2">
                  <polyline points="20,6 9,17 4,12"/>
                </svg>
              </div>
              <h2 className="text-lg font-bold text-gray-800 mb-2">Hoş Geldiniz!</h2>
              <p className="text-sm text-gray-600">Giriş başarılı</p>
              <p className="text-xs text-gray-500 mt-2">Yönlendiriliyor...</p>
            </div>
          </div>
        </div>
      ) : (
        <>
          <img
            src="https://www.tercihyazilim.com/Upload/8292/Images/web-site-nedir.jpg?width=1024"
            alt="Login Illustration"
            style={{
              width: "400px",
              height: "auto",
              borderRadius: "20px",
              boxShadow: "0 10px 20px rgba(0,0,0,0.1)",
            }}
          />

          <div className="box shadow-xl transition-all duration-500">
            <div className="text-right text-sm mb-4">
              {!showForgotPassword ? (
                isSignIn ? (
                  <>
                    <span className="text-gray-600">Don't have an account? </span>
                    <a
                      href="#"
                      className="text-blue-600 hover:underline font-medium"
                      onClick={() => setIsSignIn(false)}
                    >
                      Sign Up
                    </a>
                  </>
                ) : (
                  <>
                    <span className="text-gray-600">Already have an account? </span>
                    <a
                      href="#"
                      className="text-blue-600 hover:underline font-medium"
                      onClick={() => setIsSignIn(true)}
                    >
                      Sign In
                    </a>
                  </>
                )
              ) : (
                <>
                  <span className="text-gray-600">Remember your password? </span>
                  <a
                    href="#"
                    className="text-blue-600 hover:underline font-medium"
                    onClick={() => {
                      setShowForgotPassword(false);
                      setIsSignIn(true);
                    }}
                  >
                    Sign In
                  </a>
                </>
              )}
            </div>

            <h2 className="text-3xl font-bold text-center mb-1">Welcome</h2>
            <h1 className="text-xl text-center mb-6 text-gray-600">
              {showForgotPassword 
                ? forgotPasswordStep === 1 
                  ? "Reset your password" 
                  : showTokenDisplay
                    ? "Your reset token"
                    : "Enter new password"
                : isSignIn 
                  ? "Sign In to your account" 
                  : "Create a new account"
              }
            </h1>

            {showForgotPassword ? (
              <>
                {forgotPasswordStep === 1 ? (
                  <form onSubmit={handleForgotPasswordStep1Submit} className="space-y-4">
                    {error && (
                      <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
                        {error}
                      </div>
                    )}
                    
                    {success && (
                      <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded">
                        {success}
                      </div>
                    )}

                    <div>
                      <label htmlFor="resetEmail" className="block mb-1 font-medium text-gray-700">
                        Email Address
                      </label>
                      <input
                        type="email"
                        id="resetEmail"
                        name="email"
                        value={forgotPasswordData.email}
                        onChange={handleForgotPasswordInputChange}
                        className="input-style"
                        placeholder="Enter your email address"
                        required
                      />
                    </div>

                    <button
                      type="submit"
                      disabled={loading}
                      className={`btn-submit hover:scale-105 transition-transform ${loading ? 'opacity-50 cursor-not-allowed' : ''}`}
                    >
                      {loading ? 'Sending...' : 'Send Reset Token'}
                    </button>
                  </form>
                ) : forgotPasswordStep === 2 && showTokenDisplay ? (
                  <div className="space-y-4">
                    {success && (
                      <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded">
                        {success}
                      </div>
                    )}

                    <div>
                      <label className="block mb-2 font-medium text-gray-700">
                        Reset Token (Copy this token)
                      </label>
                      <div className="flex items-center space-x-2">
                        <input
                          type="text"
                          value={receivedToken}
                          readOnly
                          className="input-style flex-1 bg-gray-50 cursor-text"
                          onClick={(e) => e.target.select()}
                        />
                        <button
                          type="button"
                          onClick={copyTokenToClipboard}
                          className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors font-medium"
                        >
                          Copy
                        </button>
                      </div>
                      <p className="text-sm text-gray-600 mt-2">
                        Click the token to select all, then copy it. You'll need this token in the next step.
                      </p>
                    </div>

                    <button
                      type="button"
                      onClick={proceedToResetForm}
                      className="btn-submit hover:scale-105 transition-transform"
                    >
                      Continue to Reset Password
                    </button>
                  </div>
                ) : (
                  <form onSubmit={handleForgotPasswordStep2Submit} className="space-y-4">
                    {error && (
                      <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
                        {error}
                      </div>
                    )}
                    
                    {success && (
                      <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded">
                        {success}
                      </div>
                    )}

                    <div>
                      <label htmlFor="resetFormEmail" className="block mb-1 font-medium text-gray-700">
                        Email Address
                      </label>
                      <input
                        type="email"
                        id="resetFormEmail"
                        name="email"
                        value={forgotPasswordData.email}
                        onChange={handleForgotPasswordInputChange}
                        className="input-style"
                        placeholder="Enter your email address"
                        required
                      />
                    </div>

                    <div>
                      <label htmlFor="token" className="block mb-1 font-medium text-gray-700">
                        Reset Token
                      </label>
                      <input
                        type="text"
                        id="token"
                        name="token"
                        value={forgotPasswordData.token}
                        onChange={handleForgotPasswordInputChange}
                        className="input-style"
                        placeholder="Paste the reset token here"
                        required
                      />
                    </div>

                    <div>
                      <label htmlFor="newPassword" className="block mb-1 font-medium text-gray-700">
                        New Password
                      </label>
                      <div className="relative">
                        <input
                          type={showNewPassword ? "text" : "password"}
                          id="newPassword"
                          name="newPassword"
                          value={forgotPasswordData.newPassword}
                          onChange={handleForgotPasswordInputChange}
                          className="input-style pr-12"
                          placeholder="Enter new password"
                          required
                        />
                        <button
                          type="button"
                          onClick={() => setShowNewPassword(!showNewPassword)}
                          className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 hover:text-gray-700 bg-transparent border-none"
                          style={{ backgroundColor: 'transparent' }}
                        >
                          {showNewPassword ? (
                            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                              <path d="M9.88 9.88a3 3 0 1 0 4.24 4.24"/>
                              <path d="m10.73 5.08-1.46.29a11.95 11.95 0 0 0-7.27 7.27l-.29 1.46 2.76 2.76a16.98 16.98 0 0 0 12.93 0l2.76-2.76-.29-1.46a11.95 11.95 0 0 0-7.27-7.27l-1.46-.29-2.76 2.76z"/>
                              <line x1="2" y1="2" x2="22" y2="22"/>
                            </svg>
                          ) : (
                            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                              <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/>
                              <circle cx="12" cy="12" r="3"/>
                            </svg>
                          )}
                        </button>
                      </div>
                    </div>

                    <div>
                      <label htmlFor="confirmNewPassword" className="block mb-1 font-medium text-gray-700">
                        Confirm New Password
                      </label>
                      <div className="relative">
                        <input
                          type={showConfirmNewPassword ? "text" : "password"}
                          id="confirmNewPassword"
                          name="confirmPassword"
                          value={forgotPasswordData.confirmPassword}
                          onChange={handleForgotPasswordInputChange}
                          className="input-style pr-12"
                          placeholder="Confirm new password"
                          required
                        />
                        <button
                          type="button"
                          onClick={() => setShowConfirmNewPassword(!showConfirmNewPassword)}
                          className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 hover:text-gray-700 bg-transparent border-none"
                          style={{ backgroundColor: 'transparent' }}
                        >
                          {showConfirmNewPassword ? (
                            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                              <path d="M9.88 9.88a3 3 0 1 0 4.24 4.24"/>
                              <path d="m10.73 5.08-1.46.29a11.95 11.95 0 0 0-7.27 7.27l-.29 1.46 2.76 2.76a16.98 16.98 0 0 0 12.93 0l2.76-2.76-.29-1.46a11.95 11.95 0 0 0-7.27-7.27l-1.46-.29-2.76 2.76z"/>
                              <line x1="2" y1="2" x2="22" y2="22"/>
                            </svg>
                          ) : (
                            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                              <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/>
                              <circle cx="12" cy="12" r="3"/>
                            </svg>
                          )}
                        </button>
                      </div>
                    </div>

                    <div className="flex gap-2">
                      <button
                        type="button"
                        onClick={() => {
                          setForgotPasswordStep(2);
                          setShowTokenDisplay(true);
                        }}
                        className="flex-1 px-4 py-2 text-gray-600 border border-gray-300 rounded-xl hover:bg-gray-50 transition-colors font-medium"
                      >
                        Back to Token
                      </button>
                      <button
                        type="submit"
                        disabled={loading}
                        className={`flex-1 btn-submit hover:scale-105 transition-transform ${loading ? 'opacity-50 cursor-not-allowed' : ''}`}
                      >
                        {loading ? 'Resetting...' : 'Reset Password'}
                      </button>
                    </div>
                  </form>
                )}
              </>
            ) : (
              <form onSubmit={handleSubmit} className="space-y-4">
                {error && (
                  <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
                    {error}
                  </div>
                )}
                
                {success && (
                  <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded">
                    {success}
                  </div>
                )}

                <div>
                  <label htmlFor="email" className="block mb-1 font-medium text-gray-700">
                    Email Address
                  </label>
                  <input
                    type="email"
                    id="email"
                    name="email"
                    value={formData.email}
                    onChange={handleInputChange}
                    className="input-style"
                    placeholder="Enter your email"
                    required
                  />
                </div>

                {!isSignIn && (
                  <>
                    <div>
                      <label htmlFor="username" className="block mb-1 font-medium text-gray-700">
                        Username
                      </label>
                      <input
                        type="text"
                        id="username"
                        name="username"
                        value={formData.username}
                        onChange={handleInputChange}
                        className="input-style"
                        placeholder="Username"
                        required
                      />
                    </div>

                    <div>
                      <label htmlFor="phoneNumber" className="block mb-1 font-medium text-gray-700">
                        Contact Number
                      </label>
                      <input
                        type="tel"
                        id="phoneNumber"
                        name="phoneNumber"
                        value={formData.phoneNumber}
                        onChange={handleInputChange}
                        className="input-style"
                        placeholder="Phone Number"
                        required
                      />
                    </div>
                  </>
                )}

                <div>
                  <label htmlFor="password" className="block mb-1 font-medium text-gray-700">
                    Password
                  </label>
                  <div className="relative">
                    <input
                      type={showPassword ? "text" : "password"}
                      id="password"
                      name="password"
                      value={formData.password}
                      onChange={handleInputChange}
                      className="input-style pr-12"
                      placeholder="Password"
                      required
                    />
                    <button
                      type="button"
                      onClick={() => setShowPassword(!showPassword)}
                      className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 hover:text-gray-700 bg-transparent border-none"
                      style={{ backgroundColor: 'transparent' }}
                    >
                      {showPassword ? (
                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                          <path d="M9.88 9.88a3 3 0 1 0 4.24 4.24"/>
                          <path d="m10.73 5.08-1.46.29a11.95 11.95 0 0 0-7.27 7.27l-.29 1.46 2.76 2.76a16.98 16.98 0 0 0 12.93 0l2.76-2.76-.29-1.46a11.95 11.95 0 0 0-7.27-7.27l-1.46-.29-2.76 2.76z"/>
                          <line x1="2" y1="2" x2="22" y2="22"/>
                        </svg>
                      ) : (
                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                          <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/>
                          <circle cx="12" cy="12" r="3"/>
                        </svg>
                      )}
                    </button>
                  </div>
                </div>

                {!isSignIn && (
                  <div>
                    <label htmlFor="confirmPassword" className="block mb-1 font-medium text-gray-700">
                      Confirm Password
                    </label>
                    <div className="relative">
                      <input
                        type={showConfirmPassword ? "text" : "password"}
                        id="confirmPassword"
                        name="confirmPassword"
                        value={formData.confirmPassword}
                        onChange={handleInputChange}
                        className="input-style pr-12"
                        placeholder="Confirm Password"
                        required
                      />
                      <button
                        type="button"
                        onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                        className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 hover:text-gray-700 bg-transparent border-none"
                        style={{ backgroundColor: 'transparent' }}
                      >
                        {showConfirmPassword ? (
                          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                            <path d="M9.88 9.88a3 3 0 1 0 4.24 4.24"/>
                            <path d="m10.73 5.08-1.46.29a11.95 11.95 0 0 0-7.27 7.27l-.29 1.46 2.76 2.76a16.98 16.98 0 0 0 12.93 0l2.76-2.76-.29-1.46a11.95 11.95 0 0 0-7.27-7.27l-1.46-.29-2.76 2.76z"/>
                            <line x1="2" y1="2" x2="22" y2="22"/>
                          </svg>
                        ) : (
                          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                            <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/>
                            <circle cx="12" cy="12" r="3"/>
                          </svg>
                        )}
                      </button>
                    </div>
                  </div>
                )}

                <button
                  type="submit"
                  disabled={loading}
                  className={`btn-submit hover:scale-105 transition-transform ${loading ? 'opacity-50 cursor-not-allowed' : ''}`}
                >
                  {loading ? 'Loading...' : (isSignIn ? "Sign In" : "Sign Up")}
                </button>

                {isSignIn && (
                  <div className="text-sm text-center mt-2">
                    <button
                      type="button"
                      onClick={() => setShowForgotPassword(true)}
                      className="text-blue-600 hover:underline hover:bg-blue-50 px-3 py-2 rounded transition-colors font-medium border-none outline-none"
                      style={{ backgroundColor: 'transparent', boxShadow: 'none' }}
                    >
                      Forgot Password?
                    </button>
                  </div>
                )}
              </form>
            )}
          </div>
        </>
      )}
    </div>
  );
};

export default Login;
