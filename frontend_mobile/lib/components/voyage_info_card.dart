import 'package:flutter/material.dart';
import 'package:frontend_mobile/components/Voyage_details.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/intl.dart';
import 'package:frontend_mobile/service/voyage.dart';

class VoyageInfoCard extends StatelessWidget {
  final Voyage voyage;
  final String userRole;

  const VoyageInfoCard(
      {super.key, required this.voyage, required this.userRole});

  @override
  Widget build(BuildContext context) {
    // Initialize locale data for date formatting
    initializeDateFormatting('fr_FR');

    return GestureDetector(
      onTap: () {
        if (context.mounted) {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => Scaffold(
                appBar: AppBar(
                  title: Text("numéreo voyage ${voyage.idVoyage}"),
                  centerTitle: true,
                ),
                body: ListView(children: [
                  Padding(
                    padding:
                        const EdgeInsets.only(bottom: 30, left: 15, right: 15),
                    child: Container(
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(10),
                          border: Border.all(
                              color: const Color.fromRGBO(1, 55, 106, 1.0),
                              width: 2,
                              style: BorderStyle.solid),
                        ),
                        child: Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Column(children: [
                            VoyageDetails(voyage: voyage),
                            
                          ]),
                        )),
                  )
                ]),
              ),
            ),
          );
        }
      },
      child: Card(
        shape: RoundedRectangleBorder(
          side: const BorderSide(
            color: Color.fromRGBO(1, 55, 106, 1.0), //<-- SEE HERE
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
                children: _buildInfoWidgets(userRole),
              ),
              const Icon(Icons.arrow_forward_ios)
            ],
          ),
        ),
      ),
    );
  }

  List<Widget> _buildInfoWidgets(String userRole) {
    switch (userRole) {
      case 'Transporteur':
        return _buildTransporteurInfo();
      case 'Demandeur':
        return _buildDemandeurInfo();
      case 'Magasinier':
        return _buildMagasinierInfo();
      default:
        return []; // Handle unsupported role
    }
  }

  List<Widget> _buildTransporteurInfo() {
    return [
      _buildInfoRow(
        "Date et heure Demande:",
        "${DateFormat.yMd('fr_FR').format(voyage.dateSortieBon)} ${DateFormat.Hms().format(voyage.dateSortieBon)}",
      ),
      _buildInfoRow("Magasinier:", voyage.magasinierMatriculeSopal),
      _buildInfoRow("Demandeur:", voyage.demandeurMatriculeSopal),
    ];
  }

  List<Widget> _buildDemandeurInfo() {
    return [
      _buildInfoRow(
        "Date et heure Demande:",
        "${DateFormat.yMd('fr_FR').format(voyage.dateSortieBon)} ${DateFormat.Hms().format(voyage.dateSortieBon)}",
      ),
      _buildInfoRow("Transporteur:", voyage.transporteursId),
      _buildInfoRow("Magasinier:", voyage.magasinierMatriculeSopal),
    ];
  }

  List<Widget> _buildMagasinierInfo() {
    return [
      _buildInfoRow(
        "Date et heure Demande:",
        "${DateFormat.yMd('fr_FR').format(voyage.dateSortieBon)} ${DateFormat.Hms().format(voyage.dateSortieBon)}",
      ),
      _buildInfoRow("Transporteur:", voyage.transporteursId),
      _buildInfoRow("Demandeur:", voyage.demandeurMatriculeSopal),
    ];
  }

  Widget _buildInfoRow(String title, String value) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: const TextStyle(
            color: Color.fromRGBO(1, 55, 106, 1.0),
            fontWeight: FontWeight.bold,
            fontSize: 19,
          ),
        ),
        Text(
          value,
          style: const TextStyle(
            color: Color.fromRGBO(1, 55, 106, 1.0),
            fontSize: 16,
          ),
        ),
        SizedBox(
          height: 10,
        ),
      ],
    );
  }
}
