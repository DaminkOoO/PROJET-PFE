// ignore_for_file: prefer_const_constructors, prefer_const_literals_to_create_immutables

import 'package:flutter/material.dart';

import 'package:frontend_mobile/components/input.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _nomUtilisateurController =
      TextEditingController();
  final TextEditingController _motDePasseController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Center(
              child: Image(
                image: AssetImage('assets/logo.png'),
              ),
            ),
            SizedBox(
              height: 25,
            ),
            Text(
              "L O G I N",
              style: TextStyle(fontWeight: FontWeight.w400, fontSize: 40),
            ),
            SizedBox(
              height: 25,
            ),
            DecoratedBox(
              decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(15),
                  color: Color.fromRGBO(14, 61, 93, 1.0)),
              child: Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: 36, vertical: 30),
                child: Form(
                  key: _formKey,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Input(
                        inputController: _nomUtilisateurController,
                        isPassword: false,
                        labelText: "Nom d'utilisateur",
                        validator: (value) {
                          if (value!.isEmpty) {
                            return "Merci de fournir votre nom d'utilisateur";
                          }
                          return null;
                        },
                      ),
                      SizedBox(
                        height: 24,
                      ),
                      Input(
                        inputController: _motDePasseController,
                        isPassword: true,
                        labelText: "Mot de passe",
                        validator: (value) {
                          if (value!.isEmpty) {
                            return "Merci de fournir votre nom de passe";
                          }
                          return null;
                        },
                      ),
                      SizedBox(
                        height: 24,
                      ),
                      ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            elevation: 0.0,
                            shadowColor: Colors.transparent,
                            backgroundColor: Color.fromRGBO(14, 61, 93, 1.0),
                            padding: const EdgeInsets.all(10),
                            side: BorderSide(color: Colors.white),
                            textStyle: const TextStyle(fontSize: 18),
                            shape: const RoundedRectangleBorder(
                              borderRadius:
                                  BorderRadius.all(Radius.circular(20)),
                            ),
                          ),
                          onPressed: () async {
                            Navigator.popAndPushNamed(context, '/listevoyage');
                          },
                          child: Row(
                            crossAxisAlignment: CrossAxisAlignment.center,
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: [
                              Icon(
                                Icons.login,
                                color: Colors.white,
                              ),
                              SizedBox(
                                width: 10,
                              ),
                              Text(
                                "Se connecter",
                                style: TextStyle(color: Colors.white),
                              )
                            ],
                          ))
                    ],
                  ),
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
