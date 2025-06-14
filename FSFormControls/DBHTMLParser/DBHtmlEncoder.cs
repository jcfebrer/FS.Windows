#region

using System;
using System.IO;
using System.Text;
using FSLibrary;

#endregion

namespace FSFormControls
{
    public class DBHtmlEncoder
    {
        public static string EncodeValue(string value)
        {
            var output = new StringBuilder();
            var reader = new StringReader(value);
            var c = reader.Read();
            while (c != -1)
            {
                switch (c)
                {
                    case 60:
                        output.Append("&lt;");

                        break;
                    case 62:
                        output.Append("&gt;");

                        break;
                    case 34:
                        output.Append("&quot;");

                        break;
                    case 38:
                        output.Append("&amp;");

                        break;
                    case 193:
                        output.Append("&Aacute;");

                        break;
                    case 225:
                        output.Append("&aacute;");

                        break;
                    case 194:
                        output.Append("&Acirc;");

                        break;
                    case 226:
                        output.Append("&acirc;");

                        break;
                    case 180:
                        output.Append("&acute;");

                        break;
                    case 198:
                        output.Append("&AElig;");

                        break;
                    case 230:
                        output.Append("&aelig;");

                        break;
                    case 192:
                        output.Append("&Agrave;");

                        break;
                    case 224:
                        output.Append("&agrave;");

                        break;
                    case 8501:
                        output.Append("&alefsym;");

                        break;
                    case 913:
                        output.Append("&Alpha;");

                        break;
                    case 945:
                        output.Append("&alpha;");


                        break;
                    case 8743:
                        output.Append("&and;");

                        break;
                    case 8736:
                        output.Append("&ang;");

                        break;
                    case 197:
                        output.Append("&Aring;");

                        break;
                    case 229:
                        output.Append("&aring;");

                        break;
                    case 8776:
                        output.Append("&asymp;");

                        break;
                    case 195:
                        output.Append("&Atilde;");

                        break;
                    case 227:
                        output.Append("&atilde;");

                        break;
                    case 196:
                        output.Append("&Auml;");

                        break;
                    case 228:
                        output.Append("&auml;");

                        break;
                    case 8222:
                        output.Append("&bdquo;");

                        break;
                    case 914:
                        output.Append("&Beta;");

                        break;
                    case 946:
                        output.Append("&beta;");

                        break;
                    case 166:
                        output.Append("&brvbar;");

                        break;
                    case 8226:
                        output.Append("&bull;");

                        break;
                    case 8745:
                        output.Append("&cap;");

                        break;
                    case 199:
                        output.Append("&Ccedil;");

                        break;
                    case 231:
                        output.Append("&ccedil;");

                        break;
                    case 184:
                        output.Append("&cedil;");

                        break;
                    case 162:
                        output.Append("&cent;");

                        break;
                    case 935:
                        output.Append("&Chi;");

                        break;
                    case 967:
                        output.Append("&chi;");

                        break;
                    case 710:
                        output.Append("&circ;");

                        break;
                    case 9827:
                        output.Append("&clubs;");

                        break;
                    case 8773:
                        output.Append("&cong;");

                        break;
                    case 169:
                        output.Append("&copy;");

                        break;
                    case 8629:
                        output.Append("&crarr;");

                        break;
                    case 8746:
                        output.Append("&cup;");

                        break;
                    case 164:
                        output.Append("&curren;");

                        break;
                    case 8224:
                        output.Append("&dagger;");

                        break;
                    case 8225:
                        output.Append("&Dagger;");

                        break;
                    case 8595:
                        output.Append("&darr;");

                        break;
                    case 8659:
                        output.Append("&dArr;");

                        break;
                    case 176:
                        output.Append("&deg;");

                        break;
                    case 916:
                        output.Append("&Delta;");

                        break;
                    case 948:
                        output.Append("&delta;");

                        break;
                    case 9830:
                        output.Append("&diams;");

                        break;
                    case 247:
                        output.Append("&divide;");

                        break;
                    case 201:
                        output.Append("&Eacute;");

                        break;
                    case 233:
                        output.Append("&eacute;");

                        break;
                    case 202:
                        output.Append("&Ecirc;");

                        break;
                    case 234:
                        output.Append("&ecirc;");


                        break;
                    case 200:
                        output.Append("&Egrave;");

                        break;
                    case 232:
                        output.Append("&egrave;");

                        break;
                    case 8709:
                        output.Append("&empty;");

                        break;
                    case 8195:
                        output.Append("&emsp;");

                        break;
                    case 917:
                        output.Append("&Epsilon;");

                        break;
                    case 949:
                        output.Append("&epsilon;");

                        break;
                    case 8801:
                        output.Append("&equiv;");

                        break;
                    case 919:
                        output.Append("&Eta;");

                        break;
                    case 951:
                        output.Append("&eta;");

                        break;
                    case 208:
                        output.Append("&ETH;");

                        break;
                    case 240:
                        output.Append("&eth;");

                        break;
                    case 203:
                        output.Append("&Euml;");

                        break;
                    case 235:
                        output.Append("&euml;");

                        break;
                    case 128:
                        output.Append("&euro;");

                        break;
                    case 8707:
                        output.Append("&exist;");

                        break;
                    case 402:
                        output.Append("&fnof;");

                        break;
                    case 8704:
                        output.Append("&forall;");

                        break;
                    case 189:
                        output.Append("&frac12;");

                        break;
                    case 188:
                        output.Append("&frac14;");

                        break;
                    case 190:
                        output.Append("&frac34;");

                        break;
                    case 8260:
                        output.Append("&fras1;");

                        break;
                    case 915:
                        output.Append("&Gamma;");

                        break;
                    case 947:
                        output.Append("&gamma;");

                        break;
                    case 8805:
                        output.Append("&ge;");

                        break;
                    case 8596:
                        output.Append("&harr;");

                        break;
                    case 8660:
                        output.Append("&hArr;");

                        break;
                    case 9829:
                        output.Append("&hearts;");

                        break;
                    case 8230:
                        output.Append("&hellip;");

                        break;
                    case 205:
                        output.Append("&Iacute;");

                        break;
                    case 237:
                        output.Append("&iacute;");

                        break;
                    case 206:
                        output.Append("&Icirc;");

                        break;
                    case 238:
                        output.Append("&icirc;");

                        break;
                    case 161:
                        output.Append("&iexcl;");

                        break;
                    case 204:
                        output.Append("&Igrave;");

                        break;
                    case 236:
                        output.Append("&igrave;");

                        break;
                    case 8465:
                        output.Append("&image;");

                        break;
                    case 8734:
                        output.Append("&infin;");

                        break;
                    case 8747:
                        output.Append("&int;");

                        break;
                    case 921:
                        output.Append("&Iota;");


                        break;
                    case 953:
                        output.Append("&iota;");

                        break;
                    case 191:
                        output.Append("&iquest;");

                        break;
                    case 8712:
                        output.Append("&isin;");

                        break;
                    case 207:
                        output.Append("&Iuml;");

                        break;
                    case 239:
                        output.Append("&iuml;");

                        break;
                    case 922:
                        output.Append("&Kappa;");

                        break;
                    case 954:
                        output.Append("&kappa;");

                        break;
                    case 923:
                        output.Append("&Lambda;");

                        break;
                    case 955:
                        output.Append("&lambda;");

                        break;
                    case 9001:
                        output.Append("&lang;");

                        break;
                    case 171:
                        output.Append("&laquo;");

                        break;
                    case 8592:
                        output.Append("&larr;");

                        break;
                    case 8656:
                        output.Append("&lArr;");

                        break;
                    case 8968:
                        output.Append("&lceil;");

                        break;
                    case 8220:
                        output.Append("&ldquo;");

                        break;
                    case 8804:
                        output.Append("&le;");

                        break;
                    case 8970:
                        output.Append("&lfloor;");

                        break;
                    case 8727:
                        output.Append("&lowast;");

                        break;
                    case 9674:
                        output.Append("&loz;");

                        break;
                    case 8206:
                        output.Append("&lrm;");

                        break;
                    case 8249:
                        output.Append("&lsaquo;");

                        break;
                    case 8216:
                        output.Append("&lsquo;");

                        break;
                    case 175:
                        output.Append("&macr;");

                        break;
                    case 8212:
                        output.Append("&mdash;");

                        break;
                    case 181:
                        output.Append("&micro;");

                        break;
                    case 183:
                        output.Append("&middot;");

                        break;
                    case 8722:
                        output.Append("&minus;");

                        break;
                    case 924:
                        output.Append("&Mu;");

                        break;
                    case 956:
                        output.Append("&mu;");

                        break;
                    case 8711:
                        output.Append("&nabla;");

                        break;
                    case 160:
                        output.Append("&nbsp;");

                        break;
                    case 8211:
                        output.Append("&ndash;");

                        break;
                    case 8800:
                        output.Append("&ne;");

                        break;
                    case 8715:
                        output.Append("&ni;");

                        break;
                    case 172:
                        output.Append("&not;");

                        break;
                    case 8713:
                        output.Append("&notin;");

                        break;
                    case 8836:
                        output.Append("&nsub;");

                        break;
                    case 209:
                        output.Append("&Ntilde;");

                        break;
                    case 241:
                        output.Append("&ntilde;");

                        break;
                    case 925:
                        output.Append("&Nu;");


                        break;
                    case 957:
                        output.Append("&nu;");

                        break;
                    case 211:
                        output.Append("&Oacute;");

                        break;
                    case 243:
                        output.Append("&oacute;");

                        break;
                    case 212:
                        output.Append("&Ocirc;");

                        break;
                    case 244:
                        output.Append("&ocirc;");

                        break;
                    case 338:
                        output.Append("&OElig;");

                        break;
                    case 339:
                        output.Append("&oelig;");

                        break;
                    case 210:
                        output.Append("&Ograve;");

                        break;
                    case 242:
                        output.Append("&ograve;");

                        break;
                    case 8254:
                        output.Append("&oline;");

                        break;
                    case 937:
                        output.Append("&Omega;");

                        break;
                    case 969:
                        output.Append("&omega;");

                        break;
                    case 927:
                        output.Append("&Omicron;");

                        break;
                    case 959:
                        output.Append("&omicron;");

                        break;
                    case 8853:
                        output.Append("&oplus;");

                        break;
                    case 8744:
                        output.Append("&or;");

                        break;
                    case 170:
                        output.Append("&ordf;");

                        break;
                    case 186:
                        output.Append("&ordm;");

                        break;
                    case 216:
                        output.Append("&Oslash;");

                        break;
                    case 248:
                        output.Append("&oslash;");

                        break;
                    case 213:
                        output.Append("&Otilde;");

                        break;
                    case 245:
                        output.Append("&otilde;");

                        break;
                    case 8855:
                        output.Append("&otimes;");

                        break;
                    case 214:
                        output.Append("&Ouml;");

                        break;
                    case 246:
                        output.Append("&ouml;");

                        break;
                    case 182:
                        output.Append("&para;");

                        break;
                    case 8706:
                        output.Append("&part;");

                        break;
                    case 8240:
                        output.Append("&permil;");

                        break;
                    case 8869:
                        output.Append("&perp;");

                        break;
                    case 934:
                        output.Append("&Phi;");

                        break;
                    case 966:
                        output.Append("&phi;");

                        break;
                    case 928:
                        output.Append("&Pi;");

                        break;
                    case 960:
                        output.Append("&pi;");

                        break;
                    case 982:
                        output.Append("&piv;");

                        break;
                    case 177:
                        output.Append("&plusmn;");

                        break;
                    case 163:
                        output.Append("&pound;");

                        break;
                    case 8242:
                        output.Append("&prime;");

                        break;
                    case 8243:
                        output.Append("&Prime;");

                        break;
                    case 8719:
                        output.Append("&prod;");

                        break;
                    case 8733:
                        output.Append("&prop;");

                        break;
                    case 936:
                        output.Append("&Psi;");


                        break;
                    case 968:
                        output.Append("&psi;");

                        break;
                    case 8730:
                        output.Append("&radic;");

                        break;
                    case 9002:
                        output.Append("&rang;");

                        break;
                    case 187:
                        output.Append("&raquo;");

                        break;
                    case 8594:
                        output.Append("&rarr;");

                        break;
                    case 8658:
                        output.Append("&rArr;");

                        break;
                    case 8969:
                        output.Append("&rceil;");

                        break;
                    case 8221:
                        output.Append("&rdquo;");

                        break;
                    case 8476:
                        output.Append("&real;");

                        break;
                    case 174:
                        output.Append("&reg;");

                        break;
                    case 8971:
                        output.Append("&rfloor;");

                        break;
                    case 929:
                        output.Append("&Rho;");

                        break;
                    case 961:
                        output.Append("&rho;");

                        break;
                    case 8207:
                        output.Append("&rlm;");

                        break;
                    case 8250:
                        output.Append("&rsaquo;");

                        break;
                    case 8217:
                        output.Append("&rsquo;");

                        break;
                    case 8218:
                        output.Append("&sbquo;");

                        break;
                    case 352:
                        output.Append("&Scaron;");

                        break;
                    case 353:
                        output.Append("&scaron;");

                        break;
                    case 8901:
                        output.Append("&sdot;");

                        break;
                    case 167:
                        output.Append("&sect;");

                        break;
                    case 173:
                        output.Append("&shy;");

                        break;
                    case 931:
                        output.Append("&Sigma;");

                        break;
                    case 963:
                        output.Append("&sigma;");

                        break;
                    case 962:
                        output.Append("&sigmaf;");

                        break;
                    case 8764:
                        output.Append("&sim;");

                        break;
                    case 9824:
                        output.Append("&spades;");

                        break;
                    case 8834:
                        output.Append("&sub;");

                        break;
                    case 8838:
                        output.Append("&sube;");

                        break;
                    case 8721:
                        output.Append("&sum;");

                        break;
                    case 8835:
                        output.Append("&sup;");

                        break;
                    case 185:
                        output.Append("&sup1;");

                        break;
                    case 178:
                        output.Append("&sup2;");

                        break;
                    case 179:
                        output.Append("&sup3;");

                        break;
                    case 8839:
                        output.Append("&supe;");

                        break;
                    case 223:
                        output.Append("&szlig;");

                        break;
                    case 932:
                        output.Append("&Tau;");

                        break;
                    case 964:
                        output.Append("&tau;");

                        break;
                    case 8756:
                        output.Append("&there4;");

                        break;
                    case 920:
                        output.Append("&Theta;");


                        break;
                    case 952:
                        output.Append("&theta;");

                        break;
                    case 977:
                        output.Append("&thetasym;");

                        break;
                    case 8201:
                        output.Append("&thinsp;");

                        break;
                    case 222:
                        output.Append("&THORN;");

                        break;
                    case 254:
                        output.Append("&thorn;");

                        break;
                    case 732:
                        output.Append("&tilde;");

                        break;
                    case 215:
                        output.Append("&times;");

                        break;
                    case 8482:
                        output.Append("&trade;");

                        break;
                    case 218:
                        output.Append("&Uacute;");

                        break;
                    case 250:
                        output.Append("&uacute;");

                        break;
                    case 8593:
                        output.Append("&uarr;");

                        break;
                    case 8657:
                        output.Append("&uArr;");

                        break;
                    case 219:
                        output.Append("&Ucirc;");

                        break;
                    case 251:
                        output.Append("&ucirc;");

                        break;
                    case 217:
                        output.Append("&Ugrave;");

                        break;
                    case 249:
                        output.Append("&ugrave;");

                        break;
                    case 168:
                        output.Append("&uml;");

                        break;
                    case 978:
                        output.Append("&upsih;");

                        break;
                    case 933:
                        output.Append("&Upsilon;");

                        break;
                    case 965:
                        output.Append("&upsilon;");

                        break;
                    case 220:
                        output.Append("&Uuml;");

                        break;
                    case 252:
                        output.Append("&uuml;");

                        break;
                    case 8472:
                        output.Append("&weierp;");

                        break;
                    case 926:
                        output.Append("&Xi;");

                        break;
                    case 958:
                        output.Append("&xi;");

                        break;
                    case 221:
                        output.Append("&Yacute;");

                        break;
                    case 253:
                        output.Append("&yacute;");

                        break;
                    case 165:
                        output.Append("&yen;");

                        break;
                    case 376:
                        output.Append("&Yuml;");

                        break;
                    case 255:
                        output.Append("&yuml;");

                        break;
                    case 918:
                        output.Append("&Zeta;");

                        break;
                    case 950:
                        output.Append("&zeta;");

                        break;
                    case 8205:
                        output.Append("&zwj;");

                        break;
                    case 8204:
                        output.Append("&zwnj;");

                        break;
                    default:
                        if (c <= 127)
                            output.Append(Convert.ToChar(c));
                        else
                            output.Append("&#" + c + ";");

                        break;
                }

                c = reader.Read();
            }

            return output.ToString();
        }


