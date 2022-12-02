import 'package:flutter/material.dart';
import 'package:flutter_front_end/signedin_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'http_model.dart';
import 'event_model.dart';


class EventDetails extends StatefulWidget {
  final String? occur_id;
  final String? event_id;
  final String? token;
  const EventDetails({Key? key,required this.occur_id, required this.event_id, required this.token}) : super(key: key);

  @override
  State<EventDetails> createState() => _EventDetailsState(event_id: event_id,occur_id: occur_id,token: token);
}

class _EventDetailsState extends State<EventDetails> {
  @override
  final String? event_id;
  final String? occur_id;
  final String? token;
  _EventDetailsState({required this.event_id,required this.occur_id,required this.token});
  final HttpService httpService = HttpService();
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        actions: [
          TextButton(
            onPressed: () async {
             // final storage = new FlutterSecureStorage();
              //String? token = await storage.read(key: "token");
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
      body: FutureBuilder(future: httpService.getDetails(event_id, occur_id,token),
      builder: (BuildContext context, AsyncSnapshot snapshot){

        if (snapshot.connectionState == ConnectionState.done){
          if(snapshot.hasData){
            eventDetails event = snapshot.data;
            return ListView(
              children: [
                ListTile(
                title: Text(event.eventType),
              ),
                ListTile(
                  title: Text(event.department),
                ),
                ListTile(
                  title: Text(event.total_credits),
                ),
                ListTile(
                  title: Text(event.lecture_credits),
                ),
                ListTile(
                  title: Text(event.tutorial_credits),
                ),
                ListTile(
                  title: Text(event.practical_credits),
                ),
                ListTile(
                  title: Text(event.category),
                ),
                ListTile(
                  title: Text(event.next_day),
                ),
                ListTile(
                  title: Text(event.next_date),
                ),
                ListTile(
                  title: Text(event.next_start_time),
                ),
                ListTile(
                  title: Text(event.next_start_time),
                ),
              ],
            );
          }
          else{
            return const Center(child: Text("No Data"));
          }

        }
        else if (snapshot.connectionState == ConnectionState.none){
          return const Text('Error');
        }
        else{
          return const Center(child: CircularProgressIndicator());
        }

      },
      ),
    );
  }
}
