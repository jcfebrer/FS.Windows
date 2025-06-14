using System; 
using System.Runtime.InteropServices; 


using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace FSDirectShow
{
    [ ComVisible( false ) ]
    public class FilterCategory  
    { 
        public static readonly Guid AudioInputDevice = new Guid( 0X33D9A762, System.Convert.ToInt16( 0X90C8 ), System.Convert.ToInt16( 0X11D0 ), System.Convert.ToByte( 0XBD ), System.Convert.ToByte( 0X43 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XA0 ), System.Convert.ToByte( 0XC9 ), System.Convert.ToByte( 0X11 ), System.Convert.ToByte( 0XCE ), System.Convert.ToByte( 0X86 ) ); 
        
        public static readonly Guid VideoInputDevice = new Guid( "{860BB310-5D01-11D0-BD3B-00A0C911CE86}" ); 
    } 
    
    
    
    
    [ ComVisible( false ) ]
    public class Clsid  
    { 
        public static readonly Guid SystemDeviceEnum = new Guid( 0X62BE5D10, System.Convert.ToInt16( 0X60EB ), System.Convert.ToInt16( 0X11D0 ), System.Convert.ToByte( 0XBD ), System.Convert.ToByte( 0X3B ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XA0 ), System.Convert.ToByte( 0XC9 ), System.Convert.ToByte( 0X11 ), System.Convert.ToByte( 0XCE ), System.Convert.ToByte( 0X86 ) ); 
        
        public static readonly Guid FilterGraph = new Guid( unchecked ( ( ( int )( 0XE436EBB3 ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid CaptureGraphBuilder2 = new Guid( "BF87B6E1-8C27-11D0-B3F0-00AA003761C5" ); 
        
        public static readonly Guid SampleGrabber = new Guid( unchecked ( ( ( int )( 0XC1F400A0 ) ) ), System.Convert.ToInt16( 0X3F08 ), System.Convert.ToInt16( 0X11D3 ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X60 ), System.Convert.ToByte( 0X8 ), System.Convert.ToByte( 0X3 ), System.Convert.ToByte( 0X9E ), System.Convert.ToByte( 0X37 ) ); 
        
        public static readonly Guid DvdGraphBuilder = new Guid( "FCC152B7-F372-11D0-8E00-00C04FD7C08B" ); 
        
    } 
    
    
    
    
    [ ComVisible( false ) ]
    public class MediaType  
    { 
        public static readonly Guid Video = new Guid( 0X73646976, System.Convert.ToInt16( 0X0 ), System.Convert.ToInt16( 0X10 ), System.Convert.ToByte( 0X80 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X38 ), System.Convert.ToByte( 0X9B ), System.Convert.ToByte( 0X71 ) ); 
        
        public static readonly Guid Interleaved = new Guid( 0X73766169, System.Convert.ToInt16( 0X0 ), System.Convert.ToInt16( 0X10 ), System.Convert.ToByte( 0X80 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X38 ), System.Convert.ToByte( 0X9B ), System.Convert.ToByte( 0X71 ) ); 
        
        public static readonly Guid Audio = new Guid( 0X73647561, System.Convert.ToInt16( 0X0 ), System.Convert.ToInt16( 0X10 ), System.Convert.ToByte( 0X80 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X38 ), System.Convert.ToByte( 0X9B ), System.Convert.ToByte( 0X71 ) ); 
        
        public static readonly Guid Text = new Guid( 0X73747874, System.Convert.ToInt16( 0X0 ), System.Convert.ToInt16( 0X10 ), System.Convert.ToByte( 0X80 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X38 ), System.Convert.ToByte( 0X9B ), System.Convert.ToByte( 0X71 ) ); 
        
        public static readonly Guid Stream = new Guid( unchecked ( ( ( int )( 0XE436EB83 ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
    } 
    
    
    [ ComVisible( false ) ]
    public class MediaSubType  
    { 
        public static readonly Guid YUYV = new Guid( 0X56595559, System.Convert.ToInt16( 0X0 ), System.Convert.ToInt16( 0X10 ), System.Convert.ToByte( 0X80 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X38 ), System.Convert.ToByte( 0X9B ), System.Convert.ToByte( 0X71 ) ); 
        
        public static readonly Guid IYUV = new Guid( 0X56555949, System.Convert.ToInt16( 0X0 ), System.Convert.ToInt16( 0X10 ), System.Convert.ToByte( 0X80 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X38 ), System.Convert.ToByte( 0X9B ), System.Convert.ToByte( 0X71 ) ); 
        
        public static readonly Guid DVSD = new Guid( 0X44535644, System.Convert.ToInt16( 0X0 ), System.Convert.ToInt16( 0X10 ), System.Convert.ToByte( 0X80 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X38 ), System.Convert.ToByte( 0X9B ), System.Convert.ToByte( 0X71 ) ); 
        
        public static readonly Guid RGB1 = new Guid( unchecked ( ( ( int )( 0XE436EB78 ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid RGB4 = new Guid( unchecked ( ( ( int )( 0XE436EB79 ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid RGB8 = new Guid( unchecked ( ( ( int )( 0XE436EB7A ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid RGB565 = new Guid( unchecked ( ( ( int )( 0XE436EB7B ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid RGB555 = new Guid( unchecked ( ( ( int )( 0XE436EB7C ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid RGB24 = new Guid( unchecked ( ( ( int )( 0XE436EB7D ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid RGB32 = new Guid( unchecked ( ( ( int )( 0XE436EB7E ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        
        public static readonly Guid Avi = new Guid( unchecked ( ( ( int )( 0XE436EB88 ) ) ), System.Convert.ToInt16( 0X524F ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0X9F ), System.Convert.ToByte( 0X53 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X20 ), System.Convert.ToByte( 0XAF ), System.Convert.ToByte( 0XB ), System.Convert.ToByte( 0XA7 ), System.Convert.ToByte( 0X70 ) ); 
        
        public static readonly Guid Asf = new Guid( 0X3DB80F90, System.Convert.ToInt16( 0X9412 ), System.Convert.ToInt16( 0X11D1 ), System.Convert.ToByte( 0XAD ), System.Convert.ToByte( 0XED ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XF8 ), System.Convert.ToByte( 0X75 ), System.Convert.ToByte( 0X4B ), System.Convert.ToByte( 0X99 ) ); 
    } 
    
    
    
    [ ComVisible( false ) ]
    public class FormatType  
    { 
        public static readonly Guid None = new Guid( 0XF6417D6, System.Convert.ToInt16( 0XC318 ), System.Convert.ToInt16( 0X11D0 ), System.Convert.ToByte( 0XA4 ), System.Convert.ToByte( 0X3F ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XA0 ), System.Convert.ToByte( 0XC9 ), System.Convert.ToByte( 0X22 ), System.Convert.ToByte( 0X31 ), System.Convert.ToByte( 0X96 ) ); 
        
        public static readonly Guid VideoInfo = new Guid( 0X5589F80, System.Convert.ToInt16( 0XC356 ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0XBF ), System.Convert.ToByte( 0X1 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X55 ), System.Convert.ToByte( 0X59 ), System.Convert.ToByte( 0X5A ) ); 
        
        public static readonly Guid VideoInfo2 = new Guid( "F72A76A0-EB0A-11D0-ACE4-0000C0CC16BA" ); 
        
        public static readonly Guid WaveEx = new Guid( 0X5589F81, System.Convert.ToInt16( 0XC356 ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0XBF ), System.Convert.ToByte( 0X1 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X55 ), System.Convert.ToByte( 0X59 ), System.Convert.ToByte( 0X5A ) ); 
        
        public static readonly Guid MpegVideo = new Guid( 0X5589F82, System.Convert.ToInt16( 0XC356 ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0XBF ), System.Convert.ToByte( 0X1 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X55 ), System.Convert.ToByte( 0X59 ), System.Convert.ToByte( 0X5A ) ); 
        
        public static readonly Guid MpegStreams = new Guid( 0X5589F83, System.Convert.ToInt16( 0XC356 ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0XBF ), System.Convert.ToByte( 0X1 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X55 ), System.Convert.ToByte( 0X59 ), System.Convert.ToByte( 0X5A ) ); 
        
        public static readonly Guid DvInfo = new Guid( 0X5589F84, System.Convert.ToInt16( 0XC356 ), System.Convert.ToInt16( 0X11CE ), System.Convert.ToByte( 0XBF ), System.Convert.ToByte( 0X1 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XAA ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X55 ), System.Convert.ToByte( 0X59 ), System.Convert.ToByte( 0X5A ) ); 
    } 
    
    
    
    
    
    [ ComVisible( false ) ]
    public class PinCategory  
    { 
        public static readonly Guid Capture = new Guid( unchecked ( ( ( int )( 0XFB6C4281 ) ) ), System.Convert.ToInt16( 0X353 ), System.Convert.ToInt16( 0X11D1 ), System.Convert.ToByte( 0X90 ), System.Convert.ToByte( 0X5F ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XC0 ), System.Convert.ToByte( 0XCC ), System.Convert.ToByte( 0X16 ), System.Convert.ToByte( 0XBA ) ); 
        
        public static readonly Guid Preview = new Guid( unchecked ( ( ( int )( 0XFB6C4282 ) ) ), System.Convert.ToInt16( 0X353 ), System.Convert.ToInt16( 0X11D1 ), System.Convert.ToByte( 0X90 ), System.Convert.ToByte( 0X5F ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0X0 ), System.Convert.ToByte( 0XC0 ), System.Convert.ToByte( 0XCC ), System.Convert.ToByte( 0X16 ), System.Convert.ToByte( 0XBA ) ); 
    } 
    
} 
