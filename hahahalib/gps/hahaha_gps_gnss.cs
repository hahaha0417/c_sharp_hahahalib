using System;

namespace hahahalib
{
    /// <summary>
    /// GNSS / NMEA 解析工具。
    /// 目前重點是從 RMC 訊息中擷取經緯度，並盡量避免在高頻解析時產生額外配置。
    /// </summary>
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

        /// <summary>
        /// 解析像 <c>$GNRMC</c> 這類 RMC 訊息，並回傳十進位經緯度。
        /// 只接受狀態為 <c>A</c> 的有效定位資料。
        /// </summary>
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
            // NMEA 分鐘欄位格式：mm[.ffff...]
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

                // 最多保留 7 位小數，並使用第 8 位做四捨五入。
                bool roundUp = false;
                if (pos < end && Is_Digit(s[pos]))
                {
                    roundUp = s[pos] >= '5';
                    while (pos < end && Is_Digit(s[pos])) pos++;
                }

                if (fracDigits < 7)
                {
                    // 補齊到固定 1e7 比例，後續就能用整數運算。
                    long mul = 1;
                    for (int i = 0; i < (7 - fracDigits); i++) mul *= 10;
                    frac *= mul;
                }

                if (roundUp) frac++;

                // 小數進位回整數分鐘。
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
            // 緯度格式為 ddmm.mmmm，經度格式為 dddmm.mmmm。
            out_e7_abs = 0;

            if (end <= start) return false;
            if ((uint)end > (uint)s.Length) return false;

            int pos = start;

            if (!Parse_U32_Fixed_Digits(s, ref pos, end, deg_digits, out uint deg))
                return false;

            if (!Parse_Minutes_Scaled_1e7(s, ref pos, end, out long minutesScaled1e7))
                return false;

            long degE7 = (long)deg * 10_000_000L;
            long minToDegE7 = (minutesScaled1e7 + 30) / 60; // 以四捨五入方式把分鐘轉為度。
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
            if (nmea.Length < 12) return false; // 最短合法格式類似 "$??RMC,,,,,,,"

            // 只要句型是 RMC，就接受任意 talker ID。
            if (nmea[0] != '$') return false;
            if (nmea.Length < 7) return false;
            if (nmea[3] != 'R' || nmea[4] != 'M' || nmea[5] != 'C' || nmea[6] != ',') return false;

            // 欄位 1：UTC 時間，目前不使用。
            int p = 7;
            int comma = Find_Char(nmea, p, ',');
            if (comma < 0) return false;

            // 欄位 2：狀態，必須是 'A' 才視為有效定位。
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0) return false;
            if (p >= comma) return false;
            char status = nmea[p];
            if (status != 'A') return false;

            // 欄位 3：緯度。
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0 || comma == p) return false;
            if (!Parse_Coord_Ddmm_To_E7(nmea, p, comma, 2, out int latAbsE7)) return false;

            // 欄位 4：緯度半球。
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0) return false;
            if (p >= comma) return false;
            char ns = nmea[p];
            if (ns != 'N' && ns != 'S') return false;

            // 欄位 5：經度。
            p = comma + 1;
            comma = Find_Char(nmea, p, ',');
            if (comma < 0 || comma == p) return false;
            if (!Parse_Coord_Ddmm_To_E7(nmea, p, comma, 3, out int lonAbsE7)) return false;

            // 欄位 6：經度半球。
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
