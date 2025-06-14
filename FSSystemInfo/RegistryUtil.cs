using Microsoft.Win32;
using System;

namespace FSSystemInfo
{
    /// <summary>
    /// Clase con utilidades para gestionar el registro
    /// </summary>
    public class RegistryUtil
    {
        /// <summary>
        /// Tipo de origen
        /// </summary>
        public enum RegSource
        {
            /// <summary>
            /// The local machine
            /// </summary>
            LocalMachine,
            /// <summary>
            /// The current user
            /// </summary>
            CurrentUser,
            /// <summary>
            /// The current configuration
            /// </summary>
            CurrentConfig,
            /// <summary>
            /// The classes root
            /// </summary>
            ClassesRoot
        }

        private readonly RegistryKey _Where = Registry.CurrentUser;



        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryUtil"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public RegistryUtil(RegSource source)
        {
            switch (source)
            {
                case RegSource.ClassesRoot:
                    _Where = Registry.ClassesRoot;
                    break;
                case RegSource.CurrentConfig:
                    _Where = Registry.CurrentConfig;
                    break;
                case RegSource.CurrentUser:
                    _Where = Registry.CurrentUser;
                    break;
                case RegSource.LocalMachine:
                    _Where = Registry.LocalMachine;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryUtil"/> class.
        /// </summary>
        public RegistryUtil()
        {
        }

        /// <summary>
        ///     Checks if the registry entry at the given path exists or not
        /// </summary>
        /// <param name="path">path of the registry</param>
        /// <returns>True/False</returns>
        public bool CheckRegistry(string path)
        {
            try
            {
                var regkey = _Where.OpenSubKey(path);
                if (regkey == null)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Reads the registry value at the given path with the given key name
        /// </summary>
        /// <param name="path">Path of the registry entry</param>
        /// <param name="key">name of the key</param>
        /// <param name="defaultValue">valor por defecto</param>
        /// <returns>Value of the key</returns>
        public string GetValue(string path, string key, string defaultValue)
        {
            try
            {
                var regkey = _Where.OpenSubKey(path);
                if (regkey != null)
                {
                    var RegValue = regkey.GetValue(key, defaultValue).ToString();
                    if (string.IsNullOrEmpty(RegValue))
                        return defaultValue;
                    return RegValue;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        ///     Edits the registry at the given path with the new value
        /// </summary>
        /// <param name="path">registry path</param>
        /// <param name="key">key value</param>
        /// <param name="value">new value</param>
        /// <returns>bool</returns>
        public bool SetValue(string path, string key, string value)
        {
            try
            {
                var regkey = _Where.OpenSubKey(path, true);
                //RegistrySecurity rs = regkey.GetAccessControl(AccessControlSections.Access);
                //rs.SetGroup(new NTAccount("Administrators"));
                //rs.SetOwner(new NTAccount("Administrators"));
                //rs.SetAccessRuleProtection(false, false);
                //regkey.SetAccessControl(rs);
                regkey.SetValue(key, value, RegistryValueKind.String);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Creates a sub key
        /// </summary>
        /// <param name="path">Registry subkey path to create</param>
        /// <returns>bool</returns>
        public bool CreateSubKey(string path)
        {
            try
            {
                _Where.CreateSubKey(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Deletes the registry entry at the given path
        /// </summary>
        /// <param name="path">path of the registry</param>
        /// <returns>bool</returns>
        public bool DeleteRegistry(string path)
        {
            try
            {
                _Where.DeleteSubKey(path);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}