

class Slot{
  final String start_time;
  final String end_time;


  Slot({
    required this.start_time,
    required this.end_time,
  });
  factory Slot.fromJson(dynamic json){
    return Slot(start_time: json['start'] as String, end_time: json['end'] as String, );
  }
}
// class SlotList{
//   List<Slot>? slot_list;
//
//   SlotList({
//     required slot_list,
//   });
//   factory SlotList.fromJson(Map<String, dynamic> json){
//     return SlotList(slot_list: json['slot_list'].map<Slot>((dynamic json) => Slot.fromJson(json)).toList(),
//     );
//   }
// }
