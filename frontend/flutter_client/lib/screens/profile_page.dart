import 'package:flutter/material.dart';

import '../logic/destination.dart';


class ProfilePage extends StatefulWidget {
  const ProfilePage({super.key, required this.destination});

  final Destination destination;

  @override
  State<ProfilePage> createState() => _ProfilePageState();
}

class _ProfilePageState extends State<ProfilePage> {

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