﻿using System;
using System.Windows;
using System.Reflection;

namespace ANEffectsLibrary
{
    internal static class Global
    {
        public static Uri MakePackUri(string relativeFile)
        {
            string uriString = "pack://application:,,,/" + AssemblyShortName + ";component/" + relativeFile;
            return new Uri(uriString);
        }

        private static string AssemblyShortName
        {
            get
            {
                if (_assemblyShortName == null)
                {
                    Assembly a = typeof(Global).Assembly;
                    _assemblyShortName = a.ToString().Split(',')[0];
                }
                return _assemblyShortName;
            }
        }

        private static string _assemblyShortName;
    }
}