import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_front_end/event_desc.dart';
import 'package:flutter_front_end/select_faculty.dart';
import 'package:url_launcher/url_launcher.dart';
import 'google_signin_api.dart';
import 'signup_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'http_model.dart';
import 'post_model.dart';
import 'calendar.dart';
import 'classroom_info.dart';
import 'faculty_schedule.dart';
import 'select_faculty.dart';
import 'package:file_picker/file_picker.dart';
import 'about.dart';


class SignedInPage extends StatefulWidget {
 final String? token;
 final String date;
 final String photoUrl;
  const SignedInPage({
    Key? key,
    required this.token,
    required this.date,
    required this.photoUrl,
}) : super (key: key);

  @override
  State<SignedInPage> createState() => _SignedInPageState(date: date,photoUrl: photoUrl);
}

class _SignedInPageState extends State<SignedInPage> {
  final storage = new FlutterSecureStorage();
  final HttpService httpService = HttpService();
  String? token;
  String date;

  String selectedVal = "";
  final scenery = {'Monday': 'scenery.jpeg','Tuesday': 'scenery2.jpeg','Wednesday': 'scenery3.jpeg','Thursday': 'scenery4.jpeg','Friday': 'scenery5.jpeg','Saturday': 'scenery6.jpeg','Sunday': 'scenery7.jpeg'};
  final photoUrl;
  _SignedInPageState({
    required this.date,
    required this.photoUrl,
}) ;

  Future getToken() async {
    token = await storage.read(key: 'token');
    return token;
  }

  /*void initState(){
    getToken();
    print(token);
  }*/
  Future<List<Daily>> getData() async {
    await getToken();
    //Future.delayed(const Duration(seconds: 2));
    print(token);
    return httpService.getPosts(token, date, "");
  }
  //Future<List<Daily>>? future;
 // _SignedInPageState({required this.token});
  @override
  /*void initState(){
    print("here");
    super.initState();
    future = update();
    print("after update");
  }
  Future<List<Daily>> update() async {
    print("in update");
    return await httpService.getPosts(token);
  }*/

