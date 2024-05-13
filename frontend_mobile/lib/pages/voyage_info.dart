import 'dart:ffi';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:frontend_mobile/components/Voyage_details.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';
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
                          labelText: "Nombre de palettes",
                          hintText: "Nombre de palettes",
                          icon: Icon(Icons.aod_sharp),
                        ),
                      ),
                      const SizedBox(
                        height: 24,
                      ),
                      ElevatedButton(
                        style: ElevatedButton.styleFrom(
                          elevation: 0.0,
                          shadowColor: Colors.transparent,
                          backgroundColor:
                              const Color.fromRGBO(14, 61, 93, 1.0),
                          padding: const EdgeInsets.all(10),
                          side: const BorderSide(color: Colors.white),
                          textStyle: const TextStyle(fontSize: 18),
                          shape: const RoundedRectangleBorder(
                            borderRadius: BorderRadius.all(Radius.circular(20)),
                          ),
                        ),
                        onPressed: () async {
                          var connect = await Voyage.postVoyage(
                              widget.voyageToDisplay,
                              int.parse(_nbrePalettesController.text));

                          if (context.mounted) {
                            if (connect == null) {
                              showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                    title: const Text('Erreur'),
                                    content: const Text(
                                        'Probléme au niveau du serveur!'),
                                    actions: <Widget>[
                                      TextButton(
                                        child: const Text('OK'),
                                        onPressed: () {
                                          Navigator.of(context).pop();
                                        },
                                      ),
                                    ],
                                  );
                                },
                              );
                            } else {
                              if (connect.statusCode == 200) {
                                showDialog(
                                  context: context,
                                  builder: (BuildContext context) {
                                    return AlertDialog(
                                      title: const Text(
                                          'opération effectuée avec succès'),
                                      content: Text(connect.body),
                                      actions: <Widget>[
                                        TextButton(
                                          child: const Text('OK'),
                                          onPressed: () {
                                            Navigator.of(context).pop();
                                          },
                                        ),
                                      ],
                                    );
                                  },
                                );
                              } else {
                                // If the server returns an error, show an error message
                                showDialog(
                                  context: context,
                                  builder: (BuildContext context) {
                                    return AlertDialog(
                                      title: const Text('Erreur'),
                                      content: Text(connect.body),
                                      actions: <Widget>[
                                        TextButton(
                                          child: const Text('OK'),
                                          onPressed: () {
                                            Navigator.of(context).pop();
                                          },
                                        ),
                                      ],
                                    );
                                  },
                                );
                              }
                            }
                          }
                        },
                        child: const Row(
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
                              "Valider",
                              style: TextStyle(color: Colors.white),
                            )
                          ],
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
