class eventDetails{
  final String category;
  final String eventType;
  final String total_credits;
  final String lecture_credits;
  final String tutorial_credits;
  final String practical_credits;
  final String department;
  final String next_date;
  final String next_day;
  final String next_start_time;
  final String next_end_time;

  eventDetails({
    required this.category,
    required this.eventType,
    required this.total_credits,
    required this.lecture_credits,
    required this.tutorial_credits,
    required this.practical_credits,
    required this.department,
    required this.next_date,
    required this.next_day,
    required this.next_start_time,
    required this.next_end_time,
  });
  factory eventDetails.fromJson(dynamic json){
    return eventDetails(category: json['category'] as String,
    eventType: json['type'] as String,
    total_credits: json['total_credits'] as String,
      lecture_credits: json['lecture_credits'] as String,
      tutorial_credits: json['tutorial_credits'] as String,
      practical_credits: json['practical_credits'] as String,
      department: json['department'] as String,
      next_date: json['next_date'] as String,
      next_day: json['next_day'] as String,
      next_end_time: json['next_end_time'] as String,
      next_start_time: json['next_start_time'] as String,
    );
  }
}