  Widget build(BuildContext context) {
    return Scaffold(
      drawer: Drawer(
        backgroundColor: Colors.white70,
        child: ListView(
          children: <Widget>[
            const SizedBox(height: 50,),
             DrawerHeader(child:
            CircleAvatar(
              backgroundColor: Colors.transparent,
              child: ClipOval(
                child: Image.network(photoUrl!),
              ),
            ) ,
              //Text("Options",
            //  textAlign: TextAlign.center,
            //  textScaleFactor: 2,
            //   ),
           ),
            

            InkWell(
              onTap: <Future>(){
                return _dialogBuilder(context, "rooms");
              },
              child: ListTile(
                //color: Colors.white38
                title: SizedBox(
                  height: 40,
                  child: Row(
                    children: const [
                      SizedBox(width: 50,),
                      Text("Classroom info",
                        textScaleFactor: 1.2,
                      ),
                    ],
                  ),
                ),
              ),
            ),

            InkWell(
              onTap: <Future>(){
                return _dialogBuilder(context, "schedule");
              },
              child: ListTile(
                //color: Colors.white38
                title: SizedBox(
                  height: 40,
                  child: Row(
                    children: const [
                      SizedBox(width: 50,),
                      Text("Faculty Schedule",
                        textScaleFactor: 1.2,
                      ),
                    ],
                  ),
                ),
              ),
            ),

            InkWell(
              onTap: <Future>(){
                //   },
                Navigator.of(context).push(MaterialPageRoute( // do not use pushReplacement
                    builder: (context) => SelectFaculty()));
              },
              child: ListTile(
                //color: Colors.white38
                title: SizedBox(
                  height: 40,
                  child: Row(
                    children: const [
                      SizedBox(width: 50,),
                      Text("Common Slot",
                        textScaleFactor: 1.2,
                      ),
                    ],
                  ),
                ),
              ),
            ),

            InkWell(
              onTap: <Future>(){
                return _dialogBuilder(context, "calendar");
              },
              child: ListTile(
                //color: Colors.white38
                title: SizedBox(
                  height: 40,
                  child: Row(
                    children: const [
                      SizedBox(width: 50,),
                      Text("Sync with Calendar",
                        textScaleFactor: 1.2,
                      ),
                    ],
                  ),
                ),
              ),
            ),

            // InkWell(
            //   onTap: <Future>(){
            //     //   },
            //     Navigator.of(context).push(MaterialPageRoute( // do not use pushReplacement
            //         builder: (context) => About()));
            //   },
            //   child: ListTile(
            //     //color: Colors.white38
            //     title: SizedBox(
            //       height: 40,
            //       child: Row(
            //         children: const [
            //           SizedBox(width: 50,),
            //           Text("About",
            //             textScaleFactor: 1.2,
            //           ),
            //         ],
            //       ),
            //     ),
            //   ),
            // ),

            const SizedBox(height: 100,),

            Container(
                child: Align(
                    alignment: FractionalOffset.bottomCenter,
                    child: Column(
                      children: <Widget>[
                        Divider(),
                        const SizedBox(height: 20,),
                        InkWell(
                          onTap: ()=>launchUrl(Uri.parse('https://iitjammu.ac.in')),
                          child: const ListTile(
                              leading: Icon(Icons.school),
                              title: Text('IIT Jammu')),
                        ),
                        InkWell(
                          onTap: ()=>Navigator.of(context).push(MaterialPageRoute( // do not use pushReplacement
                                   builder: (context) => About())),
                          child: ListTile(
                              leading: Icon(Icons.help),
                              title: Text('About')),
                        )
                      ],
                    ))),
        // InkWell(
          //   onTap: <Future>()async{
          //     FilePickerResult? result = await FilePicker.platform.pickFiles();
          //
          //     if (result != null) {
          //       File file = File(result.files.single.path!);
          //     } else {
          //       ScaffoldMessenger.of(context).showSnackBar(SnackBar(
          //         content: Text("No File Selected"),
          //       ));
          //     }
            // InkWell(
            //   onTap: <Future>()async{
            //     FilePickerResult? result = await FilePicker.platform.pickFiles();
            //
            //     if (result != null) {
            //       File file = File(result.files.single.path!);
            //     } else {
            //       ScaffoldMessenger.of(context).showSnackBar(SnackBar(
            //         content: Text("No File Selected"),
            //       ));
            //     }
            //   },
            //   child: ListTile(
            //     //color: Colors.white38
            //     title: SizedBox(
            //       height: 40,
            //       child: Row(
            //         children: const [
            //           SizedBox(width: 50,),
            //           Text("Upload File",
            //             textScaleFactor: 1.2,
            //           ),
            //         ],
            //       ),
            //     ),
            //   ),
            // ),
            // InkWell(
            //   onTap: <Future>()async{
            //
            //   },
            //   child: ListTile(
            //     //color: Colors.white38
            //     title: SizedBox(
            //       height: 40,
            //       child: Row(
            //         children: const [
            //           SizedBox(width: 50,),
            //           Text("Insert in Calendar",
            //             textScaleFactor: 1.2,
            //           ),
            //         ],
            //       ),
            //     ),
            //   ),
            // )

          ],
        ),
      ),
    appBar: AppBar(
      title: Text("Weekly Schedule  "),
      centerTitle: true,
      backgroundColor: Colors.blue,
      actions: [
        IconButton(
            onPressed: (){

            /*  () async{
                print("here");
                await GoogleSignInAPI.logout();
                print("reached here");
                httpService.dir.deleteSync(recursive: true);
                await storage.delete(key: 'token');

              }; */
              /*Navigator.of(context).pushReplacement(MaterialPageRoute(
                  builder: (context) => SignupPage(),
              ));*/
              //Navigator.of(context,rootNavigator: true).pop(context);
              GoogleSignInAPI.logout();
              storage.delete(key: 'token');
              //Navigator.of(context,rootNavigator: true).pop(context);
              Navigator.pop(context);
            },
          icon: const Icon(Icons.logout),

        )
      ],
    ),
    body: FutureBuilder(future: Future.delayed(const Duration(milliseconds: 500),() => getData()),
    builder: (BuildContext context, AsyncSnapshot snapshot){
      if (snapshot.connectionState == ConnectionState.done) {
        if (snapshot.data == null) {
          return Center(
            child: IconButton(onPressed: (){
              /*Navigator.of(context).push(MaterialPageRoute(
                  builder: (context) => StatefulBuilder(builder: (BuildContext context, setState) { return SignedInPage(token: token, date: date,); },)
              )); */
              setState(() {

              });
            },
                icon: const Icon(Icons.refresh)),
          );
        } else {
          List<Daily>? week = snapshot.data;

          return PageView(
            children: week!.map((Daily day) => CustomScrollView(  // RefreshIndicator widget removed here
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
                      title: Text("${day.day}\n${day.date}", textScaleFactor: 0.8,)
                    ),
              ],
                  ),
                ),
                // const SliverToBoxAdapter(
                //   child: SizedBox(
                //     height: 5,
                //     child: Center(
                //       child: Text('Scroll to see the SliverAppBar in effect.'),
                //     ),
                //   ),
                // ),
                SliverList(
                  delegate: SliverChildBuilderDelegate(
                        (context, index) {
                      return Card(
                          child: InkWell(
                            onTap: (){Navigator.of(context).push(MaterialPageRoute(
                              builder: (context) =>  EventDetails(title: day.event_list![index].course_name,occur_id: day.event_list![index].occur_id, event_id: day.event_list![index].event_id,token: token,id: day.event_list![index].event_id,httpService: httpService,),
                            ));
                              },
                            child: SizedBox(height: 80,
                              child: Row(children: [
                              SizedBox(width: 10,),
                              Text(day.event_list![index].start_time+' - ',
                              ),
                              Text(day.event_list![index].end_time),
                              SizedBox(width: 40,),
                              Container(
                                constraints: BoxConstraints(maxWidth: 150),
                                child: Text(day.event_list![index].course_name,
                                ),
                              ),
                              //SizedBox(width: 20,),
                              //Text(day.event_list![index].section),
                            ],),),
                          ),
                      );
                    },
                    childCount: day.event_list?.length,
                  ),
                ),
              ],
            )).toList(),
          );
        }
      } else if (snapshot.connectionState == ConnectionState.none) {
        return const Text('Error'); // error
      } else {
        return const Center(child: CircularProgressIndicator()); // loading
      }

        },
    ),
      floatingActionButton: FloatingActionButton(
        onPressed: (){
          Navigator.of(context).push(MaterialPageRoute( // do not use pushReplacement
            builder: (context) => Calendar(faculty: "",),
          ));
        },
        child: Icon(Icons.calendar_month),
      ),
    );
  }






  static const List<String> classroom = <String>[
    '01-AC-1-01',
    '01-AC-1-02',
    '01-AC-1-03',
    '01-AC-1-04',
    '01-AC-2-01',
    '01-AC-2-03',
    '01-AC-2-07',
    '01-AC-2-08',
    '01-AC-2-09',
    '01-AC-2-10',
    '01-AC-2-14',
    '01-AC-2-15',
    '01-AC-2-16',
    '01-AC-2-19',
    '01-AC-2-20',
    '01-AC-2-21',
    'C3I Lab-I',
    'C3I Lab-II',
    'C3I Lab-III'
  ];

  static const List<String> faculty = <String>[
    'Manoj Singh Gaur',
    'Pankaj Chauhan',
    'Anup Shukla',
    'Ashok Bera',
    'Kushmanda Saurav',
    'Subhas Samanta',
    'Rahul Raghunath Salunkhe',
    'Nitin Joshi',
    'Karan Nathwani',
    'Badri Narayan Subudhi',
    'Sanat Kumar Tiwari',
    'Samrat Rao',
    'Ravikant Saini',
    'Anand Kumar Subramaniyan',
    'Rajendra Kumar Varma',
    'Shivnath Mazumder',
    'Tanmay Sarkar',
    'Ajay Singh',
    'Vinit Jakhetiya',
    'Alok Kumar Saxena',
    'Shiva S',
    'Yamuna Prasad',
    'Vijay Kumar Pal',
    'Arvind Kumar Rajput',
    'Goutam Dutta',
    'Ankit Dubey',
    'Durai Prabhakaran R T',
    'Ajeet Kumar Sharma',
    'Ajay Kumar',
    'Sudhakar Modem',
    'Gaurav Ashok Bhaduri',
    'Kankat Ghosh',
    'Gaurav Varshney',
    'Sumit Kumar Pandey',
    'Sameer Kumar Sarma Pachalla',
    'Sahil Kalra',
    'Surendra Beniwal',
    'Sukanya Mondal',
    'Rimen Jamatia',
    'Joby Varghese',
    'Satya Sekhar Bhogilla',
    'Guru Brahamam Ramani',
    'Sayantan Mandal',
    'Saurabh Biswas',
    'Subhasis Bhattacharjee',
    'Shantanu Vijay Madge',
    'Jayaramulu Kolleboyina',
    'Venkata Sathish Akella',
    'Ankur Bansal',
    'Amlan Kumar Pal',
    'Biswanath Chakraborty',
    'Yogesh Madhukarrao Nimdeo',
    'Dharitri Rath',
    'Ravi Kumar Arun',
    'Ambika Prasad Shah',
    'Roshan Udaram Patil',
    'Rahul Dattatraya Kitture',
    'Manmohan Vashisth',
    'Srishilan C',
    'Ankit Tyagi',
    'Rani Rohini',
    'Riya Bhowmik',
    'Rajiv Kumar',
    'Divyesh Varade',
    'Ashutosh Yadav',
    'Pervaiz Fathima Khatoon M',
    'Suman Banerjee',
    'Navneet Kumar',
    'Chembolu Vinay',
    'Bhaskar Jyoti Neog',
    'Pothukuchi Harish',
    'Shaifu Gupta',
    'Rajkumar V',
    'Shanmugadas K P',
    'Pratik Kumar',
    'Suresh Roland Devasahayam',
    'Suman Sarkar',
    'Aditya Shankar Sandupatla',
    'Maya Kini K',
    'Srinivasan N',
    'Vinay Sharma',
    'Arvind Kumar',
    'Prasun Halder',
    'Sivakumar G',
    'Pallippattu Krishnan Vijayan',
    'Kannan Iyer',
    'Anurag Misra',
    'Rakesh Singhai',
    'Anju Chadha',
    'Harkeerat Kaur',
    'Uma Shankar'
  ];

  Future<void> _dialogBuilder(BuildContext context, String type) {
    return showDialog<void>(
      context: context,
      builder: (BuildContext context) {
        if(type == "rooms"){
          List<String> _kOptions = classroom;
          String classNo = "";
          return AlertDialog(
            content: const Text("Enter Room No."),
            actions: <Widget>[
              //AutocompleteClass(val: 0,),
          Autocomplete<String>(
              optionsBuilder: (TextEditingValue textEditingValue) {
            if (textEditingValue.text == '') {
              return const Iterable<String>.empty();
            }
            return _kOptions.where((String option) {
              return option.toLowerCase().contains(textEditingValue.text.toLowerCase());
            });
          },
        onSelected: (String selection) {
            classNo = selection;
        },
        ),
              TextButton(
                style: TextButton.styleFrom(
                  textStyle: Theme.of(context).textTheme.labelLarge,
                ),
                child: const Text("Search"),
                onPressed: () {
                  Navigator.of(context).push(MaterialPageRoute(
                    builder: (context) =>  ClassInfo(classNo: classNo, date: date,),
                  ));
                  //Navigator.of(context).pop();
                },
              ),
            ],
          );
        }

        else if(type == "calendar"){
          return AlertDialog(
            content: const Text("Are you sure you want to sync your schedule with Google Calendar"),
            actions: <Widget>[
              TextButton(
                style: TextButton.styleFrom(
                  textStyle: Theme.of(context).textTheme.labelLarge,
                ),
                child: const Text("No"),
                onPressed: () {
                    Navigator.of(context).pop();
                },
              ),

              TextButton(
                style: TextButton.styleFrom(
                  textStyle: Theme.of(context).textTheme.labelLarge,
                ),
                child: const Text("Sync"),
                onPressed: () {
                  httpService.syncCalendar(token);
                },
              ),
            ],
          );
        }
        List<String> _kOptions = faculty;
        String name = "";
        return AlertDialog(
          content: const Text("Enter Faculty Name"),
          actions: <Widget>[
            //AutocompleteClass(val: 1,),

        Autocomplete<String>(
            optionsBuilder: (TextEditingValue textEditingValue) {
          if (textEditingValue.text == '') {
            return const Iterable<String>.empty();
          }
          return _kOptions.where((String option) {
            return option.toLowerCase().contains(textEditingValue.text.toLowerCase());
          });
        },
        onSelected: (String selection) {
              name = selection;
        },
        ),
            TextButton(
              style: TextButton.styleFrom(
                textStyle: Theme.of(context).textTheme.labelLarge,
              ),
              child: const Text('Search'),
              onPressed: () {
               // Navigator.of(context).pop();
                Navigator.of(context).push(MaterialPageRoute(
                  builder: (context) =>  FacultySched(faculty: name, date: date, token: token,),
                ));
              },
            ),
          ],
        );

      },
    );
  }
}


