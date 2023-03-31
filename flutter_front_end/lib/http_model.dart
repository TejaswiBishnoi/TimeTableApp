import 'dart:convert';
import 'dart:io';
import 'post_model.dart';
import 'event_model.dart';
import 'class_model.dart';
import 'slot.dart';
import 'package:http/http.dart';
import 'package:path_provider/path_provider.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';


class HttpService {
  final String postUrl = "http://192.168.137.1:5143/Schedule/Weekd";
  final String eventUrl = "http://192.168.137.1:5143/Schedule/EventDetails";
  final String classUrl = "http://192.168.137.1:5143/Schedule/Cweek";
  final String slotUrl = "http://192.168.137.1:5143/Meet/Meet";


  dynamic dir;

  Future<void> getDir() async {
    dir = await getTemporaryDirectory();
  }

  Future<List<Slot>> getSlotInfo(String date,int duration, List<String> faculty,String? token) async {
    print("get slots");
    print(faculty);
    print(date);
    print(duration);
    print(token);
    Response res = await post(Uri.parse(slotUrl),body: json.encode({'date': date,'duration': duration,'faculty': faculty}),headers: {"accesstoken":"bearer $token","Content-Type":"application/json"});
    // print(json.encode({'date': date,'duration': duration,'faculty': faculty}));
    // print(res.statusCode);
    if(res.statusCode == 200){
      List<dynamic> body = jsonDecode(res.body);
      print(body);
      List<Slot> slots = body.map<Slot>((dynamic item) => Slot.fromJson(item)).toList();
      return slots;
    }
    else{
      throw "Can't get slot information";
    }
  }

  Future<List<DailyEvents>> getClassInfo(String? token, String date, String classNo) async {
    String d1 = date.substring(0,4);
    String d2 = date.substring(5,7);
    String d3 = date.substring(8,10);
    Response res = await get(Uri.parse(classUrl+'?date='+d3+'-'+d2+'-'+d1+"&code="+classNo),headers: {"accesstoken":"bearer $token"});
    print(d3+d2+d1);
    print(res.body);
    if(res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<DailyEvents> Week = body.map<DailyEvents>((dynamic item) => DailyEvents.fromJson(item)).toList();
      return Week;
    }
    else{
      throw "Cant get schedule";
    }
  }




  Future<List<Daily>> getPosts(String? token, String date, String faculty) async {
    String filename = "user.json";
    if(dir==null){
      await getDir();
    }
    File file = File('${dir.path}/$filename');
    //final storage = const FlutterSecureStorage();
    //String? t = await storage.read(key: 'token');
    //token = t;
    //print(t);
    if(file.existsSync() && date.substring(0,10)==DateTime.now().toString().substring(0,10) && faculty==""){

        print("loading from cache");
        //print(token);
        //file.delete();
        var jsondata = file.readAsStringSync();
        Response res = Response(jsonDecode(jsondata), 200);
        //print(res.body);
        List<dynamic> body = jsonDecode(res.body);
        List<Daily> Week = body.map<Daily>((dynamic item) => Daily.fromJson(item)).toList();

        return Week;
    }
    else{
      print("loading form api");
      print(date);
      String d1 = date.substring(0,4);
      String d2 = date.substring(5,7);
      String d3 = date.substring(8,10);

      //var chars = date.split('');
      //date = chars.reversed.join();
      //print(date);
      //token = await storage.read(key: 'token');
      //print("no token =====$token");
      print(token);
      print(d3+d2+d1);
      Response res = await get(Uri.parse(postUrl+'?date='+d3+'-'+d2+'-'+d1+"&name=$faculty"),headers: {"accesstoken":"bearer $token"});
      //print(res.statusCode);
      print(date.substring(0,10));
      print(DateTime.now().toString().substring(0,10));
      if(res.statusCode == 200) {
        if(date.substring(0,10) == DateTime.now().toString().substring(0,10) && faculty==""){
          file.writeAsString(jsonEncode(res.body),flush: true, mode: FileMode.write);
        }

        List<dynamic> body = jsonDecode(res.body);
        List<Daily> Week = body.map<Daily>((dynamic item) => Daily.fromJson(item)).toList();
        return Week;
      }
      else{
        throw "Cant get schedule";
      }
    }
  }
  Future<eventDetails> getDetails(String? event_id, String? occur_id, String? token,String? id) async {
    //print("event functino");
    String filename = "$id.json";
    File file = File('${dir.path}/$filename');
   /* if(file.existsSync()){
      //print("loading from cache");
      var jsondata = file.readAsStringSync();
      Response res = Response(jsonDecode(jsondata), 200);
      //print(res.body);
      dynamic body = jsonDecode(res.body);
     // print(body);
      //file.delete();
      eventDetails event = eventDetails.fromJson(body);
      //print(event.next_end_time);
      return event;
    }
    else{ */
      print("loading form api");
      String url = "$eventUrl?eventID=$event_id&occurenceID=$occur_id";
      Response res = await get(Uri.parse(url),headers: {"accesstoken": "$token"});
      file.writeAsString(jsonEncode(res.body),flush: true, mode: FileMode.write);
      if(res.statusCode == 200) {
        dynamic body = jsonDecode(res.body);
        eventDetails event = eventDetails.fromJson(body);
        return event;
      }
      else{
        throw "Cant get schedule";
      }
    //}
  }
}



/*import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:localstorage/localstorage.dart';
import 'post_model.dart';
import 'package:http/http.dart';


class HttpService {
  final String postUrl = "http://192.168.193.105:5143/Schedule/WeekT";
  static LocalStorage storage = new LocalStorage("list");

  Future<List<Daily>> getPosts() async{
    await storage.ready;
    List<Daily> Week = storage.getItem("list");
    if(Week == []){
      print("this will get printed");
      return getPostsFromServer();
    }
    return Week;
  }

  Future<List<Daily>> getPosts() async {


    await storage.ready;
    List<Daily> Week = storage.getItem("list");
    if(Week != null){
      return Week;
    }
    Response res = await get(Uri.parse(postUrl));
    if(res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<Daily> Week = body.map<Daily>((dynamic item) => Daily.fromJson(item)).toList();
      storage.setItem("list", Week);
      return Week;
    }
    else{
      throw "Cant get schedule";
    }
  }
}
*/
