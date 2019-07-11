using System;
using System.Collections.Generic;
using System.Text;

namespace ZIP.Utility {
    public static class StringExtensions {
        public static string ConvertByteToString(this byte[] buffer) {
            return Convert.ToBase64String(buffer);
        }

        public static string Base64Encode(this string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
