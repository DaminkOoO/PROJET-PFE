import 'package:flutter/material.dart';

import 'package:frontend_mobile/components/voyage_info_card.dart';
import 'package:frontend_mobile/service/voyage.dart';

class ListVoyage extends StatefulWidget {
  const ListVoyage({super.key});

  @override
  State<ListVoyage> createState() => _ListVoyageState();
}

class _ListVoyageState extends State<ListVoyage> {
  late TextEditingController _searchController;
  @override
  void initState() {
    super.initState();
    _searchController = TextEditingController();
  }

  var voyagesList = Voyage.getAllVoyage();
  @override
  void dispose() {
    _searchController.dispose(); // Dispose the text editing controller
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        elevation: 0,
        flexibleSpace: Container(
          decoration: const BoxDecoration(
            gradient: LinearGradient(
              colors: [
                Color.fromRGBO(13, 128, 217, 1.0), // End color (#0D80D9)
                Color.fromRGBO(7, 94, 165, 1.0), // Additional color (#075EA5)
                Color.fromRGBO(3, 68, 126, 1.0), // Middle color (#03447E)
                Color.fromRGBO(1, 55, 106, 1.0), // Start color (#01376A)
              ],
              begin: Alignment.topCenter,
              end: Alignment.bottomCenter,
              stops: [0.0, 0.4, 0.75, 1.0],
            ),
          ),
        ),
      ),
      body: SafeArea(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            Container(
              width: double.infinity,
              decoration: const BoxDecoration(
                color: Color.fromRGBO(1, 55, 106, 1.0),
                boxShadow: [
                  BoxShadow(
                    color: Colors.grey,
                    spreadRadius: 5,
                    blurRadius: 7,
                    offset: Offset(0, 2),
                  ),
                ],
              ),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Text(
                    "Liste des voyages".toUpperCase(),
                    style: const TextStyle(
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                      fontSize: 21,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(
                    height: 20,
                  ),
                  Padding(
                    padding: const EdgeInsets.only(
                        right: 20.0, left: 20, bottom: 20),
                    child: Container(
                      width: double.infinity,
                      height: 40,
                      decoration: BoxDecoration(
                          color: const Color.fromARGB(112, 255, 255, 255),
                          borderRadius: BorderRadius.circular(5)),
                      child: Center(
                        child: TextField(
                          controller: _searchController,
                          onChanged: (value) {
                            _searchController.text = value;
                          },
                          decoration: InputDecoration(
                            prefixIcon: const Icon(Icons.search),
                            suffixIcon: _searchController.text != ""
                                ? IconButton(
                                    icon: const Icon(Icons.clear),
                                    onPressed: () {
                                      _searchController.clear();
                                    },
                                  )
                                : null,
                            hintText: 'Rechercher...',
                            border: InputBorder.none,
                          ),
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
            Expanded(
              child: ListView.builder(
                  itemCount: voyagesList.length,
                  itemBuilder: (context, index) {
                    if (index == 0) {
                      return Column(
                        children: [
                          const SizedBox(
                            height: 15,
                            child: null,
                          ),
                          VoyageInfoCard(
                            voyage: voyagesList[index],
                          ),
                        ],
                      );
                    }
                    return VoyageInfoCard(
                      voyage: voyagesList[index],
                    );
                  }),
            ),
          ],
        ),
      ),
    );
  }
}
