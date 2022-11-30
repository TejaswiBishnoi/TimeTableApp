
class Events{
  final String start_time;
  final String end_time;
  final String room_code;
  final String course_name;
  final String section;

  Events({
    required this.start_time,
    required this.end_time,
    required this.room_code,
    required this.course_name,
    required this.section,
  });
  factory Events.fromJson(dynamic json){
    return Events(start_time: json['start_time'] as String, end_time: json['end_time'] as String, room_code: json['room_code'] as String,
        course_name: json['course_name'] as String, section: json['section'] as String);
  }
}
class Daily{
  List<Events>? event_list;
  final String day;
  final String date;
  Daily({
    required this.event_list,
    required this.day,
    required this.date
  });
  factory Daily.fromJson(Map<String, dynamic> json){
    return Daily(event_list: json['event_list'].map<Events>((dynamic json) => Events.fromJson(json)).toList(),
    day: json['day'] as String,
    date: json['date'] as String
    );
  }
}
