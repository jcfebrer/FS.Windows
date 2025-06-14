using FSDisk;
using FSException;
using Microsoft.Win32;
using System;
using System.Collections.Generic;

#if NET35_OR_GREATER || NETCOREAPP
    using System.Linq;
#endif

using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace FSSystemInfo
{
    /// <summary>
    /// Información del sistema por WMI
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Clave
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Dominio
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// RootWMI
        /// </summary>
        public string RootWMI { get; set; }

        /// <summary>
        /// Tipo de datos de disco
        /// </summary>
        public enum DiskDataType
        {
            /// <summary>
            /// Tamaño total
            /// </summary>
            TotalSize,
            /// <summary>
            /// Espacio libre
            /// </summary>
            FreeSpace
        }

        /// <summary>
        /// Información del sistema
        /// </summary>
        public SystemInfo()
        {
            Ip = "";
            Domain = "";
            UserName = "";
            Password = "";
            RootWMI = "root\\CIMV2";
        }

        /// <summary>
        /// Información del sistema
        /// </summary>
        public SystemInfo(string rootWmi)
        {
            Ip = "";
            Domain = "";
            UserName = "";
            Password = "";
            RootWMI = rootWmi;
        }

        /// <summary>
        /// Información del sistema
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="domain"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SystemInfo(string ip, string domain, string username, string password)
        {
            Ip = ip;
            Domain = domain;
            UserName = username;
            Password = password;
            RootWMI = "root\\CIMV2";
        }

        /// <summary>
        /// Información del sistema
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="domain"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="rootWmi"></param>
        public SystemInfo(string ip, string domain, string username, string password, string rootWmi)
        {
            Ip = ip;
            Domain = domain;
            UserName = username;
            Password = password;
            RootWMI = rootWmi;
        }

        /// <summary>
        /// Recupera toda la información en un string
        /// </summary>
        /// <returns></returns>
        public string GetAllInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(GetOperatingSystemInfo());
            sb.AppendLine(GetProcessorInfo());
            sb.AppendLine(GetHardDisks());
            sb.AppendLine(GetWindowsServices());
            sb.AppendLine(GetNetworkAdapters());
            //sb.AppendLine(GetWifiAdapters());

            sb.AppendLine(GetWMI("Win32_CDROMDrive"));
            sb.AppendLine(GetWMI("Win32_UserAccount"));
            sb.AppendLine(GetWMI("Win32_BIOS"));
            sb.AppendLine(GetWMI("Win32_Desktop"));
            sb.AppendLine(GetWMI("Win32_CacheMemory"));
            sb.AppendLine(GetWMI("Win32_BaseBoard"));

            #region "Listado de comandos WMI"
            //Win32_OperatingSystem
            //Win32_BIOS
            //Win32_SoftwareElement
            //Win32_Process
            //Win32_Keyboard
            //Win32_DesktopMonitor
            //Win32_PointingDevice
            //Win32_Processor
            //Win32_LogicalDisk
            //Win32_MappedLogicalDisk
            //Win32_DiskPartition
            //Win32_CacheMemory
            //Win32_Volume
            //Win32_SMBIOSMemory
            //Win32_MemoryArray
            //Win32_MemoryDevice
            //Win32_NetworkAdapter
            //Win32_VideoController
            //Win32_1394Controller
            //Win32_SCSIController
            //Win32_InfraredDevice
            //Win32_PCMCIAController
            //Win32_USBController
            //Win32_SerialPort
            //Win32_ParallelPort
            //Win32_IDEController
            //Win32_Battery
            //Win32_PortableBattery
            //Win32_PnPEntity
            //Win32_Printer
            //Win32_DiskDrive
            //Win32_CDROMDrive
            //Win32_TapeDrive
            //Win32_SoundDevice
            //Win32_TemperatureProbe
            //Win32_VoltageProbe
            //Win32_CurrentProbe
            //Win32_Bus
            //Win32_USBHub
            //Win32_MotherboardDevice
            //Win32_POTSModem
            //Win32_HeatPipe
            //Win32_Refrigeration
            //Win32_Fan
            //Win32_ComputerSystem
            //Win32_NTDomain
            //Win32_PrinterDriver
            //Win32_PnPSignedDriver
            //Win32_BaseService
            //Win32_Service
            //Win32_SystemDriver
            //Win32_ApplicationService
            //Win32_ShortcutFile
            //Win32_CodecFile
            //Win32_NTEventlogFile
            //Win32_PageFile
            //Win32_Directory
            //Win32_OptionalFeature
            //Win32_Account
            //Win32_UserAccount
            //Win32_Group
            //Win32_SystemAccount
            //Win32_Thread
            //Win32_COMApplication
            //Win32_DCOMApplication
            //Win32_ScheduledJob
            //Win32_PrintJob
            //Win32_ServerSession
            //Win32_SoftwareFeature
            //Win32_DfsNode
            //Win32_ComponentCategory
            //Win32_ProgramGroupOrItem
            //Win32_LogicalProgramGroupItem
            //Win32_LogicalProgramGroup
            //Win32_NetworkConnection
            //Win32_COMClass
            //Win32_ClassicCOMClass
            //Win32_TCPIPPrinterPort
            //Win32_CommandLineAccess
            //Win32_SystemMemoryResource
            //Win32_PortResource
            //Win32_DeviceMemoryAddress
            //Win32_IRQResource
            //Win32_Environment
            //Win32_DMAChannel
            //Win32_Share
            //Win32_ClusterShare
            //Win32_NetworkProtocol
            //Win32_ShadowProvider
            //Win32_QuickFixEngineering
            //Win32_IP4RouteTable
            //Win32_ShadowCopy
            //Win32_LoadOrderGroup
            //Win32_Session
            //Win32_LogonSession
            //Win32_ServerConnection
            //Win32_DfsTarget
            //Win32_NetworkClient
            //Win32_PageFileUsage
            //Win32_IP4PersistedRouteTable
            //Win32_Registry
            //Win32_BaseBoard
            //Win32_SystemEnclosure
            //Win32_PhysicalMemoryArray
            //Win32_PhysicalMedia
            //Win32_PhysicalMemory
            //Win32_OnBoardDevice
            //Win32_SystemSlot
            //Win32_PortConnector
            //Win32_CurrentTime
            //Win32_LocalTime
            //Win32_UTCTime
            //Win32_NTLogEvent
            //Win32_ComputerSystemProduct
            //Win32_Product
            //Win32_NamedJobObjectActgInfo
            //Win32_NetworkAdapterConfiguration
            //Win32_Desktop
            //Win32_TimeZone
            //Win32_PageFileSetting
            //Win32_ShadowContext
            //Win32_MSIResource
            //Win32_ServiceControl
            //Win32_Property
            //Win32_Patch
            //Win32_PatchPackage
            //Win32_Binary
            //Win32_AutochkSetting
            //Win32_SerialPortConfiguration
            //Win32_StartupCommand
            //Win32_BootConfiguration
            //Win32_NetworkLoginProfile
            //Win32_NamedJobObjectLimitSetting
            //Win32_NamedJobObjectSecLimitSetting
            //Win32_DisplayConfiguration
            //Win32_QuotaSetting
            //Win32_SecuritySetting
            //Win32_LogicalFileSecuritySetting
            //Win32_LogicalShareSecuritySetting
            //Win32_DisplayControllerConfiguration
            //Win32_WMISetting
            //Win32_OSRecoveryConfiguration
            //Win32_COMSetting
            //Win32_ClassicCOMClassSetting
            //Win32_DCOMApplicationSetting
            //Win32_VideoConfiguration
            //Win32_ODBCAttribute
            //Win32_ODBCSourceAttribute
            //ScriptingStandardConsumerSetting
            //Win32_PrinterConfiguration
            //SoftwareLicensingProduct
            //SoftwareLicensingService
            //SoftwareLicensingTokenActivationLicense
            //Win32_PrinterDriverDll
            //Win32_LogicalDiskToPartition
            //Win32_DiskDriveToDiskPartition
            //Win32_DeviceBus
            //Win32_SCSIControllerDevice
            //Win32_USBControllerDevice
            //Win32_IDEControllerDevice
            //Win32_1394ControllerDevice
            //Win32_POTSModemToSerialPort
            //Win32_PrinterController
            //Win32_ControllerHasHub
            //Win32_AssociatedProcessorMemory
            //Win32_CIMLogicalDeviceCIMDataFile
            //Win32_PNPAllocatedResource
            //Win32_DiskDrivePhysicalMedia
            //Win32_MemoryDeviceLocation
            //Win32_MemoryArrayLocation
            //Win32_AllocatedResource
            //Win32_ApplicationCommandLine
            //Win32_SubSession
            //Win32_ShadowVolumeSupport
            //Win32_SessionConnection
            //Win32_ShadowFor
            //Win32_LogonSessionMappedDisk
            //Win32_PrinterShare
            //Win32_PnPSignedDriverCIMDataFile
            //Win32_ConnectionShare
            //Win32_LoadOrderGroupServiceDependencies
            //Win32_SessionResource
            //Win32_SessionProcess
            //Win32_SoftwareFeatureParent
            //Win32_ShadowOn
            //Win32_DependentService
            //Win32_OperatingSystemQFE
            //Win32_LoggedOnUser
            //Win32_SystemDriverPNPEntity
            //Win32_DfsNodeTarget
            //Win32_DriverForDevice
            //Win32_LogicalProgramGroupItemDataFile
            //Win32_ShadowBy
            //Win32_LogicalProgramGroupDirectory
            //Win32_ShadowDiffVolumeSupport
            //Win32_LogicalDiskRootDirectory
            //Win32_SystemDevices
            //Win32_ComputerSystemProcessor
            //Win32_SystemPartitions
            //Win32_SystemServices
            //Win32_SystemNetworkConnections
            //Win32_SystemResources
            //Win32_SystemBIOS
            //Win32_SystemLoadOrderGroups
            //Win32_SystemUsers
            //Win32_SystemOperatingSystem
            //Win32_SystemSystemDriver
            //Win32_SystemProcesses
            //Win32_COMApplicationClasses
            //Win32_ClassicCOMApplicationClasses
            //Win32_UserInDomain
            //Win32_LoadOrderGroupServiceMembers
            //Win32_SoftwareFeatureSoftwareElements
            //Win32_MemoryDeviceArray
            //Win32_GroupInDomain
            //Win32_GroupUser
            //Win32_ProgramGroupContents
            //Win32_SubDirectory
            //Win32_PhysicalMemoryLocation
            //Win32_DiskQuota
            //Win32_VolumeQuotaSetting
            //Win32_DeviceSettings
            //Win32_PrinterSetting
            //Win32_NetworkAdapterSetting
            //Win32_SerialPortSetting
            //Win32_SecuritySettingOfObject
            //Win32_SecuritySettingOfLogicalShare
            //Win32_SecuritySettingOfLogicalFile
            //Win32_UserDesktop
            //Win32_SystemSetting
            //Win32_SystemProgramGroups
            //Win32_SystemBootConfiguration
            //Win32_SystemTimeZone
            //Win32_SystemDesktop
            //Win32_ClassicCOMClassSettings
            //Win32_VolumeQuota
            //Win32_WMIElementSetting
            //Win32_COMApplicationSettings
            //Win32_VideoSettings
            //Win32_PageFileElementSetting
            //Win32_OperatingSystemAutochkSetting
            //Win32_PnPDevice
            //Win32_ActiveRoute
            //Win32_NamedJobObjectProcess
            //Win32_WinSAT
            //Win32_UserProfile
            //Win32_FolderRedirectionHealth
            //Win32_PrivilegesStatus
            //Win32_JobObjectStatus
            //Win32_Trustee
            //Win32_ACE
            //Win32_SecurityDescriptor
            //Win32_CollectionStatistics
            //Win32_NamedJobObjectStatistics
            //Win32_AccountSID
            //Win32_SecurityDescriptorHelper
            //Win32_ShortcutAction
            //Win32_ExtensionInfoAction
            //Win32_CreateFolderAction
            //Win32_RegistryAction
            //Win32_ClassInfoAction
            //Win32_SelfRegModuleAction
            //Win32_TypeLibraryAction
            //Win32_BindImageAction
            //Win32_RemoveIniAction
            //Win32_MIMEInfoAction
            //Win32_FontInfoAction
            //Win32_PublishComponentAction
            //Win32_MoveFileAction
            //Win32_DuplicateFileAction
            //Win32_RemoveFileAction
            //Win32_ProductResource
            //Win32_MountPoint
            //Win32_RoamingProfileMachineConfiguration
            //Win32_ManagedSystemElementResource
            //Win32_SoftwareElementResource
            //Win32_SID
            //Win32_ActionCheck
            //Win32_ProductSoftwareFeatures
            //Win32_ImplementedCategory
            //Win32_RoamingProfileUserConfiguration
            //Win32_InstalledSoftwareElement
            //Win32_SoftwareFeatureCheck
            //Win32_LUIDandAttributes
            //Win32_VolumeUserQuota
            //Win32_LUID
            //Win32_DirectorySpecification
            //Win32_SoftwareElementCondition
            //Win32_ODBCDriverSpecification
            //Win32_ServiceSpecification
            //Win32_FileSpecification
            //Win32_IniFileSpecification
            //Win32_LaunchCondition
            //Win32_ODBCDataSourceSpecification
            //Win32_ODBCTranslatorSpecification
            //Win32_ProgIDSpecification
            //Win32_EnvironmentSpecification
            //Win32_ReserveCost
            //Win32_Condition
            //Win32_ShadowStorage
            //Win32_DCOMApplicationAccessAllowedSetting
            //StdRegProv
            //Win32_FolderRedirection
            //Win32_TokenPrivileges
            //Win32_NamedJobObject
            //Win32_ServiceSpecificationService
            //Win32_InstalledWin32Program
            //Win32_ShareToDirectory
            //Win32_SettingCheck
            //Win32_PatchFile
            //Win32_ODBCDriverAttribute
            //Win32_ODBCDataSourceAttribute
            //Win32_ClientApplicationSetting
            //Win32_RoamingUserHealthConfiguration
            //Win32_UserStateConfigurationControls
            //Win32_SecuritySettingOwner
            //Win32_LogicalFileOwner
            //NTEventlogProviderConfig
            //Win32_ShortcutSAP
            //Win32_MethodParameterClass
            //Win32_ProcessStartup
            //Win32_PingStatus
            //Win32_SoftwareElementCheck
            //Win32_ODBCDriverSoftwareElement
            //Win32_FolderRedirectionUserConfiguration
            //Win32_Reliability
            //Win32_ReliabilityStabilityMetrics
            //Win32_ReliabilityRecords
            //Win32_InstalledProgramFramework
            //Win32_NTLogEventLog
            //Win32_ComClassAutoEmulator
            //Win32_FolderRedirectionHealthConfiguration
            //Win32_ComClassEmulator
            //Win32_SoftwareFeatureAction
            //Win32_SecuritySettingGroup
            //Win32_LogicalFileGroup
            //Win32_DCOMApplicationLaunchAllowedSetting
            //Win32_SecuritySettingAuditing
            //Win32_LogicalFileAuditing
            //Win32_LogicalShareAuditing
            //Win32_PnPDeviceProperty
            //Win32_PnPDevicePropertyString
            //Win32_PnPDevicePropertyReal32Array
            //Win32_PnPDevicePropertyReal64
            //Win32_PnPDevicePropertyUint16
            //Win32_PnPDevicePropertySint16Array
            //Win32_PnPDevicePropertySint64
            //Win32_PnPDevicePropertyUint8
            //Win32_PnPDevicePropertySint8
            //Win32_PnPDevicePropertySecurityDescriptor
            //Win32_PnPDevicePropertyReal32
            //Win32_PnPDevicePropertySint32
            //Win32_PnPDevicePropertyStringArray
            //Win32_PnPDevicePropertyUint32
            //Win32_PnPDevicePropertyUint64
            //Win32_PnPDevicePropertyBoolean
            //Win32_PnPDevicePropertyUint16Array
            //Win32_PnPDevicePropertyBinary
            //Win32_PnPDevicePropertySint32Array
            //Win32_PnPDevicePropertySint16
            //Win32_PnPDevicePropertyReal64Array
            //Win32_PnPDevicePropertyBooleanArray
            //Win32_PnPDevicePropertyUint32Array
            //Win32_PnPDevicePropertyDateTime
            //Win32_PnPDevicePropertySecurityDescriptorArray
            //Win32_PnPDevicePropertySint8Array
            //Win32_SoftwareElementAction
            //Win32_ProductCheck
            //Win32_NTLogEventUser
            //Win32_ProtocolBinding
            //Win32_NamedJobObjectLimit
            //Win32_NamedJobObjectSecLimit
            //Win32_InstalledStoreProgram
            //Win32_NTLogEventComputer
            //Win32_TokenGroups
            //Win32_DefragAnalysis
            //Win32_SIDandAttributes
            //Win32_CheckCheck
            //Win32_RoamingProfileBackgroundUploadParams
            //Win32_RoamingProfileSlowLinkParams
            //Win32_SecuritySettingAccess
            //Win32_LogicalFileAccess
            //Win32_LogicalShareAccess
            //Win32_OfflineFilesHealth
            #endregion

            sb.AppendLine(GetAllInformation());

            return sb.ToString();
        }

        /// <summary>
        /// Devuelve el scope por defecto para realizar la conexión.
        /// </summary>
        /// <returns></returns>
        public ManagementScope GetScope()
        {
            return GetScope(RootWMI);
        }


        /// <summary>
        /// Devuelve el scope para realizar la conexión.
        /// </summary>
        /// <returns></returns>
        public ManagementScope GetScope(string namespacePath)
        {
            ConnectionOptions options = new ConnectionOptions();
            ManagementPath path = new ManagementPath();
            path.NamespacePath = namespacePath;

            if (!String.IsNullOrEmpty(Ip))
            {
                if (!String.IsNullOrEmpty(UserName))
                {
                    options.Username = UserName;
                    options.Password = Password;
                }

                if(!String.IsNullOrEmpty(Domain))
                    options.Authority = "NTLMDOMAIN:" + Domain;

                options.EnablePrivileges = true;
                options.Impersonation = ImpersonationLevel.Impersonate;
                options.Authentication = AuthenticationLevel.Packet;
                options.Timeout = new TimeSpan(0, 0, 30);
                path.Server = Ip;
            }
            ManagementScope scope = new ManagementScope(path, options);
            scope.Connect();

            return scope;
        }

        /// <summary>
        /// Recupera toda la información indicando un path
        /// </summary>
        /// <returns></returns>
        public string GetAllInformation()
        {
            StringBuilder sb = new StringBuilder();
            SelectQuery query = new SelectQuery(@"SELECT * FROM meta_class WHERE __Class LIKE ""%%%"" AND NOT __Class LIKE ""[_][_]%"" AND NOT __Class LIKE ""CIM[_]%"" AND NOT __Class LIKE ""Win32_Perf%"" AND NOT __Class LIKE ""MSFT[_]%""");

            EnumerationOptions eOption = new EnumerationOptions
            {
                EnumerateDeep = true,
                UseAmendedQualifiers = true
            };

            ManagementObjectSearcher queryAllClasses = new ManagementObjectSearcher(GetScope(), query, eOption);
            foreach (ManagementClass mClass in queryAllClasses.Get())
            {
                sb.AppendLine(mClass.Path.RelativePath);
            }

            return sb.ToString();
        }

        /// <summary>
        /// GetOperatingSystemInfo
        /// </summary>
        /// <returns></returns>
        public string GetOperatingSystemInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Mostrando información del sistema operativo....\n");
            //Create an object of ManagementObjectSearcher class and pass query as parameter.
            ManagementObjectSearcher mos = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from Win32_OperatingSystem"));
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    sb.AppendLine("Operating System Name:  " + managementObject["Caption"].ToString());   //Display operating system caption
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    sb.AppendLine("Operating System Architecture:  " + managementObject["OSArchitecture"].ToString());   //Display operating system architecture.
                }
                if (managementObject["CSDVersion"] != null)
                {
                    sb.AppendLine("Operating System Service Pack:  " + managementObject["CSDVersion"].ToString());     //Display operating system version.
                }
                if (managementObject["TotalVisibleMemorySize"] != null)
                {
                    sb.AppendLine("Total Visible Memory:  " + managementObject["TotalVisibleMemorySize"].ToString());
                }
                if (managementObject["FreePhysicalMemory"] != null)
                {
                    sb.AppendLine("Free Physical Memory:  " + managementObject["FreePhysicalMemory"].ToString());
                }
                if (managementObject["TotalVirtualMemorySize"] != null)
                {
                    sb.AppendLine("Total Virtual Memory:  " + managementObject["TotalVirtualMemorySize"].ToString());
                }
                if (managementObject["FreeVirtualMemory"] != null)
                {
                    sb.AppendLine("Free Virtual Memory:  " + managementObject["FreeVirtualMemory"].ToString());
                }

                sb.AppendLine("Todas las propiedades:  ");
                foreach (PropertyData prop in managementObject.Properties)
                {
                    //add these to your arraylist or dictionary 
                    Console.WriteLine("{0}: {1}", prop.Name, prop.Value);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// GetProcessorInfo
        /// </summary>
        /// <returns></returns>
        public string GetProcessorInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n\nMostrando información del procesador....");
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    sb.AppendLine(processor_name.GetValue("ProcessorNameString").ToString());   //Display processor ingo.
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// GetWindowsServices
        /// </summary>
        /// <returns></returns>
        public string GetWindowsServices()
        {
            StringBuilder sb = new StringBuilder();

            ManagementObjectSearcher windowsServicesSearcher = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from Win32_Service"));
            ManagementObjectCollection objectCollection = windowsServicesSearcher.Get();

            sb.AppendLine(String.Format("There are {0} Windows services: ", objectCollection.Count));

            foreach (ManagementObject windowsService in objectCollection)
            {
                PropertyDataCollection serviceProperties = windowsService.Properties;
                foreach (PropertyData serviceProperty in serviceProperties)
                {
                    if (serviceProperty.Value != null)
                    {
                        if (serviceProperty.Name == "DisplayName" | serviceProperty.Name == "Status" | serviceProperty.Name == "State" | serviceProperty.Name == "Started" | serviceProperty.Name == "ProcessId")
                        {
                            sb.AppendLine(String.Format("{0}: {1}", serviceProperty.Name, serviceProperty.Value));
                        }
                    }
                }
                sb.AppendLine("---------------------------------------");
            }

            return sb.ToString();
        }

        /// <summary>
        /// GetNetworkAdapters
        /// </summary>
        /// <returns></returns>
        public string GetNetworkAdapters()
        {
            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher networkAdapterSearcher = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from Win32_NetworkAdapterConfiguration where IPEnabled=true"));
            ManagementObjectCollection objectCollection = networkAdapterSearcher.Get();

            sb.AppendLine(String.Format("There are {0} network adapaters: ", objectCollection.Count));

            foreach (ManagementObject networkAdapter in objectCollection)
            {
                PropertyDataCollection networkAdapterProperties = networkAdapter.Properties;
                foreach (PropertyData networkAdapterProperty in networkAdapterProperties)
                {
                    if (networkAdapterProperty.Value != null)
                    {
                        if (networkAdapterProperty.Name == "Caption" | networkAdapterProperty.Name == "IPAddress")
                        {
                            sb.AppendLine(String.Format("{0}: {1}", networkAdapterProperty.Name, networkAdapterProperty.Value));
                        }
                    }
                }
                sb.AppendLine("---------------------------------------");
            }
            return sb.ToString();
        }

        /// <summary>
        /// GetWifiAdapters
        /// </summary>
        /// <returns></returns>
        public string GetWifiAdapters()
        {
            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher networkAdapterSearcher = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from WiFi_AvailableNetwork"));
            ManagementObjectCollection objectCollection = networkAdapterSearcher.Get();

            sb.AppendLine(String.Format("There are {0} wifi adapaters: ", objectCollection.Count));

            foreach (ManagementObject networkAdapter in objectCollection)
            {
                PropertyDataCollection networkAdapterProperties = networkAdapter.Properties;
                foreach (PropertyData networkAdapterProperty in networkAdapterProperties)
                {
                    if (networkAdapterProperty.Value != null)
                    {
                        sb.AppendLine(String.Format("{0}: {1}", networkAdapterProperty.Name, networkAdapterProperty.Value));
                    }
                }
                sb.AppendLine("---------------------------------------");
            }
            return sb.ToString();
        }

        /// <summary>
        /// GetWMI
        /// </summary>
        /// <param name="wmiTable"></param>
        /// <returns></returns>
        public string GetWMI(string wmiTable)
        {
            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from " + wmiTable));
            ManagementObjectCollection objectCollection = searcher.Get();

            sb.AppendLine(String.Format("-------------- {0} - {1} objects: ", wmiTable, objectCollection.Count));

            foreach (ManagementObject wmi in objectCollection)
            {
                PropertyDataCollection objProperties = wmi.Properties;
                foreach (PropertyData objProperty in objProperties)
                {
                    if (objProperty.Value != null)
                    {
                        sb.AppendLine(String.Format("{0}: {1}", objProperty.Name, objProperty.Value));
                    }
                }
                sb.AppendLine("---------------------------------------");
            }
            return sb.ToString();
        }

        /// <summary>
        /// GetHardDisks
        /// </summary>
        /// <returns></returns>
        public string GetHardDisks()
        {
            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from Win32_LogicalDisk"));
            ManagementObjectCollection objectCollection = searcher.Get();

            sb.AppendLine(String.Format("There are {0} objects: ", objectCollection.Count));

            foreach (ManagementObject wmi in objectCollection)
            {
                PropertyDataCollection objProperties = wmi.Properties;
                foreach (PropertyData objProperty in objProperties)
                {
                    if (objProperty.Value != null)
                    {
                        sb.AppendLine(String.Format("{0}: {1}", objProperty.Name, objProperty.Value));
                    }
                }
                sb.AppendLine("---------------------------------------");
            }
            return sb.ToString();
        }

        /// <summary>
        /// GetMemory
        /// </summary>
        /// <returns></returns>
        public long GetMemory()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from Win32_OperatingSystem"));
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["TotalVisibleMemorySize"] != null)
                {
                    return long.Parse(managementObject["TotalVisibleMemorySize"].ToString()) * 1024;
                }
            }

            return -1;
        }

        /// <summary>
        /// GetFreeMemory
        /// </summary>
        /// <returns></returns>
        public long GetFreeMemory()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher(GetScope(), new SelectQuery("select * from Win32_OperatingSystem"));
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["FreePhysicalMemory"] != null)
                {
                    return long.Parse(managementObject["FreePhysicalMemory"].ToString()) * 1024;
                }
            }

            return -1;
        }

        /// <summary>
        /// Devuelve el número de serie del disco duro.
        /// </summary>
        /// <param name="drive">Unidad C:, D:, E:, F:, etc...</param>
        /// <returns>The serial number</returns>
        public string SerialNumber(string drive)
        {
            ManagementObject disk = new ManagementObject(GetScope(), new ManagementPath(@"Win32_LogicalDisk.DeviceId=""" + drive + @""""), new ObjectGetOptions());
            return Convert.ToString(disk.Properties["VolumeSerialNumber"].Value);
        }

        /// <summary>
        /// Devuelve el espacio del disco o tamaño.
        /// </summary>
        /// <param name="tcDrive">Letra de la unidad.</param>
        /// <param name="tnType">Si el tipo es 0 devuelve el espacio libre, si es 1 el tamaño del disco.</param>
        /// <returns></returns>
        public long DiskSpace(string tcDrive, DiskDataType tnType)
        {
            long lnRetVal = -1;
            string lcSize = "-1";
            string lcFreeSpace = "-1";

            tcDrive = DiskUtil.GetDrive(tcDrive.Trim()).ToUpper();

            var searcher = new ManagementObjectSearcher(GetScope(), new SelectQuery(@"SELECT Name, Size, FreeSpace FROM Win32_LogicalDisk WHERE Name = """ + tcDrive + @""""));

            foreach (ManagementObject disk in searcher.Get())
            {
                var diskProperties = disk.Properties;
                foreach (var diskProperty in diskProperties)
                {
                    if (diskProperty.Name == "Size")
                        lcSize = disk[diskProperty.Name].ToString();

                    if (diskProperty.Name == "FreeSpace")
                        lcFreeSpace = disk[diskProperty.Name].ToString();
                }

                switch (tnType)
                {
                    case DiskDataType.TotalSize:
                        lnRetVal = long.Parse(lcSize);
                        break;
                    case DiskDataType.FreeSpace:
                        lnRetVal = long.Parse(lcFreeSpace);
                        break;
                }

                return lnRetVal;
            }

            return lnRetVal;
        }

        /// <summary>
        /// Espacio en disco
        /// </summary>
        /// <param name="tcDrive"></param>
        /// <returns></returns>
        public long DiskSpace(string tcDrive)
        {
            return DiskSpace(tcDrive, DiskDataType.FreeSpace);
        }

        /// <summary>
        /// Tipo de unidad
        /// </summary>
        /// <param name="tcDrive"></param>
        /// <returns></returns>
        public int DriveType(string tcDrive)
        {
            var nRetVal = -1;

            tcDrive = DiskUtil.GetDrive(tcDrive.Trim()).ToUpper();

            var query = new SelectQuery(@"SELECT Name, DriveType, FreeSpace FROM Win32_LogicalDisk where Name = """ + tcDrive + @"""");
            var searcher = new ManagementObjectSearcher(GetScope(), query);

            foreach (var drive in searcher.Get())
                nRetVal = int.Parse(drive["DriveType"].ToString());

            return nRetVal;
        }

        /// <summary>
        /// Crea un proceso con el comando indicado.
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns></returns>
        public string CreateProcess(string commandLine)
        {
            ObjectGetOptions objectGetOptions = new ObjectGetOptions();
            ManagementPath managementPath = new ManagementPath("Win32_Process");
            ManagementClass processClass = new ManagementClass
                (GetScope(), managementPath, objectGetOptions);
            ManagementBaseObject inParams = processClass.GetMethodParameters("Create");
            inParams["CommandLine"] = commandLine;
            ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);
            
            return "Process ID: " + outParams["processId"] + " - Return value: " + outParams["returnValue"];
        }

        /// <summary>
        /// Modo de inicio del servicio
        /// </summary>
        /// <param name="serviceName">Nombre del servicio</param>
        /// <returns></returns>
        public string StartModeService(string serviceName)
        {
            ManagementObject wmiService = new ManagementObject(GetScope(), new ManagementPath("Win32_Service.Name='" + serviceName + "'"), new ObjectGetOptions());
            //AcceptPause
            //AcceptStop
            //Caption
            //Description
            //DisplayName
            //Name
            //PathName
            //ProcessId
            //ServiceType
            //Started
            //StartMode
            //StartName
            //State
            //Status
            return wmiService["StartMode"].ToString();
        }

        /// <summary>
        /// Iniciamos el servicio indicado
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns>Error code. Si es diferente de 0 ha habido un problema.</returns>
        public int StartService(string serviceName)
        {
            // Start service
            ManagementObject wmiService = new ManagementObject(GetScope(), new ManagementPath("Win32_Service.Name='" + serviceName + "'"), new ObjectGetOptions());
            wmiService.Get();
            ManagementBaseObject outParams = wmiService.InvokeMethod("StartService", null, null);
            int ret = (int)outParams.Properties["ReturnValue"].Value;
            if (ret != 0)
                throw new ExceptionUtil(string.Format("Error al iniciar el servicio con el código de error: {0}", ret));
            return ret;
        }

        /// <summary>
        /// Paramos el servicio indicado
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns>Error code. Si es diferente de 0 ha habido un problema.</returns>
        public int StopService(string serviceName)
        {
            // Start service
            ManagementObject wmiService = new ManagementObject(GetScope(), new ManagementPath("Win32_Service.Name='" + serviceName + "'"), new ObjectGetOptions());
            wmiService.Get();
            ManagementBaseObject outParams = wmiService.InvokeMethod("StopService", null, null);
            int ret = (int)outParams.Properties["ReturnValue"].Value;
            if (ret != 0)
                throw new ExceptionUtil(string.Format("Error al parar el servicio con el código de error: {0}", ret));
            return ret;
        }

        /// <summary>
        /// Devuelve un listado de los discos físicos.
        /// </summary>
        /// <returns></returns>
        public List<string> GetPhysicalDisks()
        {
            List<string> result = new List<string>();
            WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_DiskDrive");

#if NET35_OR_GREATER || NETCOREAPP
            using (var searcher = new ManagementObjectSearcher(GetScope(), query))
            {
                result = searcher.Get()
                                 .OfType<ManagementObject>()
                                 .Select(o => o.Properties["DeviceID"].Value.ToString())
                                 .ToList();
            }

            return result;
#else
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(GetScope(), query))
            {
                ManagementObjectCollection objectCollection = searcher.Get();

                foreach (ManagementObject obj in objectCollection)
                {
                    if (obj.Properties["DeviceID"] != null && obj.Properties["DeviceID"].Value != null)
                    {
                        result.Add(obj.Properties["DeviceID"].Value.ToString());
                    }
                    obj.Dispose(); // Importante para liberar recursos
                }
            }

            return result;
#endif
        }

        /// <summary>
        /// Devuelve información de los disco físicos.
        /// </summary>
        /// <returns></returns>
        public string GetHdInfo()
        {
            var query = new WqlObjectQuery("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(GetScope(), query);

            StringBuilder data = new StringBuilder();
            foreach (ManagementObject info in searcher.Get())
            {
                data.AppendLine(String.Format("SerialNumber: {0}", info["SerialNumber"]));
                data.AppendLine(String.Format("Signature: {0}", info["Signature"]));
                data.AppendLine(String.Format("DeviceID: {0}", info["DeviceID"]));
                data.AppendLine(String.Format("Model: {0}", info["Model"]));
                data.AppendLine(String.Format("Interface: {0}", info["InterfaceType"]));
                data.AppendLine(String.Format("Index: {0}", info["Index"]));
                //data.AppendLine(String.Format("Bootable: {0}", info["Bootable"]));
                //data.AppendLine(String.Format("BootPartition: {0}", info["BootPartition"]));
            }

            return data.ToString();
        }

        /// <summary>
        /// Devuelve la información de las unidades lógicas.
        /// </summary>
        /// <returns></returns>
        public string GetDrivesInfo()
        {
            var queryDrive = new WqlObjectQuery("SELECT * FROM Win32_LogicalDiskToPartition");
            var queryDisk = new WqlObjectQuery("SELECT * FROM Win32_LogicalDisk");

#if NET35_OR_GREATER || NETCOREAPP
            var drives = new ManagementObjectSearcher(GetScope(), queryDrive).Get().Cast<ManagementObject>();
            var disks = new ManagementObjectSearcher(GetScope(), queryDisk).Get().Cast<ManagementObject>();
#else
            ManagementObjectSearcher driveSearcher = new ManagementObjectSearcher(GetScope(), queryDrive);
            ManagementObjectSearcher diskSearcher = new ManagementObjectSearcher(GetScope(), queryDisk);

            var drives = driveSearcher.Get();
            var disks = diskSearcher.Get();
#endif

            StringBuilder data = new StringBuilder();
            foreach (var drive in drives)
            {
                var driveLetter = Regex.Match((string)drive["Dependent"], @"DeviceID=""(.*)""").Groups[1].Value;
                var driveNumber = Regex.Match((string)drive["Antecedent"], @"Disk #(\d*),").Groups[1].Value;

                data.AppendLine("Drive Letter: " + driveLetter);
                data.AppendLine("Drive Number: " + driveNumber);

#if NET35_OR_GREATER || NETCOREAPP
                // Buscar por letra de unidad
                var foundDisk = disks.Where((d) => d["Name"].ToString() == driveLetter).FirstOrDefault();

                // Si no se encontró por letra de unidad, buscar por ruta del disco
                if (foundDisk == null)
                    foundDisk = disks.Where((d) => d.Path.ToString() == drive["Dependent"].ToString()).FirstOrDefault();
#else
                // Buscar por letra de unidad
                ManagementObject foundDisk = null;
                foreach (ManagementObject disk in disks)
                {
                    if (disk["Name"] != null && disk["Name"].ToString() == driveLetter)
                    {
                        foundDisk = disk;
                        break;
                    }
                }

                // Si no se encontró por letra de unidad, buscar por ruta del disco
                if (foundDisk == null && drive != null && drive["Dependent"] != null)
                {
                    foreach (ManagementObject disk in disks)
                    {
                        if (disk.Path != null && disk.Path.ToString() == drive["Dependent"].ToString())
                        {
                            foundDisk = disk;
                            break;
                        }
                    }
                }
#endif

                if (foundDisk == null)
                    data.AppendLine("Drive Label: <Unknown>");
                else
                    data.AppendLine("Drive Label: " + foundDisk["VolumeName"]);
            }

            return data.ToString();
        }

        /// <summary>
        /// Devuelve la temperatura de la CPU en grados celsius.
        /// </summary>
        /// <returns></returns>
        public double CpuTemperature()
        {
            double value = -1;
            WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM MSAcpi_ThermalZoneTemperature");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(GetScope("root\\WMI"), query);

            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementBaseObject tempObject in collection)
            {
                value = Convert.ToDouble(tempObject["CurrentTemperature"]);

                // kelvin = temp / 10;
                // celsius = (temp / 10) - 273.15;
                // fahrenheit = ((temp / 10) - 273.15) * 9 / 5 + 32;
                value = (value / 10) - 273.15;
            }

            return value;
        }

        /// <summary>
        /// Devuelve por cada procesador el uso en porcentaje.
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> CpuUsage()
        {
            List<KeyValuePair<string, int>> results = new List<KeyValuePair<string, int>>();
            WqlObjectQuery query = new WqlObjectQuery("select * from Win32_PerfFormattedData_PerfOS_Processor");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(GetScope(), query);
            foreach (ManagementObject obj in searcher.Get())
            {
                int value = Convert.ToInt32(obj["PercentProcessorTime"]);
                string name = Convert.ToString(obj["Name"]);

                results.Add(new KeyValuePair<string, int>(name, value));
            }

            return results;
        }

        /// <summary>
        /// Devuelve el uso del procesador
        /// </summary>
        /// <returns></returns>
        public int CpuUsageTotal()
        {
            int value = -1;
            WqlObjectQuery query = new WqlObjectQuery("select * from Win32_PerfFormattedData_PerfOS_Processor where Name='_Total'");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(GetScope(), query);
            foreach (ManagementObject obj in searcher.Get())
            {
                value = Convert.ToInt32(obj["PercentProcessorTime"]);
            }

            return value;
        }

        public IPAddress GetAdapterWithInternetAccess()
        {
            WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_IP4RouteTable WHERE Destination=\"0.0.0.0\"");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(GetScope(), query);
            int interfaceIndex = -1;

            foreach (var item in searcher.Get())
                interfaceIndex = Convert.ToInt32(item["InterfaceIndex"]);

            WqlObjectQuery queryConf = new WqlObjectQuery(string.Format("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE InterfaceIndex={0}", interfaceIndex));
            searcher = new ManagementObjectSearcher(GetScope(), queryConf);

            foreach (var item in searcher.Get())
            {
                string[] IPAddresses = (string[])item["IPAddress"];

                foreach (string IP in IPAddresses)
                    return IPAddress.Parse(IP);
            }

            return null;
        }
    }
}
