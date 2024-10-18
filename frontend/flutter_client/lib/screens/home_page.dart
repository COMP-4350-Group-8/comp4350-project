/// [Home_Page]

import 'package:flutter/material.dart';

import '../logic/theme.dart';


/// [HomePage]
class HomePage extends StatefulWidget {
  const HomePage({super.key});
  @override
  State<HomePage> createState() => _HomePageState();
}

/// [_HomePageState]
class _HomePageState extends State<HomePage>{
  CustomTheme customTheme = CustomTheme();
  @override
  Widget build(BuildContext context) {
    // ignore: unused_local_variable
    final ThemeData customTheme = Theme.of(context);
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