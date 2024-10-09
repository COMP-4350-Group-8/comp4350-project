/// [Join_Page]

import 'package:flutter/material.dart';


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
    // ignore: unused_local_variable
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('Join'),
      ),
      body: SafeArea(
        child: const Text(
          'Join Page',
        )
      )
    );
  }
}