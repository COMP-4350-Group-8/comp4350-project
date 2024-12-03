import 'package:http/http.dart' as http;
import 'dart:convert';

class Api {
  static final Api _api = Api._internal();
  String apiUrl = 'localhost:5000';

  factory Api() {
    return _api;
  }

  Api._internal();

  set_api(String new_location) {
    apiUrl = new_location;
    print(apiUrl);
  }

  String get_api() {
    return apiUrl;
  }

  Future<String> sendGPX(String gpx) async {
    if (apiUrl.isEmpty) {
      return " Please enter valid URL";
    }

    try {
      final url = Uri.parse('$apiUrl/track');

      final responce = await http.put(url,
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: jsonEncode(gpx));

      return responce.statusCode.toString();
    } catch (e) {
      return "Error sending request: $e";
    }
  }
}
