import 'package:flutter/material.dart';
import 'package:flutter_front_end/signedin_page.dart';
import 'http_model.dart';
import 'event_model.dart';


class EventDetails extends StatefulWidget {
  final String? id;
  final String? occur_id;
  final String? event_id;
  final String? token;
  final HttpService httpService;
  const EventDetails({Key? key,required this.occur_id, required this.event_id, required this.token, required this.id, required this.httpService}) : super(key: key);

  @override
  State<EventDetails> createState() => _EventDetailsState(event_id: event_id,occur_id: occur_id,token: token, id: id,httpService: httpService);
}

class _EventDetailsState extends State<EventDetails> {
  @override
  final String? id;
  final String? event_id;
  final String? occur_id;
  final String? token;
  final HttpService httpService;
  _EventDetailsState({required this.event_id,required this.occur_id,required this.token, required this.id, required this.httpService});
  //final HttpService httpService = HttpService();
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        leading: TextButton(
          onPressed: () async {
            // final storage = new FlutterSecureStorage();
            //String? token = await storage.read(key: "token");
            Navigator.of(context).pushReplacement(MaterialPageRoute(
              builder: (context) => SignedInPage(token: token,),
            ));
            //Navigator.pop(context);
          },
          child: const Icon(
            Icons.arrow_back,
            color: Colors.white,
          ),
        ),
        title: Text("Class Details"),
        centerTitle: true,
        backgroundColor: Colors.blue,

      ),
      body: FutureBuilder(future: httpService.getDetails(event_id, occur_id,token, id),
      builder: (BuildContext context, AsyncSnapshot snapshot){

        if (snapshot.connectionState == ConnectionState.done){
          if(snapshot.hasData){
            eventDetails event = snapshot.data;
            return ListView(
              children: [
                ListTile(
                title: Row(
                  children: [
                    SizedBox(width: 30,),
                    Text("Class Type :"),
                    SizedBox(width: 50.0,),
                    Text(event.eventType),
                  ],
                ),
              ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Department :"),
                      SizedBox(width: 50.0,),
                      Text(event.department),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Total Credits :"),
                      SizedBox(width: 50.0,),
                      Text(event.total_credits),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Lecture Credits :"),
                      SizedBox(width: 50.0,),
                      Text(event.lecture_credits),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Tutorial credits :"),
                      SizedBox(width: 50.0,),
                      Text(event.tutorial_credits),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Practical credits :"),
                      SizedBox(width: 50.0,),
                      Text(event.practical_credits),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Course Category :"),
                      SizedBox(width: 30.0,),
                      Container(
                        constraints: BoxConstraints(maxWidth: 100,),
                          child: Text(event.category)
                      ),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Next class day :"),
                      SizedBox(width: 50.0,),
                      Text(event.next_day),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Next date :"),
                      SizedBox(width: 50.0,),
                      Text(event.next_date),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Next start time :"),
                      SizedBox(width: 50.0,),
                      Text(event.next_start_time),
                    ],
                  ),
                ),
                ListTile(
                  title: Row(
                    children: [
                      SizedBox(width: 30,),
                      Text("Next end time :"),
                      SizedBox(width: 50.0,),
                      Text(event.next_end_time),
                    ],
                  ),
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
