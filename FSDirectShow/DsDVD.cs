using System; 
using System.Runtime.InteropServices; 


using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace FSDirectShow
{
    [ Flags() ]
    public enum DvdGraphFlags 
    { 
        Default = 0X0,
        HwDecPrefer = 0X1,
        HwDecOnly = 0X2,
        SwDecPrefer = 0X4,
        SwDecOnly = 0X8,
        NoVpe = 0X100,
    } 
    
    
    [ Flags() ]
    public enum DvdStreamFlags 
    { 
        None = 0X0,
        Video = 0X1,
        Audio = 0X2,
        SubPic = 0X4,
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdRenderStatus 
    { 
        public int vpeStatus; 
        public bool volInvalid; 
        public bool volUnknown; 
        public bool noLine21In; 
        public bool noLine21Out; 
        public int numStreams; 
        public int numStreamsFailed; 
        public DvdStreamFlags failedStreams; 
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "FCC152B6-F372-11d0-8E00-00C04FD7C08B" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IDvdGraphBuilder 
    { 
        [ PreserveSig() ]
        int GetFiltergraph( out IGraphBuilder ppGB );
        
        
        [ PreserveSig() ]
        int GetDvdInterface( [ In() ]Guid riid, [ MarshalAs( UnmanagedType.IUnknown ) ]out object ppvIF );
        
        
        [ PreserveSig() ]
        int RenderDvdVideoVolume( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string lpcwszPathName, DvdGraphFlags dwFlags, out DvdRenderStatus pStatus );
        
    } 
    
    
    
    
    [ Flags() ]
    public enum DvdCmdFlags 
    { 
        None = 0X0,
        Flush = 0X1,
        SendEvt = 0X2,
        Block = 0X4,
        StartWRendered = 0X8,
        EndARendered = 0X10,
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdTimeCode 
    { 
        public byte bHours; 
        public byte bMinutes; 
        public byte bSeconds; 
        public byte bFrames; 
    } 
    
    
    public enum DvdMenuID 
    { 
        Title = 2,
        Root = 3,
        Subpicture = 4,
        Audio = 5,
        Angle = 6,
        Chapter = 7,
    } 
    
    
    
    public enum DvdRelButton 
    { 
        Upper = 1,
        Lower = 2,
        Left = 3,
        Right = 4,
    } 
    
    
    
    public enum DvdOptionFlag 
    { 
        ResetOnStop = 1,
        NotifyParentalLevelChange = 2,
        HmsfTimeCodeEvt = 3,
    } 
    
    
    
    public enum DvdAudioLangExt 
    { 
        NotSpecified = 0,
        Captions = 1,
        VisuallyImpaired = 2,
        DirectorComments1 = 3,
        DirectorComments2 = 4,
    } 
    
    
    public enum DvdSubPicLangExt 
    { 
        NotSpecified = 0,
        CaptionNormal = 1,
        CaptionBig = 2,
        CaptionChildren = 3,
        ClosedNormal = 5,
        ClosedBig = 6,
        ClosedChildren = 7,
        Forced = 9,
        DirectorCmtNormal = 13,
        DirectorCmtBig = 14,
        DirectorCmtChildren = 15,
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "33BC7430-EEC0-11D2-8201-00A0C9D74842" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IDvdControl2 
    { 
        [ PreserveSig() ]
        int PlayTitle( int ulTitle, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayChapterInTitle( int ulTitle, int ulChapter, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayAtTimeInTitle( int ulTitle, [ In() ]DvdTimeCode pStartTime, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int Stop();
        
        
        [ PreserveSig() ]
        int ReturnFromSubmenu( DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayAtTime( [ In() ]DvdTimeCode pTime, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayChapter( int ulChapter, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayPrevChapter( DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int ReplayChapter( DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayNextChapter( DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayForwards( double dSpeed, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayBackwards( double dSpeed, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int ShowMenu( DvdMenuID MenuID, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int Resume( DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int SelectRelativeButton( DvdRelButton buttonDir );
        
        
        [ PreserveSig() ]
        int ActivateButton();
        
        
        [ PreserveSig() ]
        int SelectButton( int ulButton );
        
        
        [ PreserveSig() ]
        int SelectAndActivateButton( int ulButton );
        
        
        [ PreserveSig() ]
        int StillOff();
        
        
        [ PreserveSig() ]
        int Pause( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool bState );
        
        
        [ PreserveSig() ]
        int SelectAudioStream( int ulAudio, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int SelectSubpictureStream( int ulSubPicture, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int SetSubpictureState( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool bState, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int SelectAngle( int ulAngle, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int SelectParentalLevel( int ulParentalLevel );
        
        
        [ PreserveSig() ]
        int SelectParentalCounTry( byte[] bCounTry );
        
        
        [ PreserveSig() ]
        int SelectKaraokeAudioPresentationMode( int ulMode );
        
        
        [ PreserveSig() ]
        int SelectVideoModePreference( int ulPreferredDisplayMode );
        
        
        [ PreserveSig() ]
        int SetDVDDirectory( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pszwPath );
        
        
        [ PreserveSig() ]
        int ActivateAtPosition( DsPOINT point );
        
        
        [ PreserveSig() ]
        int SelectAtPosition( DsPOINT point );
        
        
        [ PreserveSig() ]
        int PlayChaptersAutoStop( int ulTitle, int ulChapter, int ulChaptersToPlay, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int AcceptParentalLevelChange( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool bAccept );
        
        
        [ PreserveSig() ]
        int SetOption( DvdOptionFlag flag, [ In(), MarshalAs( UnmanagedType.Bool ) ]bool fState );
        
        
        [ PreserveSig() ]
        int SetState( IDvdState pState, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int PlayPeriodInTitleAutoStop( int ulTitle, [ In() ]DvdTimeCode pStartTime, [ In() ]DvdTimeCode pEndTime, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int SetGPRM( int ulIndex, short wValue, DvdCmdFlags dwFlags, out OptIDvdCmd ppCmd );
        
        
        [ PreserveSig() ]
        int SelectDefaultMenuLanguage( int Language );
        
        
        [ PreserveSig() ]
        int SelectDefaultAudioLanguage( int Language, DvdAudioLangExt audioExtension );
        
        
        [ PreserveSig() ]
        int SelectDefaultSubpictureLanguage( int Language, DvdSubPicLangExt subpictureExtension );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "5a4a97e4-94ee-4a55-9751-74b5643aa27d" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IDvdCmd 
    { 
        [ PreserveSig() ]
        int WaitForStart();
        
        [ PreserveSig() ]
        int WaitForEnd();
        
    } 
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "86303d6d-1c4a-4087-ab42-f711167048ef" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IDvdState 
    { 
        [ PreserveSig() ]
        int GetDiscID( out long pullUniqueID );
        
        [ PreserveSig() ]
        int GetParentalLevel( out int pulParentalLevel );
        
    } 
    
    
    
    public enum DvdDomain 
    { 
        FirstPlay = 1,
        VideoManagerMenu = 2,
        VideoTitleSetMenu = 3,
        Title = 4,
        Stop = 5,
    } 
    
    
    
    public enum DvdVideoCompress 
    { 
        Other = 0,
        Mpeg1 = 1,
        Mpeg2 = 2,
    } 
    
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdPlayLocation 
    { 
        public int TitleNum; 
        public int ChapterNum; 
        public DvdTimeCode timeCode; 
        public int TimeCodeFlags; 
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdMenuAttr 
    { 
        [ MarshalAs( UnmanagedType.ByValArray, SizeConst=8 ) ]public bool[] compatibleRegion; 
        public DvdVideoAttr videoAt; 
        
        public bool audioPresent; 
        public DvdAudioAttr audioAt; 
        
        public bool subPicPresent; 
        public DvdSubPicAttr subPicAt; 
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdVideoAttr 
    { 
        public bool panscanPermitted; 
        public bool letterboxPermitted; 
        public int aspectX; 
        public int aspectY; 
        public int frameRate; 
        public int frameHeight; 
        public DvdVideoCompress compression; 
        public bool line21Field1InGOP; 
        public bool line21Field2InGOP; 
        public int sourceResolutionX; 
        public int sourceResolutionY; 
        public bool isSourceLetterboxed; 
        public bool isFilmMode; 
    } 
    
    
    
    
    public enum DvdAudioAppMode 
    { 
        None = 0,
        Karaoke = 1,
        Surround = 2,
        Other = 3,
    } 
    
    
    
    public enum DvdAudioFormat 
    { 
        Ac3 = 0,
        Mpeg1 = 1,
        Mpeg1Drc = 2,
        Mpeg2 = 3,
        Mpeg2Drc = 4,
        Lpcm = 5,
        Dts = 6,
        Sdds = 7,
        Other = 8,
    } 
    
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdAudioAttr 
    { 
        public DvdAudioAppMode appMode; 
        public int appModeData; 
        public DvdAudioFormat audioFormat; 
        public int language; 
        public DvdAudioLangExt languageExtension; 
        public bool hasMultichannelInfo; 
        public int frequency; 
        public byte quantization; 
        public byte numberOfChannels; 
        public short dummy; 
        public int res1; 
        public int res2; 
    } 
    
    
    
    
    public enum DvdSubPicType 
    { 
        NotSpecified = 0,
        Language = 1,
        Other = 2,
    } 
    
    
    public enum DvdSubPicCoding 
    { 
        RunLength = 0,
        Extended = 1,
        Other = 2,
    } 
    
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdSubPicAttr 
    { 
        public DvdSubPicType type; 
        public DvdSubPicCoding coding; 
        public int language; 
        public DvdSubPicLangExt languageExt; 
    } 
    
    
    
    
    public enum DvdTitleAppMode 
    { 
        NotSpecified = 0,
        Karaoke = 1,
        Other = 3,
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdTitleAttr 
    { 
        public DvdTitleAppMode appMode; 
        public DvdVideoAttr videoAt; 
        public int numberOfAudioStreams; 
    } 
    
    
    
    
    public enum DvdDiscSide 
    { 
        A = 1,
        B = 2,
    } 
    
    
    
    
    public enum DvdCharSet 
    { 
        Iso646 = 1,
        Jis = 2,
        Iso8859 = 3,
        SiftJis = 4,
    } 
    
    
    
    
    [ Flags() ]
    public enum DvdAudioCaps 
    { 
        Ac3 = 0X1,
        Mpeg2 = 0X2,
        Lpcm = 0X4,
        Dts = 0X8,
        Sdds = 0X10,
    } 
    
    
    [ StructLayout( LayoutKind.Sequential, Pack=1 ), ComVisible( false ) ]
    public struct DvdDecoderCaps 
    { 
        public int size; 
        public DvdAudioCaps audioCaps; 
        public double fwdMaxRateVideo; 
        public double fwdMaxRateAudio; 
        public double fwdMaxRateSP; 
        public double bwdMaxRateVideo; 
        public double bwdMaxRateAudio; 
        public double bwdMaxRateSP; 
        public int res1; 
        public int res2; 
        public int res3; 
        public int res4; 
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "34151510-EEC0-11D2-8201-00A0C9D74842" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface IDvdInfo2 
    { 
        [ PreserveSig() ]
        int GetCurrentDomain( out DvdDomain pDomain );
        
        
        [ PreserveSig() ]
        int GetCurrentLocation( out DvdPlayLocation pLocation );
        
        
        [ PreserveSig() ]
        int GetTotalTitleTime( out DvdTimeCode pTotalTime, int ulTimeCodeFlags );
        
        
        [ PreserveSig() ]
        int GetCurrentButton( int pulButtonsAvailable, int pulCurrentButton );
        
        
        [ PreserveSig() ]
        int GetCurrentAngle( int pulAnglesAvailable, int pulCurrentAngle );
        
        
        [ PreserveSig() ]
        int GetCurrentAudio( int pulStreamsAvailable, int pulCurrentStream );
        
        
        [ PreserveSig() ]
        int GetCurrentSubpicture( int pulStreamsAvailable, int pulCurrentStream, [ MarshalAs( UnmanagedType.Bool ) ]out bool pbIsDisabled );
        
        
        [ PreserveSig() ]
        int GetCurrentUOPS( int pulUOPs );
        
        
        [ PreserveSig() ]
        int GetAllSPRMs( IntPtr pRegisterArray );
        
        
        [ PreserveSig() ]
        int GetAllGPRMs( IntPtr pRegisterArray );
        
        
        [ PreserveSig() ]
        int GetAudioLanguage( int ulStream, int pLanguage );
        
        
        [ PreserveSig() ]
        int GetSubpictureLanguage( int ulStream, int pLanguage );
        
        
        //[ PreserveSig() ]
        //int GetTitleAttributes( int ulTitle, out DvdMenuAttr pMenu, IntPtr pTitle )
        //{ 
        //} 
        
        
        [ PreserveSig() ]
        int GetVMGAttributes( out DvdMenuAttr pATR );
        
        
        [ PreserveSig() ]
        int GetCurrentVideoAttributes( out DvdVideoAttr pATR );
        
        
        [ PreserveSig() ]
        int GetAudioAttributes( int ulStream, out DvdAudioAttr pATR );
        
        
        [ PreserveSig() ]
        int GetKaraokeAttributes( int ulStream, IntPtr pATR );
        
        
        [ PreserveSig() ]
        int GetSubpictureAttributes( int ulStream, out DvdSubPicAttr pATR );
        
        
        [ PreserveSig() ]
        int GetDVDVolumeInfo( int pulNumOfVolumes, int pulVolume, DvdDiscSide pSide, int pulNumOfTitles );
        
        
        [ PreserveSig() ]
        int GetDVDTextNumberOfLanguages( int pulNumOfLangs );
        
        
        [ PreserveSig() ]
        int GetDVDTextLanguageInfo( int ulLangIndex, int pulNumOfStrings, int pLangCode, DvdCharSet pbCharacterSet );
        
        
        [ PreserveSig() ]
        int GetDVDTextStringAsNative( int ulLangIndex, int ulStringIndex, IntPtr pbBuffer, int ulMaxBufferSize, int pulActualSize, int pType );
        
        
        [ PreserveSig() ]
        int GetDVDTextStringAsUnicode( int ulLangIndex, int ulStringIndex, IntPtr pchwBuffer, int ulMaxBufferSize, int pulActualSize, int pType );
        
        
        [ PreserveSig() ]
        int GetPlayerParentalLevel( int pulParentalLevel, out byte[] pbCounTryCode );
        
        
        [ PreserveSig() ]
        int GetNumberOfChapters( int ulTitle, int pulNumOfChapters );
        
        
        [ PreserveSig() ]
        int GetTitleParentalLevels( int ulTitle, int pulParentalLevels );
        
        
        [ PreserveSig() ]
        int GetDVDDirectory( IntPtr pszwPath, int ulMaxSize, int pulActualSize );
        
        
        [ PreserveSig() ]
        int IsAudioStreamEnabled( int ulStreamNum, [ MarshalAs( UnmanagedType.Bool ) ]out bool pbEnabled );
        
        
        [ PreserveSig() ]
        int GetDiscID( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pszwPath, long pullDiscID );
        
        
        [ PreserveSig() ]
        int GetState( out IDvdState pStateData );
        
        
        [ PreserveSig() ]
        int GetMenuLanguages( out int[] pLanguages, int ulMaxLanguages, int pulActualLanguages );
        
        
        [ PreserveSig() ]
        int GetButtonAtPosition( DsPOINT point, int pulButtonIndex );
        
        
        [ PreserveSig() ]
        int GetCmdFromEvent( int lParam1, out IDvdCmd pCmdObj );
        
        
        [ PreserveSig() ]
        int GetDefaultMenuLanguage( int pLanguage );
        
        
        [ PreserveSig() ]
        int GetDefaultAudioLanguage( int pLanguage, DvdAudioLangExt pAudioExtension );
        
        
        [ PreserveSig() ]
        int GetDefaultSubpictureLanguage( int pLanguage, DvdSubPicLangExt pSubpictureExtension );
        
        
        [ PreserveSig() ]
        int GetDecoderCaps( DvdDecoderCaps pCaps );
        
        
        [ PreserveSig() ]
        int GetButtonRect( int ulButton, DsRECT pRect );
        
        
        [ PreserveSig() ]
        int IsSubpictureStreamEnabled( int ulStreamNum, [ MarshalAs( UnmanagedType.Bool ) ]out bool pbEnabled );
        
        
    } 
    
    
    [ StructLayout( LayoutKind.Sequential ), ComVisible( false ) ]
    public class OptIDvdCmd  
    { 
        public IDvdCmd dvdCmd; 
    } 
    
    
    
} 
