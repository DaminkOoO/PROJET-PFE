import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

Future<bool> login(String matricule, String motPasse) async {
  final response = await http.post(
    Uri.parse('http://your-api-url.com/api/user/login'),
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

    const storage = FlutterSecureStorage();
    await storage.write(key: 'token', value: token);
    return true;
  } else {
    return false;
  }
}
