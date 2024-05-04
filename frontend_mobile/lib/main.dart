// ignore_for_file: prefer_const_constructors

import 'package:flutter/material.dart';
import 'package:frontend_mobile/pages/list_voyage.dart';
import 'package:frontend_mobile/pages/voyage_info.dart';

void main() {
  runApp(const MainPage());
}

class MainPage extends StatelessWidget {
  const MainPage({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      routes: {
        '/listevoyage': (context) => ListVoyage(),
      },
      home: VoyageInfoPage(),
    );
  }
}
