﻿using Microsoft.Win32;
using System;

namespace TweakUIX.Tweaks.Personalization
{
    internal class HiddenFileFolder : TweaksBase
    {
        private static readonly ErrorHelper logger = ErrorHelper.Instance;

        private const string keyName = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        private const int desiredValue = 1;

        public override string ID()
        {
            return "Show hidden files, folders and drives in File Explorer";
        }

        public override string Info()
        {
            return "";
        }

        public override bool CheckTweak()
        {
            return !(
                 RegistryHelper.IntEquals(keyName, "Hidden", desiredValue)
            );
        }

        public override bool DoTweak()
        {
            try
            {
                Registry.SetValue(keyName, "Hidden", desiredValue, RegistryValueKind.DWord);

                logger.Log("- Hidden files, folders and drives are now visible.");
                logger.Log(keyName);
                return true;
            }
            catch (Exception ex)
            { logger.Log("Could not hide files, folders and drives {0}", ex.Message); }

            return false;
        }

        public override bool UndoTweak()
        {
            try
            {
                Registry.SetValue(keyName, "Hidden", 2, RegistryValueKind.DWord);
                logger.Log("- Files, folders and drives are now hidden again.");
                return true;
            }
            catch
            { }

            return false;
        }
    }
}