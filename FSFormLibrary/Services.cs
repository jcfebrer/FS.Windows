#if NET40_OR_GREATER || NETCOREAPP

using FSException;
using FSTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;

namespace FSFormLibrary
{
    /// <summary>
    /// Fucniones para el uso de los servicios de windows
    /// </summary>
    public class Services
    {
        /// <summary>
        /// Devuelve true/false si el servicio existe o no.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool ExistsService(string serviceName)
        {
            return ExistsService(serviceName, "");
        }

        /// <summary>
        /// Devuelve true/false si el servicio existe o no.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static bool ExistsService(string serviceName, string machineName)
        {
            ServiceController[] ctl;
            if (String.IsNullOrEmpty(machineName))
                ctl = ServiceController.GetServices();
            else
                ctl = ServiceController.GetServices(machineName);

            ServiceController sc = ctl.FirstOrDefault(s => s.ServiceName.ToLower() == serviceName.ToLower());
            if (sc == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Stops the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// or
        /// El servicio no se encuentra ejecutandose.
        /// </exception>
        public static void Stop(string serviceName)
        {
            Stop(serviceName, "");
        }

        /// <summary>
        /// Stops the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// or
        /// El servicio no se encuentra ejecutandose.
        /// </exception>
        public static void Stop(string serviceName, string machineName)
        {
            if (!ExistsService(serviceName)) throw new ExceptionUtil("Servicio inexistente: " + serviceName);

            ServiceController sc;
            if (String.IsNullOrEmpty(machineName))
                sc = new ServiceController(serviceName);
            else
                sc = new ServiceController(serviceName, machineName);

            if (sc.Status == ServiceControllerStatus.Running)
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));

                Log.TraceInfo("Servicio detenido: " + serviceName);
            }
            else
            {
                throw new ExceptionUtil("El servicio no se encuentra ejecutandose: " + serviceName);
            }
        }

        /// <summary>
        /// Starts the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// o
        /// El servicio ya se encuentra iniciado.
        /// </exception>
        public static void Start(string serviceName)
        {
            Start(serviceName, "");
        }

        /// <summary>
        /// Starts the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// o
        /// El servicio ya se encuentra iniciado.
        /// </exception>
        public static void Start(string serviceName, string machineName)
        {
            if (!ExistsService(serviceName)) throw new ExceptionUtil("Servicio inexistente: " + serviceName);

            ServiceController sc;
            if (String.IsNullOrEmpty(machineName))
                sc = new ServiceController(serviceName);
            else
                sc = new ServiceController(serviceName, machineName);

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                sc.Start();
                //new SystemInfo().StartService(serviceName);
                sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));

                Log.TraceInfo("Servicio iniciado: " + serviceName);
            }
            else
            {
                throw new ExceptionUtil("El servicio ya se encuentra iniciado: " + serviceName);
            }
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <returns></returns>
        public static string ListServices()
        {
            return ListServices("");
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static string ListServices(string machineName)
        {
            StringBuilder sb = new StringBuilder();
            ServiceController[] services;

            if (String.IsNullOrEmpty(machineName))
                services = ServiceController.GetServices();
            else
                services = ServiceController.GetServices(machineName);

            foreach (ServiceController sc in services)
            {
                sb.AppendLine(sc.ServiceName + ":" + sc.Status.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <returns></returns>
        public static List<ServiceInfo> GetServices()
        {
            return GetServices("");
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static List<ServiceInfo> GetServices(string machineName)
        {
            List<ServiceInfo> serviceData = new List<ServiceInfo>();
            ServiceController[] services;

            if (String.IsNullOrEmpty(machineName))
                services = ServiceController.GetServices();
            else
                services = ServiceController.GetServices(machineName);

            foreach (ServiceController sc in services)
            {
                ServiceInfo sd = new ServiceInfo();
                sd.ServiceName = sc.ServiceName;
                sd.Status = (ServiceInfo.ServiceStatus)sc.Status;
                sd.DisplayName = sc.DisplayName;
                sd.MachineName = sc.MachineName;
                sd.ServiceType = sc.ServiceType.ToString();
                serviceData.Add(sd);
            }
            return serviceData;
        }

        /// <summary>
        /// Información de servicio
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static ServiceInfo GetService(string serviceName)
        {
            return GetService(serviceName, "");
        }

        /// <summary>
        /// Información de servicio
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static ServiceInfo GetService(string serviceName, string machineName)
        {
            if (!ExistsService(serviceName)) throw new ExceptionUtil("Servicio inexistente: " + serviceName);

            ServiceController sc;
            if (String.IsNullOrEmpty(machineName))
                sc = new ServiceController(serviceName);
            else
                sc = new ServiceController(serviceName, machineName);

            ServiceInfo serviceInfo = new ServiceInfo();
            serviceInfo.ServiceName = sc.ServiceName;
            serviceInfo.Status = (ServiceInfo.ServiceStatus)sc.Status;
            serviceInfo.DisplayName = sc.DisplayName;
            serviceInfo.MachineName = sc.MachineName;
            serviceInfo.Description = GetServiceDescription(serviceName);
            serviceInfo.ServiceType = sc.ServiceType.ToString();
            return serviceInfo;
        }

        /// <summary>
        /// Devuelve la descripción de un servicio
        /// </summary>
        /// <param name="serviceName">Nombre del servicio</param>
        /// <returns></returns>
        public static string GetServiceDescription(string serviceName)
        {
            try
            {
                using (ManagementObject service = new ManagementObject(new ManagementPath(string.Format("Win32_Service.Name='{0}'", serviceName))))
                {
                    object description = service["Description"];
                    if (description != null)
                    {
                        return description.ToString();
                    }
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Información del servicio
    /// </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// Estado del servicio
        /// </summary>
        public enum ServiceStatus
        {
            /// <summary>
            /// Pendiente de continuar
            /// </summary>
            ContinuePending = 5,
            /// <summary>
            /// Pausado
            /// </summary>
            Paused = 7,
            /// <summary>
            /// Pendiente de pausa
            /// </summary>
            PausePending = 6,
            /// <summary>
            /// Ejecutandose
            /// </summary>
            Running = 4,
            /// <summary>
            /// Pendiente de iniciar
            /// </summary>
            StartPending = 2,
            /// <summary>
            /// Parado
            /// </summary>
            Stopped = 1,
            /// <summary>
            /// Pendiente de parar
            /// </summary>
            StopPending = 3
        }

        /// <summary>
        /// Nombre del servicio
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Estado del servicio
        /// </summary>
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Descripción del servicio
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Nombre para mostrar
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Nombre de la máquina
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// Tipo de servicio
        /// </summary>
        public string ServiceType { get; set; }
    }
}

#endif