/// [Join_Page]

import 'dart:async';
import 'dart:io';
import 'dart:math';
import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:flutter_client/logic/api.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:path_provider/path_provider.dart';
import 'package:gpx/gpx.dart';

/// [TrackPage]
class TrackPage extends StatefulWidget {
  const TrackPage({super.key});
  @override
  State<TrackPage> createState() => _TrackPageState();
}

/// [_TrackPageState]
class _TrackPageState extends State<TrackPage> {
  @override
  LatLng start = LatLng(49.763089, -94.493308);
  List<Marker> markers = [];
  List<LatLng> points = [];
  PolylinePoints polylinePoints = PolylinePoints();
  Map<PolylineId, Polyline> polylines = {};
  List<List<String>> tracks = [];
  late GoogleMapController googleMapController;
  final Completer<GoogleMapController> completer = Completer();

  void onMapCreated(GoogleMapController controller) {
    googleMapController = controller;
    if (!completer.isCompleted) {
      completer.complete(controller);
    }

    print(Api().get_api());

    getAllTracks();
  }

  getAllTracks() async {
    final directory = await getApplicationDocumentsDirectory();

    final List<FileSystemEntity> entities = await directory.list().toList();

    for (var file in entities) {
      setState(() {
        if (file.path.contains('.gpx')) {
          tracks.add([
            file.path.substring(file.path.indexOf('flutter/track_') + 14,
                file.path.indexOf('.gpx')),
            file.path
          ]);
        }
      });
    }
  }

  selectTrack(int index) async {
    // read gpx file and extract points
    try {
      final file = File(tracks[index][1]);

      markers = [];
      polylines = {};
      points = [];

      final contents = await file.readAsString();
      print(contents);
      final xmlGpx = GpxReader().fromString(contents);
      if (xmlGpx.trks.isNotEmpty) {
        for (Trk trk in xmlGpx.trks) {
          for (Trkseg seg in trk.trksegs) {
            for (Wpt wpt in seg.trkpts) {
              if (wpt.lat != null && wpt.lon != null) {
                points.add(LatLng(wpt.lat ?? 0.0, wpt.lon ?? 0.0));
              }
            }
          }
        }
      }
    } catch (e) {
      print("error with track file");
    }
    setPolylines(points);
  }

  setPolylines(List<LatLng> markers) async {
    List<LatLng> polylineCoordinates = [];

    if (markers.length > 0) {
      setState(() {
        start = markers[0];
      });
    }

    for (var i = 0; i < markers.length; i++) {
      polylineCoordinates
          .add(LatLng(markers[i].latitude, markers[i].longitude));
    }

    //newSetState(() {});

    addPolyLine(polylineCoordinates); //, newSetState);
  }

  addPolyLine(List<LatLng> polylineCoordinates) {
    //, newSetState) {
    PolylineId id = PolylineId("poly");
    Polyline polyline = Polyline(
      polylineId: id,
      color: Colors.red,
      points: polylineCoordinates,
      width: 4,
    );
    print(polyline.points);
    setState(() {
      polylines[id] = polyline;
    });

    //newSetState(() {});
  }

  @override
  Widget build(BuildContext context) {
    // ignore: unused_local_variable
    final ThemeData theme = Theme.of(context);
    return Scaffold(
        appBar: AppBar(
          title: Text('Tracks'),
        ),
        body: SafeArea(
            child: Column(children: [
          SizedBox(
            height: 100,
            child: ListView.builder(
                scrollDirection: Axis.vertical,
                padding: const EdgeInsets.all(8),
                itemCount: tracks.length,
                itemBuilder: (BuildContext context, index) {
                  return ListTile(
                    onTap: () async => await selectTrack(index),
                    title: Text(tracks[index][0]),
                  );
                }),
          ),
          SizedBox(
              height: 600,
              child: GoogleMap(
                mapToolbarEnabled: false,
                onMapCreated: onMapCreated,
                polylines: Set<Polyline>.of(polylines.values),
                initialCameraPosition: CameraPosition(target: start, zoom: 10),
                markers: markers.toSet(),
                myLocationEnabled: false,
                myLocationButtonEnabled: false,
              ))
        ])));
  }
}
