import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'package:jwt_decode/jwt_decode.dart';

Future<bool> isLoggedIn() async {
  const storage = FlutterSecureStorage();
  String? token = await storage.read(key: 'token');
  return token != null;
}

Future<String> getInfoFromToken(String info) async {
  const storage = FlutterSecureStorage();
  String? token = await storage.read(key: 'token');
  Map<String, dynamic> decodedToken = Jwt.parseJwt(token!);

  return decodedToken[info];
}


Future<http.Response?> login(String matricule, String motPasse) async {
  try {
    final response = await http.post(
      Uri.parse('http://10.255.1.239:5000/api/user/login'),
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
