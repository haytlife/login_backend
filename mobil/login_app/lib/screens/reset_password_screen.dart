import 'package:flutter/material.dart';
import 'package:login_app/services/auth_service.dart'; 
import 'package:login_app/widgets/auth_page_layout.dart';
import 'package:login_app/widgets/custom_auth_button.dart';
import 'package:login_app/widgets/custom_textfield.dart';

/// A screen where users can set their new password using the reset token.
class ResetPasswordScreen extends StatefulWidget {
  final String initialToken;
  const ResetPasswordScreen({super.key, required this.initialToken});

  @override
  State<ResetPasswordScreen> createState() => _ResetPasswordScreenState();
}

class _ResetPasswordScreenState extends State<ResetPasswordScreen> {
  late TextEditingController _tokenController;
  final _emailController = TextEditingController();
  final _newPasswordController = TextEditingController();
  final _confirmPasswordController = TextEditingController();
  bool _isLoading = false;
  final AuthService _authService = AuthService(); 

  @override
  void initState() {
    super.initState();
    _tokenController = TextEditingController(text: widget.initialToken);
  }

  /// Handles the final password reset submission.
  Future<void> _resetPassword() async {
    if (_newPasswordController.text != _confirmPasswordController.text) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Passwords do not match!'), backgroundColor: Colors.orange),
      );
      return;
    }

    setState(() { _isLoading = true; });

    try {
      await _authService.resetPassword(
        email: _emailController.text,
        token: _tokenController.text,
        password: _newPasswordController.text,
        confirmPassword: _confirmPasswordController.text,
      );

      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Password has been reset successfully!'), backgroundColor: Colors.green),
        );
        Navigator.of(context).popUntil((route) => route.isFirst);
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
    _tokenController.dispose();
    _emailController.dispose();
    _newPasswordController.dispose();
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
          const Text(
            'Enter new password',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 32,
              fontWeight: FontWeight.bold,
              color: Color(0xFF3D3D3D),
            ),
          ),
          const SizedBox(height: 30),
          CustomTextField(controller: _emailController, hintText: 'Email Address', icon: Icons.email_outlined, keyboardType: TextInputType.emailAddress),
          const SizedBox(height: 16),
          CustomTextField(controller: _tokenController, hintText: 'Reset Token', icon: Icons.vpn_key_outlined),
          const SizedBox(height: 16),
          CustomTextField(controller: _newPasswordController, hintText: 'New Password', icon: Icons.lock_open_outlined, obscureText: true),
          const SizedBox(height: 16),
          CustomTextField(controller: _confirmPasswordController, hintText: 'Confirm New Password', icon: Icons.lock_outline, obscureText: true),
          const SizedBox(height: 30),
          CustomAuthButton(
            label: 'Reset Password',
            isLoading: _isLoading,
            onPressed: _resetPassword,
            backgroundColor: const Color(0xFF5f4b8b),
            foregroundColor: Colors.white,
          ),
        ],
      ),
    );
  }
}
