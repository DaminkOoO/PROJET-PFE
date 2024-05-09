import 'package:flutter/material.dart';
import 'package:frontend_mobile/service/voyage.dart';

class VoyageDetails extends StatelessWidget {
  Voyage voyage;

  VoyageDetails({super.key, required this.voyage});

  @override
  Widget build(BuildContext context) {
    return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          ListTile(
            title: Text('ID Voyage: ${voyage.idVoyage}'),
            subtitle: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text('Date Sortie Bon: ${voyage.dateSortieBon}'),
                Text('Date Depart Voyage: ${voyage.dateDepartVoyage}'),
                Text('Etat: ${voyage.etat}'),
                Text('Demandeur: ${voyage.demandeurMatriculeSopal}'),
                Text('Priorite: ${voyage.priorite}'),
                Text(
                    'Magasinier Matricule Sopal: ${voyage.magasinierMatriculeSopal}'),
                Text('Type Voyage: ${voyage.typeVoyage}'),
                Text('Transporteurs ID: ${voyage.transporteursId}'),
                Text('Poids: ${voyage.poids}')
              ],
            ),
          ),
        ]);
  }
}
