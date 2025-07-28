import 'package:flutter/material.dart';
import 'package:login_app/screens/display_token_screen.dart';
import 'package:login_app/services/auth_service.dart';
import 'package:login_app/widgets/auth_page_layout.dart';
import 'package:login_app/widgets/custom_auth_button.dart';
import 'package:login_app/widgets/custom_textfield.dart';


/// A screen where users can request a password reset token by providing their email.
class ForgotPasswordScreen extends StatefulWidget {
  const ForgotPasswordScreen({super.key});

  @override
  State<ForgotPasswordScreen> createState() => _ForgotPasswordScreenState();
}

class _ForgotPasswordScreenState extends State<ForgotPasswordScreen> {
  final _emailController = TextEditingController();
  bool _isLoading = false;
  final AuthService _authService = AuthService();

  /// Handles the request to send a password reset token.
  Future<void> _sendResetToken() async {
    setState(() { _isLoading = true; });

    try {
      // Call AuthService to get the real token from the API
      final String token = await _authService.forgotPassword(_emailController.text);
      
      if (mounted) {
        // On success, navigate to the next screen with the real token from the API
        Navigator.of(context).push(MaterialPageRoute(
          builder: (context) => DisplayTokenScreen(
            resetToken: token, 
          ),
        ));
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text(e.toString()), backgroundColor: Colors.red),
        );
      }
    } finally {
      if (mounted) {
        setState(() { _isLoading = false; });
      }
    }
  }

  @override
  void dispose() {
    _emailController.dispose();
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
            'Reset Password',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 32,
              fontWeight: FontWeight.bold,
              color: Color(0xFF3D3D3D),
            ),
          ),
          const SizedBox(height: 10),
          const Text(
            'Enter your email to receive a reset token',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 16,
              color: Color(0xFF5A5A5A),
            ),
          ),
          const SizedBox(height: 40),

          // --- Input Field ---
          CustomTextField(
            controller: _emailController,
            hintText: 'Email Address',
            icon: Icons.email_outlined,
            keyboardType: TextInputType.emailAddress,
          ),
          const SizedBox(height: 30),

          // --- Button ---
          CustomAuthButton(
            label: 'Send Reset Token',
            isLoading: _isLoading,
            onPressed: _sendResetToken,
            backgroundColor: const Color(0xFF5f4b8b),
            foregroundColor: Colors.white,
          ),
          const SizedBox(height: 20),

          // --- DÜZELTİLMİŞ KISIM BURASI ---
          // Bu buton artık doğru metni gösteriyor ve pop() ile bir önceki sayfaya dönüyor.
          TextButton(
            onPressed: () {
              Navigator.of(context).pop();
            },
            child: const Text(
              'Remember your password? Sign In',
              style: TextStyle(
                color: Color(0xFF5f4b8b),
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
        ],
      ),
    );
  }
}