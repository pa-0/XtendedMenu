﻿using Microsoft.Win32;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using XtendedMenu.Methods;
using XtendedMenu.Properties;

[assembly: CLSCompliant(false)]
namespace XtendedMenu
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFiles)]
    [COMServerAssociation(AssociationType.ClassOfExtension, ".lnk")]
    [DisplayName("XtendedMenu")]
#pragma warning disable IDE1006 // Naming Styles
    public class XtendedMenuFiles : SharpContextMenu
#pragma warning restore IDE1006 // Naming Styles
    {
        private static readonly RegistryKey XtendedMenuSettings = Registry.CurrentUser.OpenSubKey("SOFTWARE\\XtendedMenu\\Settings", true);
        private ContextMenuStrip Menu;
        private ToolStripMenuItem XtendedMenuMenu, OpenNotepad, BlockFirewall, CopyName, CopyPath, CopyPathURL, CopyLONGPath, Attributes, SymLink, TakeOwnership;
        private ToolStripMenuItem AttributesMenu, HiddenAttributes, SystemAttributes, ReadOnlyAttributes;
        protected override bool CanShowMenu()
        {
            return true;
        }
        protected override ContextMenuStrip CreateMenu()
        {
            CheckUserSettings();
            // Main Menu
            using (Menu = new ContextMenuStrip())
            {
                Menu.Name = "XtendedMenuFiles";

                using (XtendedMenuMenu = new ToolStripMenuItem())
                {
                    XtendedMenuMenu.Name = "XtendedMenuMenu";

                    // OpenNotepad
                    using (OpenNotepad = new ToolStripMenuItem())
                    {
                        OpenNotepad.Text = Resources.OpenNotepad;
                        OpenNotepad.Name = "OpenNotepad";
                    }
                    // BlockFirewall
                    using (BlockFirewall = new ToolStripMenuItem())
                    {
                        BlockFirewall.Text = Resources.BlockText;
                        BlockFirewall.Name = "BlockFirewall";
                    }
                    // CopyName
                    using (CopyName = new ToolStripMenuItem())
                    {
                        CopyName.Text = Resources.CopyNameText;
                        CopyName.Name = "CopyName";
                    }
                    // CopyPath
                    using (CopyPath = new ToolStripMenuItem())
                    {
                        CopyPath.Text = Resources.CopyPathText;
                        CopyPath.Name = "CopyPath";
                    }
                    // CopyPathURL
                    using (CopyPathURL = new ToolStripMenuItem())
                    {
                        CopyPathURL.Text = Resources.CopyPathURLText;
                        CopyPathURL.Name = "CopyPathURL";
                    }
                    // CopyLONGPath
                    using (CopyLONGPath = new ToolStripMenuItem())
                    {
                        CopyLONGPath.Text = Resources.CopyLONGPathText;
                        CopyLONGPath.Name = "CopyLONGPath";
                    }
                    // Attributes
                    using (Attributes = new ToolStripMenuItem())
                    {
                        Attributes.Name = "Attributes";

                        Attributes.Text = Resources.AttributesText;
                        // AttributesMenu
                        using (AttributesMenu = new ToolStripMenuItem())
                        {
                            AttributesMenu.Text = Resources.AttributesMenu;
                            AttributesMenu.Name = "AttributesMenu";
                        }
                        // Get : Set Attributes
                        string[] SelectedPath = SelectedItemPaths.Cast<string>().ToArray();
                        if (SelectedPath.Length > 1)
                        {
                            foreach (string item in SelectedPath)
                            {
                                try
                                {
                                    AttributesInfo.GetFileAttributes(item);
                                }
                                catch (Exception ex)
                                {
                                    StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "\"" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + ex.GetBaseException() + Environment.NewLine + ex.TargetSite + "\"" + " -catchhandler");
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                AttributesInfo.GetFileAttributes(SelectedPath.ToStringArray(false));
                            }
                            catch (Exception ex)
                            {
                                StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "\"" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + ex.GetBaseException() + Environment.NewLine + ex.TargetSite + "\"" + " -catchhandler");
                            }
                        }
                        SetFileAttributes();
                    }
                    // SymLink
                    using (SymLink = new ToolStripMenuItem())
                    {
                        SymLink.Text = Resources.CreateSymbolicLink;
                        SymLink.Name = "SymLink";
                    }
                    // TakeOwnership
                    using (TakeOwnership = new ToolStripMenuItem())
                    {
                        TakeOwnership.Text = Resources.TakeOwnershipText;
                        TakeOwnership.Name = "TakeOwnership";
                    }
                }
            }
            MenuDeveloper();

            return Menu;
        }
        private static void CheckUserSettings()
        {
            if (XtendedMenuSettings == null)
            {
                RegistryKey Software = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                Software.CreateSubKey("XtendedMenu");
                RegistryKey SoftwareXtendedMenu = Registry.CurrentUser.OpenSubKey("SOFTWARE\\XtendedMenu", true);
                SoftwareXtendedMenu.CreateSubKey("Settings");

                SetRegistryItems.SetItems();
            }
        }
        private void MenuDeveloper()
        {
            // Main Menu
            Menu.Items.Add(XtendedMenuMenu);
            XtendedMenuMenu.Text = Resources.XtendedMenuName;

            // Icons
            XtendedMenuMenu.Image = Resources.MAIN_ICON.ToBitmap();
            OpenNotepad.Image = Resources.notepad.ToBitmap();
            BlockFirewall.Image = Resources.Firewall.ToBitmap();
            CopyPath.Image = Resources.CopyPath.ToBitmap();
            CopyPathURL.Image = Resources.CopyPath.ToBitmap();
            CopyLONGPath.Image = Resources.CopyPath.ToBitmap();
            CopyName.Image = Resources.CopyName.ToBitmap();
            Attributes.Image = Resources.FileAttributes.ToBitmap();
            AttributesMenu.Image = Resources.MAIN_ICON.ToBitmap();

            SymLink.Image = Resources.SymLink.ToBitmap();
            TakeOwnership.Image = Resources.TakeOwnership.ToBitmap();

            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            AddMenuItems(array);

            // Subscriptions
            OpenNotepad.Click += (sender, args) => OpenNotepadMethod();
            BlockFirewall.Click += (sender, args) => BlockFirewallMethod();
            CopyPath.Click += (sender, args) => CopyPathMethod();
            CopyPathURL.Click += (sender, args) => CopyPathURLMethod();
            CopyLONGPath.Click += (sender, args) => CopyLONGPathMethod();
            CopyName.Click += (sender, args) => CopyNameMethod();
            AttributesMenu.Click += (sender, args) => AttributesMenuMethod();

            HiddenAttributes.Click += (sender, args) => HiddenAttributesMethod();
            SystemAttributes.Click += (sender, args) => SystemAttributesMethod();
            ReadOnlyAttributes.Click += (sender, args) => ReadOnlyAttributesMethod();
            SymLink.Click += (sender, args) => SymLinkMethod();
            TakeOwnership.Click += (sender, args) => TakeOwnershipMethod();
        }

        // Add Menu Items
        private void AddMenuItems(string[] array)
        {
            try
            {
                // Disabler
                bool isShortcut = false;
                bool isExeDllFile = false;
                foreach (string path in array)
                {
                    try
                    {
                        if (Path.GetExtension(path) == ".lnk")
                        {
                            isShortcut = true;
                        }
                        if (Path.GetExtension(path) != ".exe" && Path.GetExtension(path) != ".dll")
                        {
                            isExeDllFile = false;
                        }
                        if (Path.GetExtension(path) == ".exe" || Path.GetExtension(path) == ".dll")
                        {
                            isExeDllFile = true;
                        }
                        if (Directory.Exists(ShortcutHandler.GetShortcutTarget(path)))
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        EasyLogger.Error(ex.Message + " Error at ShortcutHandler.GetShortcutTarget(path)");
                        continue;
                    }
                }
                object OpenNotepadFiles = XtendedMenuSettings.GetValue("OpenNotepadFiles");
                if (OpenNotepadFiles != null)
                {
                    if (OpenNotepadFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(OpenNotepad);
                    }
                }
                object BlockWithFirewallFiles = XtendedMenuSettings.GetValue("BlockWithFirewallFiles");
                if (BlockWithFirewallFiles != null)
                {
                    if (BlockWithFirewallFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(BlockFirewall);
                    }
                }
                object CopyNameFiles = XtendedMenuSettings.GetValue("CopyNameFiles");
                if (CopyNameFiles != null && !isShortcut)
                {
                    if (CopyNameFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyName);
                    }
                }
                object CopyPathFiles = XtendedMenuSettings.GetValue("CopyPathFiles");
                if (CopyPathFiles != null && !isShortcut)
                {
                    if (CopyPathFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyPath);
                    }
                }
                object CopyURLFiles = XtendedMenuSettings.GetValue("CopyURLFiles");
                if (CopyURLFiles != null && !isShortcut)
                {
                    if (CopyURLFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyPathURL);
                    }
                }
                object CopyLONGPathFiles = XtendedMenuSettings.GetValue("CopyLONGPathFiles");
                if (CopyLONGPathFiles != null && !isShortcut)
                {
                    if (CopyLONGPathFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyLONGPath);
                    }
                }
                object AttributesFiles = XtendedMenuSettings.GetValue("AttributesFiles");
                if (AttributesFiles != null)
                {
                    if (AttributesFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(Attributes);
                        Attributes.DropDownItems.Add(AttributesMenu);
                        Attributes.DropDownItems.Add(new ToolStripSeparator());
                        Attributes.DropDownItems.Add(HiddenAttributes);
                        Attributes.DropDownItems.Add(SystemAttributes);
                        Attributes.DropDownItems.Add(ReadOnlyAttributes);
                    }
                }
                object SymlinkFiles = XtendedMenuSettings.GetValue("SymlinkFiles");
                if (SymlinkFiles != null)
                {
                    if (SymlinkFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(SymLink);
                    }
                }
                object TakeOwnershipFiles = XtendedMenuSettings.GetValue("TakeOwnershipFiles");
                if (TakeOwnershipFiles != null)
                {
                    if (TakeOwnershipFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(TakeOwnership);
                    }
                }
                object AttributesShort = XtendedMenuSettings.GetValue("AttributesShort");
                if (AttributesShort != null && isShortcut)
                {
                    if (AttributesShort.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(Attributes);
                    }
                    else
                    {
                        Attributes.Dispose();
                    }
                }
                object OpenNotepadShort = XtendedMenuSettings.GetValue("OpenNotepadShort");
                if (OpenNotepadShort != null && isShortcut)
                {
                    if (OpenNotepadShort.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(OpenNotepad);
                    }
                    else
                    {
                        OpenNotepad.Dispose();
                    }
                }
                object CopyNameShortFiles = XtendedMenuSettings.GetValue("CopyNameShortFiles");
                if (CopyNameShortFiles != null && isShortcut)
                {
                    if (CopyNameShortFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyName);
                    }
                }
                object CopyPathShortFiles = XtendedMenuSettings.GetValue("CopyPathShortFiles");
                if (CopyPathShortFiles != null && isShortcut)
                {
                    if (CopyPathShortFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyPath);
                    }
                }
                object CopyURLShortFiles = XtendedMenuSettings.GetValue("CopyURLShortFiles");
                if (CopyURLShortFiles != null && isShortcut)
                {
                    if (CopyURLShortFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyPathURL);
                    }
                }
                object CopyLONGPathShortFiles = XtendedMenuSettings.GetValue("CopyLONGPathShortFiles");
                if (CopyLONGPathShortFiles != null && isShortcut)
                {
                    if (CopyLONGPathShortFiles.ToString() == "1")
                    {
                        XtendedMenuMenu.DropDownItems.Add(CopyLONGPath);
                    }
                }

                MenuItemDisabler(isShortcut, isExeDllFile);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "\"" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + ex.GetBaseException() + Environment.NewLine + ex.TargetSite + "\"" + " -catchhandler");
            }
        }
        private void MenuItemDisabler(bool isShortcut, bool isExeDllFile)
        {
            if (isShortcut)
            {
                if (BlockFirewall != null)
                {
                    BlockFirewall.Dispose();
                }

                if (SymLink != null)
                {
                    SymLink.Dispose();
                }

                if (TakeOwnership != null)
                {
                    TakeOwnership.Dispose();
                }
            }
            if (isExeDllFile)
            {
                if (OpenNotepad != null)
                {
                    OpenNotepad.Dispose();
                }
            }
            if (!isExeDllFile)
            {
                if (BlockFirewall != null)
                {
                    BlockFirewall.Dispose();
                }
            }
            bool AllDisabled = true;
            foreach (ToolStripMenuItem item in XtendedMenuMenu.DropDownItems)
            {
                if (item != null)
                {
                    AllDisabled = false;
                }
            }
            if (AllDisabled)
            {
                Menu.Dispose();
            }
        }
        // Set File Attributes
        private void SetFileAttributes()
        {
            using (HiddenAttributes = new ToolStripMenuItem())
            {
                HiddenAttributes.Text = Resources.HiddenAttributes;
                HiddenAttributes.Name = "HiddenAttributes";
            }
            if (AttributesInfo.hidden)
            {
                HiddenAttributes.Image = Resources.AttributesShow.ToBitmap();
            }
            // SystemAttributes
            using (SystemAttributes = new ToolStripMenuItem())
            {
                SystemAttributes.Text = Resources.SystemAttributes;
                SystemAttributes.Name = "SystemAttributes";
            }
            if (AttributesInfo.system)
            {
                SystemAttributes.Image = Resources.AttributesShow.ToBitmap();
            }
            // ReadOnlyAttributes
            using (ReadOnlyAttributes = new ToolStripMenuItem())
            {
                ReadOnlyAttributes.Text = Resources.ReadOnlyAttributes;
                ReadOnlyAttributes.Name = "ReadOnlyAttributes";
            }
            if (AttributesInfo.readOnly)
            {
                ReadOnlyAttributes.Image = Resources.AttributesShow.ToBitmap();
            }
        }
        // Methods

        public static bool HasExecutable(string path)
        {
            var executable = FindExecutable(path);
            return !string.IsNullOrEmpty(executable);
        }

        private static string FindExecutable(string path)
        {
            var executable = new StringBuilder(1024);
            FindExecutable(path, string.Empty, executable);
            return executable.ToString();
        }

        [DllImport("shell32.dll", EntryPoint = "FindExecutable")]
        private static extern long FindExecutable(string lpFile, string lpDirectory, StringBuilder lpResult);
        private void OpenNotepadMethod()
        {
            string textFile = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\XtendedMenu\\Version.txt";
            if (!File.Exists(textFile))
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\XtendedMenu");
                File.WriteAllText(textFile, version);
            }
            if (HasExecutable(textFile))
            {
                foreach (string filePath in SelectedItemPaths)
                {
                    StartProcess.StartInfo(FindExecutable(textFile), "\"" + filePath + "\"");
                }
            }
            else
            {
                foreach (string filePath in SelectedItemPaths)
                {
                    StartProcess.StartInfo("Notepad.exe", "\"" + filePath + "\"");
                }
            }
        }
        private void BlockFirewallMethod()
        {
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "\"" + array.ToStringArray() + "\" " + "-firewallfiles", false, true);
        }
        private void CopyNameMethod()
        {
            Clipboard.Clear();
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            Clipboard.SetText(array.ToStringArray(true));
        }
        private void CopyPathMethod()
        {
            Clipboard.Clear();
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            Clipboard.SetText(array.ToStringArray(false));
        }
        private void CopyPathURLMethod()
        {
            Clipboard.Clear();
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            try
            {
                Clipboard.SetText(new Uri(array.ToStringArray(false)).AbsoluteUri);
            }
            catch (Exception ex)
            {
                EasyLogger.Error(ex);
            }
        }
        private void CopyLONGPathMethod()
        {
            Clipboard.Clear();
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            Clipboard.SetText(@"\\?\" + array.ToStringArray(false));
        }
        private void AttributesMenuMethod()
        {
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "\"" + array.ToStringArray(false) + "\" " + "-attributesmenu");
        }
        private void HiddenAttributesMethod()
        {
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            foreach (string item in array)
            {
                FileAttributes attributes = File.GetAttributes(item);
                if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    attributes = AttributesInfo.RemoveAttribute(attributes, FileAttributes.Hidden);
                    File.SetAttributes(item, attributes);
                }
                else
                {
                    File.SetAttributes(item, File.GetAttributes(item) | FileAttributes.Hidden);
                }
            }
            StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "-refresh");
        }
        private void SystemAttributesMethod()
        {
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            foreach (string item in array)
            {
                FileAttributes attributes = File.GetAttributes(item);
                if ((attributes & FileAttributes.System) == FileAttributes.System)
                {
                    attributes = AttributesInfo.RemoveAttribute(attributes, FileAttributes.System);
                    File.SetAttributes(item, attributes);
                }
                else
                {
                    File.SetAttributes(item, File.GetAttributes(item) | FileAttributes.System);
                }
            }
            StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "-refresh");
        }
        private void ReadOnlyAttributesMethod()
        {
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            foreach (string item in array)
            {
                FileAttributes attributes = File.GetAttributes(item);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes = AttributesInfo.RemoveAttribute(attributes, FileAttributes.ReadOnly);
                    File.SetAttributes(item, attributes);
                }
                else
                {
                    File.SetAttributes(item, File.GetAttributes(item) | FileAttributes.ReadOnly);
                }
            }
        }
        private void SymLinkMethod()
        {
            foreach (string file in SelectedItemPaths)
            {
                try
                {
                    using (FolderBrowserDialog ofd = new FolderBrowserDialog())
                    {
                        ofd.Description = Path.GetFileName(file);
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            string PathName = ofd.SelectedPath + @"\" + Path.GetFileName(file);
                            StartProcess.StartInfo("cmd.exe", "/c mklink " + "\"" + PathName + "\"" + " " + "\"" + file + "\"", true, true, true);
                        }
                    }
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "\"" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + ex.GetBaseException() + Environment.NewLine + ex.TargetSite + "\"" + " -catchhandler");
                }
            }
        }
        private void TakeOwnershipMethod()
        {
            string[] array = SelectedItemPaths.Cast<string>().ToArray();
            StartProcess.StartInfo(AttributesInfo.GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "\"" + array.ToStringArray(false) + "\"" + " -ownership", false, true);
        }
    }
}
