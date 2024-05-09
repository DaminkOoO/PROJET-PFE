import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:frontend_mobile/components/Voyage_details.dart';
import 'package:frontend_mobile/service/voyage.dart'; // Import Voyage model

class VoyageInfoPage extends StatefulWidget {
  final Voyage voyageToDisplay; // Declare Voyage object as a parameter

  const VoyageInfoPage(
      {super.key, required this.voyageToDisplay}); // Update constructor

  @override
  State<VoyageInfoPage> createState() => _VoyageInfoPageState();
}

class _VoyageInfoPageState extends State<VoyageInfoPage> {
  final TextEditingController _nbrePalettesController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        elevation: 0,
        centerTitle: true,
        title: const Text(
          "Détail Voyage",
          style: TextStyle(
            color: Colors.white,
            fontWeight: FontWeight.bold,
            fontSize: 21,
          ),
        ),
        flexibleSpace: Container(
          decoration: const BoxDecoration(
            gradient: LinearGradient(
              colors: [
                Color.fromRGBO(13, 128, 217, 1.0),
                Color.fromRGBO(7, 94, 165, 1.0),
                Color.fromRGBO(3, 68, 126, 1.0),
                Color.fromRGBO(1, 55, 106, 1.0),
              ],
              begin: Alignment.topCenter,
              end: Alignment.bottomCenter,
              stops: [0.0, 0.4, 0.75, 1.0],
            ),
          ),
        ),
      ),
      body: SafeArea(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const SizedBox(
              height: 20,
            ),
            Padding(
              padding: const EdgeInsets.only(bottom: 30, left: 15, right: 15),
              child: Container(
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(10),
                  border: Border.all(
                      color: const Color.fromRGBO(1, 55, 106, 1.0),
                      width: 2,
                      style: BorderStyle.solid),
                ),
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Column(
                    children: [
                      VoyageDetails(
                        voyage: widget.voyageToDisplay,
                      ),
                      TextFormField(
                        controller: _nbrePalettesController,
                        keyboardType: TextInputType.number,
                        validator: (value) {
                          final n = num.tryParse(value!);
                          if (n == null) {
                            return '"$value" is not a valid number';
                          }
                          return "";
                        },
                        decoration: const InputDecoration(
                          labelText: "4",
                          hintText: "Nombre de palettes",
                          icon: Icon(Icons.phone_iphone),
                        ),
                      ),
                    ],
                  ), // Pass the Voyage object to VoyageDetails
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}

String numberValidator(String value) {
  final n = num.tryParse(value);
  if (n == null) {
    return '"$value" is not a valid number';
  }
  return "";
}
