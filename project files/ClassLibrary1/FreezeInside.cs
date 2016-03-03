//FreezeInside.cs:

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework.Input;
    using StardewModdingAPI;

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

            public override void Entry()
            {
                Console.WriteLine("FreezeInside Mod Has Loaded");
                //Program.LogError("Test Mod can call to Program.cs in the API");
                //Program.LogColour(ConsoleColor.Magenta, "Test Mod is just a tiny DLL file in AppData/Roaming/StardewValley/Mods");

                //Subscribe to an event from the modding API
                //Events.KeyPressed += Events_KeyPressed;
                Events.CurrentLocationChanged += Events_LocationChanged;
            }

            //void Events_KeyPressed(Keys key)
            //{
            //    Console.WriteLine("TestMod sees that the following key was pressed: " + key);
            //}
            void Events_LocationChanged(StardewValley.GameLocation location)
            {
                if (!location.isOutdoors)
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
        }
    }