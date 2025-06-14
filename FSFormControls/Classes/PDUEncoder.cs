#region

using System;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class PDUEncoder
    {
        #region ENUM_TP_DCS enum

        public enum ENUM_TP_DCS
        {
            DefaultAlphabet = 0,
            UCS2 = 8
        }

        #endregion

        #region ENUM_TP_SRI enum

        public enum ENUM_TP_SRI
        {
            Request_SMS_Report = 32,
            No_SMS_Report = 0
        }

        #endregion

        #region ENUM_TP_VALID_PERIOD enum

        public enum ENUM_TP_VALID_PERIOD
        {
            OneHour = 11,
            ThreeHours = 29,
            SixHours = 71,
            TwelveHours = 143,
            OneDay = 167,
            OneWeek = 196,
            Maximum = 255
        }

        #endregion

        #region ENUM_TP_VPF enum

        public enum ENUM_TP_VPF
        {
            Relative_Format = 16
        }

        #endregion

        protected string SC_Number;
        protected int TotalMessages;
        protected string TP_DA;
        protected ENUM_TP_DCS TP_DCS;
        protected int TP_MR;
        protected byte TP_MTI = 1;
        protected byte TP_PID;
        protected byte TP_RD;
        protected ENUM_TP_SRI TP_SRR;
        protected string TP_UD;
        protected byte TP_UDHI;
        protected int TP_UDL;
        protected ENUM_TP_VALID_PERIOD TP_VP;
        protected byte TP_VPF = 16;


        public string ServiceCenterNumber
        {
            get { return SC_Number; }
            set
            {
                if (value.IndexOf("+") > -1)
                    SC_Number = "91";
                else
                    SC_Number = "81";

                value = value.Substring(1);

                if (value.Length % 2 == 1) value += "F";
                for (var i = 0; i < value.Length; i += 2)
                {
                    var tt = value.Substring(i, 2);
                    SC_Number += Swap(ref tt);
                }

                SC_Number = ByteToHex(Convert.ToByte((SC_Number.Length - 2) / 2 + 1)) + SC_Number;
            }
        }

        public ENUM_TP_SRI TP_Status_Report_Request
        {
            get { return TP_SRR; }
            set { TP_SRR = value; }
        }

        public int TP_Message_Reference
        {
            get { return TP_MR; }
            set { TP_MR = value; }
        }

        public string TP_Destination_Address
        {
            get { return TP_DA; }
            set
            {
                TP_DA = "";

                if (value.IndexOf("+") > 0)
                    TP_DA += "91";
                else
                    TP_DA += "81";
                value = value.Replace("+", "");
                TP_DA = value.Length.ToString("X2") + TP_DA;
                var i = 0;
                if (value.Length % 2 == 1) value += "F";
                for (i = 0; i < value.Length; i += 2)
                {
                    var transTemp2 = value.Substring(i, 2);
                    TP_DA += Swap(ref transTemp2);
                }
            }
        }


        public ENUM_TP_DCS TP_Data_Coding_Scheme
        {
            get { return TP_DCS; }
            set { TP_DCS = value; }
        }

        public ENUM_TP_VALID_PERIOD TP_Validity_Period
        {
            get { return TP_VP; }
            set { TP_VP = value; }
        }

        public virtual string TP_User_Data
        {
            get { return TP_UD; }
            set
            {
                var selectVal = TP_DCS;
                if (selectVal == ENUM_TP_DCS.DefaultAlphabet)
                {
                    TP_UDL = value.Length;
                    TP_UD = Encode7Bit(value);
                }
                else if (selectVal == ENUM_TP_DCS.UCS2)
                {
                    TP_UDL = value.Length * 2;
                    TP_UD = EncodeUCS2(value);
                }
                else
                {
                    TP_UD = value;
                }
            }
        }

        public static ENUM_TP_DCS CheckForEncoding(string Content)
        {
            var i = 0;
            for (i = 1; i <= Content.Length; i++)
                if (Convert.ToInt32(char.Parse(Content.Substring(i, 1))) < 0)
                    return ENUM_TP_DCS.UCS2;
            return ENUM_TP_DCS.DefaultAlphabet;
        }


        public virtual string GetSMSPDUCode()
        {
            string PDUCode = null;
            if (TP_DCS == ENUM_TP_DCS.DefaultAlphabet)
                if (TP_UD.Length > 280)
                    throw new ExceptionUtil("User Data is TOO LONG for SMS");
            if (TP_DCS == ENUM_TP_DCS.UCS2)
                if (TP_UD.Length > 280)
                    throw new ExceptionUtil("User Data is TOO LONG for SMS");
            PDUCode = SC_Number;
            PDUCode += FirstOctet();
            PDUCode += TP_MR.ToString("X2");
            PDUCode += TP_DA;
            PDUCode += TP_PID.ToString("X2");
            PDUCode += Convert.ToInt32(TP_DCS).ToString("X2");
            PDUCode += Convert.ToInt32(TP_VP).ToString("X2");
            PDUCode += TP_UDL.ToString("X2");
            PDUCode += TP_UD;
            return PDUCode;
        }


        public string[] GetEMSPDUCode()
        {
            switch (TP_DCS)
            {
                case ENUM_TP_DCS.UCS2:
                    TotalMessages =
                        Convert.ToInt32(Convert.ToInt64(TP_UD.Length / 4) / 66 +
                                        Convert.ToInt64(TP_UD.Length / 4 % 66 == 0));
                    break;
                case ENUM_TP_DCS.DefaultAlphabet:
                    TotalMessages = TP_UD.Length / 266 - Convert.ToInt32(TP_UD.Length % 266 == 0);
                    break;
            }


            var Result = new string[TotalMessages + 1];
            var tmpTP_UD = "";
            var i = 0;
            TP_UDHI = Convert.ToByte(Math.Pow(2, 6));

            var r = new Random();

            var Reference = Convert.ToInt32((double) r.Next(100) / 100 * 65536);
            for (i = 0; i <= TotalMessages; i++)
            {
                switch (TP_DCS)
                {
                    case ENUM_TP_DCS.UCS2:
                        tmpTP_UD = TextUtil.Substring(TP_UD, i * 66 * 4, 66 * 4);
                        break;
                    case ENUM_TP_DCS.DefaultAlphabet:
                        tmpTP_UD = TextUtil.Substring(TP_UD, i * 133 * 2, 133 * 2);
                        break;
                }

                Result[i] = SC_Number;
                Result[i] += FirstOctet();
                Result[i] += TP_MR.ToString("X2");
                Result[i] += TP_DA;
                Result[i] += TP_PID.ToString("X2");
                Result[i] += Convert.ToInt32(TP_DCS).ToString("X2");
                Result[i] += Convert.ToInt32(TP_VP).ToString("X2");
                if (TP_DCS == ENUM_TP_DCS.UCS2) TP_UDL = Convert.ToInt32(tmpTP_UD.Length / 2 + 6 + 1);
                if (TP_DCS == ENUM_TP_DCS.DefaultAlphabet) TP_UDL = Convert.ToInt32((tmpTP_UD.Length + 7 * 2) * 4 / 7);
                Result[i] += TP_UDL.ToString("X2");
                Result[i] += "060804";
                Result[i] += Reference.ToString("X4");
                Result[i] += (TotalMessages + 1).ToString("X2");
                Result[i] += (i + 1).ToString("X2");
                Result[i] += tmpTP_UD;
            }

            return Result;
        }


        public virtual string FirstOctet()
        {
            return ByteToHex(Convert.ToByte(TP_MTI + TP_VPF + TP_SRR + TP_UDHI));
        }


        public static string ByteToHex(byte aByte)
        {
            string result = null;
            result = aByte.ToString("X2");
            return result;
        }


        public static string Encode7Bit(string Content)
        {
            var CharArray = Content.ToCharArray();
            var t = "";
            foreach (var c in CharArray) t = CharTo7Bits(c) + t;
            var i = 0;
            if (t.Length % 8 != 0)
            {
                var g = 8 - t.Length % 8;
                for (i = 1; i <= g; i++) t = "0" + t;
            }

            var result = "";
            for (i = t.Length - 8; i >= 0; i -= 8) result = result + BitsToHex(t.Substring(i, 8));
            return result;
        }


        public static string BitsToHex(string Bits)
        {
            int i = 0, v = 0;
            for (i = 0; i < Bits.Length; i++)
                v = Convert.ToInt32(v + Convert.ToInt32(Bits.Substring(i, 1)) * Math.Pow(2, 7 - i));
            string result = null;
            result = v.ToString("X2");
            return result;
        }


        public static string CharTo7Bits(char c)
        {
            if (c == char.Parse("@")) return "0000000";
            var Result = "";
            var i = 0;
            for (i = 0; i <= 6; i++)
                if ((Convert.ToInt32(c) & Convert.ToInt64(Math.Pow(2, i))) > 0)
                    Result = "1" + Result;
                else
                    Result = "0" + Result;
            return Result;
        }


        public static string EncodeUCS2(string Content)
        {
            int i = 0, v = 0;
            var Result = "";
            string t = null;
            for (i = 1; i <= Content.Length; i++)
            {
                v = Convert.ToInt32(Content.Substring(i, 4));
                t = v.ToString("X4");
                Result += t;
            }

            return Result;
        }


        public static string Swap(ref string TwoBitStr)
        {
            var c = TwoBitStr.ToCharArray();
            var t = char.MinValue;
            t = c[0];
            c[0] = c[1];
            c[1] = t;
            return c[0] + c[1].ToString();
        }


        public string[] GetPDU(string CenterNumber, string DestNumber, ENUM_TP_DCS DataCodingScheme,
            ENUM_TP_VALID_PERIOD ValidPeriod, int MsgReference, bool StatusReport, string UserData)
        {
            var Type = 0;
            string[] Result = null;

            switch (DataCodingScheme)
            {
                case ENUM_TP_DCS.DefaultAlphabet:
                    if (UserData.Length > 160) Type = 1;
                    break;
                case ENUM_TP_DCS.UCS2:
                    if (UserData.Length > 70) Type = 1;
                    break;
            }


            ServiceCenterNumber = CenterNumber;
            if (StatusReport)
                TP_Status_Report_Request = ENUM_TP_SRI.Request_SMS_Report;
            else
                TP_Status_Report_Request = ENUM_TP_SRI.No_SMS_Report;
            TP_Destination_Address = DestNumber;
            TP_Data_Coding_Scheme = DataCodingScheme;
            TP_Message_Reference = MsgReference;
            TP_Validity_Period = ValidPeriod;
            TP_User_Data = UserData;

            if (Type == 0)
            {
                Result = new string[1];
                Result[0] = GetSMSPDUCode();
            }
            else
            {
                Result = GetEMSPDUCode();
            }

            return Result;
        }
    }
}