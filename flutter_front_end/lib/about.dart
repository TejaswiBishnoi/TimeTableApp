import 'package:flutter/material.dart';
import 'package:flutter_front_end/class_model.dart';
import 'package:flutter_front_end/post_model.dart';
import 'http_model.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:url_launcher/url_launcher.dart';

class About extends StatelessWidget {
  const About({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Team"),
        centerTitle: true,
        backgroundColor: Colors.blue,
        leading: IconButton(
          onPressed: (){
            Navigator.pop(context);
          },
          icon: const Icon(Icons.arrow_back),
        ),
      ),
      body: Container(
        child: ListView(
          children: <Widget>[
            Card(
              child: Container(
                height: 150,
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 100,
                      backgroundColor: Colors.transparent,
                      child: ClipOval(
                        child: Image.asset('assets/vikram.jpg'),
                      ),
                    ),
                    Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text("Vikram Aditya Singh"),
                        SizedBox(height: 10,),
                        Text("2020UCS0080@iitjammu.ac.in",textScaleFactor: 0.7,)
                      ],
                    ),
                  ],
                ),
              ),
            ),
            Card(
              child: Container(
                height: 150,
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 100,
                      backgroundColor: Colors.transparent,
                      child: ClipOval(
                        child: Image.asset('assets/tejaswi.jpg'),
                      ),
                    ),
                    Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text("Tejaswi"),
                        SizedBox(height: 10,),
                        Text("2020UCS0084@iitjammu.ac.in",textScaleFactor: 0.7,)
                      ],
                    ),
                  ],
                ),
              ),
            ),
            Card(
              child: Container(
                height: 150,
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 100,
                      backgroundColor: Colors.transparent,
                      child: ClipOval(
                        child: Image.asset('assets/shrey.jpg'),
                      ),
                    ),
                    Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text("Shrey Sharma"),
                        SizedBox(height: 10,),
                        Text("2020UCS0085@iitjammu.ac.in",textScaleFactor: 0.7,)
                      ],
                    ),
                  ],
                ),
              ),
            ),
            Card(
              child: Container(
                height: 150,
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 100,
                      backgroundColor: Colors.transparent,
                      child: ClipOval(
                        child: Image.asset('assets/aman.jpg'),
                      ),
                    ),
                    Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text("Aman Chotia"),
                        SizedBox(height: 10,),
                        Text("2020UCE0055@iitjammu.ac.in",textScaleFactor: 0.7,)
                      ],
                    ),
                  ],
                ),
              ),
            ),
            Card(
              child: Container(
                height: 150,
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 100,
                      backgroundColor: Colors.transparent,
                      child: ClipOval(
                        child: Image.asset('assets/premvir.jpg'),
                      ),
                    ),
                    Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text("Premveer Singh"),
                        SizedBox(height: 10,),
                        Text("2020UCH0012@iitjammu.ac.in",textScaleFactor: 0.7,)
                      ],
                    ),
                  ],
                ),
              ),
            ),Card(
              child: Container(
                height: 150,
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 100,
                      backgroundColor: Colors.transparent,
                      child: ClipOval(
                        child: Image.asset('assets/rajnish.jpg'),
                      ),
                    ),
                    Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text("Rajnish Yadav"),
                        SizedBox(height: 10,),
                        Text("2020UCH0026@iitjammu.ac.in",textScaleFactor: 0.7,)
                      ],
                    ),
                  ],
                ),
              ),
            ),



          ],
        ),
      ),
    );
  }
}
