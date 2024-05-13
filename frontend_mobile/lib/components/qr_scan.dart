// ignore_for_file: avoid_print

import 'package:flutter/material.dart';

import 'package:barcode_scan2/barcode_scan2.dart';

import 'package:frontend_mobile/pages/voyage_info.dart';
import 'package:frontend_mobile/service/voyage.dart';

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
                var codeScanner = await BarcodeScanner.scan();
                if (!mounted) return;
                setState(() {
                  qrCodeResult = codeScanner.rawContent;
                });
                print(codeScanner);

                // TODO: focus on this tomorrow
                if (context.mounted) {
                  if (qrCodeResult.isEmpty) {
                    Navigator.pop(context);
                    Navigator.pushNamed(context, "/functions");
                  } else {
                    if (qrCodeResult.split(':')[0] == "MAG") {}
                    var v = await Voyage.fromString(qrCodeResult);
                    print("Is v==null $v");
                    if (context.mounted) {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) =>
                              VoyageInfoPage(voyageToDisplay: v),
                        ),
                      );
                    }
                  }
                }
              },
            ),
          ],
        ),
      ),
    );
  }
}
