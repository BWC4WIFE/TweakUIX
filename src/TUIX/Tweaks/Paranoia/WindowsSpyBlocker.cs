﻿using System.IO;
using System.Text.RegularExpressions;

namespace TweakUIX.Tweaks.Paranoia
{
    internal class WindowsSpyBlocker : TweaksBase
    {
        private static readonly ErrorHelper logger = ErrorHelper.Instance;
        private static string spyList = Helpers.Strings.Data.DataRootDir + "spy.txt";

        public override string ID()
        {
            return "*Block Windows telemetry with WindowsSpyBlocker";
        }

        public override string Info()
        {
            return "This app provide only the GUI for a third-party IP list.\nPowered by https://github.com/crazy-max/WindowsSpyBlocker";
        }

        internal static string Normalize(string line)
        {
            return Regex.Replace(line, @"\s{2,}", " ");
        }

        public override bool CheckTweak()
        {
            try
            {
                logger.Log("The IPs based on the following list will be blocked:");

                string[] ipAddr = File.ReadAllLines(spyList);

                foreach (var currentIpAddr in ipAddr)
                {
                    logger.Log(currentIpAddr.ToString());
                }

                return false;
            }
            catch
            {
                logger.Log("Feature not available. Add it via Menu > \"Add features\" ");
                return false;
            }
        }

        public override bool DoTweak()
        {
            try
            {
                string[] ipAddr = File.ReadAllLines(spyList);

                foreach (var currentIpAddr in ipAddr)
                {
                    if (!currentIpAddr.StartsWith("#") && (!string.IsNullOrEmpty(currentIpAddr)))
                    {
                        Normalize(currentIpAddr);

                        WindowsHelper.RunCmd($"/c netsh advfirewall firewall add rule name=\"{currentIpAddr}_TIW11\" dir=out interface=any action=block remoteip=\"{currentIpAddr}\"");
                    }
                }

                logger.Log("IP list has been blocked.");

                return true;
            }
            catch
            {
                logger.Log("spy.txt from https://github.com/crazy-max/WindowsSpyBlocker repository not found.\nDownload the file and copy it to \"Data\" directory of this app.");
                return false;
            }
        }

        public override bool UndoTweak()
        {
            string[] ipAddr = File.ReadAllLines(spyList);

            foreach (var currentIpAddr in ipAddr)
            {
                if (!currentIpAddr.StartsWith("#") && (!string.IsNullOrEmpty(currentIpAddr)))
                {
                    Normalize(currentIpAddr);

                    WindowsHelper.RunCmd($"/c netsh advfirewall firewall delete rule name=\"{currentIpAddr}_TIW11\"");
                }
            }

            logger.Log("IP list has been unblocked.");

            return true;
        }
    }
}