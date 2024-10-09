// ignore_for_file: unused_local_variable

import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

void main() {
  runApp(const MaterialApp(home: MyApp()));
}

class Destination {
  const Destination(this.index, this.path, this.title, this.icon);
  final int index;
  final String path;
  final String title;
  final IconData icon;
}

const List<Destination> navBarDestinations = <Destination>[
  Destination(0, '/', 'home', Icons.home),
  Destination(1, '/race_list', 'race_list', Icons.directions_boat),
  Destination(2, '/join', 'join', Icons.join_full),
  Destination(3, '/profile', 'profile', Icons.person),
  Destination(4, '/settings', 'settings', Icons.settings_rounded)
];

// Used for handling and storing data within each of the main pages 
late final List<GlobalKey<NavigatorState>> navigatorKeys;
late final List<GlobalKey> destinationKeys;
late final List<AnimationController> destinationFaders;
late final List<Widget> destinationViews;
  
//int selectedIndex = 0; // Current main page inde

final GlobalKey<NavigatorState> _rootNavigatorKey = GlobalKey<NavigatorState>();
final GlobalKey<NavigatorState> _shellNavigatorKey = GlobalKey<NavigatorState>();

int selectedIndex = 0;

final GoRouter _router = GoRouter(
  navigatorKey: _rootNavigatorKey,
  initialLocation: '/',
  routes: <RouteBase>[
    ShellRoute(
      navigatorKey: _shellNavigatorKey,
      builder: (context, state, nav) {
        return Scaffold(
          bottomNavigationBar: NavigationBar(
            destinations: navBarDestinations.map<NavigationDestination>(
              (Destination destination) {
                return NavigationDestination(
                  icon: Icon(destination.icon),
                  label: destination.title,
                );
              },
            ).toList(),
            onDestinationSelected: (int index) {
              selectedIndex = index;
              context.go(navBarDestinations.elementAt(index).path);
            },
            selectedIndex: selectedIndex,
          ),
        );
      },
      routes: [
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/',
          name: 'home',
          builder: (context, state){
            return const HomePage();
          }
        ),
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/race_list',
          name: 'race_ist',
          builder: (context, state){
            return const RaceListPage();
          }
        ),
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/join',
          name: 'join',
          builder: (context, state){
            return const JoinPage();
          }
        ),
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/profile',
          name: 'profile',
          builder: (context, state){
            return const ProfilePage();
          }
        ),
        GoRoute(
          parentNavigatorKey: _shellNavigatorKey,
          path: '/settings',
          name: 'settings',
          builder: (context, state){
            return const SettingsPage();
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
    return MaterialApp.router(
      title: 'Sailing Race Analyzer',
      theme: ThemeData(useMaterial3: true),
      routerConfig: _router,
    );
  }
}


/// [HomePage]
class HomePage extends StatefulWidget {
  const HomePage({super.key});
  @override
  State<HomePage> createState() => _HomePageState();
}

/// [_HomePageState]
class _HomePageState extends State<HomePage>{
  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('Home'),
      ),
      body: SafeArea(
        child: const Text(
          'Home Page',
        )
      )
    );
  }
}


/// [RaceListPage]
class RaceListPage extends StatefulWidget {
  const RaceListPage({super.key});
  @override
  State<RaceListPage> createState() => _RaceListPageState();
}

/// [_RaceListPageState]
class _RaceListPageState extends State<RaceListPage>{
  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      body: Text('Race List Page'),
    );
  }
}


/// [JoinPage]
class JoinPage extends StatefulWidget {
  const JoinPage({super.key});
  @override
  State<JoinPage> createState() => _JoinPageState();
}

/// [_JoinPageState]
class _JoinPageState extends State<JoinPage>{
  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      body: Text('Join Page'),
    );
  }
}


/// [ProfilePage]
class ProfilePage extends StatefulWidget {
  const ProfilePage({super.key});
  @override
  State<ProfilePage> createState() => _ProfilePageState();
}

/// [_ProfilePageState]
class _ProfilePageState extends State<ProfilePage>{
  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      body: Text('Profile Page'),
    );
  }
}


/// [SettingsPage]
class SettingsPage extends StatefulWidget {
  const SettingsPage({super.key});
  @override
  State<SettingsPage> createState() => _SettingsPageState();
}

/// [_SettingsPageState]
class _SettingsPageState extends State<SettingsPage>{
  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      body: Text('Settings Page'),
    );
  }
}