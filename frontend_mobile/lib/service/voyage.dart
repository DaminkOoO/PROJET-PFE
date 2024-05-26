// ignore_for_file: avoid_print

import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';
import 'package:http/http.dart';
import 'package:intl/intl.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:http/http.dart' as http;

Future<List<Voyage>> fetchVoyagesBasedOnRoleAndId(
    String? role, String? userId) async {
  switch (role) {
    case 'Transporteur':
      return await fetchVoyagesForTransporteur(userId);
    case 'Demandeur':
      return await fetchVoyagesForDemandeur(userId);
    case 'Magasinier':
      return await fetchVoyagesForMagasinier(userId);
    default:
      throw Exception('Unsupported user role: $role');
  }
}

Future<List<Voyage>> fetchVoyagesForTransporteur(String? userId) async {
  try {
    var token = await getInfoFromToken("token");
    final response = await http.get(
      Uri.parse(
          'http://20.56.21.171:5000/api/Voyage/GetVoyagesTransporteur/$userId'),
      headers: <String, String>{
        'accept': 'text/plain',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      // Decode the response body
      final List<dynamic> data = json.decode(response.body);
      List<Voyage> list =
          data.map<Voyage>((json) => Voyage.fromJson(json)).toList();
      return list;
    } else {
      // If the server did not return a 200 OK response,
      // throw an exception.
      print(response);
      throw Exception('Failed to load voyages');
    }
  } catch (e) {
    print('Error fetching voyages for transporteur: $e');
    throw Exception('Failed to fetch voyages for transporteur');
  }
}

Future<List<Voyage>> fetchVoyagesForDemandeur(String? userId) async {
  try {
    var token = await getInfoFromToken("token");
    final response = await http.get(
      Uri.parse(
          'http://20.56.21.171:5000/api/Voyage/GetVoyagesDemandeur/$userId'),
      headers: <String, String>{
        'accept': 'text/plain',
        'Authorization': 'Bearer $token',
      },
    );

    if (response.statusCode == 200) {
      // Decode the response body
      final List<dynamic> data = json.decode(response.body);
      List<Voyage> list =
          data.map<Voyage>((json) => Voyage.fromJson(json)).toList();
      return list;
    } else {
      // If the server did not return a 200 OK response,
      // throw an exception.
      print(response);
      throw Exception('Failed to load voyages');
    }
  } catch (e) {
    print('Error fetching voyages for demandeur: $e');
    throw Exception('Failed to fetch voyages for demandeur');
  }
}

Future<List<Voyage>> fetchVoyagesForMagasinier(String? userId) async {
  try {
    var token = await getInfoFromToken("token");
    String headerToken = 'Bearer $token';
    print(headerToken);
    final response = await http.get(
      Uri.parse(
          'http://20.56.21.171:5000/api/Voyage/GetVoyagesMagasinier/$userId'),
      headers: <String, String>{
        'accept': 'text/plain',
        'Authorization': headerToken,
      },
    );

    if (response.statusCode == 200) {
      // Decode the response body
      final List<dynamic> data = json.decode(response.body);
      List<Voyage> list =
          data.map<Voyage>((json) => Voyage.fromJson(json)).toList();
      return list;
    } else {
      print('Response status code: ${response.statusCode}');
      print('Response body: ${response.body}');
      throw Exception('Erreur au niveau du serveur');
    }
  } catch (e) {
    print('Error fetching voyages for magasinier: ${e}');
    throw Exception('Failed to fetch voyages for magasinier: $e');
  }
}

class Voyage {
  String idVoyage;
  DateTime dateSortieBon;
  DateTime dateDepartVoyage;
  String etat = "Préparation";
  String demandeurMatriculeSopal;
  double priorite;
  String magasinierMatriculeSopal;
  String typeVoyage;
  String transporteursId;
  double poids;

  Voyage({
    required this.idVoyage,
    required this.dateSortieBon,
    required this.dateDepartVoyage,
    required this.etat,
    required this.demandeurMatriculeSopal,
    required this.priorite,
    required this.magasinierMatriculeSopal,
    required this.typeVoyage,
    required this.transporteursId,
    required this.poids,
  });
  factory Voyage.fromJson(Map<String, dynamic> json) {
    return Voyage(
      idVoyage: json['idVoyage'] ?? '',
      dateSortieBon:
          DateTime.tryParse(json['dateSortieBon'] ?? '') ?? DateTime.now(),
      dateDepartVoyage:
          DateTime.tryParse(json['dateDepartVoyage'] ?? '') ?? DateTime(1),
      etat: json['etat'] ?? '',
      demandeurMatriculeSopal: json['demandeurMatriculeSopal'] ??
          json['magasinierDemandeurMatriculeSopal'],
      priorite: json['priorite'] ?? 0,
      magasinierMatriculeSopal: json['magasinierMatriculeSopal'] ?? '',
      typeVoyage: json['typeVoyage'] ?? '',
      transporteursId: json['transporteursId'] ?? '',
      poids: (json['poids'] ?? 0.0).toDouble(), // Convert to double
    );
  }
  static Future<Response?> postVoyage(Voyage voyage, int nbrePalettes) async {
    try {
      const storage = FlutterSecureStorage();
      String? jwtToken = await storage.read(key: 'token');
      var jsonBody = jsonEncode(<String, dynamic>{
        "idVoyage": voyage.idVoyage,
        "dateSortieBon": voyage.dateSortieBon.toIso8601String(),
        "dateDepartVoyage": DateTime.now().toIso8601String(),
        "etat": "En attente du transporteur",
        "demandeurMatriculeSopal": "",
        "magasinierMatriculeSopal": voyage.magasinierMatriculeSopal,
        "magasinierDemandeurMatriculeSopal": voyage.demandeurMatriculeSopal,
        "typeVoyage": voyage.typeVoyage,
        "poids": voyage.poids,
        "nbrePalette": nbrePalettes
      });
      print(jsonBody);
      final response = await http.post(
        Uri.parse('http://20.56.21.171:5000/api/voyage'),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': 'Bearer $jwtToken',
        },
        body: jsonBody,
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

  static Future<Response?> getPositionVoyage(String voyage) async {
    try {
      const storage = FlutterSecureStorage();
      String? jwtToken = await storage.read(key: 'token');

      final response = await http.get(
        Uri.parse(
            'http://20.56.21.171:5000/api/voyage/GetLocalisationVoyage/$voyage'),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': 'Bearer $jwtToken',
        },
      );

      return response;
    } catch (e) {
      print(e);
      return null;
    }
  }

  static Future<List<Voyage>> getVoyageByDate(String text) async {
    var voyages = await Voyage.getAllVoyage();

    return voyages.where((element) {
      return element.dateSortieBon.toString().split(" ")[0].contains(text);
    }).toList();
  }
}
