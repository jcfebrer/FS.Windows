#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBComm : DBUserControl
    {
        #region DataParity enum

        public enum DataParity
        {
            Parity_None = 0,
            Pariti_Odd,
            Parity_Even,
            Parity_Mark
        }

        #endregion

        #region DataStopBit enum

        public enum DataStopBit
        {
            StopBit_1 = 1,
            StopBit_2
        }

        #endregion

        #region EventMasks enum

        [Flags]
        public enum EventMasks
        {
            RxChar = 0X1,
            RXFlag = 0X2,
            TxBufferEmpty = 0X4,
            ClearToSend = 0X8,
            DataSetReady = 0X10,
            ReceiveLine = 0X20,
            Break = 0X40,
            StatusError = 0X80,
            Ring = 0X100
        }

        #endregion

        #region Mode enum

        public enum Mode
        {
            NonOverlapped,
            Overlapped
        }

        #endregion

        #region ModemStatusBits enum

        [Flags]
        public enum ModemStatusBits
        {
            ClearToSendOn = 0X10,
            DataSetReadyOn = 0X20,
            RingIndicatorOn = 0X40,
            CarrierDetect = 0X80
        }

        #endregion

        //public event CommEventEventHandler CommEvent; 

        private const int PURGE_RXABORT = 0X2;
        private const int PURGE_RXCLEAR = 0X8;
        private const int PURGE_TXABORT = 0X1;
        private const int PURGE_TXCLEAR = 0X4;
        private const int GENERIC_READ = unchecked((int) 0X80000000);
        private const int GENERIC_WRITE = 0X40000000;
        private const int OPEN_EXISTING = 3;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int IO_BUFFER_SIZE = 1024;
        private const int FILE_FLAG_OVERLAPPED = 0X40000000;
        private const int ERROR_IO_PENDING = 997;
        private const int WAIT_OBJECT_0 = 0;
        private const int ERROR_IO_INCOMPLETE = 996;
        private const int WAIT_TIMEOUT = (int) 0X102L;
        private const int INFINITE = unchecked((int) 0XFFFFFFFF);
        private byte[] mabtRxBuf;
        private byte[] mabtTmpTxBuf;
        private bool mbWaitOnRead;
        private readonly bool mbWaitOnWrite = false;
        private int mhRS = -1;
        private int miTimeout = 70;
        private int miTmpBytes2Read;
        private Thread moThreadTx;
        private OVERLAPPED muOverlapped;
        private OVERLAPPED muOverlappedW;

        public int BaudRate { get; set; } = 9600;

        public int BufferSize { get; set; } = 512;

        public int DataBit { get; set; } = 8;

        public bool Dtr
        {
            set
            {
                if (!(mhRS == -1))
                {
                    if (value)
                        EscapeCommFunction(mhRS, Convert.ToInt64(Lines.SetDtr));
                    else
                        EscapeCommFunction(mhRS, Convert.ToInt64(Lines.ClearDtr));
                }
            }
        }

        public virtual byte[] InputStream => mabtRxBuf;

        public virtual string InputStreamString
        {
            get
            {
                var oEncoder = new ASCIIEncoding();
                return oEncoder.GetString(InputStream);
            }
        }

        public bool IsOpen => mhRS != -1;

        public ModemStatusBits ModemStatus
        {
            get
            {
                if (mhRS == -1)
                    throw new ApplicationException("Please initialize and open " + "port before using this method");

                var lpModemStatus = 0;
                if (!GetCommModemStatus(mhRS, ref lpModemStatus))
                    throw new ApplicationException("Unable to get modem status");
                return (ModemStatusBits) lpModemStatus;
            }
        }

        public DataParity Parity { get; set; } = 0;

        public int Port { get; set; } = 1;

        public bool Rts
        {
            set
            {
                if (!(mhRS == -1))
                {
                    if (value)
                        EscapeCommFunction(mhRS, Convert.ToInt64(Lines.SetRts));
                    else
                        EscapeCommFunction(mhRS, Convert.ToInt64(Lines.ClearRts));
                }
            }
        }

        public DataStopBit StopBit { get; set; } = 0;

        public virtual int Timeout
        {
            get { return miTimeout; }
            set
            {
                miTimeout = value == 0 ? 500 : value;
                pSetTimeout();
            }
        }

        public Mode WorkingMode { get; set; }

        public event DataReceivedEventHandler DataReceived;
        public event TxCompletedEventHandler TxCompleted;


        [DllImport("kernel32.dll")]
        private static extern int BuildCommDCB(string lpDef, ref DCB lpDCB);

        [DllImport("kernel32.dll")]
        private static extern int ClearCommError(int hFile, int lpErrors, int l);

        [DllImport("kernel32.dll")]
        private static extern int CloseHandle(int hObject);

        [DllImport("kernel32.dll")]
        private static extern int CreateEvent(int lpEventAttributes, int bManualReset, int bInitialState,
            [MarshalAs(UnmanagedType.LPStr)] string lpName);

        [DllImport("kernel32.dll")]
        private static extern int CreateFile([MarshalAs(UnmanagedType.LPStr)] string lpFileName, int dwDesiredAccess,
            int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition,
            int dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("kernel32.dll")]
        private static extern bool EscapeCommFunction(int hFile, long ifunc);

        [DllImport("kernel32.dll")]
        private static extern int FormatMessage(int dwFlags, int lpSource, int dwMessageId, int dwLanguageId,
            [MarshalAs(UnmanagedType.LPStr)] string lpBuffer, int nSize,
            int Arguments);

        [DllImport("kernel32", EntryPoint = "FormatMessageA")]
        private static extern int FormatMessage(int dwFlags, int lpSource, int dwMessageId, int dwLanguageId,
            StringBuilder lpBuffer, int nSize, int Arguments);

        [DllImport("kernel32.dll")]
        public static extern bool GetCommModemStatus(int hFile, ref int lpModemStatus);

        [DllImport("kernel32.dll")]
        private static extern int GetCommState(int hCommDev, ref DCB lpDCB);

        [DllImport("kernel32.dll")]
        private static extern int GetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        [DllImport("kernel32.dll")]
        private static extern int GetLastError();

        [DllImport("kernel32.dll")]
        private static extern int GetOverlappedResult(int hFile, ref OVERLAPPED lpOverlapped,
            ref int lpNumberOfBytesTransferred, int bWait);

        [DllImport("kernel32.dll")]
        private static extern int PurgeComm(int hFile, int dwFlags);

        [DllImport("kernel32.dll")]
        private static extern int ReadFile(int hFile, byte[] Buffer, int nNumberOfBytesToRead,
            ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

        [DllImport("kernel32.dll")]
        private static extern int SetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        [DllImport("kernel32.dll")]
        private static extern int SetCommState(int hCommDev, ref DCB lpDCB);

        [DllImport("kernel32.dll")]
        private static extern int SetupComm(int hFile, int dwInQueue, int dwOutQueue);

        [DllImport("kernel32.dll")]
        private static extern int SetCommMask(int hFile, int lpEvtMask);

        [DllImport("kernel32.dll")]
        private static extern int WaitCommEvent(int hFile, ref EventMasks Mask, ref OVERLAPPED lpOverlap);

        [DllImport("kernel32.dll")]
        private static extern int WaitForSingleObject(int hHandle, int dwMilliseconds);

        [DllImport("kernel32.dll")]
        private static extern int WriteFile(int hFile, byte[] Buffer, int nNumberOfBytesToWrite,
            ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);


        public void mRead()
        {
            var iRet = Read(miTmpBytes2Read);
        }


        public void mWrite()
        {
            Write(mabtTmpTxBuf);
        }


        public void AsyncRead(int Bytes2Read)
        {
            if (WorkingMode != Mode.Overlapped)
                throw new ApplicationException("Async Methods allowed only when WorkingMode=Overlapped");
            miTmpBytes2Read = Bytes2Read;
            moThreadTx = new Thread(mRead);
            moThreadTx.Start();
        }


        public void AsyncWrite(byte[] Buffer)
        {
            if (WorkingMode != Mode.Overlapped)
                throw new ApplicationException("Async Methods allowed only when WorkingMode=Overlapped");
            if (mbWaitOnWrite)
                throw new ApplicationException("Unable to send message because of pending transmission.");
            mabtTmpTxBuf = Buffer;
            moThreadTx = new Thread(mWrite);
            moThreadTx.Start();
        }


        public void AsyncWrite(string Buffer)
        {
            var oEncoder = new ASCIIEncoding();
            var aByte = oEncoder.GetBytes(Buffer);
            AsyncWrite(aByte);
        }


        public bool CheckLineStatus(ModemStatusBits Line)
        {
            return Convert.ToBoolean(ModemStatus & Line);
        }


        public void ClearInputBuffer()
        {
            if (!(mhRS == -1)) PurgeComm(mhRS, PURGE_RXCLEAR);
        }


        public void Close()
        {
            if (mhRS != -1)
            {
                CloseHandle(mhRS);
                mhRS = -1;
            }
        }


        public void Open()
        {
            var uDcb = new DCB();
            var iRc = 0;
            object transTemp2 = FILE_FLAG_OVERLAPPED;
            object transTemp3 = 0;
            var iMode = Convert.ToInt32(WorkingMode == Mode.Overlapped ? transTemp2 : transTemp3);
            if (Port > 0)
                try
                {
                    mhRS = CreateFile("COM" + Port, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, iMode, 0);
                    if (mhRS != -1)
                    {
                        var lpErrCode = 0;
                        iRc = ClearCommError(mhRS, lpErrCode, Convert.ToInt32(0L));
                        iRc = PurgeComm(mhRS, Convert.ToInt32(PurgeBuffers.RXClear | PurgeBuffers.TxClear));
                        iRc = GetCommState(mhRS, ref uDcb);
                        var sParity = "NOEM";
                        sParity = sParity.Substring(Convert.ToInt32(Parity), 1);
                        var sDCBState = string.Format("baud={0} parity={1} data={2} stop={3}", BaudRate, sParity,
                            DataBit, Convert.ToInt32(StopBit));
                        iRc = BuildCommDCB(sDCBState, ref uDcb);
                        iRc = SetCommState(mhRS, ref uDcb);
                        if (iRc == 0)
                        {
                            var sErrTxt = pErr2Text(GetLastError());
                            throw new CIOChannelException("Unable to set COM state0" + sErrTxt);
                        }

                        iRc = SetupComm(mhRS, BufferSize, BufferSize);
                        pSetTimeout();
                    }
                    else
                    {
                        throw new CIOChannelException("Unable to open COM" + Port);
                    }
                }
                catch (Exception ex)
                {
                    throw new CIOChannelException(ex.Message, ex);
                }
            else
                throw new ApplicationException("COM Port not defined, " +
                                               "use Port property to set it before invoking InitPort");
        }


        public void Open(int Port, int BaudRate, int DataBit, DataParity Parity, DataStopBit StopBit, int BufferSize)
        {
            this.Port = Port;
            this.BaudRate = BaudRate;
            this.DataBit = DataBit;
            this.Parity = Parity;
            this.StopBit = StopBit;
            this.BufferSize = BufferSize;
            Open();
        }


        private string pErr2Text(int lCode)
        {
            var sRtrnCode = new StringBuilder(256);
            var lRet = 0;

            lRet = FormatMessage(0X1000, 0, lCode, 0, sRtrnCode, 256, 0);
            if (lRet > 0)
                return sRtrnCode.ToString();
            return "Error not found.";
        }


        private void pHandleOverlappedRead(int Bytes2Read)
        {
            int iReadChars = 0, iRc = 0, iRes = 0, iLastErr = 0;
            muOverlapped.hEvent = CreateEvent(0, 1, 0, null);
            if (muOverlapped.hEvent == 0) throw new ApplicationException("Error creating event for overlapped read.");

            if (mbWaitOnRead == false)
            {
                mabtRxBuf = new byte[Bytes2Read - 1];
                iRc = ReadFile(mhRS, mabtRxBuf, Bytes2Read, ref iReadChars, ref muOverlapped);
                if (iRc == 0)
                {
                    iLastErr = GetLastError();
                    if (iLastErr != ERROR_IO_PENDING)
                        throw new ArgumentException("Overlapped Read Error: " + pErr2Text(iLastErr));
                    mbWaitOnRead = true;
                }
                else
                {
                    if (null != DataReceived) DataReceived(this, mabtRxBuf);
                }
            }

            if (mbWaitOnRead)
            {
                iRes = WaitForSingleObject(muOverlapped.hEvent, miTimeout);
                switch (iRes)
                {
                    case WAIT_OBJECT_0:
                        if (GetOverlappedResult(mhRS, ref muOverlapped, ref iReadChars, 0) == 0)
                        {
                            iLastErr = GetLastError();
                            if (iLastErr == ERROR_IO_INCOMPLETE)
                                throw new ApplicationException("Read operation incomplete");
                            throw new ApplicationException("Read operation error " + iLastErr);
                        }
                        else
                        {
                            if (null != DataReceived) DataReceived(this, mabtRxBuf);
                            mbWaitOnRead = false;
                        }

                        break;
                    case WAIT_TIMEOUT:
                        throw new IOTimeoutException("Timeout error");
                    default:
                        throw new ApplicationException("Overlapped read error");
                }
            }
        }


        private bool pHandleOverlappedWrite(byte[] Buffer)
        {
            int iBytesWritten = 0, iRc = 0, iLastErr = 0, iRes = 0;
            var bErr = false;
            muOverlappedW.hEvent = CreateEvent(0, 1, 0, null);
            if (muOverlappedW.hEvent == 0) throw new ApplicationException("Error creating event for overlapped write.");

            PurgeComm(mhRS, PURGE_RXCLEAR | PURGE_TXCLEAR);
            mbWaitOnRead = true;
            iRc = WriteFile(mhRS, Buffer, Buffer.Length, ref iBytesWritten, ref muOverlappedW);
            if (iRc == 0)
            {
                iLastErr = GetLastError();
                if (iLastErr != ERROR_IO_PENDING)
                    throw new ArgumentException("Overlapped Read Error: " + pErr2Text(iLastErr));

                iRes = WaitForSingleObject(muOverlappedW.hEvent, INFINITE);
                switch (iRes)
                {
                    case WAIT_OBJECT_0:
                        if (GetOverlappedResult(mhRS, ref muOverlappedW, ref iBytesWritten, 0) == 0)
                        {
                            bErr = true;
                        }
                        else
                        {
                            mbWaitOnRead = false;
                            if (null != TxCompleted) TxCompleted(this);
                        }

                        break;
                }
            }
            else
            {
                bErr = false;
            }

            CloseHandle(muOverlappedW.hEvent);
            return bErr;
        }


        private void pSetTimeout()
        {
            var uCtm = new COMMTIMEOUTS();
            if (mhRS == -1)
            {
            }
            else
            {
                uCtm.ReadIntervalTimeout = 0;
                uCtm.ReadTotalTimeoutMultiplier = 0;
                uCtm.ReadTotalTimeoutConstant = miTimeout;
                uCtm.WriteTotalTimeoutMultiplier = 10;
                uCtm.WriteTotalTimeoutConstant = 100;

                SetCommTimeouts(mhRS, ref uCtm);
            }
        }


        public int Read(int Bytes2Read)
        {
            int iReadChars = 0, iRc = 0;

            if (Bytes2Read == 0) Bytes2Read = BufferSize;
            if (mhRS == -1)
                throw new ApplicationException("Please initialize and open port before using this method");
            try
            {
                if (WorkingMode == Mode.Overlapped)
                {
                    pHandleOverlappedRead(Bytes2Read);
                }
                else
                {
                    mabtRxBuf = new byte[Bytes2Read - 1];
                    var ov = new OVERLAPPED();
                    iRc = ReadFile(mhRS, mabtRxBuf, Bytes2Read, ref iReadChars, ref ov);
                    if (iRc == 0) throw new ApplicationException("ReadFile error " + iRc);

                    if (iReadChars < Bytes2Read) throw new IOTimeoutException("Timeout error");

                    mbWaitOnRead = true;
                    return iReadChars;
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil("Read Error: " + ex.Message, ex);
            }

            return 0;
        }


        public void Write(byte[] Buffer)
        {
            int iBytesWritten = 0, iRc = 0;

            if (mhRS == -1)
            {
                throw new ApplicationException("Please initialize and open port before using this method");
            }

            if (WorkingMode == Mode.Overlapped)
            {
                if (pHandleOverlappedWrite(Buffer)) throw new ApplicationException("Error in overllapped write");
            }
            else
            {
                PurgeComm(mhRS, PURGE_RXCLEAR | PURGE_TXCLEAR);
                var ov = new OVERLAPPED();
                iRc = WriteFile(mhRS, Buffer, Buffer.Length, ref iBytesWritten, ref ov);
                if (iRc == 0)
                    throw new ApplicationException("Write Error - Bytes Written " + iBytesWritten + " of " +
                                                   Buffer.Length);
            }
        }


        public void Write(string Buffer)
        {
            var oEncoder = new ASCIIEncoding();
            var aByte = oEncoder.GetBytes(Buffer);
            Write(aByte);
        }

        #region Nested type: CIOChannelException

        public class CIOChannelException : ApplicationException
        {
            public CIOChannelException(string Message) : base(Message)
            {
            }

            public CIOChannelException(string Message, Exception InnerException) : base(Message, InnerException)
            {
            }
        }

        #endregion

        #region Nested type: COMMCONFIG

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct COMMCONFIG
        {
            public readonly int dwSize;
            public readonly short wVersion;
            public readonly short wReserved;
            public readonly DCB dcbx;
            public readonly int dwProviderSubType;
            public readonly int dwProviderOffset;
            public readonly int dwProviderSize;
            public readonly byte wcProviderData;
        }

        #endregion

        #region Nested type: COMMTIMEOUTS

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        #endregion

        #region Nested type: DCB

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct DCB
        {
            public readonly int DCBlength;
            public readonly int BaudRate;
            public readonly int Bits1;
            public readonly short wReserved;
            public readonly short XonLim;
            public readonly short XoffLim;
            public readonly byte ByteSize;
            public readonly byte Parity;
            public readonly byte StopBits;
            public readonly byte XonChar;
            public readonly byte XoffChar;
            public readonly byte ErrorChar;
            public readonly byte EofChar;
            public readonly byte EvtChar;
            public readonly short wReserved2;
        }

        #endregion

        #region Nested type: IOTimeoutException

        public class IOTimeoutException : CIOChannelException
        {
            public IOTimeoutException(string Message) : base(Message)
            {
            }

            public IOTimeoutException(string Message, Exception InnerException) : base(Message, InnerException)
            {
            }
        }

        #endregion

        #region Nested type: Lines

        private enum Lines
        {
            SetRts = 3,
            ClearRts = 4,
            SetDtr = 5,
            ClearDtr = 6,
            ResetDev = 7,
            SetBreak = 8,
            ClearBreak = 9
        }

        #endregion

        #region Nested type: OVERLAPPED

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        #endregion

        #region Nested type: PurgeBuffers

        private enum PurgeBuffers
        {
            RXAbort = 0X2,
            RXClear = 0X8,
            TxAbort = 0X1,
            TxClear = 0X4
        }

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Label Label1;

        public DBComm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            Label1 = new Label();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.Dock = DockStyle.Fill;
            Label1.Location = new Point(0, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(152, 145);
            Label1.TabIndex = 0;
            Label1.Text = "DBComm";
            // 
            // DBComm
            // 
            Controls.Add(Label1);
            Name = "DBComm";
            Size = new Size(152, 145);
            ResumeLayout(false);
        }

        #endregion

        #region Delegates

        public delegate void CommEventEventHandler(DBComm Source, EventMasks Mask);

        public delegate void DataReceivedEventHandler(DBComm Source, byte[] DataBuffer);

        public delegate void TxCompletedEventHandler(DBComm Source);

        #endregion
    }
}