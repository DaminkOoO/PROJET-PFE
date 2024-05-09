import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:frontend_mobile/components/list_voyage.dart';
import 'package:frontend_mobile/components/qr_scan.dart';
import 'package:frontend_mobile/pages/voyage_info.dart';
import 'package:frontend_mobile/service/location_update.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';

class FunctionalitiesPage extends StatefulWidget {
  const FunctionalitiesPage({super.key});

  @override
  State<FunctionalitiesPage> createState() => _FunctionalitiesPageState();
}

class _FunctionalitiesPageState extends State<FunctionalitiesPage> {
  int _selectedIndex = 0;
  void _navigationBottomBar(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  final _pageList = [
    const ListVoyage(),
    const QrScan(),
  ];
  @override
  void initState() {
    super.initState();
    initializeState();
  }

  Future<void> initializeState() async {
    var tr = await getInfoFromToken("role");
    if (tr == "Transporteur") {
      LocationUpdater.startLocationUpdates();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        actions: [
          IconButton(onPressed: () {}, icon: const Icon(Icons.highlight_off)),
          IconButton(
              onPressed: () async {
                const storage = FlutterSecureStorage();
                await storage.delete(key: 'token');
                if (context.mounted) {
                  Navigator.of(context).pop();
                  Navigator.pushNamed(context, '/login');
                }
              },
              icon: const Icon(Icons.logout))
        ],
      ),
      body: _pageList[_selectedIndex],
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _selectedIndex,
        onTap: _navigationBottomBar,
        items: const [
          BottomNavigationBarItem(
            icon: Icon(Icons.fire_truck),
            label: 'Liste des voyages',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.qr_code),
            label: 'Scan QR',
          ),
        ],
      ),
    );
  }
}
