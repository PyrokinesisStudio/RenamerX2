﻿using IndexerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RenamerX
{
    internal static class Worker
    {
        public static List<FileInfo> Files { get; private set; }
        public static List<FolderInfo> Folders { get; private set; }

        public static void Load(string[] items)
        {
            Files = new List<FileInfo>();
            Folders = new List<FolderInfo>();

            foreach (string item in items)
            {
                if (File.Exists(item))
                {
                    Files.Add(new FileInfo(item));
                }
                else if (Directory.Exists(item))
                {
                    Folders.Add(new FolderInfo(item));
                }
            }
        }

        public static void Run(WorkerConfig config)
        {
            if (Program.Config.Files)
            {
                foreach (FileInfo fi in Files)
                {
                    switch (Program.Config.OperationType)
                    {
                        case OperationType.Append:
                            File.Move(fi.FullName, Path.Combine(Path.GetDirectoryName(fi.FullName), Path.GetFileNameWithoutExtension(fi.FullName) + config.OutputText) + Path.GetExtension(fi.FullName));
                            break;
                        case OperationType.Prepend:
                            File.Move(fi.FullName, Path.Combine(Path.GetDirectoryName(fi.FullName), config.OutputText + Path.GetFileNameWithoutExtension(fi.FullName)) + Path.GetExtension(fi.FullName));
                            break;
                        case OperationType.Replace:
                            string fileNameNew = Regex.Replace(fi.FullName, config.InputText, config.OutputText);
                            File.Move(fi.FullName, Path.Combine(Path.GetDirectoryName(fi.FullName), fileNameNew));
                            break;
                    }
                }
            }

            if (Program.Config.Folders)
            {
                foreach (FolderInfo di in Folders)
                {
                    switch (Program.Config.OperationType)
                    {
                        case OperationType.Append:
                            Directory.Move(di.FolderPath, di.FolderPath + config.InputText);
                            break;
                        case OperationType.Prepend:
                            Directory.Move(di.FolderPath, config.OutputText + di.FolderPath);
                            break;
                        case OperationType.Replace:
                            string dirNameNew = Regex.Replace(di.FolderName, config.InputText, config.OutputText);
                            Directory.Move(di.FolderPath, Path.Combine(Path.GetDirectoryName(di.FolderPath), dirNameNew));
                            break;
                    }
                }
            }
        }
    }

    internal class WorkerConfig
    {
        internal string InputText { get; set; }
        internal string OutputText { get; set; }
    }
}