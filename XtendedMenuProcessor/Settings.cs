﻿using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using XtendedMenu.Properties;
using static XtendedMenu.SendMessage;

namespace XtendedMenu
{
    public partial class Settings : Form
    {
        private static RegistryKey key;
        private const string SoftwareXtendedMenu = "SOFTWARE\\XtendedMenu\\Settings";
        private bool AllFilesCheckBoxesChecked = true;
        private bool ShortCheckBoxesChecked = true;
        private bool DirectoriesCheckBoxesChecked = true;
        private bool DirBackgroundCheckBoxesChecked = true;
        private object OpenNotepadFiles;
        private object BlockWithFirewallFiles;
        private object CopyNameFiles;
        private object CopyPathFiles;
        private object CopyURLFiles;
        private object CopyLongPathFiles;
        private object AttributesFiles;
        private object SymlinkFiles;
        private object TakeOwnershipFiles;
        private object AttributesShort;
        private object OpenNotepadShort;
        private object SystemFoldersDirectoryBack;
        private object CopyNameShortFiles;
        private object CopyPathShortFiles;
        private object CopyURLShortFiles;
        private object CopyLongPathShortFiles;
        private object BlockWithFirewallDirectory;
        private object CopyNameDirectory;
        private object CopyPathDirectory;
        private object CopyURLDirectory;
        private object CopyLongPathDirectory;
        private object AttributesDirectory;
        private object SymlinkDirectory;
        private object TakeOwnershipDirectory;
        private object AttributesDirectoryBack;
        private object CommandLinesDirectoryBack;
        private object FindWallpaperDirectoryBack;
        private object PasteContentsDirectoryBack;

