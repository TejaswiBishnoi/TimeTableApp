import 'package:cell_calendar/cell_calendar.dart';
import 'package:flutter/material.dart';
import 'package:flutter_front_end/signedin_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'new_week.dart';

class SelectFaculty extends StatefulWidget {
  const SelectFaculty({Key? key}) : super(key: key);

  @override
  State<SelectFaculty> createState() => _SelectFacultyState();
}

class _SelectFacultyState extends State<SelectFaculty> {
  @override
  List<String> facultyName = [];
  int listLength = 0;
  int selectionCount = 0;
  String selection = "Select Faculty Members";
  bool visibility = false;
  List<bool> _value = List.filled(1000,false);
  DateTime datetime = DateTime.now();
  DateTime time_ = DateTime.now();
  String selectDate = "Select Date";
  String selectTime = "Select Time";

  @override
  void initState() {
    facultyName = faculty;
    listLength = faculty.length;
    super.initState();
  }
  Widget build(BuildContext context) {

    //List<String> facultyName = faculty;
    //int listLength= faculty.length;
    return Scaffold(
      appBar: AppBar(
        leading: IconButton(onPressed: () {
          Navigator.pop(context);
        },
            icon: const Icon(Icons.arrow_back)),
        title: Text(selection),
        backgroundColor: Colors.blue,
        centerTitle: true,
        actions: [
          Visibility(
            visible: visibility,
            child: IconButton(
              onPressed: (){
                _dialogBuilder(context);
              },
              icon: const Icon(Icons.search),
            ),
          ),

          const SizedBox(width: 20),
        ],
      ),
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Container(
            margin: const EdgeInsets.fromLTRB(15, 0, 28, 15),
            child: TextField(
              decoration: const InputDecoration(
                label: Text('Search Faculty Name',textScaleFactor: 0.8,),
              ),
              onChanged: (String text){
                  if(text.isEmpty){
                    facultyName = faculty;
                    listLength = faculty.length;
                  }
                  else {
                    print("i am here");
                    final List<String> filteredFacultyNames = [];
                    faculty.map((name) {
                      if (name.toLowerCase().contains(text.toLowerCase())) {
                        filteredFacultyNames.add(name);
                      }
                    }).toList();
                    setState(() {
                      facultyName=filteredFacultyNames;
                      listLength=filteredFacultyNames.length;
                    });
                  }
              },
            ),
          ),
          Expanded(
            flex: 1,
            child: ListView.builder(itemCount: listLength ,itemBuilder: (context,index){
              return CheckboxListTile(
              title: Text(facultyName[index]),
              subtitle: const Text('computer science & engineering'),
              autofocus: false,
              activeColor: Colors.blue,
              checkColor: Colors.white,
              selected: _value[index],
              dense: true,
              value: _value[index],
              onChanged: (bool? value) {
              bool val;
              if(value==null){
              val=false;
              }
              else{
              val= value;
              }
              setState((){
                selectionCount+= (val==true) ? 1:-1;
                selection = selectionCount!=0 ? "Selected $selectionCount" : "Select Faculty Members";
                _value[index] = val;
                visibility = selectionCount>0 ? true : false;
              });

              },
              );
            }),
          ),
        ],
      )
    );
  }



  List<String> faculty = <String>[
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

  Future<DateTime?> pickDate() => showDatePicker(
    context: context,
    initialDate: datetime,
    firstDate: DateTime(2000),
    lastDate: DateTime(2100),
  );

  Future<TimeOfDay?> pickTime() => showTimePicker(
      context: context,
      initialTime: TimeOfDay(hour: time_.hour, minute: time_.minute),
  );

  Future<void> _dialogBuilder(context){
    return showDialog<void>(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: Text('Select Date & Time'),
            content: StatefulBuilder(
              builder: (BuildContext context, StateSetter setState){
                return Wrap(
                  direction: Axis.vertical,
                  children: [
                    InkWell(
                      onTap: () async{
                  final date = await pickDate();

                  setState((){
                    if(date!=null){
                      selectDate='${date.day}/${date.month}/${date.year}';
                    }
                  });
                },
                      child: Row(
                        children: [
                        const Icon(
                           Icons.date_range,
                         ),
                          SizedBox(width: 15,),
                          Text(selectDate),
                        ],

                      ),
                    ),
                    SizedBox(height: 20,),
                    InkWell(
                      onTap: () async{
                        final time = await pickTime();
                        setState((){
                          if(time!=null){
                            selectTime=('${time.hour.toString().padLeft(2,'')}:${time.minute.toString().padLeft(2,'')}');
                            time_ =  DateTime(
                              datetime.year,
                              datetime.month,
                              datetime.day,
                              time.hour,
                              time.minute,
                            );
                          }
                        });
                      },
                      child: Row(
                        children: [
                          Icon(
                             Icons.access_time
                          ),
                          SizedBox(width: 15,),
                          Text(selectTime)
                        ],
                      ),
                    ),
                  ],
                );
              },
            ),
          );
        }
    );
  }

}
