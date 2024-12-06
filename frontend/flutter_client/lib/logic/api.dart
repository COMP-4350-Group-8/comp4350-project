import 'package:http/http.dart' as http;
import 'dart:convert';

//  singleton class for api access and url memory
class Api {
  static final Api _api = Api._internal();
  String apiUrl = 'http://146.190.251.228:5000';

  factory Api() {
    return _api;
  }

  Api._internal();

  setApi(String newLocation) {
    apiUrl = newLocation;
  }

  String getApi() {
    return apiUrl;
  }

  Future<String> sendGPX(String gpx, int boatId, int raceId, String started,
      String finished) async {
    if (apiUrl.isEmpty) {
      return " Please enter valid URL";
    }

    try {
      final url = Uri.parse('$apiUrl/track');

      String body = '''{
  "id": 0,
  "boatId": $boatId,
  "raceId": $raceId,
  "started": $started,
  "finished": $finished, 
  "gpxData": ${jsonEncode(gpx)}
}''';

      final responce = await http.post(url,
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: body);

      return responce.statusCode.toString();
    } catch (e) {
      return "Error sending request: $e";
    }
  }
}
