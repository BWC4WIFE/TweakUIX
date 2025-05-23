﻿using Microsoft.Win32;
using System;

namespace TweakUIX.Tweaks.Personalization
{
    internal class TaskbarSearch : TweaksBase
    {
        private static readonly ErrorHelper logger = ErrorHelper.Instance;

        private const string keyName = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search";
        private const int desiredValue = 0;

        public override string ID()
        {
            return "Hide Search icon on taskbar";
        }

        public override string Info()
        {
            return "";
        }

        public override bool CheckTweak()
        {
            return !(
                 RegistryHelper.IntEquals(keyName, "SearchboxTaskbarMode", desiredValue)
            );
        }

        public override bool DoTweak()
        {
            try
            {
                Registry.SetValue(keyName, "SearchboxTaskbarMode", desiredValue, RegistryValueKind.DWord);

                logger.Log("- Search icon has been disabled.");
                logger.Log(keyName);
                return true;
            }
            catch (Exception ex)
            { logger.Log("Could not disable search icon {0}", ex.Message); }

            return false;
        }

        public override bool UndoTweak()
        {
            try
            {
                Registry.SetValue(keyName, "SearchboxTaskbarMode", 1, RegistryValueKind.DWord);
                logger.Log("- Search icon has been enabled.");
                return true;
            }
            catch
            { }

            return false;
        }
    }
}