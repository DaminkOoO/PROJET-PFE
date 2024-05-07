// ignore_for_file: prefer_const_constructors

import 'package:flutter/material.dart';
import 'package:frontend_mobile/components/list_voyage.dart';
import 'package:frontend_mobile/pages/functionalities_page.dart';
import 'package:frontend_mobile/pages/login_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

void main() {
  runApp(const MainPage());
}

class MainPage extends StatelessWidget {
  const MainPage({super.key});

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<bool>(
      future: isLoggedIn(),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return CircularProgressIndicator();
        } else {
          if (snapshot.hasData) {
            return MaterialApp(
              debugShowCheckedModeBanner: false,
              routes: {
                '/functions': (context) => FunctionalitiesPage(),
                '/login': (context) => LoginPage(),
              },
              home: snapshot.data! ? FunctionalitiesPage() : LoginPage(),
            );
          } else {
            // Handle error if isLoggedIn function fails
            return Text('Error: ${snapshot.error}');
          }
        }
      },
    );
  }
}

Future<bool> isLoggedIn() async {
  final storage = FlutterSecureStorage();
  String? token = await storage.read(key: 'token');
  return token != null;
}
