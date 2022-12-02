import 'package:flutter/material.dart';
import 'package:flutter_front_end/event_desc.dart';
import 'google_signin_api.dart';
import 'signup_page.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'http_model.dart';
import 'post_model.dart';

class SignedInPage extends StatefulWidget {
  final String? token;

  const SignedInPage({
    Key? key,
    required this.token,

}) : super (key: key);

  @override
  State<SignedInPage> createState() => _SignedInPageState(token: token);
}

class _SignedInPageState extends State<SignedInPage> {
  final HttpService httpService = HttpService();
  final String? token;
  _SignedInPageState({required this.token});
  @override
  Widget build(BuildContext context) {
    return Scaffold(
    appBar: AppBar(
      title: Text("Weekly Schedule  "),
      centerTitle: true,
      backgroundColor: Colors.black54,
      actions: [
        TextButton(
            onPressed: () async {
              await GoogleSignInAPI.logout();
              final storage = new FlutterSecureStorage();
              await storage.delete(key: 'token');
              Navigator.of(context).pushReplacement(MaterialPageRoute(
                  builder: (context) => SignupPage(),
              ));
            },
            child: const Text('Logout',
              style: TextStyle(
                  color: Colors.white60
              ),
            ),
        )
      ],
    ),
    body: FutureBuilder(future: httpService.getPosts(token),
    builder: (BuildContext context, AsyncSnapshot snapshot){
      if (snapshot.connectionState == ConnectionState.done) {
        if (snapshot.data == null) {
          return const Center(child: Text('No Data'));
        } else {
          List<Daily>? week = snapshot.data;

          return PageView(
            children: week!.map((Daily day) => CustomScrollView(
              slivers: <Widget>[
                SliverAppBar(
                  pinned: true,
                  expandedHeight: 160.0,
                  flexibleSpace: FlexibleSpaceBar(
                    title: Text("${day.day}\n${day.date}", textScaleFactor: 0.8,)


                  ),
                ),
                const SliverToBoxAdapter(
                  child: SizedBox(
                    height: 0,
                    child: Center(
                      child: Text('Scroll to see the SliverAppBar in effect.'),
                    ),
                  ),
                ),
                SliverList(
                  delegate: SliverChildBuilderDelegate(
                        (context, index) {
                      return Card(
                          child: InkWell(
                            onTap: (){Navigator.of(context).pushReplacement(MaterialPageRoute(
                              builder: (context) =>  EventDetails(occur_id: day.event_list![index].occur_id, event_id: day.event_list![index].event_id,token: token,),
                            ));
                              },
                            child: SizedBox(height: 80,child: Row(children: [
                              SizedBox(width: 20,),
                              Text(day.event_list![index].start_time+' - '),
                              Text(day.event_list![index].end_time),
                              SizedBox(width: 40,),
                              Text(day.event_list![index].course_name),
                              SizedBox(width: 20,),
                              Text(day.event_list![index].section),
                            ],),),
                          ),
                      );
                    },
                    childCount: day.event_list?.length,
                  ),
                ),
              ],
            )).toList(),
          );
        }
      } else if (snapshot.connectionState == ConnectionState.none) {
        return const Text('Error'); // error
      } else {
        return const Center(child: CircularProgressIndicator()); // loading
      }

        },
    ),
    );
  }
}

/*
if(snapshot.hasData){
         List<Daily>? week = snapshot.data;
         return PageView(
           children: week!.map((Daily day) => ListView(
             children: day.event_list!.map((Events event) => ListTile(
               title: Text(event.course_name),
             )).toList(),
           )).toList(),
         );
       }
       else{
         return const Center(child: CircularProgressIndicator());
       }
 */



/*
children: week!.map((Daily day) => ListView(
              children: day.event_list != null ? day.event_list!.map((Events event) => Card(
                child: SizedBox(
                    width: 200,
                    height: 100,
                    child: Row(
                  children: [
                    SizedBox(width: 20.0,height: 0,),
                    Text(event.start_time),
                    SizedBox(width: 20.0,height: 0,),
                    Text(event.course_name),
                    SizedBox(width: 20.0,height: 0,),
                    Text(event.section)
                  ],
                )),
              )).toList() : [],
            )).toList(),
 */



/*
children: week!.map((Daily day) => CustomScrollView(
              slivers: <Widget>[
                SliverAppBar(
                  pinned: true,
                  expandedHeight: 160.0,
                  flexibleSpace: FlexibleSpaceBar(
                    title: Text(day.day),

                  ),
                ),
                SliverToBoxAdapter(
                  child: SizedBox(
                    height: 20,
                    child: Center(
                      child: Text('Scroll to see the SliverAppBar in effect.'),
                    ),
                  ),
                ),
                SliverList(
                  delegate: SliverChildBuilderDelegate(
                        (BuildContext context, int index) {
                      return ListView(
                        children: day.event_list != null ? day.event_list!.map((Events event) => Card(
                        )).toList() : [],
                      );
                    },
                    childCount: 20,
                  ),
                ),
              ],
            )).toList(),
 */