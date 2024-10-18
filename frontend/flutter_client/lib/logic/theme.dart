
import 'package:flutter/material.dart';

@immutable
class CustomTheme extends ThemeExtension<CustomTheme> {
  const CustomTheme({
    this.primaryColor = const Color.fromARGB(255, 46, 95, 80),
    this.tertiaryColor = const Color.fromRGBO(11, 116, 112, 1),
    this.neutralColor = const Color.fromARGB(255, 142, 198, 181),
  });

  final Color primaryColor, tertiaryColor, neutralColor;

  ThemeData toThemeData(){
    return ThemeData(useMaterial3: true);
  }

  @override
  ThemeExtension<CustomTheme> copyWith({
    Color? primaryColor,
    Color? tertiaryColor,
    Color? neutralColor,
  }) => CustomTheme(
          primaryColor: primaryColor ?? this.primaryColor,
          tertiaryColor: tertiaryColor ?? this.tertiaryColor,
          neutralColor: neutralColor ?? this.neutralColor
        );

  @override
  ThemeExtension<CustomTheme> lerp(covariant ThemeExtension<CustomTheme>? other, double t) {
    if (other is! CustomTheme) return this;
    return CustomTheme(
      primaryColor: Color.lerp(primaryColor, other.primaryColor, t)!,
      tertiaryColor: Color.lerp(tertiaryColor, other.tertiaryColor, t)!,
      neutralColor: Color.lerp(neutralColor, other.neutralColor, t)!
    );
  }
}
