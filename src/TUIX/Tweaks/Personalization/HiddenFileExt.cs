﻿using Microsoft.Win32;
using System;

namespace TweakUIX.Tweaks.Personalization
{
    internal class HiddenFileExt : TweaksBase
    {
        private static readonly ErrorHelper logger = ErrorHelper.Instance;

        private const string keyName = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        private const int desiredValue = 0;

        public override string ID()
        {
            return "Show hidden file name extensions";
        }

        public override string Info()
        {
            return "";
        }

        public override bool CheckTweak()
        {
            return !(
                 RegistryHelper.IntEquals(keyName, "HideFileExt", desiredValue)
            );
        }

        public override bool DoTweak()
        {
            try
            {
                Registry.SetValue(keyName, "HideFileExt", desiredValue, RegistryValueKind.DWord);

                logger.Log("- File name extensions are now visible.");
                logger.Log(keyName);
                return true;
            }
            catch (Exception ex)
            { logger.Log("Could not hide file name extensions {0}", ex.Message); }

            return false;
        }

        public override bool UndoTweak()
        {
            try
            {
                Registry.SetValue(keyName, "HideFileExt", 1, RegistryValueKind.DWord);
                logger.Log("- File name extensions are now hidden again.");
                return true;
            }
            catch
            { }

            return false;
        }
    }
}