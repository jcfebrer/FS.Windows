using System; 
using System.Runtime.InteropServices; 


using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace FSDirectShow
{
    [ ComVisible( true ), ComImport(), Guid( "56a868b1-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IMediaControl 
    { 
        [ PreserveSig() ]
        int Run();
        
        
        [ PreserveSig() ]
        int Pause();
        
        
        [ PreserveSig() ]
        int Stop();
        
        
        [ PreserveSig() ]
        int GetState( int msTimeout, int pfs );
        
        
        [ PreserveSig() ]
        int RenderFile( string strFilename );
        
        
        [ PreserveSig() ]
        int AddSourceFilter( [ In() ]string strFilename, [ MarshalAs( UnmanagedType.IDispatch ) ]out object ppUnk );
        
        
        [ PreserveSig() ]
        int get_FilterCollection( [ MarshalAs( UnmanagedType.IDispatch ) ]out object ppUnk );
        
        
        [ PreserveSig() ]
        int get_RegFilterCollection( [ MarshalAs( UnmanagedType.IDispatch ) ]out object ppUnk );
        
        
        [ PreserveSig() ]
        int StopWhenReady();
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a868b6-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IMediaEvent 
    { 
        [ PreserveSig() ]
        int GetEventHandle( IntPtr hEvent );
        
        
        [ PreserveSig() ]
        int GetEvent( DsEvCode lEventCode, int lParam1, int lParam2, int msTimeout );
        
        
        [ PreserveSig() ]
        int WaitForCompletion( int msTimeout, int pEvCode );
        
        
        [ PreserveSig() ]
        int CancelDefaultHandling( int lEvCode );
        
        
        [ PreserveSig() ]
        int RestoreDefaultHandling( int lEvCode );
        
        
        [ PreserveSig() ]
        int FreeEventParams( DsEvCode lEvCode, int lParam1, int lParam2 );
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a868c0-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IMediaEventEx 
    { 
        #region '"IMediaEvent Methods"' 
        [ PreserveSig() ]
        int GetEventHandle( IntPtr hEvent );
        
        
        [ PreserveSig() ]
        int GetEvent( DsEvCode lEventCode, int lParam1, int lParam2, int msTimeout );
        
        
        [ PreserveSig() ]
        int WaitForCompletion( int msTimeout, out int pEvCode );
        
        
        [ PreserveSig() ]
        int CancelDefaultHandling( int lEvCode );
        
        
        [ PreserveSig() ]
        int RestoreDefaultHandling( int lEvCode );
        
        
        [ PreserveSig() ]
        int FreeEventParams( DsEvCode lEvCode, int lParam1, int lParam2 );
        
        #endregion 
        
        [ PreserveSig() ]
        int SetNotifyWindow( IntPtr hwnd, int lMsg, IntPtr lInstanceData );
        
        
        [ PreserveSig() ]
        int SetNotifyFlags( int lNoNotifyFlags );
        
        
        [ PreserveSig() ]
        int GetNotifyFlags( int lplNoNotifyFlags );
        
    } 
    
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "329bb360-f6ea-11d1-9038-00a0c9697298" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IBasicVideo2 
    { 
        [ PreserveSig() ]
        int AvgTimePerFrame( double pAvgTimePerFrame );
        
        
        [ PreserveSig() ]
        int BitRate( int pBitRate );
        
        
        [ PreserveSig() ]
        int BitErrorRate( int pBitRate );
        
        
        [ PreserveSig() ]
        int VideoWidth( int pVideoWidth );
        
        
        [ PreserveSig() ]
        int VideoHeight( int pVideoHeight );
        
        
        
        [ PreserveSig() ]
        int put_SourceLeft( int SourceLeft );
        
        [ PreserveSig() ]
        int get_SourceLeft( int pSourceLeft );
        
        
        [ PreserveSig() ]
        int put_SourceWidth( int SourceWidth );
        
        [ PreserveSig() ]
        int get_SourceWidth( int pSourceWidth );
        
        
        [ PreserveSig() ]
        int put_SourceTop( int SourceTop );
        
        [ PreserveSig() ]
        int get_SourceTop( int pSourceTop );
        
        
        [ PreserveSig() ]
        int put_SourceHeight( int SourceHeight );
        
        [ PreserveSig() ]
        int get_SourceHeight( int pSourceHeight );
        
        
        
        
        [ PreserveSig() ]
        int put_DestinationLeft( int DestinationLeft );
        
        [ PreserveSig() ]
        int get_DestinationLeft( int pDestinationLeft );
        
        
        [ PreserveSig() ]
        int put_DestinationWidth( int DestinationWidth );
        
        [ PreserveSig() ]
        int get_DestinationWidth( int pDestinationWidth );
        
        
        [ PreserveSig() ]
        int put_DestinationTop( int DestinationTop );
        
        [ PreserveSig() ]
        int get_DestinationTop( int pDestinationTop );
        
        
        [ PreserveSig() ]
        int put_DestinationHeight( int DestinationHeight );
        
        [ PreserveSig() ]
        int get_DestinationHeight( int pDestinationHeight );
        
        
        [ PreserveSig() ]
        int SetSourcePosition( int left, int top, int width, int height );
        
        [ PreserveSig() ]
        int GetSourcePosition( int left, int top, int width, int height );
        
        [ PreserveSig() ]
        int SetDefaultSourcePosition();
        
        
        
        [ PreserveSig() ]
        int SetDestinationPosition( int left, int top, int width, int height );
        
        [ PreserveSig() ]
        int GetDestinationPosition( int left, int top, int width, int height );
        
        [ PreserveSig() ]
        int SetDefaultDestinationPosition();
        
        
        
        [ PreserveSig() ]
        int GetVideoSize( int pWidth, int pHeight );
        
        
        [ PreserveSig() ]
        int GetVideoPaletteEntries( int StartIndex, int Entries, int pRetrieved, IntPtr pPalette );
        
        
        [ PreserveSig() ]
        int GetCurrentImage( int pBufferSize, IntPtr pDIBImage );
        
        
        [ PreserveSig() ]
        int IsUsingDefaultSource();
        
        [ PreserveSig() ]
        int IsUsingDefaultDestination();
        
        
        [ PreserveSig() ]
        int GetPreferredAspectRatio( int plAspectX, int plAspectY );
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a868b4-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IVideoWindow 
    { 
        [ PreserveSig() ]
        int put_Caption( string caption );
        
        [ PreserveSig() ]
        int get_Caption( out string caption );
        
        
        [ PreserveSig() ]
        int put_WindowStyle( int windowStyle );
        
        [ PreserveSig() ]
        int get_WindowStyle( int windowStyle );
        
        
        [ PreserveSig() ]
        int put_WindowStyleEx( int windowStyleEx );
        
        [ PreserveSig() ]
        int get_WindowStyleEx( int windowStyleEx );
        
        
        [ PreserveSig() ]
        int put_AutoShow( int autoShow );
        
        [ PreserveSig() ]
        int get_AutoShow( int autoShow );
        
        
        [ PreserveSig() ]
        int put_WindowState( int windowState );
        
        [ PreserveSig() ]
        int get_WindowState( int windowState );
        
        
        [ PreserveSig() ]
        int put_BackgroundPalette( int backgroundPalette );
        
        [ PreserveSig() ]
        int get_BackgroundPalette( int backgroundPalette );
        
        
        [ PreserveSig() ]
        int put_Visible( int visible );
        
        [ PreserveSig() ]
        int get_Visible( int visible );
        
        
        [ PreserveSig() ]
        int put_Left( int left );
        
        [ PreserveSig() ]
        int get_Left( int left );
        
        
        [ PreserveSig() ]
        int put_Width( int width );
        
        [ PreserveSig() ]
        int get_Width( int width );
        
        
        [ PreserveSig() ]
        int put_Top( int top );
        
        [ PreserveSig() ]
        int get_Top( int top );
        
        
        [ PreserveSig() ]
        int put_Height( int height );
        
        [ PreserveSig() ]
        int get_Height( int height );
        
        
        [ PreserveSig() ]
        int put_Owner( IntPtr owner );
        
        [ PreserveSig() ]
        int get_Owner( IntPtr owner );
        
        
        [ PreserveSig() ]
        int put_MessageDrain( IntPtr drain );
        
        [ PreserveSig() ]
        int get_MessageDrain( IntPtr drain );
        
        
        [ PreserveSig() ]
        int get_BorderColor( int color );
        
        [ PreserveSig() ]
        int put_BorderColor( int color );
        
        
        [ PreserveSig() ]
        int get_FullScreenMode( int fullScreenMode );
        
        [ PreserveSig() ]
        int put_FullScreenMode( int fullScreenMode );
        
        
        [ PreserveSig() ]
        int SetWindowForeground( int focus );
        
        
        [ PreserveSig() ]
        int NotifyOwnerMessage( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam );
        
        
        [ PreserveSig() ]
        int SetWindowPosition( int left, int top, int width, int height );
        
        
        [ PreserveSig() ]
        int GetWindowPosition( int left, int top, int width, int height );
        
        
        [ PreserveSig() ]
        int GetMinIdealImageSize( int width, int height );
        
        
        [ PreserveSig() ]
        int GetMaxIdealImageSize( int width, int height );
        
        
        [ PreserveSig() ]
        int GetRestorePosition( int left, int top, int width, int height );
        
        
        [ PreserveSig() ]
        int HideCursor( int hide_Cursor );
        
        
        [ PreserveSig() ]
        int IsCursorHidden( int hideCursor );
        
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport, Guid( "56a868b2-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IMediaPosition 
    { 
        [ PreserveSig() ]
        int get_Duration( double pLength );
        
        
        [ PreserveSig() ]
        int put_CurrentPosition( double llTime );
        
        [ PreserveSig() ]
        int get_CurrentPosition( double pllTime );
        
        
        [ PreserveSig() ]
        int get_StopTime( double pllTime );
        
        [ PreserveSig() ]
        int put_StopTime( double llTime );
        
        
        [ PreserveSig() ]
        int get_PrerollTime( double pllTime );
        
        [ PreserveSig() ]
        int put_PrerollTime( double llTime );
        
        
        [ PreserveSig() ]
        int put_Rate( double dRate );
        
        [ PreserveSig() ]
        int get_Rate( double pdRate );
        
        
        [ PreserveSig() ]
        int CanSeekForward( int pCanSeekForward );
        
        [ PreserveSig() ]
        int CanSeekBackward( int pCanSeekBackward );
        
    } 
    
    
    
    [ ComVisible( true ), ComImport, Guid( "56a868b3-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IBasicAudio 
    { 
        [ PreserveSig() ]
        int put_Volume( int lVolume );
        
        [ PreserveSig() ]
        int get_Volume( int plVolume );
        
        
        [ PreserveSig() ]
        int put_Balance( int lBalance );
        
        [ PreserveSig() ]
        int get_Balance( int plBalance );
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a868b9-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsDual ) ]
    public interface IAMCollection 
    { 
        [ PreserveSig() ]
        int get_Count( int plCount );
        
        
        [ PreserveSig() ]
        int Item( int lItem, [ MarshalAs( UnmanagedType.IUnknown ) ]out object ppUnk );
        
        
        [ PreserveSig() ]
        int get_NewEnum( [ MarshalAs( UnmanagedType.IUnknown ) ]out object ppUnk );
        
    } 
    
    
    
    public enum DsEvCode 
    { 
        None = 0X0,
        Complete = 0X1,
        UserAbort = 0X2,
        ErrorAbort = 0X3,
        Time = 0X4,
        Repaint = 0X5,
        StErrStopped = 0X6,
        StErrStPlaying = 0X7,
        ErrorStPlaying = 0X8,
        PaletteChanged = 0X9,
        VideoSizeChanged = 0XA,
        QualityChange = 0XB,
        ShuttingDown = 0XC,
        ClockChanged = 0XD,
        Paused = 0XE,
        OpeningFile = 0X10,
        BufferingData = 0X11,
        FullScreenLost = 0X12,
        Activate = 0X13,
        NeedRestart = 0X14,
        WindowDestroyed = 0X15,
        DisplayChanged = 0X16,
        Starvation = 0X17,
        OleEvent = 0X18,
        NotifyWindow = 0X19,
        
        DvdDomChange = 0X101,
        DvdTitleChange = 0X102,
        DvdChaptStart = 0X103,
        DvdAudioStChange = 0X104,
        
        DvdSubPicStChange = 0X105,
        DvdAngleChange = 0X106,
        DvdButtonChange = 0X107,
        DvdValidUopsChange = 0X108,
        DvdStillOn = 0X109,
        DvdStillOff = 0X10A,
        DvdCurrentTime = 0X10B,
        DvdError = 0X10C,
        DvdWarning = 0X10D,
        DvdChaptAutoStop = 0X10E,
        DvdNoFpPgc = 0X10F,
        DvdPlaybRateChange = 0X110,
        DvdParentalLChange = 0X111,
        DvdPlaybStopped = 0X112,
        DvdAnglesAvail = 0X113,
        DvdPeriodAStop = 0X114,
        DvdButtonAActivated = 0X115,
        DvdCmdStart = 0X116,
        DvdCmdEnd = 0X117,
        DvdDiscEjected = 0X118,
        DvdDiscInserted = 0X119,
        DvdCurrentHmsfTime = 0X11A,
        DvdKaraokeMode = 0X11B,
    } 
    
    
} 
