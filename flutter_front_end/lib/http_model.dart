import 'dart:convert';
import 'package:flutter/material.dart';

import 'post_model.dart';
import 'package:http/http.dart';


class HttpService {
  final String postUrl = "http://192.168.193.105:5143/Schedule/WeekT";

  Future<List<Daily>> getPosts() async {
    Response res = await get(Uri.parse(postUrl));
    if(res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<Daily> Week = body.map<Daily>((dynamic item) => Daily.fromJson(item)).toList();
      return Week;
    }
    else{
      throw "Cant get schedule";
    }
  }
}