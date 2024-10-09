/// [Race_List_Page]

import 'package:flutter/material.dart';


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
    // ignore: unused_local_variable
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('Race List'),
      ),
      body: SafeArea(
        child: const Text(
          'Race List Page',
        )
      )
    );
  }
}