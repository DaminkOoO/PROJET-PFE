// ignore_for_file: must_be_immutable, avoid_unnecessary_containers

import 'package:flutter/material.dart';
import 'package:frontend_mobile/service/voyage.dart';
import 'package:intl/date_symbol_data_local.dart';

import 'package:intl/intl.dart';

class VoyageInfoCard extends StatelessWidget {
  VoyageInfoCard({super.key, required this.voyage});
  Voyage voyage;
  @override
  Widget build(BuildContext context) {
    initializeDateFormatting('fr_FR');
    return GestureDetector(
      onTap: () {},
      child: Card(
        shape: RoundedRectangleBorder(
          side: const BorderSide(
            color: Colors.deepPurpleAccent, //<-- SEE HERE
          ),
          borderRadius: BorderRadius.circular(20.0),
        ),
        margin: const EdgeInsets.symmetric(horizontal: 10, vertical: 6),
        elevation: 0,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 15, vertical: 15),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Column(
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Container(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            "Date et heure Demande:",
                            style: TextStyle(
                              color: Color.fromRGBO(1, 55, 106, 1.0),
                              fontWeight: FontWeight.bold,
                              fontSize: 19,
                            ),
                          ),
                          Text(
                            "${DateFormat.yMd('fr_FR').format(voyage.dateSortieBon)} ${DateFormat.Hms().format(voyage.dateSortieBon)}",
                            style: const TextStyle(
                                color: Color.fromRGBO(1, 55, 106, 1.0),
                                fontSize: 16),
                          ),
                        ],
                      ),
                    ),
                    const SizedBox(
                      height: 10,
                    ),
                    Container(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            "Demandeur:",
                            style: TextStyle(
                              fontWeight: FontWeight.bold,
                              fontSize: 19,
                              color: Color.fromRGBO(1, 55, 106, 1.0),
                            ),
                            textAlign: TextAlign.right,
                          ),
                          Text(voyage.demandeurMatriculeSopal,
                              style: const TextStyle(
                                  color: Color.fromRGBO(1, 55, 106, 1.0),
                                  fontSize: 16)),
                        ],
                      ),
                    ),
                    const SizedBox(
                      height: 10,
                    ),
                    Container(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            "Destination:",
                            style: TextStyle(
                              color: Color.fromRGBO(1, 55, 106, 1.0),
                              fontWeight: FontWeight.bold,
                              fontSize: 19,
                            ),
                          ),
                          Text(voyage.magasinierMatriculeSopal,
                              style: const TextStyle(
                                  color: Color.fromRGBO(1, 55, 106, 1.0),
                                  fontSize: 16)),
                        ],
                      ),
                    ),
                  ]),
              const Icon(
                Icons.arrow_forward_ios_sharp,
                color: Color.fromRGBO(1, 55, 106, 1.0),
              )
            ],
          ),
        ),
      ),
    );
  }
}
