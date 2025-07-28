import 'package:flutter/material.dart';

/// A reusable layout widget for authentication screens (e.g., Login, Register).
///
/// This widget provides a consistent look and feel with a gradient background
/// and a centered content box, ensuring a uniform design across all auth pages.
class AuthPageLayout extends StatelessWidget {
  /// The primary content to be displayed inside the centered box.
  ///
  /// This is typically a [Column] of input fields, buttons, and text.
  final Widget child;

  /// Creates a standard layout for authentication pages.
  ///
  /// The [child] widget is required and will be rendered within the layout.
  const AuthPageLayout({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        // The main container that holds the gradient background.
        decoration: const BoxDecoration(
          gradient: LinearGradient(
            colors: [
              Color(0xFF5f4b8b), // Dark purple
              Color(0xFF8e7cc3), // Light purple
            ],
            begin: Alignment.topLeft,
            end: Alignment.bottomRight,
          ),
        ),
        child: SafeArea(
          child: Center(
            // Ensures the content is scrollable on smaller screens.
            child: SingleChildScrollView(
              padding:
                  const EdgeInsets.symmetric(horizontal: 24.0, vertical: 32.0),
              // The centered box that contains the form elements.
              child: Container(
                padding: const EdgeInsets.all(24.0),
                decoration: BoxDecoration(
                  color: const Color(0xFFF2ECFF), // Light lilac box color
                  borderRadius: BorderRadius.circular(16.0),
                  border: Border.all(
                    color: Colors.white.withOpacity(0.2),
                    width: 1.5,
                  ),
                ),
                // The child widget passed to the layout is rendered here.
                child: child,
              ),
            ),
          ),
        ),
      ),
    );
  }
}
