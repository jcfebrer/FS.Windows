using System; 
using System.Runtime.InteropServices; 


using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace FSDirectShow
{
    [ ComVisible( true ), ComImport(), Guid( "93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface ICaptureGraphBuilder2 
    { 
        [ PreserveSig() ]
        int SetFiltergraph( [ In() ]IGraphBuilder pfg );
        
        
        [ PreserveSig() ]
        int GetFiltergraph( out IGraphBuilder ppfg );
        
        
        [ PreserveSig() ]
        int SetOutputFileName( [ In() ]Guid pType, [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpstrFile, out IBaseFilter ppbf, out IFileSinkFilter ppSink );
        
        
        [ PreserveSig() ]
        int FindInterface( [ In() ]Guid pCategory, [ In() ]Guid pType, [ In() ]IBaseFilter pbf, [ In() ]Guid riid, [ MarshalAs( UnmanagedType.IUnknown ) ]out object ppint );
        
        
        [ PreserveSig() ]
        int RenderStream( [ In() ]Guid pCategory, [ In() ]Guid pType, [ In(), MarshalAs( UnmanagedType.IUnknown ) ]object pSource, [ In() ]IBaseFilter pfCompressor, [ In() ]IBaseFilter pfRenderer );
        
        
        [ PreserveSig() ]
        int ControlStream( [ In() ]Guid pCategory, [ In() ]Guid pType, [ In() ]IBaseFilter pFilter, [ In() ]IntPtr pstart, [ In() ]IntPtr pstop, [ In() ]short wStartCookie, [ In() ]short wStopCookie );
        
        
        [ PreserveSig() ]
        int AllocCapFile( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpstrFile, [ In() ]long dwlSize );
        
        
        [ PreserveSig() ]
        int CopyCaptureFile( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpwstrOld, [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpwstrNew, [ In() ]int fAllowEscAbort, [ In() ]IAMCopyCaptureFileProgress pFilter );
        
        
        [ PreserveSig() ]
        int FindPin( [ In() ]object pSource, [ In() ]int pindir, [ In() ]Guid pCategory, [ In() ]Guid pType, [ In(), MarshalAs( UnmanagedType.Bool ) ]bool fUnconnected, [ In() ]int num, out IPin ppPin );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "56a868a9-0ad4-11ce-b03a-0020af0ba770" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IGraphBuilder 
    { 
        #region '"IFilterGraph Methods"' 
        [ PreserveSig() ]
        int AddFilter( [ In() ]IBaseFilter pFilter, [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pName );
        
        
        [ PreserveSig() ]
        int RemoveFilter( [ In() ]IBaseFilter pFilter );
        
        
        [ PreserveSig() ]
        int EnumFilters( out IEnumFilters ppEnum );
        
        
        [ PreserveSig() ]
        int FindFilterByName( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pName, out IBaseFilter ppFilter );
        
        
        [ PreserveSig() ]
        int ConnectDirect( [ In() ]IPin ipinOut, [ In() ]IPin ipinIn, [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int Reconnect( [ In() ]IPin ppin );
        
        
        [ PreserveSig() ]
        int Disconnect( [ In() ]IPin ppin );
        
        
        [ PreserveSig() ]
        int SetDefaultSyncSource();
        
        #endregion 
        
        [ PreserveSig() ]
        int Connect( [ In() ]IPin ipinOut, [ In() ]IPin ipinIn );
        
        
        [ PreserveSig() ]
        int Render( [ In() ]IPin ipinOut );
        
        
        [ PreserveSig() ]
        int RenderFile( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpcwstrFile, [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpcwstrPlayList );
        
        
        [ PreserveSig() ]
        int AddSourceFilter( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpcwstrFileName, [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpcwstrFilterName, out IBaseFilter ppFilter );
        
        
        [ PreserveSig() ]
        int SetLogFile( IntPtr hFile );
        
        
        [ PreserveSig() ]
        int Abort();
        
        
        [ PreserveSig() ]
        int ShouldOperationContinue();
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "a2104830-7c70-11cf-8bce-00aa00a3f1a6" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IFileSinkFilter 
    { 
        [ PreserveSig() ]
        int SetFileName( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pszFileName, [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int GetCurFile( [ MarshalAs( UnmanagedType.LPWStr ) ]out string pszFileName, [ MarshalAs( UnmanagedType.LPStruct ) ]out AMMediaType pmt );
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "670d1d20-a068-11d0-b3f0-00aa003761c5" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IAMCopyCaptureFileProgress 
    { 
        [ PreserveSig() ]
        int Progress( int iProgress );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "e46a9787-2b71-444d-a4b5-1fab7b708d6a" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IVideoFrameStep 
    { 
        [ PreserveSig() ]
        int Step( int dwFrames, [ In(), MarshalAs( UnmanagedType.IUnknown ) ]object pStepObject );
        
        
        [ PreserveSig() ]
        int CanStep( int bMultiple, [ In(), MarshalAs( UnmanagedType.IUnknown ) ]object pStepObject );
        
        
        [ PreserveSig() ]
        int CancelStep();
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "C6E13340-30AC-11d0-A18C-00A0C9118956" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IAMStreamConfig 
    { 
        [ PreserveSig() ]
        int SetFormat( [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int GetFormat( [ MarshalAs( UnmanagedType.LPStruct ) ]out AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int GetNumberOfCapabilities( int piCount, int piSize );
        
        
        [ PreserveSig() ]
        int GetStreamCaps( int iIndex, [ MarshalAs( UnmanagedType.LPStruct ) ]out AMMediaType ppmt, IntPtr pSCC );
        
    } 
    
    
    
    
    [ ComVisible( false ) ]
    public enum AMTunerSubChannel 
    { 
        NoTune = -2,
        Default = -1,
    } 
    
    
    [ ComVisible( false ) ]
    public enum AMTunerSignalStrength 
    { 
        NA = -1,
        NoSignal = 0,
        SignalPresent = 1,
    } 
    
    
    [ Flags(), ComVisible( false ) ]
    public enum AMTunerModeType 
    { 
        Default = 0X0,
        TV = 0X1,
        FMRadio = 0X2,
        AMRadio = 0X4,
        Dss = 0X8,
    } 
    
    
    [ ComVisible( false ) ]
    public enum AMTunerEventType 
    { 
        Changed = 0X1,
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "211A8761-03AC-11d1-8D13-00AA00BD8339" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IAMTuner 
    { 
        [ PreserveSig() ]
        int put_Channel( int lChannel, AMTunerSubChannel lVideoSubChannel, AMTunerSubChannel lAudioSubChannel );
        
        
        [ PreserveSig() ]
        int get_Channel( int plChannel, int plVideoSubChannel, int plAudioSubChannel );
        
        
        [ PreserveSig() ]
        int ChannelMinMax( int lChannelMin, int lChannelMax );
        
        
        [ PreserveSig() ]
        int put_CounTryCode( int lCounTryCode );
        
        
        [ PreserveSig() ]
        int get_CounTryCode( int plCounTryCode );
        
        
        [ PreserveSig() ]
        int get_TuningSpace( int plTuningSpace );
        
        
        [ PreserveSig() ]
        int Logon( IntPtr hCurrentUser );
        
        
        [ PreserveSig() ]
        int Logout();
        
        
        [ PreserveSig() ]
        int SignalPresent( AMTunerSignalStrength plSignalStrength );
        
        
        [ PreserveSig() ]
        int put_Mode( AMTunerModeType lMode );
        
        
        [ PreserveSig() ]
        int get_Mode( AMTunerModeType plMode );
        
        
        [ PreserveSig() ]
        int GetAvailableModes( AMTunerModeType plModes );
        
        
        [ PreserveSig() ]
        int RegisterNotificationCallBack( IAMTunerNotification pNotify, AMTunerEventType lEvents );
        
        
        [ PreserveSig() ]
        int UnRegisterNotificationCallBack( IAMTunerNotification pNotify );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "211A8760-03AC-11d1-8D13-00AA00BD8339" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IAMTunerNotification 
    { 
        [ PreserveSig() ]
        int OnEvent( AMTunerEventType Event );
        
    } 
    
    
    
    
    [ Flags(), ComVisible( false ) ]
    public enum AnalogVideoStandard 
    { 
        None = 0X0,
        NTSC_M = 0X1,
        NTSC_M_J = 0X2,
        NTSC_433 = 0X4,
        PAL_B = 0X10,
        PAL_D = 0X20,
        PAL_G = 0X40,
        PAL_H = 0X80,
        PAL_I = 0X100,
        PAL_M = 0X200,
        PAL_N = 0X400,
        PAL_60 = 0X800,
        SECAM_B = 0X1000,
        SECAM_D = 0X2000,
        SECAM_G = 0X4000,
        SECAM_H = 0X8000,
        SECAM_K = 0X10000,
        SECAM_K1 = 0X20000,
        SECAM_L = 0X40000,
        SECAM_L1 = 0X80000,
        PAL_N_COMBO = 0X100000,
    } 
    
    
    [ ComVisible( false ) ]
    public enum TunerInputType 
    { 
        Cable,
        Antenna,
    } 
    
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "211A8766-03AC-11d1-8D13-00AA00BD8339" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IAMTVTuner 
    { 
        
        #region '"IAMTuner Methods"' 
        [ PreserveSig() ]
        int put_Channel( int lChannel, AMTunerSubChannel lVideoSubChannel, AMTunerSubChannel lAudioSubChannel );
        
        
        [ PreserveSig() ]
        int get_Channel( int plChannel, int plVideoSubChannel, int plAudioSubChannel );
        
        
        [ PreserveSig() ]
        int ChannelMinMax( int lChannelMin, int lChannelMax );
        
        
        [ PreserveSig() ]
        int put_CounTryCode( int lCounTryCode );
        
        
        [ PreserveSig() ]
        int get_CounTryCode( int plCounTryCode );
        
        
        [ PreserveSig() ]
        int put_TuningSpace( int lTuningSpace );
        
        
        [ PreserveSig() ]
        int get_TuningSpace( int plTuningSpace );
        
        
        [ PreserveSig() ]
        int Logon( IntPtr hCurrentUser );
        
        
        [ PreserveSig() ]
        int Logout();
        
        
        [ PreserveSig() ]
        int SignalPresent( AMTunerSignalStrength plSignalStrength );
        
        
        [ PreserveSig() ]
        int put_Mode( AMTunerModeType lMode );
        
        
        [ PreserveSig() ]
        int get_Mode( AMTunerModeType plMode );
        
        
        [ PreserveSig() ]
        int GetAvailableModes( AMTunerModeType plModes );
        
        
        [ PreserveSig() ]
        int RegisterNotificationCallBack( IAMTunerNotification pNotify, AMTunerEventType lEvents );
        
        
        [ PreserveSig() ]
        int UnRegisterNotificationCallBack( IAMTunerNotification pNotify );
        
        #endregion 
        
        [ PreserveSig() ]
        int get_AvailableTVFormats( AnalogVideoStandard lAnalogVideoStandard );
        
        
        [ PreserveSig() ]
        int get_TVFormat( AnalogVideoStandard lAnalogVideoStandard );
        
        
        [ PreserveSig() ]
        int AutoTune( int lChannel, int plFoundSignal );
        
        
        [ PreserveSig() ]
        int StoreAutoTune();
        
        
        [ PreserveSig() ]
        int get_NumInputConnections( int plNumInputConnections );
        
        
        [ PreserveSig() ]
        int put_InputType( int lIndex, TunerInputType inputType );
        
        
        [ PreserveSig() ]
        int get_InputType( int lIndex, TunerInputType inputType );
        
        
        [ PreserveSig() ]
        int put_ConnectInput( int lIndex );
        
        
        [ PreserveSig() ]
        int get_ConnectInput( int lIndex );
        
        
        [ PreserveSig() ]
        int get_VideoFrequency( int lFreq );
        
        
        [ PreserveSig() ]
        int get_AudioFrequency( int lFreq );
        
    } 
    
} 
