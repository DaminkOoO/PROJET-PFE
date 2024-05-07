import 'package:flutter/material.dart';

class VoyageDetails extends StatelessWidget {
  const VoyageDetails({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
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
          style: const TextStyle(color: Colors.black, fontSize: 16),
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
          style: const TextStyle(color: Colors.black, fontSize: 16),
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
          style: const TextStyle(color: Colors.black, fontSize: 16),
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
          style: const TextStyle(color: Colors.black, fontSize: 16),
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
          style: const TextStyle(color: Colors.black, fontSize: 16),
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
          style: const TextStyle(color: Colors.black, fontSize: 16),
        ),
      ],
    );
  }
}