        public static string DecodeValue(string value)
        {
            var output = new StringBuilder();
            var reader = new StringReader(value);
            StringBuilder token = null;
            var c = reader.Read();
            while (c != -1)
            {
                token = new StringBuilder();
                while ((c != Convert.ToInt32(char.Parse("&"))) & (c != -1))
                {
                    token.Append(Convert.ToChar(c));
                    c = reader.Read();
                }

                output.Append(token);
                if (c == Convert.ToInt32(char.Parse("&")))
                {
                    token = new StringBuilder();
                    while ((c != Convert.ToInt32(char.Parse(";"))) & (c != -1))
                    {
                        token.Append(Convert.ToChar(c));
                        c = reader.Read();
                    }

                    if (c == Convert.ToInt32(char.Parse(";")))
                    {
                        c = reader.Read();
                        token.Append(";");
                        if (token[1] == char.Parse("#"))
                        {
                            var v = 0;
                            int.TryParse(token.ToString().Substring(2, token.Length - 3), out v);
                            output.Append(Convert.ToChar(v));
                        }
                        else
                        {
                            switch (token.ToString())
                            {
                                case "&lt;":
                                    output.Append("<");

                                    break;
                                case "&gt;":
                                    output.Append(">");

                                    break;
                                case "&quot;":
                                    output.Append(Convert.ToChar(34));

                                    break;
                                case "&amp;":
                                    output.Append("&");

                                    break;
                                case "&Aacute;":
                                    output.Append(Convert.ToChar(193));

                                    break;
                                case "&aacute;":
                                    output.Append(Convert.ToChar(225));

                                    break;
                                case "&Acirc;":
                                    output.Append(Convert.ToChar(194));

                                    break;
                                case "&acirc;":
                                    output.Append(Convert.ToChar(226));

                                    break;
                                case "&acute;":
                                    output.Append(Convert.ToChar(180));

                                    break;
                                case "&AElig;":
                                    output.Append(Convert.ToChar(198));

                                    break;
                                case "&aelig;":
                                    output.Append(Convert.ToChar(230));

                                    break;
                                case "&Agrave;":
                                    output.Append(Convert.ToChar(192));

                                    break;
                                case "&agrave;":
                                    output.Append(Convert.ToChar(224));

                                    break;
                                case "&alefsym;":
                                    output.Append(Convert.ToChar(8501));

                                    break;
                                case "&Alpha;":
                                    output.Append(Convert.ToChar(913));

                                    break;
                                case "&alpha;":
                                    output.Append(Convert.ToChar(945));


                                    break;
                                case "&and;":
                                    output.Append(Convert.ToChar(8743));

                                    break;
                                case "&ang;":
                                    output.Append(Convert.ToChar(8736));

                                    break;
                                case "&Aring;":
                                    output.Append(Convert.ToChar(197));

                                    break;
                                case "&aring;":
                                    output.Append(Convert.ToChar(229));

                                    break;
                                case "&asymp;":
                                    output.Append(Convert.ToChar(8776));

                                    break;
                                case "&Atilde;":
                                    output.Append(Convert.ToChar(195));

                                    break;
                                case "&atilde;":
                                    output.Append(Convert.ToChar(227));

                                    break;
                                case "&Auml;":
                                    output.Append(Convert.ToChar(196));

                                    break;
                                case "&auml;":
                                    output.Append(Convert.ToChar(228));

                                    break;
                                case "&bdquo;":
                                    output.Append(Convert.ToChar(8222));

                                    break;
                                case "&Beta;":
                                    output.Append(Convert.ToChar(914));

                                    break;
                                case "&beta;":
                                    output.Append(Convert.ToChar(946));

                                    break;
                                case "&brvbar;":
                                    output.Append(Convert.ToChar(166));

                                    break;
                                case "&bull;":
                                    output.Append(Convert.ToChar(8226));

                                    break;
                                case "&cap;":
                                    output.Append(Convert.ToChar(8745));

                                    break;
                                case "&Ccedil;":
                                    output.Append(Convert.ToChar(199));

                                    break;
                                case "&ccedil;":
                                    output.Append(Convert.ToChar(231));

                                    break;
                                case "&cedil;":
                                    output.Append(Convert.ToChar(184));

                                    break;
                                case "&cent;":
                                    output.Append(Convert.ToChar(162));

                                    break;
                                case "&Chi;":
                                    output.Append(Convert.ToChar(935));

                                    break;
                                case "&chi;":
                                    output.Append(Convert.ToChar(967));

                                    break;
                                case "&circ;":
                                    output.Append(Convert.ToChar(710));

                                    break;
                                case "&clubs;":
                                    output.Append(Convert.ToChar(9827));

                                    break;
                                case "&cong;":
                                    output.Append(Convert.ToChar(8773));

                                    break;
                                case "&copy;":
                                    output.Append(Convert.ToChar(169));

                                    break;
                                case "&crarr;":
                                    output.Append(Convert.ToChar(8629));

                                    break;
                                case "&cup;":
                                    output.Append(Convert.ToChar(8746));

                                    break;
                                case "&curren;":
                                    output.Append(Convert.ToChar(164));

                                    break;
                                case "&dagger;":
                                    output.Append(Convert.ToChar(8224));

                                    break;
                                case "&Dagger;":
                                    output.Append(Convert.ToChar(8225));

                                    break;
                                case "&darr;":
                                    output.Append(Convert.ToChar(8595));

                                    break;
                                case "&dArr;":
                                    output.Append(Convert.ToChar(8659));

                                    break;
                                case "&deg;":
                                    output.Append(Convert.ToChar(176));

                                    break;
                                case "&Delta;":
                                    output.Append(Convert.ToChar(916));

                                    break;
                                case "&delta;":
                                    output.Append(Convert.ToChar(948));

                                    break;
                                case "&diams;":
                                    output.Append(Convert.ToChar(9830));

                                    break;
                                case "&divide;":
                                    output.Append(Convert.ToChar(247));

                                    break;
                                case "&Eacute;":
                                    output.Append(Convert.ToChar(201));

                                    break;
                                case "&eacute;":
                                    output.Append(Convert.ToChar(233));

                                    break;
                                case "&Ecirc;":
                                    output.Append(Convert.ToChar(202));

                                    break;
                                case "&ecirc;":
                                    output.Append(Convert.ToChar(234));


                                    break;
                                case "&Egrave;":
                                    output.Append(Convert.ToChar(200));

                                    break;
                                case "&egrave;":
                                    output.Append(Convert.ToChar(232));

                                    break;
                                case "&empty;":
                                    output.Append(Convert.ToChar(8709));

                                    break;
                                case "&emsp;":
                                    output.Append(Convert.ToChar(8195));

                                    break;
                                case "&Epsilon;":
                                    output.Append(Convert.ToChar(917));

                                    break;
                                case "&epsilon;":
                                    output.Append(Convert.ToChar(949));

                                    break;
                                case "&equiv;":
                                    output.Append(Convert.ToChar(8801));

                                    break;
                                case "&Eta;":
                                    output.Append(Convert.ToChar(919));

                                    break;
                                case "&eta;":
                                    output.Append(Convert.ToChar(951));

                                    break;
                                case "&ETH;":
                                    output.Append(Convert.ToChar(208));

                                    break;
                                case "&eth;":
                                    output.Append(Convert.ToChar(240));

                                    break;
                                case "&Euml;":
                                    output.Append(Convert.ToChar(203));

                                    break;
                                case "&euml;":
                                    output.Append(Convert.ToChar(235));

                                    break;
                                case "&euro;":
                                    output.Append(Convert.ToChar(128));

                                    break;
                                case "&exist;":
                                    output.Append(Convert.ToChar(8707));

                                    break;
                                case "&fnof;":
                                    output.Append(Convert.ToChar(402));

                                    break;
                                case "&forall;":
                                    output.Append(Convert.ToChar(8704));

                                    break;
                                case "&frac12;":
                                    output.Append(Convert.ToChar(189));

                                    break;
                                case "&frac14;":
                                    output.Append(Convert.ToChar(188));

                                    break;
                                case "&frac34;":
                                    output.Append(Convert.ToChar(190));

                                    break;
                                case "&fras1;":
                                    output.Append(Convert.ToChar(8260));

                                    break;
                                case "&Gamma;":
                                    output.Append(Convert.ToChar(915));

                                    break;
                                case "&gamma;":
                                    output.Append(Convert.ToChar(947));

                                    break;
                                case "&ge;":
                                    output.Append(Convert.ToChar(8805));

                                    break;
                                case "&harr;":
                                    output.Append(Convert.ToChar(8596));

                                    break;
                                case "&hArr;":
                                    output.Append(Convert.ToChar(8660));

                                    break;
                                case "&hearts;":
                                    output.Append(Convert.ToChar(9829));

                                    break;
                                case "&hellip;":
                                    output.Append(Convert.ToChar(8230));

                                    break;
                                case "&Iacute;":
                                    output.Append(Convert.ToChar(205));

                                    break;
                                case "&iacute;":
                                    output.Append(Convert.ToChar(237));

                                    break;
                                case "&Icirc;":
                                    output.Append(Convert.ToChar(206));

                                    break;
                                case "&icirc;":
                                    output.Append(Convert.ToChar(238));

                                    break;
                                case "&iexcl;":
                                    output.Append(Convert.ToChar(161));

                                    break;
                                case "&Igrave;":
                                    output.Append(Convert.ToChar(204));

                                    break;
                                case "&igrave;":
                                    output.Append(Convert.ToChar(236));

                                    break;
                                case "&image;":
                                    output.Append(Convert.ToChar(8465));

                                    break;
                                case "&infin;":
                                    output.Append(Convert.ToChar(8734));

                                    break;
                                case "&int;":
                                    output.Append(Convert.ToChar(8747));

                                    break;
                                case "&Iota;":
                                    output.Append(Convert.ToChar(921));


                                    break;
                                case "&iota;":
                                    output.Append(Convert.ToChar(953));

                                    break;
                                case "&iquest;":
                                    output.Append(Convert.ToChar(191));

                                    break;
                                case "&isin;":
                                    output.Append(Convert.ToChar(8712));

                                    break;
                                case "&Iuml;":
                                    output.Append(Convert.ToChar(207));

                                    break;
                                case "&iuml;":
                                    output.Append(Convert.ToChar(239));

                                    break;
                                case "&Kappa;":
                                    output.Append(Convert.ToChar(922));

                                    break;
                                case "&kappa;":
                                    output.Append(Convert.ToChar(954));

                                    break;
                                case "&Lambda;":
                                    output.Append(Convert.ToChar(923));

                                    break;
                                case "&lambda;":
                                    output.Append(Convert.ToChar(955));

                                    break;
                                case "&lang;":
                                    output.Append(Convert.ToChar(9001));

                                    break;
                                case "&laquo;":
                                    output.Append(Convert.ToChar(171));

                                    break;
                                case "&larr;":
                                    output.Append(Convert.ToChar(8592));

                                    break;
                                case "&lArr;":
                                    output.Append(Convert.ToChar(8656));

                                    break;
                                case "&lceil;":
                                    output.Append(Convert.ToChar(8968));

                                    break;
                                case "&ldquo;":
                                    output.Append(Convert.ToChar(8220));

                                    break;
                                case "&le;":
                                    output.Append(Convert.ToChar(8804));

                                    break;
                                case "&lfloor;":
                                    output.Append(Convert.ToChar(8970));

                                    break;
                                case "&lowast;":
                                    output.Append(Convert.ToChar(8727));

                                    break;
                                case "&loz;":
                                    output.Append(Convert.ToChar(9674));

                                    break;
                                case "&lrm;":
                                    output.Append(Convert.ToChar(8206));

                                    break;
                                case "&lsaquo;":
                                    output.Append(Convert.ToChar(8249));

                                    break;
                                case "&lsquo;":
                                    output.Append(Convert.ToChar(8216));

                                    break;
                                case "&macr;":
                                    output.Append(Convert.ToChar(175));

                                    break;
                                case "&mdash;":
                                    output.Append(Convert.ToChar(8212));

                                    break;
                                case "&micro;":
                                    output.Append(Convert.ToChar(181));

                                    break;
                                case "&middot;":
                                    output.Append(Convert.ToChar(183));

                                    break;
                                case "&minus;":
                                    output.Append(Convert.ToChar(8722));

                                    break;
                                case "&Mu;":
                                    output.Append(Convert.ToChar(924));

                                    break;
                                case "&mu;":
                                    output.Append(Convert.ToChar(956));

                                    break;
                                case "&nabla;":
                                    output.Append(Convert.ToChar(8711));

                                    break;
                                case "&nbsp;":
                                    output.Append(Convert.ToChar(160));

                                    break;
                                case "&ndash;":
                                    output.Append(Convert.ToChar(8211));

                                    break;
                                case "&ne;":
                                    output.Append(Convert.ToChar(8800));

                                    break;
                                case "&ni;":
                                    output.Append(Convert.ToChar(8715));

                                    break;
                                case "&not;":
                                    output.Append(Convert.ToChar(172));

                                    break;
                                case "&notin;":
                                    output.Append(Convert.ToChar(8713));

                                    break;
                                case "&nsub;":
                                    output.Append(Convert.ToChar(8836));

                                    break;
                                case "&Ntilde;":
                                    output.Append(Convert.ToChar(209));

                                    break;
                                case "&ntilde;":
                                    output.Append(Convert.ToChar(241));

                                    break;
                                case "&Nu;":
                                    output.Append(Convert.ToChar(925));


                                    break;
                                case "&nu;":
                                    output.Append(Convert.ToChar(957));

                                    break;
                                case "&Oacute;":
                                    output.Append(Convert.ToChar(211));

                                    break;
                                case "&oacute;":
                                    output.Append(Convert.ToChar(243));

                                    break;
                                case "&Ocirc;":
                                    output.Append(Convert.ToChar(212));

                                    break;
                                case "&ocirc;":
                                    output.Append(Convert.ToChar(244));

                                    break;
                                case "&OElig;":
                                    output.Append(Convert.ToChar(338));

                                    break;
                                case "&oelig;":
                                    output.Append(Convert.ToChar(339));

                                    break;
                                case "&Ograve;":
                                    output.Append(Convert.ToChar(210));

                                    break;
                                case "&ograve;":
                                    output.Append(Convert.ToChar(242));

                                    break;
                                case "&oline;":
                                    output.Append(Convert.ToChar(8254));

                                    break;
                                case "&Omega;":
                                    output.Append(Convert.ToChar(937));

                                    break;
                                case "&omega;":
                                    output.Append(Convert.ToChar(969));

                                    break;
                                case "&Omicron;":
                                    output.Append(Convert.ToChar(927));

                                    break;
                                case "&omicron;":
                                    output.Append(Convert.ToChar(959));

                                    break;
                                case "&oplus;":
                                    output.Append(Convert.ToChar(8853));

                                    break;
                                case "&or;":
                                    output.Append(Convert.ToChar(8744));

                                    break;
                                case "&ordf;":
                                    output.Append(Convert.ToChar(170));

                                    break;
                                case "&ordm;":
                                    output.Append(Convert.ToChar(186));

                                    break;
                                case "&Oslash;":
                                    output.Append(Convert.ToChar(216));

                                    break;
                                case "&oslash;":
                                    output.Append(Convert.ToChar(248));

                                    break;
                                case "&Otilde;":
                                    output.Append(Convert.ToChar(213));

                                    break;
                                case "&otilde;":
                                    output.Append(Convert.ToChar(245));

                                    break;
                                case "&otimes;":
                                    output.Append(Convert.ToChar(8855));

                                    break;
                                case "&Ouml;":
                                    output.Append(Convert.ToChar(214));

                                    break;
                                case "&ouml;":
                                    output.Append(Convert.ToChar(246));

                                    break;
                                case "&para;":
                                    output.Append(Convert.ToChar(182));

                                    break;
                                case "&part;":
                                    output.Append(Convert.ToChar(8706));

                                    break;
                                case "&permil;":
                                    output.Append(Convert.ToChar(8240));

                                    break;
                                case "&perp;":
                                    output.Append(Convert.ToChar(8869));

                                    break;
                                case "&Phi;":
                                    output.Append(Convert.ToChar(934));

                                    break;
                                case "&phi;":
                                    output.Append(Convert.ToChar(966));

                                    break;
                                case "&Pi;":
                                    output.Append(Convert.ToChar(928));

                                    break;
                                case "&pi;":
                                    output.Append(Convert.ToChar(960));

                                    break;
                                case "&piv;":
                                    output.Append(Convert.ToChar(982));

                                    break;
                                case "&plusmn;":
                                    output.Append(Convert.ToChar(177));

                                    break;
                                case "&pound;":
                                    output.Append(Convert.ToChar(163));

                                    break;
                                case "&prime;":
                                    output.Append(Convert.ToChar(8242));

                                    break;
                                case "&Prime;":
                                    output.Append(Convert.ToChar(8243));

                                    break;
                                case "&prod;":
                                    output.Append(Convert.ToChar(8719));

                                    break;
                                case "&prop;":
                                    output.Append(Convert.ToChar(8733));

                                    break;
                                case "&Psi;":
                                    output.Append(Convert.ToChar(936));

                                    break;
                                case "&psi;":
                                    output.Append(Convert.ToChar(968));

                                    break;
                                case "&radic;":
                                    output.Append(Convert.ToChar(8730));

                                    break;
                                case "&rang;":
                                    output.Append(Convert.ToChar(9002));

                                    break;
                                case "&raquo;":
                                    output.Append(Convert.ToChar(187));

                                    break;
                                case "&rarr;":
                                    output.Append(Convert.ToChar(8594));

                                    break;
                                case "&rArr;":
                                    output.Append(Convert.ToChar(8658));

                                    break;
                                case "&rceil;":
                                    output.Append(Convert.ToChar(8969));

                                    break;
                                case "&rdquo;":
                                    output.Append(Convert.ToChar(8221));

                                    break;
                                case "&real;":
                                    output.Append(Convert.ToChar(8476));

                                    break;
                                case "&reg;":
                                    output.Append(Convert.ToChar(174));

                                    break;
                                case "&rfloor;":
                                    output.Append(Convert.ToChar(8971));

                                    break;
                                case "&Rho;":
                                    output.Append(Convert.ToChar(929));

                                    break;
                                case "&rho;":
                                    output.Append(Convert.ToChar(961));

                                    break;
                                case "&rlm;":
                                    output.Append(Convert.ToChar(8207));

                                    break;
                                case "&rsaquo;":
                                    output.Append(Convert.ToChar(8250));

                                    break;
                                case "&rsquo;":
                                    output.Append(Convert.ToChar(8217));

                                    break;
                                case "&sbquo;":
                                    output.Append(Convert.ToChar(8218));

                                    break;
                                case "&Scaron;":
                                    output.Append(Convert.ToChar(352));

                                    break;
                                case "&scaron;":
                                    output.Append(Convert.ToChar(353));

                                    break;
                                case "&sdot;":
                                    output.Append(Convert.ToChar(8901));

                                    break;
                                case "&sect;":
                                    output.Append(Convert.ToChar(167));

                                    break;
                                case "&shy;":
                                    output.Append(Convert.ToChar(173));

                                    break;
                                case "&Sigma;":
                                    output.Append(Convert.ToChar(931));

                                    break;
                                case "&sigma;":
                                    output.Append(Convert.ToChar(963));

                                    break;
                                case "&sigmaf;":
                                    output.Append(Convert.ToChar(962));

                                    break;
                                case "&sim;":
                                    output.Append(Convert.ToChar(8764));

                                    break;
                                case "&spades;":
                                    output.Append(Convert.ToChar(9824));

                                    break;
                                case "&sub;":
                                    output.Append(Convert.ToChar(8834));

                                    break;
                                case "&sube;":
                                    output.Append(Convert.ToChar(8838));

                                    break;
                                case "&sum;":
                                    output.Append(Convert.ToChar(8721));

                                    break;
                                case "&sup;":
                                    output.Append(Convert.ToChar(8835));

                                    break;
                                case "&sup1;":
                                    output.Append(Convert.ToChar(185));

                                    break;
                                case "&sup2;":
                                    output.Append(Convert.ToChar(178));

                                    break;
                                case "&sup3;":
                                    output.Append(Convert.ToChar(179));

                                    break;
                                case "&supe;":
                                    output.Append(Convert.ToChar(8839));

                                    break;
                                case "&szlig;":
                                    output.Append(Convert.ToChar(223));

                                    break;
                                case "&Tau;":
                                    output.Append(Convert.ToChar(932));

                                    break;
                                case "&tau;":
                                    output.Append(Convert.ToChar(964));

                                    break;
                                case "&there4;":
                                    output.Append(Convert.ToChar(8756));

                                    break;
                                case "&Theta;":
                                    output.Append(Convert.ToChar(920));


                                    break;
                                case "&theta;":
                                    output.Append(Convert.ToChar(952));

                                    break;
                                case "&thetasym;":
                                    output.Append(Convert.ToChar(977));

                                    break;
                                case "&thinsp;":
                                    output.Append(Convert.ToChar(8201));

                                    break;
                                case "&THORN;":
                                    output.Append(Convert.ToChar(222));

                                    break;
                                case "&thorn;":
                                    output.Append(Convert.ToChar(254));

                                    break;
                                case "&tilde;":
                                    output.Append(Convert.ToChar(732));

                                    break;
                                case "&times;":
                                    output.Append(Convert.ToChar(215));

                                    break;
                                case "&trade;":
                                    output.Append(Convert.ToChar(8482));

                                    break;
                                case "&Uacute;":
                                    output.Append(Convert.ToChar(218));

                                    break;
                                case "&uacute;":
                                    output.Append(Convert.ToChar(250));

                                    break;
                                case "&uarr;":
                                    output.Append(Convert.ToChar(8593));

                                    break;
                                case "&uArr;":
                                    output.Append(Convert.ToChar(8657));

                                    break;
                                case "&Ucirc;":
                                    output.Append(Convert.ToChar(219));

                                    break;
                                case "&ucirc;":
                                    output.Append(Convert.ToChar(251));

                                    break;
                                case "&Ugrave;":
                                    output.Append(Convert.ToChar(217));

                                    break;
                                case "&ugrave;":
                                    output.Append(Convert.ToChar(249));

                                    break;
                                case "&uml;":
                                    output.Append(Convert.ToChar(168));

                                    break;
                                case "&upsih;":
                                    output.Append(Convert.ToChar(978));

                                    break;
                                case "&Upsilon;":
                                    output.Append(Convert.ToChar(933));

                                    break;
                                case "&upsilon;":
                                    output.Append(Convert.ToChar(965));

                                    break;
                                case "&Uuml;":
                                    output.Append(Convert.ToChar(220));

                                    break;
                                case "&uuml;":
                                    output.Append(Convert.ToChar(252));

                                    break;
                                case "&weierp;":
                                    output.Append(Convert.ToChar(8472));

                                    break;
                                case "&Xi;":
                                    output.Append(Convert.ToChar(926));

                                    break;
                                case "&xi;":
                                    output.Append(Convert.ToChar(958));

                                    break;
                                case "&Yacute;":
                                    output.Append(Convert.ToChar(221));

                                    break;
                                case "&yacute;":
                                    output.Append(Convert.ToChar(253));

                                    break;
                                case "&yen;":
                                    output.Append(Convert.ToChar(165));

                                    break;
                                case "&Yuml;":
                                    output.Append(Convert.ToChar(376));

                                    break;
                                case "&yuml;":
                                    output.Append(Convert.ToChar(255));

                                    break;
                                case "&Zeta;":
                                    output.Append(Convert.ToChar(918));

                                    break;
                                case "&zeta;":
                                    output.Append(Convert.ToChar(950));

                                    break;
                                case "&zwj;":
                                    output.Append(Convert.ToChar(8205));

                                    break;
                                case "&zwnj;":
                                    output.Append(Convert.ToChar(8204));


                                    break;
                                default:
                                    output.Append(token);

                                    break;
                            }
                        }
                    }
                    else
                    {
                        output.Append(token);
                    }
                }
            }

            return output.ToString();
        }


