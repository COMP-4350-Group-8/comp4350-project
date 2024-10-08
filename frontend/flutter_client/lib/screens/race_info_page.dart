import 'package:flutter/material.dart';

import '../logic/destination.dart';

class RaceInfoPage extends StatefulWidget {
  const RaceInfoPage({super.key, required this.destination});

  final Destination destination;

  @override
  State<RaceInfoPage> createState() => _RaceInfoPageState();
}

class _RaceInfoPageState extends State<RaceInfoPage> {
  late final TextEditingController textController;

  @override
  void initState() {
    super.initState();
    textController = TextEditingController(text: 'Sample Race Info Text');
  }

  @override
  void dispose() {
    textController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('${widget.destination.title} Race Info'),
        foregroundColor: Colors.white,
      ),
      body: Container(
        padding: const EdgeInsets.all(32.0),
        alignment: Alignment.center,
        child: TextField(
          controller: textController,
          style: theme.primaryTextTheme.headlineMedium?.copyWith(
          ),
          decoration: InputDecoration(
            focusedBorder: UnderlineInputBorder(
              borderSide: BorderSide(
                width: 3.0,
              ),
            ),
          ),
        ),
      ),
    );
  }
}