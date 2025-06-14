using System; 
using System.Text; 
using System.Runtime.InteropServices; 


using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace FSDirectShow
{
    [ ComVisible( false ) ]
    public enum PinDirection 
    { 
        Input,
        Output,
    } 
    
    
    
    
    [ ComVisible( false ) ]
    public class DsHlp  
    { 
        public const int OATRUE = -1; 
        public const int OAFALSE = 0; 
        
        [ DllImport( "quartz.dll", CharSet=CharSet.Auto ) ]
        public static extern int AMGetErrorText( int hr, StringBuilder buf, int max );
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a86891-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IPin 
    { 
        [ PreserveSig() ]
        int Connect( [ In() ]IPin pReceivePin, [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int ReceiveConnection( [ In() ]IPin pReceivePin, [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int Disconnect();
        
        
        [ PreserveSig() ]
        int ConnectedTo( out IPin ppPin );
        
        
        [ PreserveSig() ]
        int ConnectionMediaType( [ MarshalAs( UnmanagedType.LPStruct ) ]out AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int QueryPinInfo( IntPtr pInfo );
        
        
        [ PreserveSig() ]
        int QueryDirection( PinDirection pPinDir );
        
        
        [ PreserveSig() ]
        int QueryId( [ MarshalAs( UnmanagedType.LPWStr ) ]out string Id );
        
        
        [ PreserveSig() ]
        int QueryAccept( [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int EnumMediaTypes( IntPtr ppEnum );
        
        
        [ PreserveSig() ]
        int QueryInternalConnections( IntPtr apPin, out int nPin );
        
        
        [ PreserveSig() ]
        int EndOfStream();
        
        
        [ PreserveSig() ]
        int BeginFlush();
        
        
        [ PreserveSig() ]
        int EndFlush();
        
        
        [ PreserveSig() ]
        int NewSegment( long tStart, long tStop, double dRate );
        
    } 
    
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a8689f-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IFilterGraph 
    { 
        [ PreserveSig() ]
        int AddFilter( [ In() ]IBaseFilter pFilter, [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pName );
        
        
        [ PreserveSig() ]
        int RemoveFilter( [ In() ]IBaseFilter pFilter );
        
        
        [ PreserveSig() ]
        int EnumFilters( out IEnumFilters ppEnum );
        
        
        [ PreserveSig() ]
        int FindFilterByName( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pName, out IBaseFilter ppFilter );
        
        
        [ PreserveSig() ]
        int ConnectDirect( [ In() ]IPin ppinOut, [ In() ]IPin ppinIn, [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int Reconnect( [ In() ]IPin ppin );
        
        
        [ PreserveSig() ]
        int Disconnect( [ In() ]IPin ppin );
        
        
        [ PreserveSig() ]
        int SetDefaultSyncSource();
        
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "0000010c-0000-0000-C000-000000000046" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IPersist 
    { 
        [ PreserveSig() ]
        int GetClassID( out Guid pClassID );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a86899-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IMediaFilter 
    { 
        #region '"IPersist Methods"' 
        [ PreserveSig() ]
        int GetClassID( out Guid pClassID );
        
        #endregion 
        
        [ PreserveSig() ]
        int Stop();
        
        
        [ PreserveSig() ]
        int Pause();
        
        
        [ PreserveSig() ]
        int Run( long tStart );
        
        
        [ PreserveSig() ]
        int GetState( int dwMilliSecsTimeout, int filtState );
        
        
        [ PreserveSig() ]
        int SetSyncSource( [ In() ]IReferenceClock pClock );
        
        
        [ PreserveSig() ]
        int GetSyncSource( out IReferenceClock pClock );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a86895-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IBaseFilter 
    { 
        #region '"IPersist Methods"' 
        [ PreserveSig() ]
        int GetClassID( out Guid pClassID );
        
        #endregion 
        
        #region '"IMediaFilter Methods"' 
        [ PreserveSig() ]
        int Stop();
        
        
        [ PreserveSig() ]
        int Pause();
        
        
        [ PreserveSig() ]
        int Run( long tStart );
        
        
        [ PreserveSig() ]
        int GetState( int dwMilliSecsTimeout, out int filtState );
        
        
        [ PreserveSig() ]
        int SetSyncSource( [ In() ]IReferenceClock pClock );
        
        
        [ PreserveSig() ]
        int GetSyncSource( out IReferenceClock pClock );
        
        #endregion 
        
        [ PreserveSig() ]
        int EnumPins( out IEnumPins ppEnum );
        
        
        [ PreserveSig() ]
        int FindPin( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string Id, out IPin ppPin );
        
        
        [ PreserveSig() ]
        int QueryFilterInfo( out FilterInfo pInfo );
        
        
        [ PreserveSig() ]
        int JoinFilterGraph( [ In() ]IFilterGraph pGraph, [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pName );
        
        
        [ PreserveSig() ]
        int QueryVendorInfo( [ MarshalAs( UnmanagedType.LPWStr ) ]out string pVendorInfo );
        
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Unicode ), ComVisible( false ) ]
    public class FilterInfo  
    { 
        [ MarshalAs( UnmanagedType.ByValTStr, SizeConst=128 ) ]public string achName; 
        [ MarshalAs( UnmanagedType.IUnknown ) ]public object pUnk; 
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "36b73880-c2c8-11cf-8b46-00805f6cef60" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IMediaSeeking 
    { 
        [ PreserveSig() ]
        int GetCapabilities( SeekingCapabilities pCapabilities );
        
        
        [ PreserveSig() ]
        int CheckCapabilities( out SeekingCapabilities pCapabilities );
        
        
        [ PreserveSig() ]
        int IsFormatSupported( [ In() ]Guid pFormat );
        
        [ PreserveSig() ]
        int QueryPreferredFormat( out Guid pFormat );
        
        
        [ PreserveSig() ]
        int GetTimeFormat( out Guid pFormat );
        
        [ PreserveSig() ]
        int IsUsingTimeFormat( [ In() ]Guid pFormat );
        
        [ PreserveSig() ]
        int SetTimeFormat( [ In() ]Guid pFormat );
        
        
        [ PreserveSig() ]
        int GetDuration( long pDuration );
        
        [ PreserveSig() ]
        int GetStopPosition( long pStop );
        
        [ PreserveSig() ]
        int GetCurrentPosition( long pCurrent );
        
        
        [ PreserveSig() ]
        int ConvertTimeFormat( long pTarget, [ In() ]Guid pTargetFormat, long Source, [ In() ]Guid pSourceFormat );
        
        
        [ PreserveSig() ]
        int SetPositions( [ MarshalAs( UnmanagedType.LPStruct ) ]out DsOptInt64 pCurrent, SeekingFlags dwCurrentFlags, [ MarshalAs( UnmanagedType.LPStruct ) ]out DsOptInt64 pStop, SeekingFlags dwStopFlags );
        
        
        [ PreserveSig() ]
        int GetPositions( long pCurrent, long pStop );
        
        
        [ PreserveSig() ]
        int GetAvailable( long pEarliest, long pLatest );
        
        
        [ PreserveSig() ]
        int SetRate( double dRate );
        
        [ PreserveSig() ]
        int GetRate( double pdRate );
        
        
        [ PreserveSig() ]
        int GetPreroll( long pllPreroll );
        
    } 
    
    
    
    [ Flags(), ComVisible( false ) ]
    public enum SeekingCapabilities 
    { 
        CanSeekAbsolute = 0X1,
        CanSeekForwards = 0X2,
        CanSeekBackwards = 0X4,
        CanGetCurrentPos = 0X8,
        CanGetStopPos = 0X10,
        CanGetDuration = 0X20,
        CanPlayBackwards = 0X40,
        CanDoSegments = 0X80,
        Source = 0X100,
    } 
    
    
    
    [ Flags(), ComVisible( false ) ]
    public enum SeekingFlags 
    { 
        NoPositioning = 0X0,
        AbsolutePositioning = 0X1,
        RelativePositioning = 0X2,
        IncrementalPositioning = 0X3,
        PositioningBitsMask = 0X3,
        SeekToKeyFrame = 0X4,
        ReturnTime = 0X8,
        Segment = 0X10,
        NoFlush = 0X20,
    } 
    
    
    
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a86897-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IReferenceClock 
    { 
        [ PreserveSig() ]
        int GetTime( long pTime );
        
        
        [ PreserveSig() ]
        int AdviseTime( long baseTime, long streamTime, IntPtr hEvent, int pdwAdviseCookie );
        
        
        [ PreserveSig() ]
        int AdvisePeriodic( long startTime, long periodTime, IntPtr hSemaphore, int pdwAdviseCookie );
        
        
        [ PreserveSig() ]
        int Unadvise( int dwAdviseCookie );
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a86893-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IEnumFilters 
    { 
        [ PreserveSig() ]
        int Next( [ In() ]int cFilters, [ MarshalAs( UnmanagedType.LPArray, SizeParamIndex=2 ) ]out IBaseFilter[] ppFilter, out int pcFetched );
        
        
        [ PreserveSig() ]
        int Skip( [ In() ]int cFilters );
        
        void Reset();
        
        void Clone( out IEnumFilters ppEnum );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a86892-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IEnumPins 
    { 
        [ PreserveSig() ]
        int Next( [ In() ]int cPins, [ MarshalAs( UnmanagedType.LPArray, SizeParamIndex=2 ) ]out IPin[] ppPins, out int pcFetched );
        
        
        [ PreserveSig() ]
        int Skip( [ In() ]int cPins );
        
        void Reset();
        
        void Clone( out IEnumPins ppEnum );
        
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential ), ComVisible( false ) ]
    public class AMMediaType  
    { 
        public Guid majorType; 
        public Guid subType; 
        [ MarshalAs( UnmanagedType.Bool ) ]public bool fixedSizeSamples; 
        [ MarshalAs( UnmanagedType.Bool ) ]public bool temporalCompression; 
        public int sampleSize; 
        public Guid formatType; 
        public IntPtr unkPtr; 
        public int formatSize; 
        public IntPtr formatPtr; 
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a8689a-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IMediaSample 
    { 
        [ PreserveSig() ]
        int GetPointer( IntPtr ppBuffer );
        
        [ PreserveSig() ]
        int GetSize();
        
        
        [ PreserveSig() ]
        int GetTime( long pTimeStart, long pTimeEnd );
        
        
        [ PreserveSig() ]
        int SetTime( [ In(), MarshalAs( UnmanagedType.LPStruct ) ]DsOptInt64 pTimeStart, [ In(), MarshalAs( UnmanagedType.LPStruct ) ]DsOptInt64 pTimeEnd );
        
        
        [ PreserveSig() ]
        int IsSyncPoint();
        
        [ PreserveSig() ]
        int SetSyncPoint( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool bIsSyncPoint );
        
        
        [ PreserveSig() ]
        int IsPreroll();
        
        [ PreserveSig() ]
        int SetPreroll( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool bIsPreroll );
        
        
        [ PreserveSig() ]
        int GetActualDataLength();
        
        [ PreserveSig() ]
        int SetActualDataLength( int len );
        
        
        [ PreserveSig() ]
        int GetMediaType( [ MarshalAs( UnmanagedType.LPStruct ) ]out AMMediaType ppMediaType );
        
        
        [ PreserveSig() ]
        int SetMediaType( [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pMediaType );
        
        
        [ PreserveSig() ]
        int IsDiscontinuity();
        
        [ PreserveSig() ]
        int SetDiscontinuity( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool bDiscontinuity );
        
        
        [ PreserveSig() ]
        int GetMediaTime( long pTimeStart, long pTimeEnd );
        
        
        [ PreserveSig() ]
        int SetMediaTime( [ In(), MarshalAs( UnmanagedType.LPStruct ) ]DsOptInt64 pTimeStart, [ In(), MarshalAs( UnmanagedType.LPStruct ) ]DsOptInt64 pTimeEnd );
        
    } 
    
} 
