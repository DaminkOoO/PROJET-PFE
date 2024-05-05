// ignore_for_file: prefer_const_constructors, prefer_const_literals_to_create_immutables

import 'package:flutter/material.dart';

class VoyageInfoPage extends StatefulWidget {
  const VoyageInfoPage({super.key});

  @override
  State<VoyageInfoPage> createState() => _VoyageInfoPageState();
}

class _VoyageInfoPageState extends State<VoyageInfoPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        elevation: 0,
        centerTitle: true,
        title: Text(
          "Détail Voyage",
          style: const TextStyle(
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
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      const Text(
                        "Identifiant du voyage:",
                        style: TextStyle(
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                          fontSize: 19,
                        ),
                      ),
                      Text(
                        "SLPF25/03/2023:8:48:33SO1-PF",
                        style:
                            const TextStyle(color: Colors.black, fontSize: 16),
                      ),
                      const Text(
                        "Date et heure demande:",
                        style: TextStyle(
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                          fontSize: 19,
                        ),
                      ),
                      Text(
                        "25/03/2023 8:48",
                        style:
                            const TextStyle(color: Colors.black, fontSize: 16),
                      ),
                      const Text(
                        "Demandeur:",
                        style: TextStyle(
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                          fontSize: 19,
                        ),
                      ),
                      Text(
                        "Fathi Hamrouni",
                        style:
                            const TextStyle(color: Colors.black, fontSize: 16),
                      ),
                      const Text(
                        "Dépot Demandeur:",
                        style: TextStyle(
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                          fontSize: 19,
                        ),
                      ),
                      Text(
                        "SL-PF\nDépôt PF Sopal Thyna Km10",
                        style:
                            const TextStyle(color: Colors.black, fontSize: 16),
                      ),
                      const Text(
                        "Dépot d'expédition:",
                        style: TextStyle(
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                          fontSize: 19,
                        ),
                      ),
                      Text(
                        "SO1-PF\nDépôt PF Sopal Batiment SOPAL 5 route de Gabes KM1.5",
                        style:
                            const TextStyle(color: Colors.black, fontSize: 16),
                      ),
                      const Text(
                        "Etat:",
                        style: TextStyle(
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                          fontSize: 19,
                        ),
                      ),
                      Text(
                        "En Cours",
                        style:
                            const TextStyle(color: Colors.black, fontSize: 16),
                      ),
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
