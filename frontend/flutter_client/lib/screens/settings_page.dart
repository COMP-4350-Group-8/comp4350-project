/// [Settings_Page]

import 'package:flutter/material.dart';


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
    // ignore: unused_local_variable
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('Settings'),
      ),
      body: SafeArea(
        child: const Text(
          'Settings Page',
        )
      )
    );
  }
}