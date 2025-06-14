using System; 
using System.Text; 
using System.Runtime.InteropServices; 

using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Security.Permissions;

namespace FSDirectShow
{
    public class DsBugWO  
    { 
        public static object CreateDsInstance( ref Guid clsid, ref Guid riid ) 
        { 
            IntPtr ptrIf = new System.IntPtr(); 
            int hr = CoCreateInstance( clsid, IntPtr.Zero, CLSCTX.Inproc, riid, ptrIf ); 
            if ( ( hr != 0 ) | ( ptrIf == IntPtr.Zero ) ) 
            { 
                Marshal.ThrowExceptionForHR( hr ); 
            } 
            
            Guid iu = new Guid( "00000000-0000-0000-C000-000000000046" ); 
            IntPtr ptrXX = new System.IntPtr(); 
            hr = Marshal.QueryInterface( ptrIf, ref iu, out ptrXX );

#if NETFRAMEWORK
            object ooo = System.Runtime.Remoting.Services.EnterpriseServicesHelper.WrapIUnknownWithComObject( ptrIf ); 
#else
			//TODO: REVISAR
            object ooo = null;
#endif            
            int ct = Marshal.Release( ptrIf ); 
            return ooo; 
        }

        [ DllImport( "ole32.dll" ) ]
        private static extern int CoCreateInstance( Guid clsid, IntPtr pUnkOuter, CLSCTX dwClsContext, Guid iid, IntPtr ptrIf );
    } 
    
    
    
    [ Flags() ]
    internal enum CLSCTX 
    { 
        Inproc = 0X3,
        Server = 0X15,
        All = 0X17,
    } 
    
} 
