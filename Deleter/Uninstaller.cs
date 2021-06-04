﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using XtendedMenu;
using XtendedMenu.Properties;

namespace Deleter
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            try
            {
                Icon = Resources.MAIN_ICON_256;
                InitializeComponent();

                try
                {
                    Process[] chrome = Process.GetProcessesByName("chrome");
                    Process[] OUTLOOK = Process.GetProcessesByName("OUTLOOK");

                    if (chrome.Length > 0 && OUTLOOK.Length > 0)
                    {
                        SendMessage.MessageForm("It appears that Chrome and Outlook are currently running and may lock some files. Please close them and press OK to continue. You may stil need to reboot after uninstallation!", "XtendedMenu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (chrome.Length > 0)
                    {
                        SendMessage.MessageForm("It appears that Chrome is currently running and may lock some files. Please close Chrome and press OK to continue. You may stil need to reboot after uninstallation!", "XtendedMenu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (OUTLOOK.Length > 0)
                    {
                        SendMessage.MessageForm("It appears that Outlook is currently running and may lock some files. Please close Outlook and press OK to continue. You may stil need to reboot after uninstallation!", "XtendedMenu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (args.Length > 0)
                {
                    Thread t = new Thread(() => DELETER(args[0]))
                    {
                        IsBackground = true
                    };
                    t.Start();
                }
                else
                {
                    Environment.Exit(0);
                }

                TopMost = false;

                WindowState = FormWindowState.Normal;
            }
            catch
            {
            }
        }

        private void DELETER(string directory)
        {
            try
            {
                Thread.Sleep(5000);
                while (Directory.Exists(directory))
                {
                    try
                    {
                        Directory.Delete(directory, true);
                    }
                    catch
                    {
                        break;
                    }

                    Thread.Sleep(1000);
                }

                try
                {
                    using (StreamWriter sw = File.CreateText(Path.GetTempPath() + "Deleter.bat"))
                    {
                        sw.WriteLine("timeout 5");
                        sw.WriteLine("del " + "\"" + Path.GetTempPath() + "Deleter.exe" + "\" /f /q");
                        sw.WriteLine("del " + "\"" + Path.GetTempPath() + "Deleter.bat" + "\" /f /q");
                        sw.WriteLine("pause");
                    }
                    using (Process p = new Process())
                    {
                        p.StartInfo.FileName = Path.GetTempPath() + "Deleter.bat";
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                    }
                }
                catch
                {
                }
            }
            catch
            {
            }

            if (File.Exists(directory + "\\XtendedMenu.exe") || File.Exists(directory + "\\XtendedMenu.dll") || File.Exists(directory + "\\SharpShell.dll"))
            {
                MessageBox.Show("It appears that a program is locking some files and preventing them from being deleted. You will need to restart your computer to complete the uninstallation or manually delete it.", "XtendedMenu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Environment.Exit(0);
        }
    }
}
