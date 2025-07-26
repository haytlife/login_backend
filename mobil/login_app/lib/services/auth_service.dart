import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:login_app/models/user_model.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';

/// A service class responsible for handling all authentication-related API requests.
///
/// This class provides methods for user login and registration by communicating
/// with the backend server.
class AuthService {
  final String _baseUrl = '${dotenv.env['API_BASE_URL']}/api/Auth';

  /// Authenticates a user by sending their credentials to the backend API.
  Future<User> login(String email, String password) async {
    final response = await http.post(
      Uri.parse('$_baseUrl/login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'email': email,
        'password': password,
      }),
    );

    if (response.statusCode == 200) {
      return User.fromJson(jsonDecode(response.body));
    } else {
      throw Exception('Login failed. Error: ${response.body}');
    }
  }

  /// Registers a new user by sending their details to the backend API.
  Future<void> register({
    required String email,
    required String username,
    required String phoneNumber,
    required String password,
    required String confirmPassword,
  }) async {
    final response = await http.post(
      Uri.parse('$_baseUrl/register'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'email': email,
        'username': username,
        'phoneNumber': phoneNumber,
        'password': password,
        'confirmPassword': confirmPassword,
      }),
    );

    if (response.statusCode != 201 && response.statusCode != 200) {
      throw Exception('Registration failed: ${response.body}');
    }
  }

  /// Requests a password reset token from the API for the given [email].
  Future<String> forgotPassword(String email) async {
    final response = await http.post(
      Uri.parse('$_baseUrl/forgot-password'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'email': email,
      }),
    );

    if (response.statusCode == 200) {
      final jsonBody = jsonDecode(response.body);
      final tokenWithMessage = jsonBody['resetToken'] as String? ?? '';
      final token = tokenWithMessage.split(': ').last;
      
      return token;
    } else {
      throw Exception('Failed to request reset token: ${response.body}');
    }
  }

  /// Submits the new password to the API along with the email and reset token.
  Future<void> resetPassword({
    required String email,
    required String token,
    required String password,
    required String confirmPassword,
  }) async {
    final response = await http.post(
      Uri.parse('$_baseUrl/reset-password'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'email': email,
        'token': token,
        'NewPassword': password,
        'ConfirmPassword': confirmPassword,
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to reset password: ${response.body}');
    }
  }
}
