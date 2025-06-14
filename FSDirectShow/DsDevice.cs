using System; 
using System.Collections; 
using System.Runtime.InteropServices; 


using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace FSDirectShow
{
    [ ComVisible( false ) ]
    public class DsDev  
    { 
        
        public static bool GetDevicesOfCat( Guid cat, ref ArrayList devs ) 
        { 
            devs = null; 
            int hr = 0; 
            object comObj = null; 
            DsDevice.ICreateDevEnum enumDev = null; 
            System.Runtime.InteropServices.ComTypes.IEnumMoniker enumMon = null; 
            System.Runtime.InteropServices.ComTypes.IMoniker[] mon = new System.Runtime.InteropServices.ComTypes.IMoniker[ 2 ]; 
            try 
            { 
                Type srvType = Type.GetTypeFromCLSID( Clsid.SystemDeviceEnum ); 
                if ( srvType == null ) 
                { 
                    throw new NotImplementedException( "System Device Enumerator" ); 
                } 
                
                comObj = System.Activator.CreateInstance( srvType ); 
                enumDev = ( ( DsDevice.ICreateDevEnum )( comObj ) ); 
                
                hr = enumDev.CreateClassEnumerator( cat, out enumMon, 0 ); 
                if ( hr != 0 ) 
                { 
                    throw new NotSupportedException( "No devices of the category" ); 
                } 
                
                IntPtr f = new IntPtr(); 
                int count = 0; 
                
                do 
                { 
                    hr = enumMon.Next( 1, mon, f ); 
                    if ( ( hr != 0 ) | ( mon[ 0 ] == null ) ) 
                    { 
                        return false; 
                    } 
                    DsDevice dev = new DsDevice(); 
                    dev.Name = GetFriendlyName( mon[ 0 ] ); 
                    if ( devs == null ) 
                    { 
                        devs = new ArrayList(); 
                    } 
                    dev.Mon = mon[ 0 ]; 
                    mon[ 0 ] = null; 
                    devs.Add( dev ); 
                    dev = null; 
                    count = count + 1; 
                    
                } 
                while ( ( true ) ); 
                
                //return count > 0; 
            } 
            catch ( Exception ) 
            { 
                if ( !( devs == null ) ) 
                { 
                    foreach ( DsDevice d in devs ) 
                    { 
                        d.Dispose(); 
                    }
                    devs = null; 
                } 
                return false; 
            } 
            finally 
            { 
                enumDev = null; 
                if ( !( mon[ 0 ] == null ) ) 
                { 
                    Marshal.ReleaseComObject( mon[ 0 ] ); 
                    mon[ 0 ] = null; 
                } 
                if ( !( enumMon == null ) ) 
                { 
                    Marshal.ReleaseComObject( enumMon ); 
                    enumMon = null; 
                } 
                if ( !( comObj == null ) ) 
                { 
                    Marshal.ReleaseComObject( comObj ); 
                    comObj = null; 
                } 
            } 
        } 
        
        
        private static string GetFriendlyName( System.Runtime.InteropServices.ComTypes.IMoniker mon ) 
        { 
            object bagObj = null; 
            DsDevice.IPropertyBag bag = null; 
            try 
            { 
                Guid bagId = typeof( DsDevice.IPropertyBag ).GUID; 
                mon.BindToStorage( null, null, ref bagId, out bagObj ); 
                bag = ( ( DsDevice.IPropertyBag )( bagObj ) ); 
                object val = ""; 
                int hr = bag.Read( "FriendlyName", out val, IntPtr.Zero ); 
                if ( hr != 0 ) 
                { 
                    Marshal.ThrowExceptionForHR( hr ); 
                } 
                string ret = System.Convert.ToString( val ); 
                if ( ( ret == null ) | ( ret.Length < 1 ) ) 
                { 
                    throw new NotImplementedException( "Device FriendlyName" ); 
                } 
                return ret; 
            } 
            catch  
            { 
                return null; 
            } 
            finally 
            { 
                bag = null; 
                if ( !( bagObj == null ) ) 
                { 
                    Marshal.ReleaseComObject( bagObj ); 
                    bagObj = null; 
                } 
            } 
        } 
        
    } 
    
    
    
    [ ComVisible( false ) ]
    public class DsDevice : IDisposable 
    { 
        public string Name; 
        public System.Runtime.InteropServices.ComTypes.IMoniker Mon; 
        
        public void Dispose() 
        { 
            if ( !( Mon == null ) ) 
            { 
                Marshal.ReleaseComObject( Mon ); 
                Mon = null; 
            } 
        } 
        // interface methods implemented by Dispose
        void IDisposable.Dispose()
        { 
            Dispose();
        }
        
        
        [ ComVisible( true ), ComImport(), Guid( "29840822-5B84-11D0-BD3B-00A0C911CE86" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
        public interface ICreateDevEnum 
        { 
            [ PreserveSig() ]
            int CreateClassEnumerator( [ In() ]Guid pType, out System.Runtime.InteropServices.ComTypes.IEnumMoniker ppEnumMoniker, [ In() ]int dwFlags );
            
        } 
        
        
        
        [ ComVisible( true ), ComImport(), Guid( "55272A00-42CB-11CE-8135-00AA004BB851" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown ) ]
        public interface IPropertyBag 
        { 
            [ PreserveSig() ]
            int Read( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pszPropName, [ MarshalAs( UnmanagedType.Struct ) ]out object pVar, IntPtr pErrorLog );
            
            
            [ PreserveSig() ]
            int Write( [ In(), MarshalAs( UnmanagedType.LPWStr ) ]string pszPropName, [ In(), MarshalAs( UnmanagedType.Struct ) ]object pVar );
            
        } 
        
        
    } 
    
} 
