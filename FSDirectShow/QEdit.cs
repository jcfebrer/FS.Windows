using System; 
using System.Runtime.InteropServices; 


using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace FSDirectShow
{
    [ ComVisible( true ), ComImport(), Guid( "6B652FFF-11FE-4fce-92AD-0266B5D7C78F" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface ISampleGrabber 
    { 
        [ PreserveSig() ]
        int SetOneShot( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool OneShot );
        
        
        [ PreserveSig() ]
        int SetMediaType( [ In(), MarshalAs( UnmanagedType.LPStruct ) ]AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int GetConnectedMediaType( [ MarshalAs( UnmanagedType.LPStruct ) ]out AMMediaType pmt );
        
        
        [ PreserveSig() ]
        int SetBufferSamples( [ In(), MarshalAs( UnmanagedType.Bool ) ]bool BufferThem );
        
        
        [ PreserveSig() ]
        int GetCurrentBuffer( int pBufferSize, IntPtr pBuffer );
        
        
        [ PreserveSig() ]
        int GetCurrentSample( IntPtr ppSample );
        
        
        [ PreserveSig() ]
        int SetCallback( ISampleGrabberCB pCallback, int WhichMethodToCallback );
        
    } 
    
    
    
    
    [ ComVisible( true ), ComImport(), Guid( "0579154A-2B53-4994-B0D0-E773148EFF85" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
    public interface ISampleGrabberCB 
    { 
        [ PreserveSig() ]
        int SampleCB( double SampleTime, IMediaSample pSample );
        
        
        [ PreserveSig() ]
        int BufferCB( double SampleTime, IntPtr pBuffer, int BufferLen );
        
    } 
    
    
    
    [ StructLayout( LayoutKind.Sequential ), ComVisible( false ) ]
    public class VideoInfoHeader  
    { 
        public DsRECT SrcRect; 
        public DsRECT TagRect; 
        public int BitRate; 
        public int BitErrorRate; 
        public long AvgTimePerFrame; 
        public DsBITMAPINFOHEADER BmiHeader; 
    } 
    
} 
