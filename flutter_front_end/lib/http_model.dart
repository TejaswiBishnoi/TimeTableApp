import 'dart:convert';
import 'dart:io';
import 'post_model.dart';
import 'package:http/http.dart';


class HttpService {
  final String postUrl = "http://192.168.137.1:5143/Schedule/Week";
  Future<List<Daily>> getPosts(String? token) async {
   /* Map<String,String>? header;
    print('this is token  ${token!}   this is token');
    if(token!=null){
      header = {'Authorization': "bearer $token"};
    }
    print(header!["Authorization"]); */

    Response res = await get(Uri.parse(postUrl),headers: {"accesstoken":"bearer $token"});
    if(res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<Daily> Week = body.map<Daily>((dynamic item) => Daily.fromJson(item)).toList();;
      return Week;
    }
    else{
      print(res.statusCode);
      throw "Cant get schedule";
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
