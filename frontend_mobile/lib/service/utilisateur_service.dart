import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

Future<http.Response?> login(String matricule, String motPasse) async {
  try {
    final response = await http.post(
      Uri.parse('http://10.0.2.2:5052/api/user/login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'matriculeSopal': matricule,
        'password': motPasse,
      }),
    );

    if (response.statusCode == 200) {

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
