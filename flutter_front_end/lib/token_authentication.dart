import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'dart:async';

class TokenAuth{
    final storage = const FlutterSecureStorage();
    Future token_check(GoogleSignInAuthentication token) async {
        http.Response res = await http.get(Uri.parse('http://192.168.137.1:5143/Auth/login?acctoken=${token.idToken}'));
        await storage.write(key: "token", value: res.body);
    }
}