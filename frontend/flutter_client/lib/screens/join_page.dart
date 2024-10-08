import 'package:flutter/material.dart';

import '../logic/destination.dart';


class JoinPage extends StatefulWidget {
  const JoinPage({super.key, required this.destination});

  final Destination destination;

  @override
  State<JoinPage> createState() => _JoinPageState();
}

class _JoinPageState extends State<JoinPage> {

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