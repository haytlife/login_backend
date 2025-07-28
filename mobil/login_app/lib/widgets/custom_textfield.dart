import 'package:flutter/material.dart';

/// A reusable text field widget with a standardized design for the application.
///
/// This widget encapsulates the common styling for text input fields,
/// including decoration, padding, and icons, to ensure a consistent
/// user interface across different screens.
class CustomTextField extends StatelessWidget {
  /// The controller for the text field to manage its content.
  final TextEditingController controller;

  /// The placeholder text displayed when the field is empty.
  final String hintText;

  /// The icon displayed at the beginning of the text field.
  final IconData icon;

  /// Whether the text should be obscured (e.g., for passwords). Defaults to false.
  final bool obscureText;

  /// The type of keyboard to use for editing the text. Defaults to [TextInputType.text].
  final TextInputType keyboardType;

  /// Creates a customized text field.
  const CustomTextField({
    super.key,
    required this.controller,
    required this.hintText,
    required this.icon,
    this.obscureText = false,
    this.keyboardType = TextInputType.text,
  });

  @override
  Widget build(BuildContext context) {
    return TextField(
      controller: controller,
      obscureText: obscureText,
      keyboardType: keyboardType,
      style: const TextStyle(color: Colors.black87),
      decoration: InputDecoration(
        hintText: hintText,
        contentPadding: const EdgeInsets.symmetric(vertical: 14.0),
        filled: true,
        fillColor: Colors.white.withOpacity(0.9),
        prefixIcon: Icon(icon, color: const Color(0xFF5f4b8b)),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(12.0),
          borderSide: BorderSide.none,
        ),
      ),
    );
  }
}