        public static string URLEncode(string StringToEncode, bool UsePlusRatherThanHexForSpace)
        {
            string uRLEncodeReturn = null;

            var TempAns = "";
            var CurChr = 0;
            CurChr = 1;
            while (!(CurChr - 1 == StringToEncode.Length))
            {
                var selectVal = Convert.ToInt32(char.Parse(TextUtil.Substring(StringToEncode, CurChr, 1)));
                if (48 <= selectVal && selectVal <= 57 || 65 <= selectVal && selectVal <= 90 ||
                    97 <= selectVal && selectVal <= 122)
                {
                    TempAns = TempAns + TextUtil.Substring(StringToEncode, CurChr, 1);
                }
                else if (selectVal == 32)
                {
                    if (UsePlusRatherThanHexForSpace)
                    {
                        TempAns = TempAns + "+";
                    }
                    else
                    {
                        var transTemp0 = 32;
                        TempAns = TempAns + "%" + Convert.ToString(Convert.ToInt64(transTemp0), 16).ToUpper();
                    }
                }
                else
                {
                    var transTemp1 = Convert.ToInt32(TextUtil.Substring(StringToEncode, CurChr, 1));
                    TempAns = TempAns + "%" + Convert.ToString(Convert.ToInt64(transTemp1), 16).ToUpper();
                }

                CurChr = CurChr + 1;
            }

            uRLEncodeReturn = TempAns;
            return uRLEncodeReturn;
        }


        public static string URLEncode(string StringToEncode)
        {
            return URLEncode(StringToEncode, false);
        }


        public static string URLDecode(string StringToDecode)
        {
            string uRLDecodeReturn = null;

            var TempAns = "";
            var CurChr = 0;

            CurChr = 1;

            while (!(CurChr - 1 == StringToDecode.Length))
            {
                switch (TextUtil.Substring(StringToDecode, CurChr, 1))
                {
                    case "+":
                        TempAns = TempAns + " ";
                        break;
                    case "%":
                        TempAns = TempAns +
                                  Convert.ToChar(
                                      Convert.ToInt32(
                                          Convert.ToInt32("&h" +
                                                          TextUtil.Substring(StringToDecode, CurChr + 1, 2))));
                        CurChr = CurChr + 2;
                        break;
                    default:
                        TempAns = TempAns + TextUtil.Substring(StringToDecode, CurChr, 1);
                        break;
                }


                CurChr = CurChr + 1;
            }

            uRLDecodeReturn = TempAns;
            return uRLDecodeReturn;
        }
    }
}