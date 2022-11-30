import 'package:flutter/material.dart';
import 'package:flutter_front_end/signedin_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class EventDetails extends StatefulWidget {
  const EventDetails({Key? key}) : super(key: key);

  @override
  State<EventDetails> createState() => _EventDetailsState();
}

class _EventDetailsState extends State<EventDetails> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        actions: [
          TextButton(
            onPressed: () async {
              final storage = new FlutterSecureStorage();
              String? token = await storage.read(key: "token");
              Navigator.of(context).pushReplacement(MaterialPageRoute(
                builder: (context) => SignedInPage(token: token),
              ));
            },
            child: const Icon(
                Icons.arrow_back
            ),
          )
        ],
        title: Text("Class Details"),
        centerTitle: true,
        backgroundColor: Colors.black45,

      ),
    );
  }
}
