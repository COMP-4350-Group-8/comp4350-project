/// [Home_Page]

import 'package:flutter/material.dart';
import '../logic/theme.dart';
import 'package:geolocator/geolocator.dart';
import 'package:path_provider/path_provider.dart';
import 'dart:io';
import 'package:xml/xml.dart';

/// [HomePage]
class HomePage extends StatefulWidget {
  const HomePage({super.key});
  @override
  State<HomePage> createState() => _HomePageState();
}

/// [_HomePageState]
class _HomePageState extends State<HomePage> {
  String _status = 'Get Location';
  bool _isTracking = false;
  final List<Position> _points = [];

  CustomTheme customTheme = CustomTheme();
  @override
  Widget build(BuildContext context) {
    // ignore: unused_local_variable
    final ThemeData customTheme = Theme.of(context);
    return Scaffold(
        appBar: AppBar(
          title: Text('Home'),
        ),
        body: Center(
          child: Column(
            children: [
              TextButton(
                  onPressed: _isTracking ? null : _startTracking,
                  child: Text("Start Tracking")),
              TextButton(
                  onPressed: _isTracking ? _stopTracking : null,
                  child: Text("Stop Tracking")),
              Text(
                _status,
                textAlign: TextAlign.center,
              )
            ],
          ),
        ));
  }

  Future<bool> _checkLocationPermission() async {
    LocationPermission permission = await Geolocator.checkPermission();

    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
    }

    if (permission == LocationPermission.deniedForever) {
      setState(() {
        _status = "location permissions are denied";
      });
      return false;
    }

    return true;
  }

  void _startTracking() async {
    if (!await _checkLocationPermission()) return;

    setState(() {
      _isTracking = true;
      _status = 'tracking started';
      _points.clear();
    });

    Geolocator.getPositionStream(
        locationSettings: LocationSettings(
      accuracy: LocationAccuracy.high,
      distanceFilter: 5, //update every 5m
    )).listen((Position position) {
      setState(() {
        _points.add(position);
      });
    });
  }

  Future<void> _stopTracking() async {
    setState(() {
      _isTracking = false;
      _status = 'tracking stopped saving file';
    });

    await _saveToGPX();
  }

  Future<void> _saveToGPX() async {
    if (_points.isEmpty) {
      setState(() {
        _status = 'no points to save';
      });
      return;
    }

    try {
      final directory = await getApplicationDocumentsDirectory();

      final timestamp = DateTime.now().toIso8601String().replaceAll(':', '-');
      final file = File('${directory.path}/track_$timestamp.gpx');

      final builder = XmlBuilder();

      await file.writeAsString(builder.buildDocument().toString());

      setState(() {
        _status = "GPX file saved: ${file.path}";
      });
    } catch (e) {
      setState(() {
        _status = "Error: $e";
      });
    }
  }
}
