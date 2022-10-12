import 'package:flutter/material.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'google_signin_api.dart';
import 'signup_page.dart';

class SignedInPage extends StatelessWidget {
  final GoogleSignInAccount user;
  SignedInPage({
    Key? key,
    required this.user,
}) : super (key: key);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
    appBar: AppBar(
      title: Text("User"),
      centerTitle: true,
    ),
    body: Container(
      child: CircleAvatar(
        radius: 40,
        backgroundImage: NetworkImage(user.photoUrl!),
      ),
    ),
    );
  }
}
