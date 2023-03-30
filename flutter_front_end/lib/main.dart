import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_front_end/signedin_page.dart';
import 'signup_page.dart';
import 'token_authentication.dart';


class MyHttpOverrides extends HttpOverrides{
  @override
  HttpClient createHttpClient(SecurityContext? context){
    return super.createHttpClient(context)
      ..badCertificateCallback = (X509Certificate cert, String host, int port)=> true;
  }
}

void main() async {
  // await Future.delayed(const Duration(seconds: 10));
  HttpOverrides.global = MyHttpOverrides();
  // final TokenAuth tauth = TokenAuth();
  // Future.delayed(Duration.zero,()async {
  //   String? value = await tauth.storage.read(key: "token");
  //   DateTime currDate = DateTime.now();
  //   String date = currDate.toString();
  //   if(value!=null){
  //     runApp(MyApp(loggedIn: true,token: value,date: date));
  //     // Navigator.of(context).push(MaterialPageRoute(
  //     //     builder: (context) => SignedInPage(token: value,date: date,)
  //     // ));
  //   }
  //   else{
  //     runApp(MyApp(loggedIn: false,token: value,date: date,));
  //   }
  // });
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);
  // final bool loggedIn;
  // final String? token;
  // final String date;
  @override
  Widget build(BuildContext context) {
    SystemChrome.setPreferredOrientations([
      DeviceOrientation.portraitUp,
      DeviceOrientation.portraitDown
    ]);
    // if(loggedIn){
    //   return SignedInPage(token: token, date: date);
    // }
    return const MaterialApp(
      debugShowCheckedModeBanner: false,
      home: SignupPage(),
    );
  }
}

