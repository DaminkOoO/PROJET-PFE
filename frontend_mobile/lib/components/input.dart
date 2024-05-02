import 'package:flutter/material.dart';

class Input extends StatelessWidget {
  final TextEditingController inputController;

  final bool isPassword;

  final String labelText;
  final String? Function(String?)? validator;

  const Input(
      {super.key,
      required this.inputController,
      required this.labelText,
      required this.isPassword,
      required this.validator});

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      controller: inputController,
      obscureText: isPassword,
      decoration: InputDecoration(
        fillColor: Colors.white,
        labelText: labelText,
        labelStyle: const TextStyle(color: Colors.white),
        border: const UnderlineInputBorder(),
        errorBorder: const OutlineInputBorder(
          borderSide: BorderSide(color: Color.fromRGBO(255, 129, 129, 1)),
        ),
        focusedErrorBorder: const UnderlineInputBorder(
          borderSide: BorderSide(color: Color.fromRGBO(255, 129, 129, 1)),
        ),
        errorStyle: const TextStyle(color: Color.fromRGBO(255, 129, 129, 1)),
        focusedBorder: const UnderlineInputBorder(
            borderSide: BorderSide(color: Colors.white)),
      ),
      style: const TextStyle(color: Colors.white),
      validator: validator,
    );
  }
}
