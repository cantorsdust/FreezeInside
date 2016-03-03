//FreezeInside.cs:

using System;
using System.Collections.Generic;
using System.Linq;
//using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
//using System.Configuration;
//using System.Web.Script.Serialization;

    namespace FreezeInsideMod
    {
        public class FreezeInside : Mod
        {
            public override string Name
            {
                get { return "Freeze Inside"; }
            }

            public override string Authour
            {
                get { return "cantorsdust with credit to Karyme for the idea and r3dteam for technical help"; }
            }

            public override string Version
            {
                get { return "1.0 v all inside"; }
            }

            public override string Description
            {
                get { return "Freezes time inside buildings"; }
            }

            public bool FreezeTimeInMines;
            /*
            {
                get
                {
                    bool flag = false;
                    Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["FreezeTimeInMines"]);
                    bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["FreezeTimeInMines"], out flag);
                    return flag;
                }
            }
             */

            public override void Entry()
            {
                runConfig();
                Console.WriteLine("FreezeInside Mod Has Loaded");
                //Program.LogError("Test Mod can call to Program.cs in the API");
                //Program.LogColour(ConsoleColor.Magenta, "Test Mod is just a tiny DLL file in AppData/Roaming/StardewValley/Mods");

                //Subscribe to an event from the modding API
                //Events.KeyPressed += Events_KeyPressed;
                Events.CurrentLocationChanged += Events_LocationChanged;
                Events.DayOfMonthChanged += Events_DayChanged;
                
            }

            //void Events_KeyPressed(Keys key)
            //{
            //    Console.WriteLine("TestMod sees that the following key was pressed: " + key);
            //}
            void runConfig()
            {
                //bool.TryParse(System.Configuration.ConfigurationSettings.AppSettings["FreezeTimeInMines"], out FreezeTimeInMines);
                //bool.TryParse(System.Configuration!System.Configuration.ConfigurationManager.AppSettings["FreezeTimeInMines"], out FreezeTimeInMines);

                //System.Xml.Linq.XElement settings = System.Xml.Linq.XElement.Load("FreezeInsideConfig.xml");
                //IEnumerable<System.Xml.Linq.XElement> FreezeTimeInMines =
                
                //var filepath = Environment.ExpandEnvironmentVariables("%AppData%\\\\StardewValley\\Mods\\FreezeInsideConfig.ini");
                

                try
                {
                    System.IO.StreamReader reader;
                    try
                    {
                        reader = System.IO.File.OpenText(Environment.ExpandEnvironmentVariables("%AppData%\\StardewValley\\Mods\\FreezeInsideConfig.ini"));
                        Console.WriteLine("found INI in %appdata%");
                    }
                    catch
                    {
                        reader = System.IO.File.OpenText("FreezeInsideConfig.ini");
                        Console.WriteLine("found INI in Stardew Valley-Mods");
                    }
                    string line = reader.ReadLine();
                    char[] delimiterChars = { '=' };
                    Console.WriteLine(line);
                    string[] words = line.Split(delimiterChars);
                    bool.TryParse(words[1], out FreezeTimeInMines);
                }
                catch
                {
                    FreezeTimeInMines = false;
                    Console.WriteLine("WARNING:  Could not find INI, defaulting FreezeTimeInMines to false");
                }

                

                if (FreezeTimeInMines)
                {
                    Console.WriteLine("FreezeTimeInMines is true");
                }
                else
                {
                    Console.WriteLine("FreezeTimeInMines is false");
                }
                
                
            }
            void Events_LocationChanged(StardewValley.GameLocation location)
            {
                if (!location.isOutdoors && (!((location is StardewValley.Locations.MineShaft) || (location is StardewValley.Locations.FarmCave)) || FreezeTimeInMines))
                {
                    //Console.WriteLine("FreezeInside sees that you are inside");
                    Command.CallCommand("world_freezetime 1");
                }
                else
                {
                    //Console.WriteLine("FreezeInside sees that you are outside");
                    Command.CallCommand("world_freezetime 0");
                }
            }
            void Events_DayChanged(Int32 date)
            {
                Command.CallCommand("world_settime 600");
            }
        }
    }