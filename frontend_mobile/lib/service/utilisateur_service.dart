// ignore_for_file: avoid_print

import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'package:jwt_decode/jwt_decode.dart';

Future<bool> isLoggedIn() async {
  const storage = FlutterSecureStorage();
  String? token = await storage.read(key: 'token');
  return token != null;
}

Future<List<dynamic>?> getAllVoyagesForTransporteur() async {
  try {
    var token = getInfoFromToken("token");
    final response = await http.get(
      Uri.parse(
          'http://localhost:5000/api/Voyage/GetVoyagesTransporteur/transporteur'),
      headers: <String, String>{
        'accept': 'text/plain',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      // Decode the response body
      final List<dynamic> data = json.decode(response.body);
      return data;
    } else {
      // If the server did not return a 200 OK response,
      // throw an exception.
      throw Exception('Failed to load voyages');
    }
  } catch (e) {
    print(e);
    return null;
  }
}

Future<String> getInfoFromToken(String info) async {
  const storage = FlutterSecureStorage();
  String? token = await storage.read(key: 'token');
  if (info == "token") {
    return token!;
  }
  Map<String, dynamic> decodedToken = Jwt.parseJwt(token!);
  
  return decodedToken[info];
}

Future<http.Response?> login(String matricule, String motPasse) async {
  try {
    final response = await http.post(
      Uri.parse('http://10.255.1.222:5000/api/user/login'),
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

Future<http.Response?> updateMessageToken(
    String matricule, String token) async {
  try {
    final response = await http.put(
      Uri.parse(
          'http://10.255.1.222:5000/api/user/token/$matricule'),
      headers: <String, String>{
        'accept': '*/*',
        'Content-Type': 'application/json-patch+json',
      },
      body: jsonEncode(token), // Assuming token is a string
    );
    print(response.body);
    return response;
  } catch (e) {
    print(e);
    return null;
  }
}
