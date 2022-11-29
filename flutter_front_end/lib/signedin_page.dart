import 'package:flutter/material.dart';
import 'google_signin_api.dart';
import 'signup_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'http_model.dart';
import 'post_model.dart';

class SignedInPage extends StatelessWidget {
  final HttpService httpService = HttpService();
  final String? token;

  SignedInPage({
    Key? key,
    required this.token,

}) : super (key: key);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
    appBar: AppBar(
      title: Text("'s Schedule  "),
      centerTitle: true,
      backgroundColor: Colors.black45,
      actions: [
        TextButton(
            onPressed: () async {
              await GoogleSignInAPI.logout();
              final storage = new FlutterSecureStorage();
              await storage.delete(key: 'token');
              Navigator.of(context).pushReplacement(MaterialPageRoute(
                  builder: (context) => SignupPage(),
              ));
            },
            child: const Text('Logout',
              style: TextStyle(
                  color: Colors.white60
              ),
            ),
        )
      ],
    ),
    body: FutureBuilder(future: httpService.getPosts(),
    builder: (BuildContext context, AsyncSnapshot snapshot){
      if (snapshot.connectionState == ConnectionState.done) {
        if (snapshot.data == null) {
          return const Text('no data');
        } else {
          List<Daily>? week = snapshot.data;

          return PageView(
            children: week!.map((Daily day) => ListView(
              children: day.event_list != null ? day.event_list!.map((Events event) => Card(
                child: SizedBox(
                    width: 200,
                    height: 100,
                    child: Row(
                  children: [
                    SizedBox(width: 20.0,height: 0,),
                    Text(event.start_time),
                    SizedBox(width: 20.0,height: 0,),
                    Text(event.course_name),
                    SizedBox(width: 20.0,height: 0,),
                    Text(event.section)
                  ],
                )),
              )).toList() : [],
            )).toList(),
          );
        }
      } else if (snapshot.connectionState == ConnectionState.none) {
        return const Text('Error'); // error
      } else {
        return Center(child: const CircularProgressIndicator()); // loading
      }

        },
    ),
    );
  }
}

/*
if(snapshot.hasData){
         List<Daily>? week = snapshot.data;
         return PageView(
           children: week!.map((Daily day) => ListView(
             children: day.event_list!.map((Events event) => ListTile(
               title: Text(event.course_name),
             )).toList(),
           )).toList(),
         );
       }
       else{
         return const Center(child: CircularProgressIndicator());
       }
 */