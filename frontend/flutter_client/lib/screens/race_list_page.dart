import 'package:flutter/material.dart';

import '../logic/destination.dart';

class RaceListPage extends StatelessWidget {
  const RaceListPage({super.key, required this.destination});

  final Destination destination;

  @override
  Widget build(BuildContext context) {
    const int itemCount = 50;
    final ColorScheme colorScheme = Theme.of(context).colorScheme;
    final ButtonStyle buttonStyle = OutlinedButton.styleFrom(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
        side: BorderSide(
          color: colorScheme.onSurface.withOpacity(0.12),
        ),
      ),
      fixedSize: const Size.fromHeight(64),
      textStyle: Theme.of(context).textTheme.headlineSmall,
    );
    return Scaffold(
      appBar: AppBar(
        title: Text('${destination.title} RaceListPage '),
        foregroundColor: Colors.white,
      ),
      body: SizedBox.expand(
        child: ListView.builder(
          itemCount: itemCount,
          itemBuilder: (BuildContext context, int index) {
            return Padding(
              padding: const EdgeInsets.symmetric(vertical: 4, horizontal: 8),
              child: OutlinedButton(
                style: buttonStyle.copyWith(
                  backgroundColor: WidgetStatePropertyAll<Color>(
                    Colors.white
                  ),
                ),
                onPressed: () {
                  Navigator.pushNamed(context, '/raceInfo');
                },
                child: Text('Push /raceInfo [$index]'),
              ),
            );
          },
        ),
      ),
    );
  }
}