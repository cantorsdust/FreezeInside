/*
    Copyright 2016 cantorsdust

    Storm is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Storm is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Storm.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Storm.ExternalEvent;
using Storm.StardewValley;
using Storm.StardewValley.Event;
using Storm.StardewValley.Wrapper;

namespace FreezeInside
{
    [Mod]
    public class FreezeInside : DiskResource
    {
        public int lasttime = 600;
        public Config ModConfig { get; private set; }
        public bool firsttick = true;

        [Subscribe]
        public void InitializeCallback(InitializeEvent @event)
        {
            var configLocation = Path.Combine(PathOnDisk, "Config.json");
            if (!File.Exists(configLocation))
            {
                Console.WriteLine("The config file for FreezeInside was not found, attempting creation...");
                ModConfig = new Config();
                ModConfig.FreezeTimeInMines = false;
                ModConfig.LetMachinesRunWhileTimeFrozen = true;
                File.WriteAllBytes(configLocation, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ModConfig, Formatting.Indented)));
                Console.WriteLine("The config file for FreezeInside has been loaded. \n\tFreezeTimeInMines: {0}, LetMachinesRunWhileTimeFrozen: {1}",
                    ModConfig.FreezeTimeInMines, ModConfig.LetMachinesRunWhileTimeFrozen);
            }
            else
            {
                ModConfig = JsonConvert.DeserializeObject<Config>(Encoding.UTF8.GetString(File.ReadAllBytes(configLocation)));
                Console.WriteLine("The config file for FreezeInside has been loaded.\n\tFreezeTimeInMines: {0}, LetMachinesRunWhileTimeFrozen: {1}",
                    ModConfig.FreezeTimeInMines, ModConfig.LetMachinesRunWhileTimeFrozen);
            }

            Console.WriteLine("FreezeInside Initialization Completed");
        }

        [Subscribe]
        public void PostNewDayCallback(PostNewDayEvent @event)
        {
            lasttime = 600;
        }

        [Subscribe] 
        public void Pre10MinuteClockUpdateCallback(Pre10MinuteClockUpdateEvent @event)
        {
            Console.WriteLine("Firing Pre10MinuteClockUpdateEvent");
            var location = @event.Root.CurrentLocation;
            if (location != null)
            {
                Console.WriteLine("Location name is: " + location.Name);
                Console.WriteLine("Location is outdoors is: " + location.IsOutdoors.ToString());
            }
            int time = @event.Root.TimeOfDay;
            Console.WriteLine("time is " + time.ToString("G"));
            if (location != null && !location.IsOutdoors && ((!location.Name.Equals("UndergroundMine") && !location.Name.Equals("FarmCave")) || ModConfig.FreezeTimeInMines) && (time - lasttime <= 10 || (time % 100 == 0 && time - lasttime == 50) || firsttick))
            {
                //if location is not null
                //if location is not outdoors
                //if location name is not UndergroundMine or FarmCave or alternatively, if FreezeTimeInMines is true
                //if time is not jumping by more than 10 minutes (some festivals do this and I don't want to break them)
                //first tick seems wonky, added bool
                firsttick = false;
                Console.WriteLine("location requirements met, resetting time");
                if (ModConfig.LetMachinesRunWhileTimeFrozen)
                {
                    if (time == 600)
                    {
                        if ((time % 100) == 0)
                        {
                            @event.Root.TimeOfDay = lasttime - 50;
                        }
                        else
                        {
                            @event.Root.TimeOfDay = lasttime;
                        }
                    }
                    else
                    {
                        @event.Root.TimeOfDay = lasttime;
                    }

                    Console.WriteLine("resetting time to: " + lasttime.ToString("G"));
                }
                else
                {
                    @event.ReturnEarly = true;
                    @event.Root.GameTimeInterval = 0;
                }
                
            }
            else
            {
                lasttime = time;
                Console.WriteLine("location requirements not met, time advancing normally");
            }
        }
        
        
    }

    public class Config
    {
        public bool FreezeTimeInMines { get; set; }
        public bool LetMachinesRunWhileTimeFrozen { get; set; }
    }
}
