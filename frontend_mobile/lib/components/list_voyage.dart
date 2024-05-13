import 'package:flutter/material.dart';
import 'package:frontend_mobile/components/voyage_info_card.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';
import 'package:frontend_mobile/service/voyage.dart';

class ListVoyage extends StatefulWidget {
  const ListVoyage({Key? key});

  @override
  State<ListVoyage> createState() => _ListVoyageState();
}

class _ListVoyageState extends State<ListVoyage> {
  late TextEditingController _searchController;
  late Future<List<Voyage>> voyagesList =
      Future.value([]); // Initialize voyagesList
  late String userRole; // Add user role here

  @override
  void initState() {
    super.initState();
    _searchController = TextEditingController();
    _fetchVoyagesBasedOnUserRole(); // Fetch voyages based on user role
  }

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  Future<void> _fetchVoyagesBasedOnUserRole() async {
    userRole = await getInfoFromToken('role');
    setState(() {
      voyagesList = Voyage.getAllVoyage();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            SizedBox(
              width: double.infinity,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  const SizedBox(height: 20),
                  Text(
                    "Liste des voyages".toUpperCase(),
                    style: const TextStyle(
                      color: Color.fromRGBO(1, 55, 106, 1.0),
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
                          color: const Color.fromRGBO(113, 143, 171, 1.0),
                          borderRadius: BorderRadius.circular(5)),
                      child: Center(
                        child: TextField(
                          style: const TextStyle(
                            color: Colors.white,
                          ),
                          controller: _searchController,
                          onChanged: (value) {
                            _searchController.text = value;
                          },
                          decoration: InputDecoration(
                            prefixIcon: const Icon(
                              Icons.search,
                              color: Colors.white,
                            ),
                            suffixIcon: _searchController.text != ""
                                ? IconButton(
                                    icon: const Icon(
                                      Icons.clear,
                                      color: Colors.white,
                                    ),
                                    onPressed: () {
                                      _searchController.clear();
                                    },
                                  )
                                : null,
                            hintText: 'Rechercher...',
                            hintStyle: const TextStyle(
                                color: Colors.white,
                                fontWeight: FontWeight.w300),
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
              child: FutureBuilder<List<Voyage>>(
                future: voyagesList,
                builder: (context, snapshot) {
                  if (snapshot.connectionState == ConnectionState.waiting) {
                    return const CircularProgressIndicator();
                  } else if (snapshot.hasError) {
                    return Text('Error: ${snapshot.error}');
                  } else {
                    return ListView.builder(
                      itemCount: snapshot.data!.length,
                      itemBuilder: (context, index) {
                        return Padding(
                          padding: const EdgeInsets.symmetric(horizontal: 10),
                          child: VoyageInfoCard(
                            voyage: snapshot.data![index],
                            userRole: userRole, // Pass user role here
                          ),
                        );
                      },
                    );
                  }
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
