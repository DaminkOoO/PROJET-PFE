// ignore_for_file: avoid_print, library_prefixes

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart'
    as PolyLinePoints;
import 'package:http/http.dart' as http;
import 'package:latlong2/latlong.dart';

class VoyageMapWidget extends StatefulWidget {
  final int voyageId;

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
    try {
      final response = await http.get(
        Uri.parse('https://your-api.com/voyages/${widget.voyageId}/locations'),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body) as Map<String, dynamic>;
        final startLat = data['startLatitude'];
        final startLng = data['startLongitude'];
        final endLat = data['endLatitude'];
        final endLng = data['endLongitude'];

        setState(() {
          startLocation = LatLng(startLat, startLng);
          endLocation = LatLng(endLat, endLng);
        });
      } else {
        print('Failed to fetch voyage locations: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching voyage locations: $e');
    }
  }

  Future<void> _fetchRoute() async {
    try {
      final response = await http.get(
        Uri.parse(
          'https://router.project-osrm.org/v1/driving/${startLocation!.longitude},${startLocation!.latitude};${endLocation!.longitude},${endLocation!.latitude}?steps=true',
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
        print('Failed to fetch route: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching route: $e');
    }
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      await _fetchVoyageLocations();
      await _fetchRoute();
    });
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 200,
      child: FlutterMap(
        options: MapOptions(
          initialCenter:
              startLocation!, // Replace with your desired initial center
          initialZoom: 10,
        ),
        children: [
          TileLayer(
            urlTemplate: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
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
          )
        ],
      ),
    );
  }
}
