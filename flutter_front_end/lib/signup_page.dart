import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class SignupPage extends StatefulWidget {
  const SignupPage({Key? key}) : super(key: key);

  @override
  State<SignupPage> createState() => _SignupPageState();
}

class _SignupPageState extends State<SignupPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.blue[200],
      body: SafeArea(
          child: Center(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                Container(
                      padding: EdgeInsets.symmetric(horizontal: 30.0),
                      child: Image.asset('assets/iit_jammu_logo.png')
                ),
                SizedBox(height: 50,),
                Container(
                  padding: EdgeInsets.symmetric(horizontal: 30.0),
                  child: Text("CONNECT WITH OFFICIAL GOOGLE ID",
                    style: GoogleFonts.roboto(
                      fontSize: 15,
                      color: Colors.blue[900],
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                SizedBox(height: 10,),
                Container(
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(12),
                    color: Colors.white,
                  ),
                  width: 300,
                  height: 70,
                 // color: Colors.white,
                  child: Image.asset('assets/google.png'),
                ),
                SizedBox(height: 20,),
                Container(
                    padding: EdgeInsets.symmetric(horizontal: 42,vertical: 10),
                    child: Image.asset('assets/campus.jpg'),
                ),
                SizedBox(height: 10,),
                Center(
                  child: Text("IIT JAMMU OFFICIAL TIME TABLE APP",
                    style: GoogleFonts.roboto(
                      fontSize: 18,
                      color: Colors.blue[900],
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),

              ],
            ),
          ),
      ),
    );
  }
}
