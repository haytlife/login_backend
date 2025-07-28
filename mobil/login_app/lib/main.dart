import 'package:flutter/material.dart';
import 'package:login_app/screens/home_screen.dart';
import 'package:login_app/screens/login_screen.dart';
import 'package:login_app/screens/register_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';

Future<void> main() async {
  // Load the environment variables from the .env file.
  await dotenv.load(fileName: ".env");
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'VBT PROJECT',
      theme: ThemeData(
        primarySwatch: Colors.blue,
        useMaterial3: true,
      ),
      // start screen
      home: const LoginScreen(),
    );
  }
}