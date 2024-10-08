import 'package:flutter/material.dart';

import 'logic/destination.dart';
//import 'screens/raceList_page.dart';
//import 'screens/raceInfo_page.dart';

void main() {
  runApp(const MaterialApp(home: Home()));
}

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

/// [!NOTE] This class was made (almost word for word) from [https://api.flutter.dev/flutter/material/NavigationBar-class.html#material.NavigationBar.3]  
/// [_HomeState] contains the data ([State]) for the root rout 
class _HomeState extends State<Home> with TickerProviderStateMixin<Home> {
  // All the destinations accessable through the navigation bar
  static const List<Destination> allDestinations = <Destination>[
    Destination(0, 'Info', Icons.home),
    Destination(1, 'Races', Icons.directions_boat),
    Destination(2, 'Join', Icons.join_full),
    Destination(3, 'Profile', Icons.person),
    Destination(4, 'Settings', Icons.settings_rounded)
  ];

  // Used for handling and storing data within each of the main pages 
  late final List<GlobalKey<NavigatorState>> navigatorKeys;
  late final List<GlobalKey> destinationKeys;
  late final List<AnimationController> destinationFaders;
  late final List<Widget> destinationViews;
  
  int selectedIndex = 0; // Current main page index

  // Returns a method that manages the speed of a animations, and may request the destination to be rebuilt if it was destroyed
  AnimationController buildFaderController() {
    return AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 300),
    )..addStatusListener((AnimationStatus status) {
        if (status.isDismissed) {
          setState(() {}); // Rebuild unselected destinations offstage.
        }
      });
  }

  @override
  void initState() {
    super.initState();

    navigatorKeys = List<GlobalKey<NavigatorState>>.generate(
      allDestinations.length,
      (int index) => GlobalKey(),
    ).toList();

    destinationFaders = List<AnimationController>.generate(
      allDestinations.length,
      (int index) => buildFaderController(),
    ).toList();
    destinationFaders[selectedIndex].value = 1.0;

    final CurveTween tween = CurveTween(curve: Curves.fastOutSlowIn);
    destinationViews = allDestinations.map<Widget>(
      (Destination destination) {
        return FadeTransition(
          opacity: destinationFaders[destination.index].drive(tween),
          child: DestinationView(
            destination: destination,
            navigatorKey: navigatorKeys[destination.index],
          ),
        );
      },
    ).toList();
  }

  @override
  void dispose() {
    for (final AnimationController controller in destinationFaders) {
      controller.dispose();
    }
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return NavigatorPopHandler(
      onPop: () {
        final NavigatorState navigator =
            navigatorKeys[selectedIndex].currentState!;
        navigator.pop();
      },
      child: Scaffold(
        body: SafeArea(
          top: false,
          child: Stack(
            fit: StackFit.expand,
            children: allDestinations.map(
              (Destination destination) {
                final int index = destination.index;
                final Widget view = destinationViews[index];
                if (index == selectedIndex) {
                  destinationFaders[index].forward();
                  return Offstage(offstage: false, child: view);
                } else {
                  destinationFaders[index].reverse();
                  if (destinationFaders[index].isAnimating) {
                    return IgnorePointer(child: view);
                  }
                  return Offstage(child: view);
                }
              },
            ).toList(),
          ),
        ),
        bottomNavigationBar: NavigationBar(
          selectedIndex: selectedIndex,
          onDestinationSelected: (int index) {
            setState(() {
              selectedIndex = index;
            });
          },
          destinations: allDestinations.map<NavigationDestination>(
            (Destination destination) {
              return NavigationDestination(
                icon: Icon(destination.icon),
                label: destination.title,
              );
            },
          ).toList(),
        ),
      ),
    );
  }
}

