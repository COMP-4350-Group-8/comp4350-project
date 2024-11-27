/// [Home_Page]

import 'dart:convert';

import 'package:flutter/material.dart';
import '../logic/theme.dart';
import 'package:geolocator/geolocator.dart';
import 'package:path_provider/path_provider.dart';
import 'dart:io';
import 'package:xml/xml.dart';
import 'package:http/http.dart' as http;

/// [HomePage]
class HomePage extends StatefulWidget {
  const HomePage({super.key});
  @override
  State<HomePage> createState() => _HomePageState();
}

/// [_HomePageState]
class _HomePageState extends State<HomePage> {
  String _status = '';
  bool _isTracking = false;
  final List<Position> _points = [];
  final String _api = 'localhost:5000';
  String _gpx = '';

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
                  style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.amberAccent),
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
      print(position);
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
      builder.processing('xml', 'version="1.0" encoding="UTF-8"');
      builder.element('gpx', attributes: {
        'version': '1.1',
        'creator': 'Flutter GPS Tracker',
        'xmlns': 'http://www.topografix.com/GPX/1/1',
        'xmlns:xsi': 'http://www.w3.org/2001/XMLSchema-instance',
        'xsi:schemaLocation':
            'http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd'
      }, nest: () {
        builder.element('trk', nest: () {
          builder.element('name', nest: () {
            builder.text('Track $timestamp');
          });
          builder.element('trkseg', nest: () {
            for (var point in _points) {
              builder.element('trkpt', attributes: {
                'lat': point.latitude.toString(),
                'lon': point.longitude.toString()
              }, nest: () {
                // Optional: Add elevation and timestamp
                builder.element('ele', nest: () {
                  builder.text(point.altitude.toString());
                });
                builder.element('time', nest: () {
                  builder.text(DateTime.now().toIso8601String());
                });
              });
            }
          });
        });
      });

      final gpx = builder.buildDocument().toString();
      await file.writeAsString(gpx);

      setState(() {
        _status = "GPX file saved: ${file.path}";
        _gpx = gpx;
      });
      _sendGPX();
    } catch (e) {
      setState(() {
        _status = "Error: $e";
      });
    }
  }

  Future<void> _sendGPX() async {
    if (_api.isEmpty) {
      setState(() {
        _status = "please enter api url";
      });
      return;
    }

    try {
      final url = Uri.parse('$_api/track');

      final responce = await http.put(url,
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: jsonEncode(_gpx));
    } catch (e) {
      setState(() {
        _status = "Error sending request: $e";
      });
    }
  }
}
