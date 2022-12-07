import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'google_signin_api.dart';
import 'signedin_page.dart';
import 'token_authentication.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class SignupPage extends StatefulWidget {
  const SignupPage({Key? key}) : super(key: key);

  @override
  State<SignupPage> createState() => _SignupPageState();
}

class _SignupPageState extends State<SignupPage> {
  @override
  final TokenAuth tauth = TokenAuth();
  void initState() {
    super.initState();
    Future.delayed(Duration.zero,()async {
      print("&&&&&&&&&&&&&&&&&");

      String? value = await tauth.storage.read(key: "token");


      if(value!=null){
        Navigator.of(context).pushReplacement(MaterialPageRoute(
            builder: (context) => SignedInPage(token: value,)
        ));
      }
    });

  }
  Widget build(BuildContext context) {
    return Scaffold(

     // backgroundColor: Colors.blue[200],
      body: Container(
        decoration: const BoxDecoration(
          image: DecorationImage(
            image: AssetImage('assets/eberhard.jpg'),
            fit: BoxFit.cover,
          ),
        ),
        child: SafeArea(
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
                    alignment: Alignment.topLeft,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(12),
                      color: Colors.white,
                    ),
                    width: 300,
                    height: 70,
                    child: TextButton(
                      onPressed: signIn,
                      child: Row(
                          children:<Widget>[
                           const SizedBox(width: 10,),
                            Image.asset('assets/google.png'),
                           const  SizedBox(width: 25,),
                           const Text("Signin with Google",
                              style: TextStyle(
                                color: Colors.black,
                                fontSize: 17,
                              ),
                            ),
                          ],

                      ),
                    ),
                  ),
                  SizedBox(height: 20,),
                /*  Container(
                      padding: EdgeInsets.symmetric(horizontal: 42,vertical: 10),
                      child: Image.asset('assets/campus.jpg'),
                  ), */
               //   SizedBox(height: 10,),
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
      ),
    );
  }
  Future signIn() async {
    final user = await GoogleSignInAPI.login();
    if(user == null){
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Sign in failed')));
    }
    else{
      final gauth = await user.authentication;
      tauth.token_check(gauth);
      String? token = await tauth.storage.read(key: "token");
      Navigator.of(context).pushReplacement(MaterialPageRoute(
        builder: (context) => SignedInPage(token: token,)
    ));
    }
  }


}



