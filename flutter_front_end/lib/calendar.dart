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
        leading: IconButton(onPressed: () {
          //token = await storage.read(key: 'token');
          //DateTime datetime = DateTime.now();
          //String date = datetime.toString();
          /*Navigator.of(context).pushReplacement(MaterialPageRoute(
            builder: (context) => SignedInPage(token: token,date: date,),
          )); */
          //Navigator.of(context,rootNavigator: true).pop(context);
          Navigator.pop(context);
        },
            icon: const Icon(Icons.arrow_back)),
        title: Text("Calendar"),
        backgroundColor: Colors.blue,
        centerTitle: true,

      ),
      body: CellCalendar(
          onCellTapped: (date) {
            String newDate = date.toString();
            
            print("$date is tapped !");
            Navigator.of(context).pushReplacement(MaterialPageRoute(
              builder: (context) => SignedInPage(token: token,date: newDate,),
            ));
          }

      ),
    );
  }
}
