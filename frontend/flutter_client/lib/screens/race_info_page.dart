/// [Race_Info]

import 'package:flutter/material.dart';


/// [RaceInfoPage]
class RaceInfoPage extends StatefulWidget {
  const RaceInfoPage({super.key});
  @override
  State<RaceInfoPage> createState() => _RaceInfoPageState();
}

/// [_RaceInfoPageState]
class _RaceInfoPageState extends State<RaceInfoPage>{
  @override
  Widget build(BuildContext context) {
    // ignore: unused_local_variable
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('Race Info'),
      ),
      body: SafeArea(
        child: const Text(
          'Race Info Page',
        )
      )
    );
  }
}