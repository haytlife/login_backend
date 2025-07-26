import 'package:flutter/material.dart';
import 'package:login_app/models/user_model.dart';
import 'package:login_app/screens/login_screen.dart';

/// The main screen displayed after a user successfully authenticates.
///
/// It shows a personalized welcome message and provides a way to log out.
class HomeScreen extends StatelessWidget {
  /// The data of the authenticated user, passed from the login screen.
  final User user;

  /// Creates the home screen for the given [user].
  const HomeScreen({super.key, required this.user});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // The background color is set to match the theme of the auth screens.
      backgroundColor: const Color(0xFFF2ECFF),
      appBar: AppBar(
        title: const Text('Home Page'),
        backgroundColor: const Color(0xFF5f4b8b),
        foregroundColor: Colors.white,
        // Removes the automatic back button.
        automaticallyImplyLeading: false, 
        actions: [
          // An icon button in the AppBar to handle the logout action.
          IconButton(
            icon: const Icon(Icons.logout),
            tooltip: 'Logout',
            onPressed: () {
              // TODO: In a future version, clear the saved token from device storage here.
              // For now, it navigates back to the LoginScreen and removes all previous routes.
              Navigator.of(context).pushAndRemoveUntil(
                MaterialPageRoute(builder: (context) => const LoginScreen()),
                (Route<dynamic> route) => false,
              );
            },
          ),
        ],
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Icon(
              Icons.check_circle_outline,
              size: 100,
              color: Color(0xFF5f4b8b),
            ),
            const SizedBox(height: 30),
            // A personalized welcome message using the user's first name.
            Text(
              'Welcome, ${user.firstName}!',
              textAlign: TextAlign.center,
              style: const TextStyle(
                fontSize: 28,
                fontWeight: FontWeight.bold,
                color: Color(0xFF3D3D3D),
              ),
            ),
            const SizedBox(height: 10),
            const Text(
              'You have successfully logged in.',
              style: TextStyle(
                fontSize: 18,
                color: Color(0xFF5A5A5A),
              ),
            ),
          ],
        ),
      ),
    );
  }
}