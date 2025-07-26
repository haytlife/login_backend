import 'package:flutter/material.dart';

/// A reusable button widget for primary authentication actions like 'Sign In' or 'Sign Up'.
///
/// This widget standardizes the appearance of authentication buttons and
/// includes built-in support for a loading state, displaying a circular
/// progress indicator when an asynchronous operation is in progress.
class CustomAuthButton extends StatelessWidget {
  /// The text label displayed on the button.
  final String label;

  /// The callback function that is executed when the button is tapped.
  final VoidCallback? onPressed;

  /// A boolean flag to indicate if the button is in a loading state.
  /// When true, it displays a progress indicator and disables the button.
  final bool isLoading;

  /// The background color of the button.
  final Color backgroundColor;

  /// The color of the text and the progress indicator.
  final Color foregroundColor;

  /// Creates a customized authentication button.
  const CustomAuthButton({
    super.key,
    required this.label,
    required this.onPressed,
    this.isLoading = false,
    required this.backgroundColor,
    required this.foregroundColor,
  });

  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      style: ElevatedButton.styleFrom(
        padding: const EdgeInsets.symmetric(vertical: 16.0),
        backgroundColor: backgroundColor,
        foregroundColor: foregroundColor,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(12.0),
        ),
      ),
      // If isLoading is true, onPressed is set to null, disabling the button.
      onPressed: isLoading ? null : onPressed,
      child: isLoading
          ? CircularProgressIndicator(
              valueColor: AlwaysStoppedAnimation<Color>(foregroundColor),
            )
          : Text(
              label,
              style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
    );
  }
}
