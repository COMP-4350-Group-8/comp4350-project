import 'package:flutter/material.dart';

import '../logic/destination.dart';

import '../screens/home_page.dart';
import '../screens/join_page.dart';
import '../screens/profile_page.dart';
import '../screens/race_info_page.dart';

class ScaffoldWithNavBar extends StatefulWidget{
  const ScaffoldWithNavBar({required this.child, super.key});

  final Widget child;

  @override
  State<ScaffoldWithNavBar> createState() => _ScaffoldWithNavBarState();
}

class _ScaffoldWithNavBarState extends State<ScaffoldWithNavBar>{
  int currentPageIndex = 0;
  NavigationDestinationLabelBehavior labelBehavior = NavigationDestinationLabelBehavior.alwaysShow;

  static const List<Destination> navBarDestinations = <Destination>[
  Destination(0, '/home', 'home', Icons.home),
  Destination(1, '/race_list', 'race_list', Icons.directions_boat),
  Destination(2, '/join', 'join', Icons.join_full),
  Destination(3, '/profile', 'profile', Icons.person),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      bottomNavigationBar: NavigationBar(
        destinations: navBarDestinations.map<NavigationDestination>(
          (Destination destination) {
            return NavigationDestination(
              icon: Icon(destination.icon),
              label: destination.label,
            );
          }
        ).toList(),
        selectedIndex: currentPageIndex,
        onDestinationSelected: (int index){
          setState(() {
            currentPageIndex = index;
          });
        }
      ),
      body: [
        HomePage(),
        RaceInfoPage(), // Should acutally be RaceListPage(), then from the Race List Page you would click on the desired RaceInfoPage()
        JoinPage(),
        ProfilePage()
      ][currentPageIndex]    
    );
  }
}