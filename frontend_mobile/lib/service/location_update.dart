// ignore_for_file: avoid_print

import 'dart:async';
import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';
import 'package:geolocator/geolocator.dart';
import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:permission_handler/permission_handler.dart';

class LocationUpdater {
  static const updateInterval = Duration(minutes: 3);

  static void startLocationUpdates() {
    // Start periodic location updates
    Timer.periodic(updateInterval, (timer) {
      _updateLocation();
    });
  }

  static Future<void> _updateLocation() async {
    var status = await Permission.location.request();
    if (status.isGranted) {
      Position position = await Geolocator.getCurrentPosition(
          desiredAccuracy: LocationAccuracy.best);

      // Send location data to backend
      try {
        var storage = const FlutterSecureStorage();
        var token = await storage.read(key: 'token');
        var userId = JwtDecoder.decode(token!)['userId'];
        print(position);
        String apiUrl =
            'http://10.255.1.105:5000/api/user/transporteur/$userId';

        // Construct the request body
        Map<String, dynamic> requestBody = {
          'lattitude': position.latitude.toString(),
          'longitude': position.longitude.toString(),
        };

        // Encode the request body to JSON
        String requestBodyJson = json.encode(requestBody);

        // Make the PUT request
        var response = await http.put(
          Uri.parse(apiUrl),
          headers: <String, String>{
            'accept': '*/*',
            'Content-Type': 'application/json-patch+json',
            'Authorization': 'Bearer $token',
          },
          body: requestBodyJson,
        );

        if (response.statusCode == 200) {
          print('Location update sent successfully');
        } else {
          print('Error sending location update: ${response.body} ');
        }
      } catch (e) {
        print('Error sending location update: $e');
      }
    }
  }
}
