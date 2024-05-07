import 'dart:async';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:geolocator/geolocator.dart';
import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';

class LocationUpdater {
  static const updateInterval = Duration(minutes: 3);

  static void startLocationUpdates() {
    // Start periodic location updates
    Timer.periodic(updateInterval, (timer) {
      _updateLocation();
    });
  }

  static Future<void> _updateLocation() async {
    // Get current location
    Position position = await Geolocator.getCurrentPosition(
        desiredAccuracy: LocationAccuracy.high);

    // Send location data to backend
    try {
      var storage = const FlutterSecureStorage();
      var token = await storage.read(key: 'token');
      var userId = JwtDecoder.decode(token!)['role'];
      await http.post(Uri.parse('http://your-backend-api.com/location'), body: {
        'latitude': position.latitude.toString(),
        'longitude': position.longitude.toString(),
        
      });
      print('Location update sent successfully');
    } catch (e) {
      print('Error sending location update: $e');
    }
  }
}
