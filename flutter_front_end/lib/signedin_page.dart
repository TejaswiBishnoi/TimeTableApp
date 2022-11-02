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
      title: Text(user.displayName!),
      centerTitle: true,
      backgroundColor: Colors.black12,
      actions: [
        TextButton(
            onPressed: () async {
              await GoogleSignInAPI.logout();

              Navigator.of(context).pushReplacement(MaterialPageRoute(
                  builder: (context) => SignupPage(),
              ));
            },
            child: const Text('Logout',
              style: TextStyle(
                  color: Colors.black
              ),
            ),
        )
      ],
    ),
    body: Container(
      child: PageView(
        children: [
          Column(
            children: [
              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Wednusday  12/10/2022'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot A    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot B    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot C    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot D    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),
            ],
          ),


          Column(
            children: [
              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Thursday  13/10/2022'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot A    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot B    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot C    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot D    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),
            ],
          ),



          Column(
            children: [
              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Friday  14/10/2022'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot A    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot B    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot C    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),

              Card(
                child: Padding(
                  padding: const EdgeInsets.all(20),
                  child: Column(
                    children: [
                      Text('Slot D    Details'),
                    ],
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                  ),
                ),
                margin: EdgeInsets.fromLTRB(10, 5, 10, 0),
              ),
            ],
          ),
        ],
      ),
    ),
    );
  }
}
