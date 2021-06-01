﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace XtendedMenu
{
    public partial class CustomEntries : Form
    {
        public CustomEntries()
        {
            InitializeComponent();

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\XtendedMenu\\Settings\\CustomEntries"))
            {
                if (key.GetValue("CustomName") != null)
                {
                    if (key.GetValue("CustomName") is string)
                    {
                        EntryBox.Items.Add((string)key.GetValue("CustomName"));
                    }
                    else
                    {
                        EntryBox.Items.AddRange((string[])key.GetValue("CustomName"));
                    }
                }
            }
        }

        private void ProcessBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Program to run";
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;

                DialogResult dr = openFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    ProcessBox.Text = openFileDialog.FileName;
                    DirectoryBox.Text = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            RemoveEntry();
        }

        private void RemoveEntry()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\XtendedMenu\\Settings\\CustomEntries"))
            {
                int index = 0;
                var CustomNameList = new List<string>();
                CustomNameList.AddRange((string[])key.GetValue("CustomName"));
                string[] CustomNameArray = CustomNameList.ToArray();
                foreach (string value in CustomNameArray)
                {
                    if (value == (string)EntryBox.SelectedItem)
                    {
                        CustomNameArray = CustomNameArray.Where(w => w != value).ToArray();
                        key.SetValue("CustomName", CustomNameArray, RegistryValueKind.MultiString);


                        var CustomProcessList = new List<string>();
                        CustomProcessList.AddRange((string[])key.GetValue("CustomProcess"));
                        CustomProcessList.RemoveAt(index);
                        string[] CustomProcessArray = CustomProcessList.ToArray();
                        key.SetValue("CustomProcess", CustomProcessArray, RegistryValueKind.MultiString);

                        var CustomArgumentsList = new List<string>();
                        CustomArgumentsList.AddRange((string[])key.GetValue("CustomArguments"));
                        CustomArgumentsList.RemoveAt(index);
                        string[] CustomArgumentsArray = CustomArgumentsList.ToArray();
                        key.SetValue("CustomArguments", CustomArgumentsArray, RegistryValueKind.MultiString);


                        var CustomDirectoryList = new List<string>();
                        CustomDirectoryList.AddRange((string[])key.GetValue("CustomDirectory"));
                        CustomDirectoryList.RemoveAt(index);
                        string[] CustomDirectoryArray = CustomDirectoryList.ToArray();
                        key.SetValue("CustomDirectory", CustomDirectoryArray, RegistryValueKind.MultiString);


                        var CustomIconList = new List<string>();
                        CustomIconList.AddRange((string[])key.GetValue("CustomIcon"));
                        CustomIconList.RemoveAt(index);
                        string[] CustomIconArray = CustomIconList.ToArray();
                        key.SetValue("CustomIcon", CustomIconArray, RegistryValueKind.MultiString);


                        var CustomLocationList = new List<string>();
                        CustomLocationList.AddRange((string[])key.GetValue("CustomLocation"));
                        CustomLocationList.RemoveAt(index);
                        string[] CustomLocationArray = CustomLocationList.ToArray();
                        key.SetValue("CustomLocation", CustomLocationArray, RegistryValueKind.MultiString);
                    }

                    index++;
                }
            }
            EntryBox.Text = "";
            EntryBox.Items.Clear();
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\XtendedMenu\\Settings\\CustomEntries"))
            {
                if (key.GetValue("CustomName") != null)
                {
                    if (key.GetValue("CustomName") is string)
                    {
                        EntryBox.Items.Add((string)key.GetValue("CustomName"));
                    }
                    else
                    {
                        EntryBox.Items.AddRange((string[])key.GetValue("CustomName"));
                    }
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryBox.Text))
            {
                RemoveEntry();
            }

            if (string.IsNullOrEmpty(NameBox.Text))
            {
                MessageBox.Show("Make sure there is a Name before adding an entry.");
                return;
            }
            if (string.IsNullOrEmpty(ProcessBox.Text))
            {
                MessageBox.Show("Make sure there is a Process before adding an entry.");
                return;
            }
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\XtendedMenu\\Settings\\CustomEntries"))
            {
                // CustomName
                if (key.GetValue("CustomName") == null)
                {
                    key.SetValue("CustomName", NameBox.Text, RegistryValueKind.String);
                }
                else
                {
                    if (key.GetValue("CustomName") is string)
                    {
                        if ((string)key.GetValue("CustomName") == NameBox.Text)
                        {
                            MessageBox.Show("This Name already exists.");
                            return;
                        }
                        string CustomName = (string)key.GetValue("CustomName");

                        var myList = new List<string>
                        {
                            CustomName,
                            NameBox.Text
                        };
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomName", newArray, RegistryValueKind.MultiString);
                    }
                    else
                    {
                        var myListCheck = new List<string>();
                        myListCheck.AddRange((string[])key.GetValue("CustomName"));
                        string[] newArrayCheck = myListCheck.ToArray();
                        foreach (string value in newArrayCheck)
                        {
                            if (value == NameBox.Text)
                            {
                                MessageBox.Show("This Name already exists.");
                                return;
                            }
                        }

                        string[] CustomName = (string[])key.GetValue("CustomName");

                        var myList = new List<string>();
                        myList.AddRange(CustomName);
                        myList.Add(NameBox.Text);
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomName", newArray, RegistryValueKind.MultiString);
                    }
                }
                // CustomArguments
                if (key.GetValue("CustomArguments") == null)
                {
                    key.SetValue("CustomArguments", ArgumentsBox.Text, RegistryValueKind.String);
                }
                else
                {
                    if (key.GetValue("CustomArguments") is string)
                    {
                        string CustomArguments = (string)key.GetValue("CustomArguments");

                        var myList = new List<string>
                        {
                            CustomArguments,
                            ArgumentsBox.Text
                        };
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomArguments", newArray, RegistryValueKind.MultiString);
                    }
                    else
                    {
                        string[] CustomArguments = (string[])key.GetValue("CustomArguments");

                        var myList = new List<string>();
                        myList.AddRange(CustomArguments);
                        myList.Add(ArgumentsBox.Text);
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomArguments", newArray, RegistryValueKind.MultiString);
                    }
                }
                // CustomDirectory
                if (key.GetValue("CustomDirectory") == null)
                {
                    key.SetValue("CustomDirectory", DirectoryBox.Text, RegistryValueKind.String);
                }
                else
                {
                    if (key.GetValue("CustomDirectory") is string)
                    {
                        string CustomDirectory = (string)key.GetValue("CustomDirectory");

                        var myList = new List<string>
                        {
                            CustomDirectory,
                            DirectoryBox.Text
                        };
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomDirectory", newArray, RegistryValueKind.MultiString);
                    }
                    else
                    {
                        string[] CustomDirectory = (string[])key.GetValue("CustomDirectory");

                        var myList = new List<string>();
                        myList.AddRange(CustomDirectory);
                        myList.Add(DirectoryBox.Text);
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomDirectory", newArray, RegistryValueKind.MultiString);
                    }
                }
                // CustomProcess
                if (key.GetValue("CustomProcess") == null)
                {
                    key.SetValue("CustomProcess", ProcessBox.Text, RegistryValueKind.String);
                }
                else
                {
                    if (key.GetValue("CustomProcess") is string)
                    {
                        string CustomProcess = (string)key.GetValue("CustomProcess");

                        var myList = new List<string>
                        {
                            CustomProcess,
                            ProcessBox.Text
                        };
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomProcess", newArray, RegistryValueKind.MultiString);
                    }
                    else
                    {
                        string[] CustomProcess = (string[])key.GetValue("CustomProcess");

                        var myList = new List<string>();
                        myList.AddRange(CustomProcess);
                        myList.Add(ProcessBox.Text);
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomProcess", newArray, RegistryValueKind.MultiString);
                    }
                }
                // CustomIcon
                if (key.GetValue("CustomIcon") == null)
                {
                    key.SetValue("CustomIcon", IconBox.Text, RegistryValueKind.String);
                }
                else
                {
                    if (key.GetValue("CustomIcon") is string)
                    {
                        string CustomIcon = (string)key.GetValue("CustomIcon");

                        var myList = new List<string>
                        {
                            CustomIcon,
                            IconBox.Text
                        };
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomIcon", newArray, RegistryValueKind.MultiString);
                    }
                    else
                    {
                        string[] CustomIcon = (string[])key.GetValue("CustomIcon");

                        var myList = new List<string>();
                        myList.AddRange(CustomIcon);
                        myList.Add(IconBox.Text);
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomIcon", newArray, RegistryValueKind.MultiString);
                    }
                }
                // CustomLocation
                string checkedLocations = string.Empty;
                if (AllFilesCB.Checked)
                {
                    checkedLocations += 1;
                }
                if (ShortcutsCB.Checked)
                {
                    checkedLocations += 2;
                }
                if (DirectoriesCB.Checked)
                {
                    checkedLocations += 3;
                }
                if (BackgroundCB.Checked)
                {
                    checkedLocations += 4;
                }
                if (key.GetValue("CustomLocation") == null)
                {
                    key.SetValue("CustomLocation", checkedLocations, RegistryValueKind.String);
                }
                else
                {
                    if (key.GetValue("CustomLocation") is string)
                    {
                        string CustomLocation = (string)key.GetValue("CustomLocation");

                        var myList = new List<string>
                        {
                            CustomLocation,
                            checkedLocations
                        };
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomLocation", newArray, RegistryValueKind.MultiString);
                    }
                    else
                    {
                        string[] CustomLocation = (string[])key.GetValue("CustomLocation");

                        var myList = new List<string>();
                        myList.AddRange(CustomLocation);
                        myList.Add(checkedLocations);
                        string[] newArray = myList.ToArray();

                        key.SetValue("CustomLocation", newArray, RegistryValueKind.MultiString);
                    }
                }
            }

            EntryBox.Text = "";
            EntryBox.Items.Clear();
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\XtendedMenu\\Settings\\CustomEntries"))
            {
                if (key.GetValue("CustomName") != null)
                {
                    if (key.GetValue("CustomName") is string)
                    {
                        EntryBox.Items.Add((string)key.GetValue("CustomName"));
                    }
                    else
                    {
                        EntryBox.Items.AddRange((string[])key.GetValue("CustomName"));
                    }
                }
            }

            NameBox.Clear();
            ProcessBox.Clear();
            ArgumentsBox.Clear();
            DirectoryBox.Clear();
            IconBox.Clear();

            AllFilesCB.Checked = true;
            ShortcutsCB.Checked = true;
            DirectoriesCB.Checked = true;
            BackgroundCB.Checked = true;

            EntryBox.Text = "";
            AddButton.Text = "Add Entry";

            NameBox.Select();
        }

        private void IconBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NameBox.Text))
                {
                    MessageBox.Show("Make sure there is a Name before adding an Icon.");
                    return;
                }
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "Icon (*.ico;*.exe;*.dll)|*.ico;*.exe;*.dll";

                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.Title = "Icon to show";
                    openFileDialog.CheckFileExists = true;
                    openFileDialog.CheckPathExists = true;

                    DialogResult dr = openFileDialog.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        IconBox.Text = openFileDialog.FileName;

                        string IconPath = AppDomain.CurrentDomain.BaseDirectory + "ICONS\\";
                        Directory.CreateDirectory(IconPath);

                        string executablePath = openFileDialog.FileName;

                        if (Path.GetExtension(openFileDialog.FileName) == "exe" || Path.GetExtension(openFileDialog.FileName) == "exe")
                        {
                            Icon theIcon = ExtractIcon.ExtractIconFromFilePath(executablePath);

                            if (theIcon != null)
                            {
                                if (File.Exists(IconPath + NameBox.Text + ".ico"))
                                {
                                    DialogResult dialogResult = MessageBox.Show(IconPath + NameBox.Text + ".ico" + " already exists! Do you want to overwrite this file?", "ICON", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (dialogResult == DialogResult.OK)
                                    {
                                        using (FileStream stream = new FileStream(IconPath + NameBox.Text + ".ico", FileMode.CreateNew))
                                        {
                                            theIcon.Save(stream);
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    using (FileStream stream = new FileStream(IconPath + NameBox.Text + ".ico", FileMode.CreateNew))
                                    {
                                        theIcon.Save(stream);
                                    }
                                }
                            }
                        }
                        else
                        {
                            File.Copy(openFileDialog.FileName, IconPath + NameBox.Text + ".ico", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult dr = folderBrowserDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    DirectoryBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void EntryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryBox.Text))
            {
                AddButton.Text = "Update Entry";
            }
            else
            {
                AddButton.Text = "Add Entry";
            }
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\XtendedMenu\\Settings\\CustomEntries"))
            {
                int index = 0;
                var CustomNameList = new List<string>();
                CustomNameList.AddRange((string[])key.GetValue("CustomName"));
                string[] CustomNameArray = CustomNameList.ToArray();
                foreach (string value in CustomNameArray)
                {
                    if (value == (string)EntryBox.SelectedItem)
                    {
                        NameBox.Text = (string)EntryBox.SelectedItem;

                        var CustomProcessList = new List<string>();
                        CustomProcessList.AddRange((string[])key.GetValue("CustomProcess"));
                        ProcessBox.Text = CustomProcessList[index];

                        var CustomArgumentsList = new List<string>();
                        CustomArgumentsList.AddRange((string[])key.GetValue("CustomArguments"));
                        string[] CustomArgumentsArray = CustomArgumentsList.ToArray();
                        ArgumentsBox.Text = CustomArgumentsArray[index];

                        var CustomDirectoryList = new List<string>();
                        CustomDirectoryList.AddRange((string[])key.GetValue("CustomDirectory"));
                        string[] CustomDirectoryArray = CustomDirectoryList.ToArray();
                        DirectoryBox.Text = CustomDirectoryArray[index];

                        var CustomIconList = new List<string>();
                        CustomIconList.AddRange((string[])key.GetValue("CustomIcon"));
                        string[] CustomIconArray = CustomIconList.ToArray();
                        IconBox.Text = CustomIconArray[index];
                    }

                    index++;
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            NameBox.Clear();
            ProcessBox.Clear();
            ArgumentsBox.Clear();
            DirectoryBox.Clear();
            IconBox.Clear();

            AllFilesCB.Checked = true;
            ShortcutsCB.Checked = true;
            DirectoriesCB.Checked = true;
            BackgroundCB.Checked = true;

            EntryBox.Text = "";
            AddButton.Text = "Add Entry";

            NameBox.Select();
        }
    }
}