import 'package:flutter/material.dart';

import '../logic/destination.dart';


class RootPage extends StatefulWidget {
  const RootPage({super.key, required this.destination});

  final Destination destination;

  @override
  State<RootPage> createState() => _RootPageState();
}

class _RootPageState extends State<RootPage> {

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
        title: Text('${widget.destination.title} ROOT - /root'),
        foregroundColor: Colors.white,
      ),
      body: Center(
        child: Text('Root Text'),
      ),
    );
  }
}