/// [Profile_Page]

import 'package:flutter/material.dart';


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
    // ignore: unused_local_variable
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('Profile'),
      ),
      body: SafeArea(
        child: const Text(
          'Profile Page',
        )
      )
    );
  }
}