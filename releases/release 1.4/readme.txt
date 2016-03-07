Freezes the game clock inside.  Unfreezes when outside.
By cantorsdust.

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
INSTALLATION
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

There is one folder in this .zip containing two files, FreezeInside.dll and Config.json.  This folder may be placed in %appdata%\StardewValley\Mods.

Thus, the total path for both of the two files required for this mod to function are:
%appdata%\StardewValley\Mods\FreezeInside\FreezeInside.dll

AND

%appdata%\StardewValley\Mods\FreezeInside\Config.json.


REQUIRES Storm to be installed!
https://gitlab.com/Demmonic/Storm

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
USAGE
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Run StormLoader.exe in your main Stardew Valley folder.  This will load the mods and then start the game.
The game time will remain unchanged while you are inside.

Please note that this game comes with a Config.json file with two options:

1.  FreezeTimeInMines, which defaults to false.  If set to true, the time in the Mines (both of them) and the Farm Cave should freeze in addition to just the buildings.
2.  LetMachinesRunWhileTimeFrozen, which defaults to true.  If set to true, machines continue to run while you are inside and time is frozen.  Some consider this "cheaty".  Setting it to false will prevent machines from running while you are inside.


-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CHANGELOG
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

v1.4
Ported to Storm.  v1.3.1 will likely be final SMAPI version.
Added config for LetMachinesRunWhileTimeFrozen

v1.3.1
Fixed logic error preventing frozen time inside mines and caves.

v1.3
Updated for SMAPI 0.37

v1.2.1
Fixes bug where time is being set to XX:90 when jumping back from XX:00.

v1.2
Will now create an INI if you don't have one.

v1.1.2
Caught mistake in one of my file paths, corrected.  Thank you Eagle1337.

v1.1.1
adds catch for not finding the INI at all, so that won't crash and I won't have to answer a thousand questions

v1.1
fixes time setting bug on sleep, adds config INI for whether mines and caves should be counted as "inside"

v1.0
Initial release.





Place dll in %appdata%\Stardew Valley\Mods.  REQUIRES BOTH SMAPI AND TrainerMod.dll to be installed!

By cantorsdust with credit to Karyme for the idea and r3dteam for technical help.



