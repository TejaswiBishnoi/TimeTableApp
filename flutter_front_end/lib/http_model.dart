import 'dart:convert';
import 'dart:io';
import 'post_model.dart';
import 'event_model.dart';
import 'package:http/http.dart';
import 'package:path_provider/path_provider.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class HttpService {
  final String postUrl = "http://192.168.137.1:5143/Schedule/Week";
  final String eventUrl = "http://192.168.137.1:5143/Schedule/EventDetails";

  dynamic dir;

  Future<void> getDir() async {
    dir = await getTemporaryDirectory();
  }
  Future<List<Daily>> getPosts(String? token) async {
    String filename = "user.json";
    if(dir==null){
      await getDir();
    }
    File file = File('${dir.path}/$filename');
    final storage = const FlutterSecureStorage();
    //String? t = await storage.read(key: 'token');
    //token = t;
    //print(t);
    if(file.existsSync()){
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
      //token = await storage.read(key: 'token');
      //print("no token =====$token");
      print(token);
      Response res = await get(Uri.parse(postUrl),headers: {"accesstoken":"bearer $token"});
      //print(res.statusCode);
      print(res.body);
      if(res.statusCode == 200) {

        file.writeAsString(jsonEncode(res.body),flush: true, mode: FileMode.write);
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
    if(file.existsSync()){
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
    else{
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
    }
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
