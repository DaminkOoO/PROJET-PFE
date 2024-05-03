class Voyage {
  String? idVoyage;
  DateTime dateSortieBon;
  DateTime? dateDepartVoyage;
  String? etat;
  String? depotDestination;
  String? demandeurMatriculeSopal;
  String? magasinierMatriculeSopal;
  String? typeVoyage;
  String demandeur;
  Voyage(
      {this.idVoyage = '',
      required this.dateSortieBon,
      this.dateDepartVoyage,
      this.etat = '',
      this.demandeurMatriculeSopal = '',
      this.magasinierMatriculeSopal = '',
      this.typeVoyage,
      this.depotDestination = '',
      required this.demandeur});
  static List<Voyage> getAllVoyage() {
    final List<Voyage> listVoyage = [];
    listVoyage.add(Voyage(
        dateSortieBon: DateTime.now(),
        demandeur: "Fathi Hamrouni",
        depotDestination: "Sopal 5"));
    listVoyage.add(Voyage(
        dateSortieBon: DateTime.now(),
        demandeur: "Ali Hamrouni",
        depotDestination: "Sopal 2"));
    listVoyage.add(Voyage(
        dateSortieBon: DateTime.now(),
        demandeur: "Salah Hamrouni",
        depotDestination: "Sopal 12"));
    listVoyage.add(Voyage(
        dateSortieBon: DateTime.now(),
        demandeur: "Salah Hamrouni",
        depotDestination: "Sopal 4"));
    listVoyage.add(Voyage(
        dateSortieBon: DateTime.now(),
        demandeur: "Salah Hamrouni",
        depotDestination: "Sopal 5"));
    return listVoyage;
  }
}
