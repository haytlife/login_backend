/// Represents the authenticated user data received from the API.
class User {
  final int id;
  final String token;
  final String username;
  final String firstName; 

  /// Creates an instance of a [User].
  User({
    required this.id,
    required this.token,
    required this.username,
    required this.firstName, 
  });

  /// A factory constructor that creates a [User] instance from a JSON map.
  factory User.fromJson(Map<String, dynamic> json) {
    final userJson = json['user'] as Map<String, dynamic>? ?? {};

    return User(
      id: userJson['id'] ?? 0,
      username: userJson['username'] ?? '',
      firstName: userJson['firstName'] ?? '', 
      token: json['token'] ?? '',
    );
  }
}
