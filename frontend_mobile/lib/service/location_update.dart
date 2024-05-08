import 'dart:async';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';
import 'package:geolocator/geolocator.dart';
import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:permission_handler/permission_handler.dart';

class LocationUpdater {
  static const updateInterval = Duration(seconds: 15);

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
        // await http.post(Uri.parse('http://your-backend-api.com/location'), body: {
        //   'latitude': position.latitude.toString(),
        //   'longitude': position.longitude.toString(),
        //   'UserId': userId
        // });
        print('Location update sent successfully');
      } catch (e) {
        print('Error sending location update: $e');
      }
    }
  }
}
