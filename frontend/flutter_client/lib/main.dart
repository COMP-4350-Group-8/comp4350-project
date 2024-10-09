// ignore_for_file: unused_local_variable

import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import 'widgets/navigation_bar_widget.dart';
import 'logic/theme.dart';

void main() {
  runApp(const MaterialApp(home: MyApp()));
}

// Used for handling and storing data within each of the main pages 
late final List<GlobalKey<NavigatorState>> navigatorKeys;
late final List<GlobalKey> destinationKeys;
late final List<AnimationController> destinationFaders;
late final List<Widget> destinationViews;
  
final GlobalKey<NavigatorState> _rootNavigatorKey = GlobalKey<NavigatorState>();
final GlobalKey<NavigatorState> _shellNavigatorKey = GlobalKey<NavigatorState>();


final GoRouter _router = GoRouter(
  navigatorKey: _rootNavigatorKey,
  initialLocation: '/home',
  routes: <RouteBase>[
    ShellRoute(
      navigatorKey: _shellNavigatorKey,
      builder: (context, state, child) {
        return ScaffoldWithNavBar(
          child:Text('data')
        );
      },
      routes: [
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/home',
          name: 'home',
          builder: (context, state){
            return const Text('place a list here');
          }
        ),
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/race_list',
          name: 'race_ist',
          builder: (context, state){
            return const Text('place a list here');
          }
        ),
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/join',
          name: 'join',
          builder: (context, state){
            return const Text('place temp here');
          }
        ),
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/profile',
          name: 'profile',
          builder: (context, state){
            return const Text('place temp here');
          }
        )
      ]
    )
  ]
);



class MyApp extends StatelessWidget{
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    const customTheme = CustomTheme();
    return MaterialApp.router(
      title: 'Sailing Race Analyzer',
      theme: customTheme.toThemeData(),
      routerConfig: _router,
    );
  }
}