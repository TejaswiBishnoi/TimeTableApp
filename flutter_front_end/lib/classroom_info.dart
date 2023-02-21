import 'package:flutter/material.dart';


class ClassInfo extends StatelessWidget {
  const ClassInfo({Key? key, required this.classNo}) : super(key: key);

  final String classNo;


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
      body: Text(classNo),
    );
  }
}

