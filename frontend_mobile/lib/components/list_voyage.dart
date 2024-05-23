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
  late TextEditingController _dateController;
  late TextEditingController _dateFinController;
  late Future<List<Voyage>> voyagesList =
      Future.value([]); // Initialize voyagesList
  late Future<List<Voyage>> voyagesListFiltered =
      Future.value([]); // Initialize voyagesList
  late String userRole; // Add user role here
  bool isFiltered = false;

  @override
  void initState() {
    super.initState();
    _dateController = TextEditingController();
    _dateFinController = TextEditingController();
    _fetchVoyagesBasedOnUserRole(); // Fetch voyages based on user role
  }

  @override
  void dispose() {
    _dateController.dispose();
    _dateFinController.dispose();
    super.dispose();
  }

  Future<void> _fetchVoyagesBasedOnUserRole() async {
    userRole = await getInfoFromToken('role');
    var voyages = await Voyage.getAllVoyage();
    setState(() {
      if (voyages.isNotEmpty) {
        voyagesList = Future.value(voyages);
      }
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
                    child: Row(
                      children: [
                        Container(
                          width: 250,
                          height: 40,
                          decoration: BoxDecoration(
                              color: const Color.fromRGBO(113, 143, 171, 1.0),
                              borderRadius: BorderRadius.circular(5)),
                          child: TextField(
                            style: const TextStyle(
                              color: Colors.white,
                            ),
                            controller: _dateController,
                            onChanged: (value) {
                              _dateController.text = value;
                            },
                            decoration: const InputDecoration(
                              contentPadding: EdgeInsets.only(top: 3.0),
                              prefixIcon: Icon(
                                Icons.calendar_month,
                                color: Colors.white,
                              ),
                              hintText: 'Date voyage',
                              hintStyle: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.w300),
                              border: InputBorder.none,
                            ),
                            readOnly: true,
                            onTap: () => _selectDate(_dateController),
                          ),
                        ),
                        const SizedBox(
                          width: 20,
                        ),
                        SizedBox(
                          width: 70,
                          child: ElevatedButton(
                            onPressed: () {
                              var voyages =
                                  Voyage.getVoyageByDate(_dateController.text);
                              setState(() {
                                if (_dateController.text.isNotEmpty) {
                                  isFiltered = true;
                                  voyagesList = voyages;
                                }
                              });
                            },
                            style: ElevatedButton.styleFrom(
                              alignment: Alignment.center, // Add this line
                            ),
                            child: const Icon(Icons.search),
                          ),
                        )
                      ],
                    ),
                  ),
                ],
              ),
            ),
            if (isFiltered)
              ElevatedButton(
                  onPressed: () async {
                    await _fetchVoyagesBasedOnUserRole();
                    setState(() {
                      isFiltered = false;
                    });
                  },
                  child: const Text('Annuler le filtre')),
            Expanded(
              child: FutureBuilder<List<Voyage>>(
                future: voyagesList,
                builder: (context, snapshot) {
                  if (snapshot.connectionState == ConnectionState.waiting) {
                    return const SizedBox(
                      height: 50,
                      width: 50,
                      child: CircularProgressIndicator(),
                    );
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

  Future<void> _selectDate(TextEditingController controller) async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: DateTime.now(),
      firstDate: DateTime(2015, 8),
      lastDate: DateTime(2101),
    );
    if (picked != null) {
      setState(() {
        controller.text = picked.toString().split(" ")[0];
      });
    }
  }
}
