using System;

namespace hahahalib
{
    public class hahaha_gps_gnss
    {
        public hahaha_gps_gnss()
        {
            Reset();
        }

        public int Reset()
        {
            return 0;
        }

        // Parse NMEA RMC sentence ($GNRMC / $GMRMC / $GPRMC ...), return degrees.
        // Only accepts status 'A' (valid fix). Avoids allocations and parsing helpers.
        public bool Parse_Rmc_Lat_Lon(string nmea, out double latitude_deg, out double longitude_deg)
        {
            latitude_deg = 0.0;
            longitude_deg = 0.0;

            if (!Parse_Rmc_Lat_Lon_E7(nmea, out int lat_e7, out int lon_e7))
                return false;

            latitude_deg = lat_e7 * 1e-7;
            longitude_deg = lon_e7 * 1e-7;
            return true;
        }

        public (bool valid, double latitude_deg, double longitude_deg) Parse_Rmc_Lat_Lon(string nmea)
        {
            bool valid = Parse_Rmc_Lat_Lon(nmea, out double lat, out double lon);
            return (valid, lat, lon);
        }

        private bool Is_Digit(char c) => c >= '0' && c <= '9';

        private int Find_Char(string s, int pos, char c)
        {
            int n = s.Length;
            for (int i = pos; i < n; i++)
            {
                char ch = s[i];
                if (ch == c) return i;
                if (ch == '\r' || ch == '\n') return -1;
            }
            return -1;
        }

        private bool Parse_U32_Fixed_Digits(string s, ref int pos, int end, int digits, out uint value)
        {
            value = 0;

            if (end < pos) return false;
            if (end - pos < digits) return false;

            uint acc = 0;
            for (int i = 0; i < digits; i++)
            {
                char c = s[pos++];
                if (!Is_Digit(c)) return false;
                acc = (acc * 10u) + (uint)(c - '0');
            }

            value = acc;
            return true;
        }

        private bool Parse_Minutes_Scaled_1e7(string s, ref int pos, int end, out long minutes_scaled_1e7)
        {
            // NMEA: mm[.ffff...]
            minutes_scaled_1e7 = 0;

            if (!Parse_U32_Fixed_Digits(s, ref pos, end, 2, out uint minutesInt))
                return false;

            long scaled = (long)minutesInt * 10_000_000L;

            if (pos < end && s[pos] == '.')
            {
                pos++;

                long frac = 0;
                int fracDigits = 0;
                while (pos < end && Is_Digit(s[pos]) && fracDigits < 7)
                {
                    frac = (frac * 10) + (s[pos] - '0');
                    pos++;
                    fracDigits++;
                }

                // Optional rounding by the 8th digit.
                bool roundUp = false;
                if (pos < end && Is_Digit(s[pos]))
                {
                    roundUp = s[pos] >= '5';
                    while (pos < end && Is_Digit(s[pos])) pos++;
                }

                if (fracDigits < 7)
                {
                    // multiply by 10^(7-fracDigits)
                    long mul = 1;
                    for (int i = 0; i < (7 - fracDigits); i++) mul *= 10;
                    frac *= mul;
                }

                if (roundUp) frac++;

                // carry into integer minutes
                if (frac >= 10_000_000L)
                {
                    frac -= 10_000_000L;
                    scaled += 10_000_000L;
                }

                scaled += frac;
            }

            if (pos != end) return false;
            minutes_scaled_1e7 = scaled;
            return true;
        }

        private bool Parse_Coord_Ddmm_To_E7(string s, int start, int end, int deg_digits, out int out_e7_abs)
        {
            // latitude: ddmm.mmmm, longitude: dddmm.mmmm
            out_e7_abs = 0;

            if (end <= start) return false;
            if ((uint)end > (uint)s.Length) return false;

            int pos = start;

            if (!Parse_U32_Fixed_Digits(s, ref pos, end, deg_digits, out uint deg))
                return false;

            if (!Parse_Minutes_Scaled_1e7(s, ref pos, end, out long minutesScaled1e7))
                return false;

            long degE7 = (long)deg * 10_000_000L;
            long minToDegE7 = (minutesScaled1e7 + 30) / 60; // rounded
            long coordE7 = degE7 + minToDegE7;

            if (coordE7 > int.MaxValue) return false;

            out_e7_abs = (int)coordE7;
            return true;
        }

        private bool Parse_Rmc_Lat_Lon_E7(string nmea, out int latitude_e7, out int longitude_e7)
        {
            latitude_e7 = 0;
            longitude_e7 = 0;

            if (nmea == null) return false;
            if (nmea.Length < 12) return false; // minimal "$??RMC,,,,,,,"

            // Accept: $GNRMC,... or $GMRMC,... etc (any 2-letter talker).
            if (nmea[0] != '$') return false;
            if (nmea.Length < 7) return false;
            if (nmea[3] != 'R' || nmea[4] != 'M' || nmea[5] != 'C' || nmea[6] != ',') return false;

            // Field 1: time (skip)
            int p = 7;
            int comma = Find_Char(nmea, p, ',');
            if (comma < 0) return false;

            // Field 2: status
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0) return false;
            if (p >= comma) return false;
            char status = nmea[p];
            if (status != 'A') return false;

            // Field 3: latitude
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0 || comma == p) return false;
            if (!Parse_Coord_Ddmm_To_E7(nmea, p, comma, 2, out int latAbsE7)) return false;

            // Field 4: N/S
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0) return false;
            if (p >= comma) return false;
            char ns = nmea[p];
            if (ns != 'N' && ns != 'S') return false;

            // Field 5: longitude
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0 || comma == p) return false;
            if (!Parse_Coord_Ddmm_To_E7(nmea, p, comma, 3, out int lonAbsE7)) return false;

            // Field 6: E/W
            p = comma + 1;
            if (p >= nmea.Length) return false;
            char ew = nmea[p];
            if (ew != 'E' && ew != 'W') return false;

            latitude_e7 = (ns == 'S') ? -latAbsE7 : latAbsE7;
            longitude_e7 = (ew == 'W') ? -lonAbsE7 : lonAbsE7;
            return true;
        }
    }
}
