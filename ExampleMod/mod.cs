/**
* <author></author>
* <url>lifxmod.com</url>
* <credits></credits>
* <description></description>
* <license>GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007</license>
*/

// Register your mod as an object in the game engine, important for loading and unloading a package (mod)
if (!isObject(ExampleMod))
{
    new ScriptObject(ExampleMod)
    {
    };
}


// LiFx expect each mod to be it's own unique package
package ExampleMod
{
  // Returns a string as a version, LiFx will look for this specific function to output version to new connecting players
  // Takes no parameters, is a reserved function for LiFx compatability.
  function ExampleMod::version() {
    return "v1.0.0" 
  }
  
  // The setup method is required, and will be looked for by the framework, if it doesn't have it your mod will not execute
  // This is where you tell the framework, which hooks you use and what object types you have added, so that the framework can call your code at the appropiate time
  function ExampleMod::setup() {
    // Register callback hooks, do not run any form of code that does anything here, just register the hook
	/**
	* LiFx::registerCallback is a global framework function, it takes 3 parameters
    * Parameter 1: The hook to register your function on
    * Parameter 2: Non scoped name of function in your package
    * Parameter 3: The package name to scope your function appropiately.
	*/
    LiFx::registerCallback($LiFx::hooks::onSpawnCallbacks, onSpawn, ExampleMod);
    
	/**
	* LiFx::registerObjectsTypes is a global framework function, it takes 2 parameters
	* It is used to write to the dump.sql on start, prior to the server reading it, and is necessary as bitbox wipes the objectstypes table on each start up.
    * Parameter 1: The function including scope to your objectstypes definition
    * Parameter 2: The package name of your mod
	*/
    LiFx::registerObjectsTypes(ExampleMod::ObjectsTypesExampleBuilding(), ExampleMod);
  }
  
  // Example function references from setup above, this code will execute when the hook is called by the LiFx framework
  function ExampleMod::onSpawn(%this, %client) {
    echo(%this.getName() SPC %client.getName());
    
  }
  // The function name should match the object you create as a return object inside of the function
  function ExampleMod::ObjectsTypesExampleBuilding() {

    // this returns and writes to the dump.sql file for ease of distribution
	// The name of the object should be the same as the function name it registers
	// Each of the variables in the object, corrosponds to the column value in the database
    return new ScriptObject(ObjectsTypesExampleBuilding : ObjectsTypes)
    {
      id = 2420; // *UNIQUE INT* Has to be a unique id
      ObjectName = "ExampleBuilding"; // *STRING* Name of your object
      ParentID = 61; // *INT* ParentID decides what type of object you have, think of it as class inheritance
      IsContainer = 0; // *BOOL* 1 (true) or 0 (false) - If your object is supposed to have a container referenced
      IsMovableObject = 0; // *BOOL* 1 (true) or 0 (false) - If your object is supposed to be movable
      IsUnmovableobject = 1; // *BOOL* 1 (true) or 0 (false) - If your object is supposed to be unmovable
      IsTool = 0; // *BOOL* 1 (true) or 0 (false) - If your object is supposed to be a tool to cut down trees or build buildings
      IsDevice = 0; // *BOOL* 1 (true) or 0 (false) - If your object is a device used in crafting or other interactions
      IsDoor = 0; // *BOOL* 1 (true) or 0 (false) - If your object has a door
      IsPremium = 0; // *BOOL* 1 (true) or 0 (false) - Premium defintion currently is not in use for Your Own.
      MaxContSize = 0; // *INT* The Max size of the container, if IsContainer is true
      Length = 0;  // *INT* Length of your object, used to decide if your object fits in a particular container
      MaxStackSize = 0; // *INT* Max number of items in a stack of your item in the inventory
      UnitWeight = 5000; // *INT* The weight of your object, used in calculation of encumbarance
      BackgrndImage = ""; // *STRING* Image reference to your inventory background, must be set if your object has a container
      WorkAreaTop = 0;
      WorkAreaLeft = 0;
      WorkAreaWidth = 0;
      WorkAreaHeight = 0;
      BtnCloseTop = 0;
      BtnCloseLeft = 0;
      FaceImage = "mods/ExampleMod/BuildingImages/Example.png"; // *STRING* Reference to png that will be displayed in crafting menu
      Description = ""; // *STRING* Used in crafting and skill to describe your object 
      BasePrice = 0; // *INT* Used as price for sacrificing to monuments, high value gives more maintenance points
      OwnerTimeout = 0; // *INT* Used to set a timer on the object when made or dropped.
      AllowExportFromRed = 0; // Not in use
      AllowExportFromGreen = 0; // Not in use
    };
  }
};

// This command is from Torque, and activates your package so that the engine can reference it
// This is required for your mod to work, and have the code loaded in torque engine.
activatePackage(ExampleMod);