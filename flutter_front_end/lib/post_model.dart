import 'package:flutter/foundation.dart';

class Events{
  final String start_time;
  final String end_time;
  final String class_code;
  final String course_name;

  Events({
    required this.start_time,
    required this.end_time,
    required this.class_code,
    required this.course_name,
  });
}
class Daily{
  List<Events>? event_list;

  Daily({
    this.event_list = const <Events>[]
  });

}
class Weekly{
  List<Daily>? day_list;
  Weekly({
    this.day_list = const <Daily>[]
  });

  factory Weekly.fromJson(Map<String, dynamic> json){
    return Weekly(day_list: json['day_list'] as List<Daily>);
  }
}