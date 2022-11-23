import 'dart:convert';
import 'dart:html';
import 'post_model.dart';
import 'package:http/http.dart';
import 'post_model.dart';

class HttpService {
  final Uri postUrl = "";

  Future<List<Weekly>> getPosts() async {
    Response res = await get(postUrl);

    if(res.statusCode == 200){
      List<dynamic> body = jsonDecode(res.body);

      List<Weekly> Week = body.map((dynamic item) => Weekly.fromJson(item) ).toList();
      return Week;
    }
    else{
      throw "Cant get schedule";
    }
  }
}