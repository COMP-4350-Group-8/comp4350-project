import 'package:flutter/material.dart';

import '../logic/destination.dart';


class SettingsPage extends StatefulWidget {
  const SettingsPage({super.key, required this.destination});

  final Destination destination;

  @override
  State<SettingsPage> createState() => _SettingsPageState();
}

class _SettingsPageState extends State<SettingsPage> {

  @override
  void initState() {
    super.initState();
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('${widget.destination.title} ROOT'),
        foregroundColor: Colors.white,
      ),
      body: Center(
        child: Text('Text'),
      ),
    );
  }
}