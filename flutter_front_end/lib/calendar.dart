import 'package:cell_calendar/cell_calendar.dart';
import 'package:flutter/material.dart';
import 'package:flutter_front_end/signedin_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class Calendar extends StatelessWidget {
  final storage = new FlutterSecureStorage();
  String? token;


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        leading: IconButton(onPressed: () async {
          token = await storage.read(key: 'token');
          Navigator.of(context).pushReplacement(MaterialPageRoute(
            builder: (context) => SignedInPage(token: token,),
          ));
        },
            icon: const Icon(Icons.arrow_back)),
        title: Text("Calendar"),
        backgroundColor: Colors.blue,
        centerTitle: true,

      ),
      body: CellCalendar(),
    );
  }
}
