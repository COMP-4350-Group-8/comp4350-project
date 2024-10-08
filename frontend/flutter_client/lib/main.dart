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