        public Settings()
        {
            InitializeComponent();

            key = Registry.CurrentUser.CreateSubKey(SoftwareXtendedMenu);
            key = Registry.CurrentUser.OpenSubKey(SoftwareXtendedMenu, true);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            label1.Text = Resources.Version + version;

            RegistryKey subKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\XtendedMenu");
            if (subKey == null)
            {
                StartProcess.StartInfo(GetAssembly.AssemblyInformation("directory") + @"\XtendedMenu.exe", "-install", false, true);
                Environment.Exit(0);
            }
            GetSettings();
        }
        private void GetSettings()
        {
            try
            {
                if (key != null)
                {
                    OpenNotepadFiles = key.GetValue("OpenNotepadFiles");
                    BlockWithFirewallFiles = key.GetValue("BlockWithFirewallFiles");
                    CopyNameFiles = key.GetValue("CopyNameFiles");
                    CopyPathFiles = key.GetValue("CopyPathFiles");
                    CopyURLFiles = key.GetValue("CopyURLFiles");
                    CopyLongPathFiles = key.GetValue("CopyLongPathFiles");
                    AttributesFiles = key.GetValue("AttributesFiles");
                    SymlinkFiles = key.GetValue("SymlinkFiles");
                    TakeOwnershipFiles = key.GetValue("TakeOwnershipFiles");
                    AttributesShort = key.GetValue("AttributesShort");
                    OpenNotepadShort = key.GetValue("OpenNotepadShort");
                    SystemFoldersDirectoryBack = key.GetValue("SystemFoldersDirectoryBack");
                    CopyNameShortFiles = key.GetValue("CopyNameShortFiles");
                    CopyPathShortFiles = key.GetValue("CopyPathShortFiles");
                    CopyURLShortFiles = key.GetValue("CopyURLShortFiles");
                    CopyLongPathShortFiles = key.GetValue("CopyLongPathShortFiles");
                    PasteContentsDirectoryBack = key.GetValue("PasteContentsDirectoryBack");

                    if (OpenNotepadFiles != null)
                    {
                        if (OpenNotepadFiles.ToString() == "1")
                        {
                            NotepadCheckBox.Checked = true;
                        }
                    }
                    if (BlockWithFirewallFiles != null)
                    {
                        if (BlockWithFirewallFiles.ToString() == "1")
                        {
                            BlockWithFirewallCheckBox.Checked = true;
                        }
                    }
                    if (CopyNameFiles != null)
                    {
                        if (CopyNameFiles.ToString() == "1")
                        {
                            CopyFileNameCheckBox.Checked = true;
                        }
                    }
                    if (CopyPathFiles != null)
                    {
                        if (CopyPathFiles.ToString() == "1")
                        {
                            CopyFilePathCheckBox.Checked = true;
                        }
                    }
                    if (CopyURLFiles != null)
                    {
                        if (CopyURLFiles.ToString() == "1")
                        {
                            CopyURLFilesCheckBox.Checked = true;
                        }
                    }
                    if (CopyLongPathFiles != null)
                    {
                        if (CopyLongPathFiles.ToString() == "1")
                        {
                            CopyLongPathFilesCheckBox.Checked = true;
                        }
                    }
                    if (AttributesFiles != null)
                    {
                        if (AttributesFiles.ToString() == "1")
                        {
                            FileAttributesCheckBox.Checked = true;
                        }
                    }
                    if (SymlinkFiles != null)
                    {
                        if (SymlinkFiles.ToString() == "1")
                        {
                            FileSymLinkCheckBox.Checked = true;
                        }
                    }
                    if (TakeOwnershipFiles != null)
                    {
                        if (TakeOwnershipFiles.ToString() == "1")
                        {
                            TakeOwnershipFileCheckBox.Checked = true;
                        }
                    }
                    if (AttributesShort != null)
                    {
                        if (AttributesShort.ToString() == "1")
                        {
                            AttributesShortCheckbox.Checked = true;
                        }
                    }
                    if (OpenNotepadShort != null)
                    {
                        if (OpenNotepadShort.ToString() == "1")
                        {
                            ShortNotepadCheckbox.Checked = true;
                        }
                    }
                    if (CopyNameShortFiles != null)
                    {
                        if (CopyNameShortFiles.ToString() == "1")
                        {
                            CopyNameShortCheckbox.Checked = true;
                        }
                    }
                    if (CopyPathShortFiles != null)
                    {
                        if (CopyPathShortFiles.ToString() == "1")
                        {
                            CopyPathShortCheckbox.Checked = true;
                        }
                    }
                    if (CopyURLShortFiles != null)
                    {
                        if (CopyURLShortFiles.ToString() == "1")
                        {
                            CopyURLShortCheckbox.Checked = true;
                        }
                    }
                    if (CopyLongPathShortFiles != null)
                    {
                        if (CopyLongPathShortFiles.ToString() == "1")
                        {
                            CopyLongPathShortCheckbox.Checked = true;
                        }
                    }
                    GetSettingsFinal(key);
                }
                else
                {
                    SetRegistryItems.SetItems();
                }
            }
            catch (Win32Exception ex)
            {
                MessageForm(ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + ex.GetBaseException() + Environment.NewLine + ex.TargetSite, "XtendedMenu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetSettingsFinal(RegistryKey key)
        {
            BlockWithFirewallDirectory = key.GetValue("BlockWithFirewallDirectory");
            CopyNameDirectory = key.GetValue("CopyNameDirectory");
            CopyPathDirectory = key.GetValue("CopyPathDirectory");
            CopyURLDirectory = key.GetValue("CopyURLDirectory");
            CopyLongPathDirectory = key.GetValue("CopyLongPathDirectory");
            AttributesDirectory = key.GetValue("AttributesDirectory");
            SymlinkDirectory = key.GetValue("SymlinkDirectory");
            TakeOwnershipDirectory = key.GetValue("TakeOwnershipDirectory");
            AttributesDirectoryBack = key.GetValue("AttributesDirectoryBack");
            CommandLinesDirectoryBack = key.GetValue("CommandLinesDirectoryBack");
            FindWallpaperDirectoryBack = key.GetValue("FindWallpaperDirectoryBack");
            SystemFoldersDirectoryBack = key.GetValue("SystemFoldersDirectoryBack");
            PasteContentsDirectoryBack = key.GetValue("PasteContentsDirectoryBack");
            if (BlockWithFirewallDirectory != null)
            {
                if (BlockWithFirewallDirectory.ToString() == "1")
                {
                    BlockFirewallDirectoryCheckBox.Checked = true;
                }
            }
            if (CopyNameDirectory != null)
            {
                if (CopyNameDirectory.ToString() == "1")
                {
                    CopyNameDirectoryCheckbox.Checked = true;
                }
            }
            if (CopyPathDirectory != null)
            {
                if (CopyPathDirectory.ToString() == "1")
                {
                    CopyPathDirectoryCheckbox.Checked = true;
                }
            }
            if (CopyURLDirectory != null)
            {
                if (CopyURLDirectory.ToString() == "1")
                {
                    CopyURLDirectoryCheckbox.Checked = true;
                }
            }
            if (CopyLongPathDirectory != null)
            {
                if (CopyLongPathDirectory.ToString() == "1")
                {
                    CopyLongPathDirectoryCheckbox.Checked = true;
                }
            }
            if (AttributesDirectory != null)
            {
                if (AttributesDirectory.ToString() == "1")
                {
                    AttributesDirectoryCheckbox.Checked = true;
                }
            }
            if (SymlinkDirectory != null)
            {
                if (SymlinkDirectory.ToString() == "1")
                {
                    SymLinkDirectoryCheckbox.Checked = true;
                }
            }
            if (TakeOwnershipDirectory != null)
            {
                if (TakeOwnershipDirectory.ToString() == "1")
                {
                    TakeOwnershipDirectoryCheckbox.Checked = true;
                }
            }
            if (AttributesDirectoryBack != null)
            {
                if (AttributesDirectoryBack.ToString() == "1")
                {
                    DirBackAttributesCheckbox.Checked = true;
                }
            }
            if (CommandLinesDirectoryBack != null)
            {
                if (CommandLinesDirectoryBack.ToString() == "1")
                {
                    DirBackComLinesCheckbox.Checked = true;
                }
            }
            if (SystemFoldersDirectoryBack != null)
            {
                if (SystemFoldersDirectoryBack.ToString() == "1")
                {
                    SystemFoldersCheckbox.Checked = true;
                }
            }
            if (FindWallpaperDirectoryBack != null)
            {
                if (FindWallpaperDirectoryBack.ToString() == "1")
                {
                    DirBackWallpaperCheckbox.Checked = true;
                }
            }
            if (PasteContentsDirectoryBack != null)
            {
                if (PasteContentsDirectoryBack.ToString() == "1")
                {
                    PasteContentsCheckbox.Checked = true;
                }
            }
            CheckBoxCheck();
        }
        private void CheckBoxCheck()
        {
            foreach (CheckBox checkbox in tabPage1.Controls)
            {
                if (!checkbox.Checked && checkbox.Text != Resources.SelectAll)
                {
                    AllFilesCheckBoxesChecked = false;
                }
            }
            foreach (CheckBox checkbox in tabPage2.Controls)
            {
                if (!checkbox.Checked && checkbox.Text != Resources.SelectAll)
                {
                    ShortCheckBoxesChecked = false;
                }
            }
            foreach (CheckBox checkbox in tabPage3.Controls)
            {
                if (!checkbox.Checked && checkbox.Text != Resources.SelectAll)
                {
                    DirectoriesCheckBoxesChecked = false;
                }
            }
            foreach (CheckBox checkbox in tabPage4.Controls)
            {
                if (!checkbox.Checked && checkbox.Text != Resources.SelectAll)
                {
                    DirBackgroundCheckBoxesChecked = false;
                }
            }
            if (AllFilesCheckBoxesChecked)
            {
                AllFilesSelectAllCheckbox.Checked = true;
            }
            if (ShortCheckBoxesChecked)
            {
                ShortSelectAllCheckbox.Checked = true;
            }
            if (DirectoriesCheckBoxesChecked)
            {
                DirSelectAllCheckbox.Checked = true;
            }
            if (DirBackgroundCheckBoxesChecked)
            {
                DirBackSelectAllCheckbox.Checked = true;
            }
        }
        // All Files
        private void NotepadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NotepadCheckBox.Checked)
            {
                key.SetValue("OpenNotepadFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("OpenNotepadFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void BlockWithFirewallCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BlockWithFirewallCheckBox.Checked)
            {
                key.SetValue("BlockWithFirewallFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("BlockWithFirewallFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyFileNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyFileNameCheckBox.Checked)
            {
                key.SetValue("CopyNameFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyNameFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyFilePathCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyFilePathCheckBox.Checked)
            {
                key.SetValue("CopyPathFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyPathFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyURLFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyURLFilesCheckBox.Checked)
            {
                key.SetValue("CopyURLFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyURLFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyLongPathFiles_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyLongPathFilesCheckBox.Checked)
            {
                key.SetValue("CopyLongPathFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyLongPathFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void FileAttributesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FileAttributesCheckBox.Checked)
            {
                key.SetValue("AttributesFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("AttributesFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void FileSymLinkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FileSymLinkCheckBox.Checked)
            {
                key.SetValue("SymlinkFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("SymlinkFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void TakeOwnershipFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TakeOwnershipFileCheckBox.Checked)
            {
                key.SetValue("TakeOwnershipFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("TakeOwnershipFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }
        // All Files Short
        private void AttributesShortCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (AttributesShortCheckbox.Checked)
            {
                key.SetValue("AttributesShort", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("AttributesShort", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void ShortNotepadCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShortNotepadCheckbox.Checked)
            {
                key.SetValue("OpenNotepadShort", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("OpenNotepadShort", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyNameShortCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyNameShortCheckbox.Checked)
            {
                key.SetValue("CopyNameShortFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyNameShortFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyPathShortCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyPathShortCheckbox.Checked)
            {
                key.SetValue("CopyPathShortFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyPathShortFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyURLShortCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyURLShortCheckbox.Checked)
            {
                key.SetValue("CopyURLShortFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyURLShortFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyLongPathShortCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyLongPathShortCheckbox.Checked)
            {
                key.SetValue("CopyLongPathShortFiles", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyLongPathShortFiles", 0x00000000, RegistryValueKind.DWord);
            }
        }
        // Directories
        private void BlockFirewallDirectoryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BlockFirewallDirectoryCheckBox.Checked)
            {
                key.SetValue("BlockWithFirewallDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("BlockWithFirewallDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyNameDirectoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyNameDirectoryCheckbox.Checked)
            {
                key.SetValue("CopyNameDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyNameDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyPathDirectoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyPathDirectoryCheckbox.Checked)
            {
                key.SetValue("CopyPathDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyPathDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyURLDirectoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyURLDirectoryCheckbox.Checked)
            {
                key.SetValue("CopyURLDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyURLDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void CopyLongPathDirectoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyLongPathDirectoryCheckbox.Checked)
            {
                key.SetValue("CopyLongPathDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CopyLongPathDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void AttributesDirectoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (AttributesDirectoryCheckbox.Checked)
            {
                key.SetValue("AttributesDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("AttributesDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void SymLinkDirectoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SymLinkDirectoryCheckbox.Checked)
            {
                key.SetValue("SymlinkDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("SymlinkDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void TakeOwnershipDirectoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (TakeOwnershipDirectoryCheckbox.Checked)
            {
                key.SetValue("TakeOwnershipDirectory", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("TakeOwnershipDirectory", 0x00000000, RegistryValueKind.DWord);
            }
        }
        // Directory Background
        private void DirBackAttributesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DirBackAttributesCheckbox.Checked)
            {
                key.SetValue("AttributesDirectoryBack", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("AttributesDirectoryBack", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void DirBackComLinesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DirBackComLinesCheckbox.Checked)
            {
                key.SetValue("CommandLinesDirectoryBack", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("CommandLinesDirectoryBack", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void DirBackWallpaperCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DirBackWallpaperCheckbox.Checked)
            {
                key.SetValue("FindWallpaperDirectoryBack", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("FindWallpaperDirectoryBack", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void SystemFoldersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SystemFoldersCheckbox.Checked)
            {
                key.SetValue("SystemFoldersDirectoryBack", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("SystemFoldersDirectoryBack", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void PasteContentsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SystemFoldersCheckbox.Checked)
            {
                key.SetValue("PasteContentsDirectoryBack", 0x00000001, RegistryValueKind.DWord);
            }
            else
            {
                key.SetValue("PasteContentsDirectoryBack", 0x00000000, RegistryValueKind.DWord);
            }
        }

        private void AllFilesSelectAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (AllFilesSelectAllCheckbox.Checked)
            {
                AllFilesSelectAllCheckbox.Text = Resources.SelectNone;
                NotepadCheckBox.Checked = true;
                BlockWithFirewallCheckBox.Checked = true;
                CopyFileNameCheckBox.Checked = true;
                CopyFilePathCheckBox.Checked = true;
                CopyURLFilesCheckBox.Checked = true;
                CopyLongPathFilesCheckBox.Checked = true;
                FileAttributesCheckBox.Checked = true;
                FileSymLinkCheckBox.Checked = true;
                TakeOwnershipFileCheckBox.Checked = true;
            }
            else
            {
                AllFilesSelectAllCheckbox.Text = Resources.SelectAll;
                NotepadCheckBox.Checked = false;
                BlockWithFirewallCheckBox.Checked = false;
                CopyFileNameCheckBox.Checked = false;
                CopyFilePathCheckBox.Checked = false;
                CopyURLFilesCheckBox.Checked = false;
                CopyLongPathFilesCheckBox.Checked = false;
                FileAttributesCheckBox.Checked = false;
                FileSymLinkCheckBox.Checked = false;
                TakeOwnershipFileCheckBox.Checked = false;
            }
        }

        private void DirSelectAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DirSelectAllCheckbox.Checked)
            {
                DirSelectAllCheckbox.Text = Resources.SelectNone;
                AttributesDirectoryCheckbox.Checked = true;
                BlockFirewallDirectoryCheckBox.Checked = true;
                CopyNameDirectoryCheckbox.Checked = true;
                CopyPathDirectoryCheckbox.Checked = true;
                CopyURLDirectoryCheckbox.Checked = true;
                CopyLongPathDirectoryCheckbox.Checked = true;
                SymLinkDirectoryCheckbox.Checked = true;
                TakeOwnershipDirectoryCheckbox.Checked = true;
            }
            else
            {
                DirSelectAllCheckbox.Text = Resources.SelectAll;
                AttributesDirectoryCheckbox.Checked = false;
                BlockFirewallDirectoryCheckBox.Checked = false;
                CopyNameDirectoryCheckbox.Checked = false;
                CopyPathDirectoryCheckbox.Checked = false;
                CopyURLDirectoryCheckbox.Checked = false;
                CopyLongPathDirectoryCheckbox.Checked = false;
                SymLinkDirectoryCheckbox.Checked = false;
                TakeOwnershipDirectoryCheckbox.Checked = false;
            }
        }

        private void ShortSelectAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShortSelectAllCheckbox.Checked)
            {
                ShortSelectAllCheckbox.Text = Resources.SelectNone;
                AttributesShortCheckbox.Checked = true;
                ShortNotepadCheckbox.Checked = true;
                CopyNameShortCheckbox.Checked = true;
                CopyPathShortCheckbox.Checked = true;
                CopyURLShortCheckbox.Checked = true;
                CopyLongPathShortCheckbox.Checked = true;
            }
            else
            {
                ShortSelectAllCheckbox.Text = Resources.SelectAll;
                AttributesShortCheckbox.Checked = false;
                ShortNotepadCheckbox.Checked = false;
                CopyNameShortCheckbox.Checked = false;
                CopyPathShortCheckbox.Checked = false;
                CopyURLShortCheckbox.Checked = false;
                CopyLongPathShortCheckbox.Checked = false;
            }
        }

        private void DirBackSelectAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DirBackSelectAllCheckbox.Checked)
            {
                DirBackSelectAllCheckbox.Text = Resources.SelectNone;
                DirBackAttributesCheckbox.Checked = true;
                DirBackComLinesCheckbox.Checked = true;
                SystemFoldersCheckbox.Checked = true;
                DirBackWallpaperCheckbox.Checked = true;
                PasteContentsCheckbox.Checked = true;
            }
            else
            {
                DirBackSelectAllCheckbox.Text = Resources.SelectAll;
                DirBackAttributesCheckbox.Checked = false;
                DirBackComLinesCheckbox.Checked = false;
                SystemFoldersCheckbox.Checked = false;
                DirBackWallpaperCheckbox.Checked = false;
                PasteContentsCheckbox.Checked = false;
            }
        }
    }
}
