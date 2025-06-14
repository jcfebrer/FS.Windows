using System; 
using System.Collections;
using FSException;
using NetFwTypeLib; /* c:\windows\system32\FirewallAPI.dll */


namespace FSFirewall
{

    public class FirewallUtil
    {
        /// <summary>
        /// Añade una regla al firewall.
        /// </summary>
        /// <param name="name">Nombre del ejecutable</param>
        /// <param name="executablePath">Path al executable.</param>
        public static void AddAppRule(string name, string executablePath)
        {
            if (ExistsRule(name))
                return;

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            INetFwRule2 firewallRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule.Enabled = true;
            firewallRule.InterfaceTypes = "All";
            firewallRule.ApplicationName = executablePath;
            firewallRule.Name = name;
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;

            firewallPolicy.Rules.Add(firewallRule);
        }

        public static void AddPortRule(string name, int port)
        {
            AddPortRule(name, port.ToString());
        }

        public static void AddPortRule(string name, string ports)
        {
            if (ExistsRule(name))
                return;

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            var currentProfiles = firewallPolicy.CurrentProfileTypes;

            INetFwRule2 firewallRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule.Enabled = true;
            firewallRule.InterfaceTypes = "All";
            firewallRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            firewallRule.LocalPorts = ports;
            firewallRule.Name = name;
            firewallRule.Profiles = currentProfiles;
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;

            firewallPolicy.Rules.Add(firewallRule);
        }

        /// <summary>
        /// Elimina una regla del firewall.
        /// </summary>
        /// <param name="appName">Nombre del ejecutable</param>
        public static void RemoveRule(string name)
        {
            if (!ExistsRule(name))
                throw new ExceptionUtil("Regla no existente.");

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Remove(name);
        }


        public static bool ExistsRule(string name)
        {
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            foreach (INetFwRule rule in firewallPolicy.Rules)
            {
                if (rule.Name.ToLower() == name.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}