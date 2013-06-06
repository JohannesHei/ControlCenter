﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace BF2Statistics
{
    public partial class MapList : Form
    {
        /// <summary>
        /// An array of map names found in the servers "levels" folder
        /// </summary>
        private string[] Maps;

        /// <summary>
        /// The full path to the current selected mod folder
        /// </summary>
        private string ModPath;

        /// <summary>
        /// The full path to the current selected mod's "levels" folder
        /// </summary>
        private string LevelsPath;

        /// <summary>
        /// Full path to the maplist.con file for the selected mod
        /// </summary>
        private string MaplistFile;

        /// <summary>
        /// Specifies whether a map is currently selected in the form
        /// </summary>
        private bool isMapSelected = false;

        /// <summary>
        /// A list of supported game modes for the selected map
        /// </summary>
        List<string> GameModes;

        /// <summary>
        /// A dictionary, of each "gameMode" => List("Supported Map Sizes")
        /// </summary>
        Dictionary<string, List<string>> ModeSizes;

        /// <summary>
        /// Constructor
        /// </summary>
        public MapList()
        {
            InitializeComponent();

            // Define our paths
            ModPath = Path.Combine(MainForm.Config.ServerPath, "mods", MainForm.SelectedMod);
            LevelsPath = Path.Combine(ModPath, "levels");
            MaplistFile = Path.Combine(ModPath, "settings", "maplist.con");

            // Make sure maplist.con file exists!
            if (!File.Exists(MaplistFile))
            {
                this.Load += new EventHandler(CloseOnStart);
                MessageBox.Show("Maplist.con file is missing! Please make sure your server path is set properly", "Error");
                return;
            }

            // Make sure the levels folder exists!
            if (!Directory.Exists(LevelsPath))
            {
                this.Load += new EventHandler(CloseOnStart);
                MessageBox.Show("The current selected mod does not contain a 'levels' folder. Please select a valid mod.", "Error");
                return;
            }

            // Fetch all maps for the selected mod
            Maps = Directory.GetDirectories(LevelsPath);
            foreach (string M in Maps)
                MapListSelect.Items.Add(new Item(M.Remove(0, LevelsPath.Length + 1), 1));

            // Get the current maplist and display it
            MapListBox.Text = File.ReadAllText(MaplistFile).Trim();
        }

        #region Events

        /// <summary>
        /// Called when a map is selected or changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapListSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If an error occurs while loading the maps .desc file, then just return
            if (MapListSelect.SelectedIndex == -1)
                return;

            // Dont Need to reset anything if its the first time loading a map
            if (!isMapSelected)
            {
                GameModeSelect.Enabled = true;
                isMapSelected = true;
            }
            else
            {
                // Reset select list's text
                MapSizeSelect.Text = "";
                GameModeSelect.Text = "";

                // Remove all items from the Mode select and MapSize select
                GameModeSelect.Items.Clear();
                MapSizeSelect.Items.Clear();

                // Disable MapList select
                MapSizeSelect.Enabled = false;
                AddToMapList.Enabled = false;
            }

            // Load our current selected map
            LoadMap();

            // Add all map supported game modes to the GameMode select list
            foreach (string mode in GameModes)
                GameModeSelect.Items.Add(new KeyValueItem(mode, GameModeToString(mode)));
        }

        /// <summary>
        /// Adds completed map selections to the Maplist
        /// </summary>
        private void AddToMapList_Click(object sender, EventArgs e)
        {
            string map = MapListSelect.SelectedItem.ToString();
            string mode = ((KeyValueItem) GameModeSelect.SelectedItem).Key;
            string size = MapSizeSelect.SelectedItem.ToString();
            if (!String.IsNullOrEmpty(MapListBox.Text))
                MapListBox.Text += Environment.NewLine;
            MapListBox.Text += String.Format("mapList.append {0} {1} {2}", map, mode, size);
        }

        /// <summary>
        /// Clears the current Maplist
        /// </summary>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            MapListBox.Text = "";
        }

        /// <summary>
        /// Saves the maplist to the maplist.com file
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(MaplistFile, MapListBox.Lines);
            this.Close();
        }

        /// <summary>
        /// Event fired when the client selects a Map Gamemode
        /// </summary>
        private void GameModeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Remove all Mapsize selects
            MapSizeSelect.Items.Clear();
            MapSizeSelect.Enabled = false;
            AddToMapList.Enabled = false;

            // Add new map sizes for the selected game mode
            string mode = ((KeyValueItem) GameModeSelect.SelectedItem).Key;

            // Add all supported map sizes. If we donot have mapsize support, I assume
            // we are in a Sp1 mod.
            if (ModeSizes[mode].Count > 0)
            {
                foreach (string size in ModeSizes[mode])
                    MapSizeSelect.Items.Add(new Item(size, 1));

                MapSizeSelect.Enabled = true;
            }
            else
            {
                switch (mode)
                {
                    case "sp1":
                        MapSizeSelect.Items.Add(new Item("16", 1));
                        break;
                    case "sp2":
                        MapSizeSelect.Items.Add(new Item("32", 1));
                        break;
                    case "sp3":
                        MapSizeSelect.Items.Add(new Item("64", 1));
                        break;
                    default:
                        MapSizeSelect.Items.Add(new Item("16", 1));
                        MapSizeSelect.Items.Add(new Item("32", 1));
                        MapSizeSelect.Items.Add(new Item("64", 1));
                        MapSizeSelect.Enabled = true;
                        break;
                }

                // Set default index
                MapSizeSelect.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Event fired when a mapsize is selected
        /// </summary>
        private void MapSizeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToMapList.Enabled = true;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Method for loading the Map Desc file, which holds all the gamemode
        /// and map size information for the selected map
        /// </summary>
        private void LoadMap()
        {
            // Initialize new GameModes and MapSizes
            GameModes = new List<string>();
            ModeSizes = new Dictionary<string, List<string>>();
            string map = MapListSelect.SelectedItem.ToString();
            string dFile = Path.Combine(LevelsPath, map, "Info", map + ".desc");

            // Load the map description file
            try
            {
                XmlDocument Doc = new XmlDocument();
                Doc.Load(dFile);

                // Get a list of supported modes, and add them to the GameModes and Mode Sizes variables
                XmlNodeList Modes = Doc.GetElementsByTagName("mode");
                foreach (XmlNode m in Modes)
                {
                    string mode = m.Attributes["type"].InnerText;
                    GameModes.Add(mode);

                    List<string> temp = new List<string>();
                    foreach (XmlNode c in m.ChildNodes)
                        temp.Add(c.Attributes["players"].InnerText);

                    ModeSizes.Add(mode, temp);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error loading the map descriptor file"
                    + Environment.NewLine + Environment.NewLine
                    + "Message: " + e.Message, 
                    "Error"
                );

                // Reset the GUI
                MapListSelect.SelectedIndex = -1;
                GameModeSelect.Enabled = false;
                isMapSelected = false;
            }
        }

        /// <summary>
        /// Parses a maplist.con game mode variable into a human readable one.
        /// </summary>
        /// <param name="mode">The gamemode from the maplist.con</param>
        /// <returns></returns>
        protected string GameModeToString(string mode)
        {
            switch (mode)
            {
                case "gpm_coop":
                    return "Co-op";
                case "gpm_cq":
                    return "Conquest";
                case "sp1":
                    return "Singleplayer 16";
                case "sp2":
                    return "Singleplayer 32";
                case "sp3":
                    return "Singleplayer 64";
                default:
                    return mode;
            }
        }

        #endregion

        /// <summary>
        /// Event closes the form when fired
        /// </summary>
        private void CloseOnStart(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
