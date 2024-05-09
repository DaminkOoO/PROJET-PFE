import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';
import 'package:http/http.dart';
import 'package:intl/intl.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:http/http.dart' as http;

Future<List<Voyage>> fetchVoyagesBasedOnRoleAndId(
    String? role, String? userId) async {
  // Implement your API call logic here and return a list of Voyage objects
  return [];
}

class Voyage {
  String idVoyage;
  DateTime dateSortieBon;
  DateTime dateDepartVoyage;
  String etat = "Préparation";
  String demandeurMatriculeSopal;
  int priorite;
  String magasinierMatriculeSopal;
  String typeVoyage;
  String transporteursId;
  double poids;

  Voyage(
      {required this.idVoyage,
      required this.dateSortieBon,
      required this.dateDepartVoyage,
      required this.etat,
      required this.demandeurMatriculeSopal,
      required this.priorite,
      required this.magasinierMatriculeSopal,
      required this.typeVoyage,
      required this.transporteursId,
      required this.poids});
  static Future<Response?> postVoyage(Voyage voyage, int nbrePalettes) async {
    try {
      const storage = FlutterSecureStorage();
      String? jwtToken = await storage.read(key: 'token');
      final response = await http.post(
        Uri.parse('http://10.255.1.239:5000/api/voyage'),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': 'Bearer $jwtToken',
        },
        body: jsonEncode(<String, dynamic>{
          "idVoyage": voyage.idVoyage,
          "dateSortieBon": "2024-05-09T09:00:26.740Z",
          "etat": "En attente du transporteur",
          "demandeurMatriculeSopal": voyage.demandeurMatriculeSopal,
          "magasinierMatriculeSopal": voyage.magasinierMatriculeSopal,
          "magasinierDemandeurMatriculeSopal": voyage.demandeurMatriculeSopal,
          "typeVoyage": voyage.typeVoyage,
          "poids": voyage.poids,
          "nbrePalette": nbrePalettes
        }),
      );

      return response;
    } catch (e) {
      print(e);
      return null;
    }
  }

  static Future<List<Voyage>> getAllVoyage() async {
    const storage = FlutterSecureStorage();
    String? token = await storage.read(key: 'token');

    if (token != null) {
      // Decode the token and extract the user role and user ID
      Map<String, dynamic> tokenData = JwtDecoder.decode(token);
      String? userRole = tokenData['role'];
      String? userId = tokenData['userId'];

      if (userRole == 'Transporteur' ||
          userRole == 'Demandeur' ||
          userRole == 'Magasinier') {
        // Fetch voyages based on the user's role and ID
        // Replace the below line with your API call to fetch voyages
        List<Voyage> voyages =
            await fetchVoyagesBasedOnRoleAndId(userRole, userId);
        return voyages;
      }
    }
    throw Exception('User role is not authorized or token is missing');
  }

  static Future<Voyage> fromString(String qrCodeString) async {
    Map<String, String> parts = {
      for (var item in qrCodeString.split(';'))
        item.split(':')[0]: item.split(':')[1]
    };

    DateFormat format = DateFormat("dd/MM/yyyy HH/mm/ss");
    var v = Voyage(
        idVoyage: parts['Numero']!.trim(),
        dateSortieBon: DateTime
            .now(), // Set this to the current date as it's not provided in the QR code
        dateDepartVoyage: format.parse(parts['DateHeureDemande']!.trim()),
        etat:
            'En Cours', // Set this to a default value as it's not provided in the QR code
        demandeurMatriculeSopal: await getInfoFromToken("userId"),
        priorite:
            1, // Set this to a default value as it's not provided in the QR code
        magasinierMatriculeSopal: parts['Expedition']!
            .trim(), // Set this to a default value as it's not provided in the QR code
        typeVoyage: parts['Titre']!.trim(),
        transporteursId: parts['Expedition']!.trim(),
        poids: double.parse(parts['Poids']!.trim()));
    return v;
  }

  static Map<String, dynamic> toJson(Voyage voyage) {
    return {
      'idVoyage': voyage.idVoyage,
      'dateSortieBon': voyage.dateSortieBon.toIso8601String(),
      'dateDepartVoyage': voyage.dateDepartVoyage.toIso8601String(),
      'etat': voyage.etat,
      'demandeurMatriculeSopal': voyage.demandeurMatriculeSopal,
      'priorite': voyage.priorite,
      'magasinierMatriculeSopal': voyage.magasinierMatriculeSopal,
      'typeVoyage': voyage.typeVoyage,
      'transporteursId': voyage.transporteursId,
    };
  }
}
