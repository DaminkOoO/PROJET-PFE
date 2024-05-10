import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:frontend_mobile/service/utilisateur_service.dart';

class FirebaseApi {
  final _firebaseMessaging = FirebaseMessaging.instance;

  Future<void> initNotifications(String userId) async {
    await _firebaseMessaging.requestPermission();
    final fCMToken = await _firebaseMessaging.getToken();
    await updateMessageToken(userId, fCMToken!);
    print('FCM Token: $fCMToken');
  }
}
