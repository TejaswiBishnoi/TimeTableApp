import 'package:flutter/material.dart';
import 'package:flutter_front_end/event_desc.dart';
import 'google_signin_api.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'http_model.dart';
import 'post_model.dart';

class NewWeek extends StatefulWidget {
  final String? token;
  final String date;
  const NewWeek({
    Key? key,
    required this.token,
    required this.date,
  }) : super (key: key);

  @override
  State<NewWeek> createState() => _NewWeek(date: date);
}

class _NewWeek extends State<NewWeek> {
  final storage = new FlutterSecureStorage();
  final HttpService httpService = HttpService();
  String? token;
  String date;

  String selectedVal = "";


  _NewWeek({
    required this.date,
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
    return httpService.getPosts(token, date);
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
      appBar: AppBar(
        title: Text("Weekly Schedule  "),
        centerTitle: true,
        backgroundColor: Colors.blue,
        leading: IconButton(
          onPressed: (){
            Navigator.pop(context);
          },
          icon: const Icon(Icons.arrow_back),
        ),
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
                children: week!.map((Daily day) => RefreshIndicator(
                  onRefresh: (){
                    return Navigator.of(context).pushReplacement(MaterialPageRoute(
                        builder: (context) => StatefulBuilder(builder: (BuildContext context, setState) { return NewWeek(token: token,date: date,); },)
                    ));
                  },
                  child: CustomScrollView(
                    slivers: <Widget>[
                      SliverAppBar(
                        automaticallyImplyLeading: false,
                        pinned: true,
                        expandedHeight: 130.0,
                        flexibleSpace: FlexibleSpaceBar(
                            title: Text("${day.day}\n${day.date}", textScaleFactor: 0.8,)


                        ),
                      ),
                      const SliverToBoxAdapter(
                        child: SizedBox(
                          height: 0,
                          child: Center(
                            child: Text('Scroll to see the SliverAppBar in effect.'),
                          ),
                        ),
                      ),
                      SliverList(
                        delegate: SliverChildBuilderDelegate(
                              (context, index) {
                            return Card(
                              child: InkWell(
                                onTap: (){Navigator.of(context).push(MaterialPageRoute(
                                  builder: (context) =>  EventDetails(occur_id: day.event_list![index].occur_id, event_id: day.event_list![index].event_id,token: token,id: day.event_list![index].event_id,httpService: httpService,),
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
                  ),
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