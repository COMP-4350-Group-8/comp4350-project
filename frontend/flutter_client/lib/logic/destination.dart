import 'package:flutter/material.dart';
import '../screens/race_info_page.dart';
import '../screens/race_list_page.dart';
import '../screens/root_page.dart';

/// The [Destination] object contains
/// a [index] for the current page location
/// a [tile] for the page title
/// a [icon] for the the page icon
class Destination {
  const Destination(this.index, this.title, this.icon);
  final int index;
  final String title;
  final IconData icon;
}


class DestinationView extends StatefulWidget {
  const DestinationView({
    super.key,
    required this.destination,
    required this.navigatorKey,
  });

  final Destination destination;
  final Key navigatorKey;

  @override
  State<DestinationView> createState() => _DestinationViewState();
}

/// [!NOTE] This class was made (almost word for word) from [https://api.flutter.dev/flutter/material/NavigationBar-class.html#material.NavigationBar.3]
class _DestinationViewState extends State<DestinationView> {
  @override
  Widget build(BuildContext context) {
    return Navigator(
      key: widget.navigatorKey,
      onGenerateRoute: (RouteSettings settings) {
        return MaterialPageRoute<void>(
          settings: settings,
          builder: (BuildContext context) {
            switch (settings.name) {
              case '/':
                return RootPage(destination: widget.destination);
              case '/raceList':
                return RaceListPage(destination: widget.destination);
              case '/raceInfo':
                return RaceInfoPage(destination: widget.destination);
            }
            assert(false);
            return const SizedBox();
          },
        );
      },
    );
  }
}