/*
class AutocompleteClass extends StatelessWidget {
  const AutocompleteClass({super.key, required this.val});
  final val;

  static const List<String> classroom = <String>[
    '01-AC-1-01',
    '01-AC-1-03',
    '01-AC-2-01',
    '01-AC-2-03',
    '01-AC-2-07',
    '01-AC-2-08',
    '01-AC-2-09',
    '01-AC-2-10',
    '01-AC-2-14',
    '01-AC-2-15',
    '01-AC-2-16',
    '01-AC-2-19',
    '01-AC-2-20',
    '01-AC-2-21',
    'C3I Lab-I',
    'C3I Lab-II',
    'C3I Lab-III'
  ];

  static const List<String> faculty = <String>[
    'Manoj Singh Gaur',
    'Pankaj Chauhan',
    'Anup Shukla',
    'Ashok Bera',
    'Kushmanda Saurav',
    'Subhas Samanta',
    'Rahul Raghunath Salunkhe',
    'Nitin Joshi',
    'Karan Nathwani',
    'Badri Narayan Subudhi',
    'Sanat Kumar Tiwari',
    'Samrat Rao',
    'Ravikant Saini',
    'Anand Kumar Subramaniyan',
    'Rajendra Kumar Varma',
    'Shivnath Mazumder',
    'Tanmay Sarkar',
    'Ajay Singh',
    'Vinit Jakhetiya',
    'Alok Kumar Saxena',
    'Shiva S',
    'Yamuna Prasad',
    'Vijay Kumar Pal',
    'Arvind Kumar Rajput',
    'Goutam Dutta',
    'Ankit Dubey',
    'Durai Prabhakaran R T',
    'Ajeet Kumar Sharma',
    'Ajay Kumar',
    'Sudhakar Modem',
    'Gaurav Ashok Bhaduri',
    'Kankat Ghosh',
    'Gaurav Varshney',
    'Sumit Kumar Pandey',
    'Sameer Kumar Sarma Pachalla',
    'Sahil Kalra',
    'Surendra Beniwal',
    'Sukanya Mondal',
    'Rimen Jamatia',
    'Joby Varghese',
    'Satya Sekhar Bhogilla',
    'Guru Brahamam Ramani',
    'Sayantan Mandal',
    'Saurabh Biswas',
    'Subhasis Bhattacharjee',
    'Shantanu Vijay Madge',
    'Jayaramulu Kolleboyina',
    'Venkata Sathish Akella',
    'Ankur Bansal',
    'Amlan Kumar Pal',
    'Biswanath Chakraborty',
    'Yogesh Madhukarrao Nimdeo',
    'Dharitri Rath',
    'Ravi Kumar Arun',
    'Ambika Prasad Shah',
    'Roshan Udaram Patil',
    'Rahul Dattatraya Kitture',
    'Manmohan Vashisth',
    'Srishilan C',
    'Ankit Tyagi',
    'Rani Rohini',
    'Riya Bhowmik',
    'Rajiv Kumar',
    'Divyesh Varade',
    'Ashutosh Yadav',
    'Pervaiz Fathima Khatoon M',
    'Suman Banerjee',
    'Navneet Kumar',
    'Chembolu Vinay',
    'Bhaskar Jyoti Neog',
    'Pothukuchi Harish',
    'Shaifu Gupta',
    'Rajkumar V',
    'Shanmugadas K P',
    'Pratik Kumar',
    'Suresh Roland Devasahayam',
    'Suman Sarkar',
    'Aditya Shankar Sandupatla',
    'Maya Kini K',
    'Srinivasan N',
    'Vinay Sharma',
    'Arvind Kumar',
    'Prasun Halder',
    'Sivakumar G',
    'Pallippattu Krishnan Vijayan',
    'Kannan Iyer',
    'Anurag Misra',
    'Rakesh Singhai',
    'Anju Chadha',
    'Harkeerat Kaur',
    'Uma Shankar'
  ];



  @override
  Widget build(BuildContext context) {
    List<String> _kOptions;
    if(val==0){
       _kOptions = classroom;
    }
    else{
      _kOptions = faculty;
    }
    return Autocomplete<String>(
      optionsBuilder: (TextEditingValue textEditingValue) {
        if (textEditingValue.text == '') {
          return const Iterable<String>.empty();
        }
        return _kOptions.where((String option) {
          return option.toLowerCase().contains(textEditingValue.text.toLowerCase());
        });
      },
      onSelected: (String selection) {
      },
    );
  }
}

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



/*
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
 */



/*
children: week!.map((Daily day) => CustomScrollView(
              slivers: <Widget>[
                SliverAppBar(
                  pinned: true,
                  expandedHeight: 160.0,
                  flexibleSpace: FlexibleSpaceBar(
                    title: Text(day.day),

                  ),
                ),
                SliverToBoxAdapter(
                  child: SizedBox(
                    height: 20,
                    child: Center(
                      child: Text('Scroll to see the SliverAppBar in effect.'),
                    ),
                  ),
                ),
                SliverList(
                  delegate: SliverChildBuilderDelegate(
                        (BuildContext context, int index) {
                      return ListView(
                        children: day.event_list != null ? day.event_list!.map((Events event) => Card(
                        )).toList() : [],
                      );
                    },
                    childCount: 20,
                  ),
                ),
              ],
            )).toList(),
 */