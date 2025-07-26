import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:login_app/screens/reset_password_screen.dart';
import 'package:login_app/widgets/auth_page_layout.dart';
import 'package:login_app/widgets/custom_auth_button.dart';

/// A screen to display the password reset token to the user.
class DisplayTokenScreen extends StatelessWidget {
  final String resetToken;

  const DisplayTokenScreen({super.key, required this.resetToken});

  @override
  Widget build(BuildContext context) {
    final tokenController = TextEditingController(text: resetToken);

    return AuthPageLayout(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          // --- Header Section ---
          const Text(
            'Your reset token',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 32,
              fontWeight: FontWeight.bold,
              color: Color(0xFF3D3D3D),
            ),
          ),
          const SizedBox(height: 10),
          const Text(
            'Copy this token for the next step',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 16,
              color: Color(0xFF5A5A5A),
            ),
          ),
          const SizedBox(height: 40),

          // --- Token Display and Copy ---
          Row(
            children: [
              Expanded(
                child: TextField(
                  controller: tokenController,
                  readOnly: true,
                  style: const TextStyle(color: Colors.black87, letterSpacing: 2),
                  decoration: InputDecoration(
                    contentPadding: const EdgeInsets.symmetric(vertical: 14.0, horizontal: 16.0),
                    filled: true,
                    fillColor: Colors.white.withOpacity(0.9),
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(12.0),
                      borderSide: BorderSide.none,
                    ),
                  ),
                ),
              ),
              const SizedBox(width: 10),
              ElevatedButton(
                onPressed: () {
                  Clipboard.setData(ClipboardData(text: resetToken));
                  ScaffoldMessenger.of(context).showSnackBar(
                    const SnackBar(content: Text('Token copied to clipboard!')),
                  );
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xFF3D3D3D),
                  foregroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12.0),
                  ),
                  padding: const EdgeInsets.symmetric(vertical: 14.0, horizontal: 20.0),
                ),
                child: const Text('Copy'),
              ),
            ],
          ),
          const SizedBox(height: 30),

          // --- Continue Button ---
          CustomAuthButton(
            label: 'Continue to Reset Password',
            onPressed: () {
              Navigator.of(context).push(MaterialPageRoute(
                builder: (context) => ResetPasswordScreen(
                  initialToken: resetToken,
                ),
              ));
            },
            backgroundColor: const Color(0xFF5f4b8b),
            foregroundColor: Colors.white,
          ),
        ],
      ),
    );
  }
}
