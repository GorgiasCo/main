using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Gorgias.Infrastruture.Core.Encoder
{
    public static class EncodingHelper
    {
        public static string Base64ForUrlEncode(string str)
        {
            var encbuff = Encoding.UTF8.GetBytes(str);
            return HttpServerUtility.UrlTokenEncode(encbuff);
        }

        public static string Base64ForUrlDecode(string str)
        {
            var decbuff = HttpServerUtility.UrlTokenDecode(str);
            return decbuff != null ? Encoding.UTF8.GetString(decbuff) : null;
        }
    }
}