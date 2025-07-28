import 'package:flutter/material.dart';
import 'package:login_app/models/user_model.dart';
import 'package:login_app/screens/home_screen.dart';
import 'package:login_app/screens/register_screen.dart';
import 'package:login_app/services/auth_service.dart';
import 'package:login_app/widgets/auth_page_layout.dart';
import 'package:login_app/widgets/custom_auth_button.dart';
import 'package:login_app/widgets/custom_textfield.dart';
import 'package:login_app/screens/forgot_password_screen.dart';

/// A screen that provides a user interface for user authentication.
/// It is built using reusable widgets for a clean and maintainable structure.
class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  bool _isLoading = false;
  final AuthService _authService = AuthService();

  /// Handles the login process.
  Future<void> _login() async {
    setState(() { _isLoading = true; });
    try {
      // Call the login method and get the user object in return
      final User user = await _authService.login(
        _emailController.text,
        _passwordController.text,
      );
      // Use mounted check to avoid calling setState on unmounted widget
      if (!mounted) return;
      // Pass the user object to the HomeScreen
      Navigator.of(context).pushReplacement(
        MaterialPageRoute(builder: (context) => HomeScreen(user: user)),
      );
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Login failed: ${e.toString()}'),
          backgroundColor: Colors.red.withOpacity(0.9),
        ),
      );
    } finally {
      if (mounted) {
        setState(() { _isLoading = false; });
      }
    }
  }

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // The entire page layout is managed by the AuthPageLayout widget.
    return AuthPageLayout(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          // --- Header Section ---
          const Text(
            'Welcome!',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 32,
              fontWeight: FontWeight.bold,
              color: Color(0xFF3D3D3D),
            ),
          ),
          const SizedBox(height: 10),
          const Text(
            'Sign in to continue',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 16,
              color: Color(0xFF5A5A5A),
            ),
          ),
          const SizedBox(height: 40),

          // --- Input Fields (using custom widgets) ---
          CustomTextField(
            controller: _emailController,
            hintText: 'Email',
            icon: Icons.email_outlined,
            keyboardType: TextInputType.emailAddress,
          ),
          const SizedBox(height: 20),
          CustomTextField(
            controller: _passwordController,
            hintText: 'Password',
            icon: Icons.lock_open_outlined,
            obscureText: true,
          ),
          const SizedBox(height: 30),
          // --- Forgot Password Link ---
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              TextButton(
                onPressed: () {
                  // Navigate to the ForgotPasswordScreen
                  Navigator.of(context).push(
                    MaterialPageRoute(
                      builder: (context) => const ForgotPasswordScreen(),
                    ),
                  );
                },
                child: const Text(
                  'Forgot your password?',
                  style: TextStyle(
                    color: Color(0xFF5A5A5A),
                    fontWeight: FontWeight.w600,
                  ),
                ),
              ),
            ],
          ),

          // --- Login Button (using custom widget) ---
          CustomAuthButton(
            label: 'Sign In',
            isLoading: _isLoading,
            onPressed: _login,
            backgroundColor: const Color(0xFF5f4b8b),
            foregroundColor: Colors.white,
          ),
          const SizedBox(height: 20),

          // --- Register Link ---
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Text(
                "Don't have an account?",
                style: TextStyle(color: Color(0xFF5A5A5A)),
              ),
              TextButton(
                onPressed: () {
                  Navigator.of(context).push(
                    MaterialPageRoute(builder: (context) => const RegisterScreen()),
                  );
                },
                child: const Text(
                  'Sign Up',
                  style: TextStyle(
                    color: Color(0xFF5f4b8b),
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
