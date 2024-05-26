// ignore_for_file: avoid_print, library_prefixes

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart'
    as PolyLinePoints;
import 'package:frontend_mobile/service/utilisateur_service.dart';
import 'package:http/http.dart' as http;
import 'package:latlong2/latlong.dart';

class VoyageMapWidget extends StatefulWidget {
  final String voyageId;

  const VoyageMapWidget({super.key, required this.voyageId});

  @override
  State<VoyageMapWidget> createState() => _VoyageMapWidgetState();
}

class _VoyageMapWidgetState extends State<VoyageMapWidget> {
  final _polylinePoints = PolyLinePoints.PolylinePoints();
  List<PolyLinePoints.PointLatLng> polylineCoordinates = [];
  List<LatLng> convertedPoints = [];
  LatLng? startLocation;
  LatLng? endLocation;
  Future<void> _fetchVoyageLocations() async {
    print("suiiii");
    try {
      var token = await getInfoFromToken("token");
      final response = await http.get(
        Uri.parse(
            'http://20.56.21.171:5000/api/voyage/GetLocalisationVoyage/${widget.voyageId}'),
        headers: <String, String>{
          'Authorization': 'Bearer $token',
        },
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body) as Map<String, dynamic>;
        print(data);
        var startLat = data['localisationDebut']["lattitude"];
        startLat = (startLat * 100000).round() / 100000;
        var startLng = data['localisationDebut']["longitude"];
        startLng = (startLng * 100000).round() / 100000;
        var endLat = data['localisationFin']["lattitude"];
        endLat = (endLat * 100000).round() / 100000;
        var endLng = data['localisationFin']["longitude"];
        endLng = (endLng * 100000).round() / 100000;

        setState(() {
          startLocation = LatLng(startLat, startLng);
          endLocation = LatLng(endLat, endLng);
        });
      } else {
        print('Failed to fetch voyage locations: ${response.body}');
      }
    } catch (e) {
      print('Error fetching voyage locations: $e');
    }
  }

  Future<void> _fetchRoute() async {
    try {
      print(
          "https://router.project-osrm.org/v1/driving/${startLocation!.longitude},${startLocation!.latitude};${endLocation!.longitude},${endLocation!.latitude}?steps=true&annotations=true&geometries=geojson&overview=full");
      String url =
          'http://router.project-osrm.org/route/v1/driving/${startLocation!.longitude},${startLocation!.latitude};${endLocation!.longitude},${endLocation!.latitude}?overview=full';
      print(url);
      final response = await http.get(
        /*

        Replace the URL below with the URL of your OSRM server.
        http://router.project-osrm.org/route/v1/driving/$v2,$v1;$v4,$v3?steps=true&annotations=true&geometries=geojson&overview=full
        

        */
        Uri.parse(
          url,
        ),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body) as Map<String, dynamic>;
        final routes = data['routes'] as List;
        final geometry = routes[0]['geometry'] as String;

        polylineCoordinates = _polylinePoints.decodePolyline(geometry);
        for (var point in polylineCoordinates) {
          convertedPoints.add(LatLng(point.latitude, point.longitude));
        }
        setState(() {});
      } else {
        print('Failed to fetch route: ${response.body}');
      }
    } catch (e) {
      print('Error fetching route: $e');
    }
  }

  @override
  void initState() {
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      await _fetchVoyageLocations();
      await _fetchRoute();
    });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return startLocation != null && endLocation != null
        ? SizedBox(
            height: 400,
            width: 400,
            child: FlutterMap(
              options: MapOptions(
                initialCenter:
                    startLocation!, // Replace with your desired initial center
                initialZoom: 13,
              ),
              children: [
                TileLayer(
                  urlTemplate: "https://tile.openstreetmap.org/{z}/{x}/{y}.png",
                  userAgentPackageName: 'com.example.app',
                ),
                PolylineLayer(
                  polylineCulling: false,
                  polylines: [
                    Polyline(
                      points: convertedPoints,
                      color: Colors.blue,
                      strokeWidth: 9,
                    )
                  ],
                ),
                MarkerLayer(markers: [
                  Marker(
                    width: 80.0,
                    height: 80.0,
                    point: startLocation!,
                    child: const Icon(
                      Icons.man,
                      color: Colors.green,
                      size: 40,
                    ),
                  ),
                  Marker(
                    width: 80.0,
                    height: 80.0,
                    point: endLocation!,
                    child: Container(
                      child: Icon(
                        Icons.warehouse,
                        color: Colors.red,
                        size: 40,
                      ),
                    ),
                  ),
                ]),
              ],
            ),
          )
        : const SizedBox(
            height: 200,
            width: 200,
            child: Center(child: Text('Loading voyage data...')),
          );
  }
}
