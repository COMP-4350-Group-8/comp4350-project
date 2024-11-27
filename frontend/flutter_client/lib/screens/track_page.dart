/// [Join_Page]

import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

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
  PolylinePoints polylinePoints = PolylinePoints();
  Map<PolylineId, Polyline> polylines = {};
  late GoogleMapController googleMapController;
  final Completer<GoogleMapController> completer = Completer();

  void onMapCreated(GoogleMapController controller) {
    googleMapController = controller;
    if (!completer.isCompleted) {
      completer.complete(controller);
    }
  }

  addMarker(latLng, newSetState) {
    markers.add(Marker(
        consumeTapEvents: true,
        markerId: MarkerId(latLng.toString()),
        position:
            latLng, // We adding onTap paramater for when click marker, remove from map
        onTap: () {
          markers.removeWhere((element) =>
              element.markerId ==
              MarkerId(latLng
                  .toString())); // markers length must be greater than 1 because polyline needs two // points
          if (markers.length > 1) {
            getDirections(markers, newSetState);
          } // When we added markers then removed all, this time polylines seems //in map because of we should clear polylines
          else {
            polylines.clear();
          } // newState parameter of function, we are openin map in alertDialog, // contexts are different in page and alert dialog because of we use // different setState
          newSetState(() {});
        }));
    if (markers.length > 1) {
      getDirections(markers, newSetState);
    }

    newSetState(() {});
  }

  getDirections(List<Marker> markers, newSetState) async {
    List<LatLng> polylineCoordinates = [];

    for (var i = 0; i < markers.length; i++) {
      polylineCoordinates.add(
          LatLng(markers[i].position.latitude, markers[i].position.longitude));
    }

    newSetState(() {});

    addPolyLine(polylineCoordinates, newSetState);
  }

  addPolyLine(List<LatLng> polylineCoordinates, newSetState) {
    PolylineId id = PolylineId("poly");
    Polyline polyline = Polyline(
      polylineId: id,
      color: Colors.red,
      points: polylineCoordinates,
      width: 4,
    );
    polylines[id] = polyline;

    newSetState(() {});
  }

  Widget build(BuildContext context) {
    // ignore: unused_local_variable
    final ThemeData theme = Theme.of(context);
    return Scaffold(
        appBar: AppBar(
          title: Text('Tracks'),
        ),
        body: SafeArea(
            child: GoogleMap(
          mapToolbarEnabled: false,
          onMapCreated: onMapCreated,
          polylines: Set<Polyline>.of(polylines.values),
          initialCameraPosition: CameraPosition(target: start),
          markers: markers.toSet(),
          myLocationEnabled: false,
          myLocationButtonEnabled: false,
        )));
  }
}
