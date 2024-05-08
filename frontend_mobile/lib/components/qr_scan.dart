import 'package:flutter/material.dart';

import 'package:barcode_scan2/barcode_scan2.dart';
import 'package:frontend_mobile/components/voyage_details.dart';

class QrScan extends StatefulWidget {
  const QrScan({super.key});

  @override
  State<QrScan> createState() => _QrScanState();
}

class _QrScanState extends State<QrScan> {
  String qrCodeResult = "";
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          children: [
            const Text("Cliquez sur bouton Scan pour lancer le voyage!"),
            ElevatedButton(
              child: const Text("Scan"),
              onPressed: () async {
                var codeScanner = await BarcodeScanner.scan(); //barcode scanner
                setState(() {
                  qrCodeResult = codeScanner.rawContent;
                });
                print(codeScanner);

                // Navigate to VoyageDetails page with qrCodeResult
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) =>
                        VoyageDetails(qrCodeResult: qrCodeResult),
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
