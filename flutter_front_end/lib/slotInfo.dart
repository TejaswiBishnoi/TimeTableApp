import 'package:flutter/material.dart';
import 'http_model.dart';
import 'slot.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class SlotInfo extends StatefulWidget {
  SlotInfo({Key? key, required this.faculty, required this.date, required this.duration}) : super(key: key);
  List<String> faculty;
  String date;
  String duration;

  @override
  State<SlotInfo> createState() => _SlotInfoState();
}

class _SlotInfoState extends State<SlotInfo> {
  String? token;

  final storage = new FlutterSecureStorage();

  final HttpService httpService = HttpService();

  Future getToken() async {
    token = await storage.read(key: 'token');
    return token;
  }

  Future<List<Slot>> getData() async {
    token = await getToken();
    //Future.delayed(const Duration(seconds: 2));
    return httpService.getSlotInfo(widget.date,widget.duration,widget.faculty,token);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          leading: IconButton(onPressed: () {
            Navigator.pop(context);
            // Navigator.of(context).popUntil((route) => route.isFirst);
          },
              icon: const Icon(Icons.arrow_back)),
          title: Text("Available Slots"),
          backgroundColor: Colors.blue,
          centerTitle: true,

        ),

      body: FutureBuilder(
        future: getData(),
        builder: (BuildContext context, AsyncSnapshot snapshot){
          if(snapshot.connectionState == ConnectionState.done){
            if(snapshot.data == null){
              return Center(
                child: IconButton(
                  onPressed: (){setState((){});},
                  icon: Icon(Icons.refresh),
                ),
              );
            }
            else{
              List<Slot> slots = snapshot.data;
              return ListView.builder(
                 itemCount: slots.length,
                itemBuilder: (context,index){
                   return ListTile(
                     title: Text('${slots[index].start_time}  -  ${slots[index].end_time}'),
                   );
                },
              );
            }
          }

          else if(snapshot.connectionState == ConnectionState.none){
            return const Text("error");
          }

          else{
            return const Center(child: CircularProgressIndicator());
          }
        },
      ),
    );
  }
}
