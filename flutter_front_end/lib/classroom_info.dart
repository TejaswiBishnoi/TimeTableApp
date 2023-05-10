import 'package:flutter/material.dart';
import 'package:flutter_front_end/class_model.dart';
import 'package:flutter_front_end/post_model.dart';
import 'http_model.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'event_desc.dart';


class ClassInfo extends StatefulWidget {
  const ClassInfo({Key? key, required this.classNo, required this.date}) : super(key: key);
  final String date;
  final String classNo;
  @override
  State<ClassInfo> createState() => _ClassInfoState(date: date, classNo: classNo);
}

class _ClassInfoState extends State<ClassInfo> {
  final storage = new FlutterSecureStorage();
  final HttpService httpService = HttpService();
  String? token;
  String date;
  String classNo;
  final scenery = {'Monday': 'scenery.jpeg','Tuesday': 'scenery2.jpeg','Wednesday': 'scenery3.jpeg','Thursday': 'scenery4.jpeg','Friday': 'scenery5.jpeg','Saturday': 'scenery6.jpeg','Sunday': 'scenery7.jpeg'};
  _ClassInfoState({required this.date,required this.classNo});

  Future getToken() async {
    token = await storage.read(key: 'token');
    return token;
  }

  Future<List<DailyEvents>> getData() async {
    await getToken();
    //Future.delayed(const Duration(seconds: 2));
    print(token);
    return httpService.getClassInfo(token, date, classNo);
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Classroom Information"),
        centerTitle: true,
        backgroundColor: Colors.blue,
        leading: IconButton(
          onPressed: (){
            Navigator.pop(context);
          },
          icon: const Icon(Icons.arrow_back),
        ),
      ),
      body: FutureBuilder(
        future: getData(),
        builder: (BuildContext context, AsyncSnapshot snapshot){
          if(snapshot.connectionState == ConnectionState.done){
            if(snapshot.data==null){
              return Center(
                child: IconButton(
                  onPressed: (){
                    setState(() {

                    });
                  },
                  icon: Icon(Icons.refresh),
                ),
              );
            }
            else{
              List<DailyEvents>? week = snapshot.data;
              return PageView(
                children: week!.map((DailyEvents day) => CustomScrollView(
                  slivers: <Widget>[
                    SliverAppBar(
                      automaticallyImplyLeading: false,
                      pinned: true,
                      expandedHeight: 130.0,

                      flexibleSpace: Stack(
                        children: <Widget>[

                          // Positioned.fill(
                          //   child: Image.asset(
                          //     'assets/${scenery[day.day]}',
                          //     fit: BoxFit.cover,
                          //   ),
                          // ),
                          FlexibleSpaceBar(
                              title: Text("${day.day}\n${day.date}", textScaleFactor: 1,),
                          ),
                  ]
                      ),
                    ),

                    SliverList(
                      delegate: SliverChildBuilderDelegate(
                          (context,index) {
                            return SizedBox(
                              height: 100,
                              child: InkWell(
                                onTap: (){
                                  Navigator.of(context).push(MaterialPageRoute(
                                    builder: (context) =>  EventDetails(title: day.event_list![index].course_name,occur_id: day.event_list![index].occur_id, event_id: day.event_list![index].event_id,token: token,id: day.event_list![index].event_id,httpService: httpService,),
                                  ));
                                },
                                child: Card(
                                  child: Row(
                                    children: [
                                      const SizedBox(width: 20,),
                                      Text(day.event_list![index].start_time),
                                      const SizedBox(width: 20,),
                                      Text(day.event_list![index].end_time),
                                      const SizedBox(width: 20,),
                                      Expanded(child: Text(day.event_list![index].faculty,))
                                    ],
                                  ),
                                ),
                              ),
                            );
                          },
                        childCount: day.event_list!.length,
                      ),
                    ),
                  ],
                ),
                  /* ListView.builder(
                  itemCount: day.event_list!.length+1,
                  itemBuilder: (BuildContext context, int index){
                    if(index==0){
                      return SizedBox(
                        height: 150,
                        child: Card(
                          color: Colors.blue,
                          child: Text("${day.day}\n${day.date}",
                          style: const TextStyle(
                            color: Colors.white,
                          ),
                          ),
                        ) ,
                      );
                    }
                    return SizedBox(
                      height: 100,
                      child: Card(
                        child: Row(
                          children: [
                            const SizedBox(width: 20,),
                            Text(day.event_list![index-1].start_time),
                            const SizedBox(width: 20,),
                            Text(day.event_list![index-1].end_time),
                            const SizedBox(width: 20,),
                            Text(day.event_list![index-1].faculty)
                          ],
                        ),
                      ),
                    );
                  },
                ), */
              ).toList());
            }
          }
          else if (snapshot.connectionState == ConnectionState.none) {
            return const Text('Error'); // error
          } else {
            return const Center(child: CircularProgressIndicator()); // loading
          }
        },
      ),
    );
  }
}

