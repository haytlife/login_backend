import 'package:flutter/material.dart';
import 'package:login_app/services/auth_service.dart';
import 'package:login_app/widgets/auth_page_layout.dart';
import 'package:login_app/widgets/custom_auth_button.dart';
import 'package:login_app/widgets/custom_textfield.dart';

/// A screen that provides a user interface for new user registration.
/// It is built using reusable widgets for a clean and maintainable structure.
class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  // Controllers updated according to the new registration requirements.
  final _emailController = TextEditingController();
  final _usernameController = TextEditingController();
  final _phoneNumberController = TextEditingController();
  final _passwordController = TextEditingController();
  final _confirmPasswordController = TextEditingController();
  
  bool _isLoading = false;
  final AuthService _authService = AuthService();

  /// Handles the registration process.
  Future<void> _register() async {
    if (_passwordController.text != _confirmPasswordController.text) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Passwords do not match!'),
          backgroundColor: Colors.orange,
        ),
      );
      return;
    }
    setState(() { _isLoading = true; });
    try {
      // Call the updated register method in AuthService.
      await _authService.register(
        email: _emailController.text,
        username: _usernameController.text,
        phoneNumber: _phoneNumberController.text,
        password: _passwordController.text,
        confirmPassword: _confirmPasswordController.text,
      );
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Registration successful! Please sign in.'),
          backgroundColor: Colors.green,
        ),
      );
      Navigator.of(context).pop();
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Registration failed: ${e.toString()}'),
          backgroundColor: Colors.red,
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
    _usernameController.dispose();
    _phoneNumberController.dispose();
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AuthPageLayout(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          // --- Header Section ---
          const Text(
            'Create a new account',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 32,
              fontWeight: FontWeight.bold,
              color: Color(0xFF3D3D3D),
            ),
          ),
          const SizedBox(height: 30),

          // --- Input Fields (Updated to match the new design) ---
          CustomTextField(controller: _emailController, hintText: 'Email Address', icon: Icons.email_outlined, keyboardType: TextInputType.emailAddress),
          const SizedBox(height: 16),
          CustomTextField(controller: _usernameController, hintText: 'Username', icon: Icons.account_circle_outlined),
          const SizedBox(height: 16),
          CustomTextField(controller: _phoneNumberController, hintText: 'Contact Number', icon: Icons.phone_outlined, keyboardType: TextInputType.phone),
          const SizedBox(height: 16),
          CustomTextField(controller: _passwordController, hintText: 'Password', icon: Icons.lock_open_outlined, obscureText: true),
          const SizedBox(height: 16),
          CustomTextField(controller: _confirmPasswordController, hintText: 'Confirm Password', icon: Icons.lock_outline, obscureText: true),
          const SizedBox(height: 30),

          // --- Register Button ---
          CustomAuthButton(
            label: 'Sign Up',
            isLoading: _isLoading,
            onPressed: _register,
            backgroundColor: const Color(0xFF5f4b8b),
            foregroundColor: Colors.white,
          ),
          const SizedBox(height: 16),

          // --- Login Link ---
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Text(
                "Already have an account?",
                style: TextStyle(color: Color(0xFF5A5A5A)),
              ),
              TextButton(
                onPressed: () {
                  Navigator.of(context).pop();
                },
                child: const Text(
                  'Sign In',
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
