import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

Future<http.Response?> login(String matricule, String motPasse) async {
  try {
    final response = await http.post(
      Uri.parse('http://http://127.0.0.1:5052/api/user/login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'matriculeSopal': matricule,
        'password': motPasse,
      }),
    );

    if (response.statusCode == 200) {
      // If the server returns a 200 OK response, parse the JWT token
      final token = jsonDecode(response.body)['token'];
      print(token);

      const storage = FlutterSecureStorage();
      await storage.write(key: 'token', value: token);
    }
    return response;
  } catch (e) {
    print(e);
    return null;
  }
}
