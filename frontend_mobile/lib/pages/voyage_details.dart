import 'package:flutter/material.dart';
import 'package:frontend_mobile/components/maps.dart';
import 'package:frontend_mobile/components/voyage_details.dart';
import 'package:frontend_mobile/service/voyage.dart';

class VoyageDetailsPage extends StatelessWidget {
  final Voyage voyage;
  const VoyageDetailsPage({super.key, required this.voyage});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("numéro voyage ${voyage.idVoyage}"),
        centerTitle: true,
      ),
      body: ListView(
        children: [
          Padding(
            padding: const EdgeInsets.only(bottom: 30, left: 15, right: 15),
            child: Container(
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(10),
                border: Border.all(
                  color: const Color.fromRGBO(1, 55, 106, 1.0),
                  width: 2,
                  style: BorderStyle.solid,
                ),
              ),
              child: Padding(
                padding: const EdgeInsets.all(8.0),
                child: Column(children: [
                  VoyageDetails(voyage: voyage),
                ]),
              ),
            ),
          )
        ],
      ),
    );
  }
}
