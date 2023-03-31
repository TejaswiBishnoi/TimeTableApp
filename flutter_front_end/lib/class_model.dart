

class ClassEvent{
  final String start_time;
  final String end_time;
  final String course_name;
  final String section;
  final String event_id;
  final String occur_id;
  final String faculty;

  ClassEvent({
    required this.start_time,
    required this.end_time,
    required this.faculty,
    required this.course_name,
    required this.section,
    required this.event_id,
    required this.occur_id,
  });
  factory ClassEvent.fromJson(dynamic json){
    return ClassEvent(start_time: json['start_time'] as String, end_time: json['end_time'] as String, faculty: json['instructor'] as String,
      course_name: json['course_name'] as String, section: json['section'] as String,
      event_id: json['event_id'] as String,
      occur_id: json['occurence_id'] as String,);
  }
}
class DailyEvents{
  List<ClassEvent>? event_list;
  final String day;
  final String date;
  DailyEvents({
    required this.event_list,
    required this.day,
    required this.date
  });
  factory DailyEvents.fromJson(Map<String, dynamic> json){
    return DailyEvents(event_list: json['event_list'].map<ClassEvent>((dynamic json) => ClassEvent.fromJson(json)).toList(),
        day: json['day'] as String,
        date: json['date'] as String
    );
  }
}
