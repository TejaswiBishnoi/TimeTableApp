import 'package:google_sign_in/google_sign_in.dart';

class GoogleSignInAPI {
  static final _googleSignIn = GoogleSignIn();

  static Future<GoogleSignInAccount?> login()=> _googleSignIn.signIn();

  /*static handleLogOut() async {
    await _googleSignIn.disconnect().whenComplete(() async {
      await _googleSignIn.signOut();
    });
  }*/

 static Future logout() => _googleSignIn.disconnect();
}