/*
/*
/// The [Destination] object contains
/// a [index] for the current page location
/// a [tile] for the page title
/// a [icon] for the the page icon
class Destination {
  const Destination(this.index, this.title, this.icon);
  final int index;
  final String title;
  final IconData icon;
}


class DestinationView extends StatefulWidget {
  const DestinationView({
    super.key,
    required this.destination,
    required this.navigatorKey,
  });

  final Destination destination;
  final Key navigatorKey;

  @override
  State<DestinationView> createState() => _DestinationViewState();
}

/// [!NOTE] This class was made (almost word for word) from [https://api.flutter.dev/flutter/material/NavigationBar-class.html#material.NavigationBar.3]
class _DestinationViewState extends State<DestinationView> {
  @override
  Widget build(BuildContext context) {
    return Navigator(
      key: widget.navigatorKey,
      onGenerateRoute: (RouteSettings settings) {
        return MaterialPageRoute<void>(
          settings: settings,
          builder: (BuildContext context) {
            switch (settings.name) {
              case '/':
                return RootPage(destination: widget.destination);
              case '/list':
                return RacePage(destination: widget.destination);
              case '/text':
                return TextPage(destination: widget.destination);
            }
            assert(false);
            return const SizedBox();
          },
        );
      },
    );
  }
}*/


class PageRaces extends StatelessWidget {
  const PageRaces({super.key});
  @override
  Widget build(BuildContext context){
    return Scaffold(
      
    );
  }
}


class HomePage extends StatefulWidget {
  const HomePage({super.key, required this.title});
  /* [TIPS] 
  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".
  */

  final String title;

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  int _counter = 0;

  void _incrementCounter() {
    setState(() {
      /* [TIPS]    
      // This call to setState tells the Flutter framework that something has
      // changed in this State, which causes it to rerun the build method below
      // so that the display can reflect the updated values. If we changed
      // _counter without calling setState(), then the build method would not be
      // called again, and so nothing would appear to happen.
      */
      _counter++;
    });
  }

  @override
  Widget build(BuildContext context) {
    int currentPageIndex = 0;
    /* [TIPS]
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    */
    
    return Scaffold(
      bottomNavigationBar: NavigationBar(
        destinations: [
          NavigationDestination(
            icon: Icon(Icons.home),
            label: 'Info'
          ),
          NavigationDestination(
            icon: Icon(Icons.directions_boat),
            label: 'Races'
          ),
          NavigationDestination(
            icon: Icon(Icons.join_full),
            label: 'Join'
          ),
          NavigationDestination(
            icon: Icon(Icons.person),
            label: 'Profile'
          ),
          NavigationDestination(
            icon: Icon(Icons.settings_rounded),
            label: 'Settings'
          ),
        ],
        selectedIndex: currentPageIndex,
        onDestinationSelected: (int index){
          setState(() {
            currentPageIndex = index;
          });
        },
      ),
      appBar: AppBar(
        /* [TIPS] 
        // TRY THIS: Try changing the color here to a specific color (to
        // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
        // change color while the other colors stay the same.
        */
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        /* [TIPS] 
        // Here we take the value from the HomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        */
        title: Text(widget.title),
      ),
      //body: [Widget1, Widget2, Widget3, Widget4, Widget5][currentPageIndex],
      
      /*Center(
        /* [TIPS] 
        // Center is a layout widget. It takes a single child and positions it
        // in the middle of the parent.
        */
        child: Column(
          /* [TIPS] 
          // Column is also a layout widget. It takes a list of children and
          // arranges them vertically. By default, it sizes itself to fit its
          // children horizontally, and tries to be as tall as its parent.
          //
          // Column has various properties to control how it sizes itself and
          // how it positions its children. Here we use mainAxisAlignment to
          // center the children vertically; the main axis here is the vertical
          // axis because Columns are vertical (the cross axis would be
          // horizontal).
          //
          // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
          // action in the IDE, or press "p" in the console), to see the
          // wireframe for each widget.
          */
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text(
              'You have pushed the button this many times:',
            ),
            Text(
              '$_counter',
              style: Theme.of(context).textTheme.headlineMedium,
            ),
          ],
        ),
      ),*/
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
*/