#region

using System;
using System.Text;
using FSLibrary;

//using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class SMS
    {
        public string RemoveInternationalDiallingCode(string tTelNumber)
        {
            string removeInternationalDiallingCodeReturn = null;
            removeInternationalDiallingCodeReturn = "0" + tTelNumber.Substring(4);
            return removeInternationalDiallingCodeReturn;
        }


        public string GetSMSNum(string tSMSNotification)
        {
            long lCRPos = 0;
            var s = "";

            lCRPos = TextUtil.LastIndexOf(tSMSNotification, Global.Cr);
            if (lCRPos > 0) s = tSMSNotification.Substring(Convert.ToInt32(lCRPos - 2), 2);
            var transTemp0 = 1;
            var transTemp1 = 1;
            if (s.Substring(0, transTemp0) == ",") s = s.Substring(s.Length - transTemp1);

            return s;
        }


        public bool ReadSMS(DBComm comms, string tSMSNum, string tMessage, string tSender, string tDate, string tTime,
            bool bRead)
        {
            var readSMSReturn = false;
            var tChar = char.MinValue;
            var tRead = "";
            long i = 0;
            long lQuoteNum = 0;
            var tDateTime = "";
            string tData = null;
            long timeGetTime = 0;

            const long TIMEOUT = 10000;

            tData = "AT+CMGR=" + tSMSNum + Global.Cr;
            comms.Write(Encoding.ASCII.GetBytes(tData));
            tData = "";

            while ((timeGetTime <= TIMEOUT) & (tData.IndexOf("OK" + "\r\n") + 1 == 0) &
                   (tData.IndexOf("ERROR" + "\r\n") + 1 == 0))
            {
                //System.Threading.Thread.Sleep (100);
                //Application.DoEvents();
                try
                {
                    if (comms.Read(1) != -1) tData = tData + Convert.ToChar(comms.InputStream[0]);
                }
                catch
                {
                }

                timeGetTime = timeGetTime + 1;
            }

            var transTemp2 = "ERROR";
            if ((tData.IndexOf(transTemp2) + 1 == 0) & (tData != ""))
            {
                for (i = 1; i <= tData.Length; i++)
                {
                    tChar = char.Parse(tData.Substring(Convert.ToInt32(i), 1));
                    if (tChar == char.Parse(@"""")) lQuoteNum = lQuoteNum + 1;
                    if ((lQuoteNum == 1) & (tChar != char.Parse(@""""))) tRead = tRead + tChar;
                    if ((lQuoteNum == 3) & (tChar != char.Parse(@""""))) tSender = tSender + tChar;
                    if ((lQuoteNum == 5) & (tChar != char.Parse(@""""))) tDateTime = tDateTime + tChar;
                    if ((lQuoteNum == 6) & (tChar != char.Parse(@""""))) tMessage = tMessage + tChar;
                }

                bRead = tRead == "REC READ";
                var transTemp3 = 8;
                tDate = tDateTime.Substring(0, transTemp3);
                tTime = tDateTime.Substring(10, 8);
                tMessage = tMessage.Substring(3);
                tMessage = tMessage.Substring(1, tMessage.IndexOf(Global.Cr) + 1 - 1);
                readSMSReturn = true;
            }

            return readSMSReturn;
        }


        public bool DeleteAllSMSes(DBComm comms)
        {
            var deleteAllSMSesReturn = false;
            long i = 0;

            for (i = 1; i <= 15; i++)
            {
                var ret = TransmitAndReceiveData(comms, "AT+CMGD=" + i + Global.Cr);
                deleteAllSMSesReturn = ret.IndexOf("OK") > 0 ? true : false;
            }

            return deleteAllSMSesReturn;
        }


        public bool DeleteSMS(DBComm comms, long lSMSNum)
        {
            var deleteSMSReturn = false;
            var ret = TransmitAndReceiveData(comms, "AT+CMGD=" + lSMSNum + Global.Cr);
            deleteSMSReturn = ret.IndexOf("OK") > 0 ? true : false;
            return deleteSMSReturn;
        }


        public bool SendSMS(DBComm comms, string tSMSNum, string tMessage)
        {
            var sendSMSReturn = false;
            TransmitAndReceiveData(comms, "AT+CMGF=1");
            var ret = TransmitAndReceiveData(comms,
                "AT+CMGS=" + @"""" + tSMSNum + @"""" + Global.Cr + tMessage +
                Convert.ToChar(26));
            sendSMSReturn = ret.IndexOf("OK") > 0 ? true : false;
            return sendSMSReturn;
        }


        public bool SendEMS(DBComm comms, ref string[] tMessage)
        {
            var lon = 0;
            var f = 0;

            TransmitAndReceiveData(comms, "AT+CMGF=0");

            Array transTemp10 = tMessage;
            for (f = 0; f <= transTemp10.GetUpperBound(0); f++)
            {
                var transTemp11 = tMessage[f];
                lon =
                    Convert.ToInt32((transTemp11.Length - Convert.ToInt32("&H" + tMessage[f].Substring(1, 2)) * 2 - 2) /
                                    2);
                TransmitAndReceiveData(comms, "AT+CMGS=" + lon + Global.Cr);
                TransmitAndReceiveData(comms, tMessage[f] + Convert.ToChar(26));
            }

            return false;
        }


        public string TestModem(DBComm comms)
        {
            string testModemReturn = null;
            testModemReturn = TransmitAndReceiveData(comms, "AT");
            return testModemReturn;
        }


        public string ManufacturerInfo(DBComm comms)
        {
            string manufacturerInfoReturn = null;
            manufacturerInfoReturn = TransmitAndReceiveData(comms, "AT+CGMI");
            return manufacturerInfoReturn;
        }


        public string ModelInfo(DBComm comms)
        {
            string modelInfoReturn = null;
            modelInfoReturn = TransmitAndReceiveData(comms, "AT+CGMM");
            return modelInfoReturn;
        }


        public string FirmwareInfo(DBComm comms)
        {
            string firmwareInfoReturn = null;
            firmwareInfoReturn = TransmitAndReceiveData(comms, "AT+CGMR");
            return firmwareInfoReturn;
        }


        public string IMEIInfo(DBComm comms)
        {
            string iMEIInfoReturn = null;
            iMEIInfoReturn = TransmitAndReceiveData(comms, "AT+CGSN");
            return iMEIInfoReturn;
        }


        public string IMSIInfo(DBComm comms)
        {
            string iMSIInfoReturn = null;
            iMSIInfoReturn = TransmitAndReceiveData(comms, "AT+CIMI");
            return iMSIInfoReturn;
        }


        public string EF_CCIDInfo(DBComm comms)
        {
            string eF_CCIDInfoReturn = null;
            eF_CCIDInfoReturn = TransmitAndReceiveData(comms, "AT+CCID");
            return eF_CCIDInfoReturn;
        }


        public string NetworkRegStatus(DBComm comms)
        {
            string networkRegStatusReturn = null;
            networkRegStatusReturn = TransmitAndReceiveData(comms, "AT+CREG?");
            return networkRegStatusReturn;
        }


        public string AvailablePLMNs(DBComm comms)
        {
            string availablePLMNsReturn = null;
            availablePLMNsReturn = TransmitAndReceiveData(comms, "AT+COPS?");
            return availablePLMNsReturn;
        }


        public string NetworkFieldStrength(DBComm comms)
        {
            string networkFieldStrengthReturn = null;
            networkFieldStrengthReturn = TransmitAndReceiveData(comms, "AT+CSQ");
            return networkFieldStrengthReturn;
        }


        public string MainCellMainParams(DBComm comms)
        {
            string mainCellMainParamsReturn = null;
            mainCellMainParamsReturn = TransmitAndReceiveData(comms, "AT+CCED=0");
            return mainCellMainParamsReturn;
        }


        public string TransmitAndReceiveData(DBComm comms, string tData)
        {
            string transmitAndReceiveDataReturn = null;
            long timeGetTime = 0;

            const long TIMEOUT = 100000;

            tData = tData + Global.Cr;

            var transTemp13 = comms;
            transTemp13.Write(Encoding.ASCII.GetBytes(tData));
            tData = "";

            var transTemp12 = "OK";
            while ((timeGetTime <= TIMEOUT) & (tData.IndexOf(transTemp12) + 1 == 0) &
                   (tData.IndexOf("ERROR" + "\r\n") + 1 == 0))
            {
                //System.Threading.Thread.Sleep (100);
                //Application.DoEvents();
                try
                {
                    tData = tData + transTemp13.InputStreamString;
                }
                catch
                {
                }

                timeGetTime = timeGetTime + 1;
            }


            transmitAndReceiveDataReturn = tData;
            return transmitAndReceiveDataReturn;
        }
    }
}