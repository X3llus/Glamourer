using OtterGui.Widgets;

namespace Glamourer.Gui;

public class GlamourerChangelog
{
    public const     int           LastChangelogVersion = 0;
    private readonly Configuration _config;
    public readonly  Changelog     Changelog;

    public GlamourerChangelog(Configuration config)
    {
        _config   = config;
        Changelog = new Changelog("Glamourer Changelog", ConfigData, Save);

        Add1_0_0_0(Changelog);
        Add1_0_0_1(Changelog);
        Add1_0_0_2(Changelog);
        Add1_0_0_3(Changelog);
        Add1_0_0_6(Changelog);
        Add1_0_1_1(Changelog);
        Add1_0_2_0(Changelog);
        Add1_0_3_0(Changelog);
        Add1_0_4_0(Changelog);
        Add1_0_5_0(Changelog);
        Add1_0_6_0(Changelog);
        Add1_0_7_0(Changelog);
    }

    private (int, ChangeLogDisplayType) ConfigData()
        => (_config.Ephemeral.LastSeenVersion, _config.ChangeLogDisplayType);

    private void Save(int version, ChangeLogDisplayType type)
    {
        if (_config.Ephemeral.LastSeenVersion != version)
        {
            _config.Ephemeral.LastSeenVersion = version;
            _config.Ephemeral.Save();
        }

        if (_config.ChangeLogDisplayType != type)
        {
            _config.ChangeLogDisplayType = type;
            _config.Save();
        }
    }

    private static void Add1_0_7_0(Changelog log)
        => log.NextVersion("Version 1.0.7.0")
            .RegisterHighlight("Glamourer now can set the free company crests on body slots, head slots and shields.")
            .RegisterEntry("Fixed an issue with tooltips in certain combo selectors.")
            .RegisterEntry("Fixed some issues with Hide Hat Gear and monsters turned into humans.")
            .RegisterEntry(
                "Hopefully fixed issues with icons used by Glamourer that are modified through Penumbra preventing Glamourer to even start in some cases.")
            .RegisterEntry("Those icons might still not appear if they fail to load, but Glamourer should at least still work.", 1)
            .RegisterEntry("Pre-emptively fixed a potential issue for the holidays.");

    private static void Add1_0_6_0(Changelog log)
        => log.NextVersion("Version 1.0.6.0")
            .RegisterHighlight("Added the option to define custom color groups and associate designs with them.")
            .RegisterEntry("You can create and name design colors in Settings -> Colors -> Custom Design Colors.", 1)
            .RegisterEntry(
                "By default, all designs have an automatic coloring corresponding to the current system, that chooses a color dynamically based on application rules.",
                1)
            .RegisterEntry(
                "Example: You create a custom color named 'Test' and make it bright blue. Now you assign 'Test' to some design in its Design Details, and it will always display bright blue in the design list.",
                1)
            .RegisterEntry("Design colors are stored by name. If a color can not be found, the design will display the Missing Color instead.",
                1)
            .RegisterEntry("You can filter for designs using specific colors via c:", 1)
            .RegisterHighlight(
                "You can now filter for the special case 'None' for filters where that makes sense (like Tags or Mod Associations).")
            .RegisterHighlight(
                "When selecting multiple designs, you can now add or remove tags from them at once, and set their colors at once.")
            .RegisterEntry("Improved tri-state checkboxes. The colors of the new symbols can be changed in Color Settings.")
            .RegisterEntry("Removed half-baked localization of customization names and fixed some names in application rules.")
            .RegisterEntry("Improved Brio compatibility")
            .RegisterEntry("Fixed some display issues with text color on locked designs.")
            .RegisterEntry("Fixed issues with automatic design color display for customization-only designs.")
            .RegisterEntry("Removed borders from the quick design window regardless of custom styling.")
            .RegisterEntry("Improved handling of (un)available customization options.")
            .RegisterEntry(
                "Some configuration like the currently selected tab states are now stored in a separate file that is not backed up and saved less often.")
            .RegisterEntry("Added option to open the Glamourer main window at game start independently of Debug Mode.");

    private static void Add1_0_5_0(Changelog log)
        => log.NextVersion("Version 1.0.5.0")
            .RegisterHighlight("Dyes are can now be favorited the same way equipment pieces can.")
            .RegisterHighlight(
                "The quick design bar combo can now be scrolled through via mousewheel when hovering over the combo without opening it.")
            .RegisterEntry(
                "Control-Rightclicking the quick design bar now not only jumps to the corresponding design, but also opens the main window if it is not currently open.")
            .RegisterHighlight("You can now filter for designs containing specific items by using \"i:partial item name\".")
            .RegisterEntry(
                "When overwriting a saved designs data entirely from clipboard, you can now undo this change and restore the prior design data once via a button top-left.")
            .RegisterEntry("Removed the \"Enabled\" checkbox in the settings since it was barely doing anything but breaking Glamourer.")
            .RegisterEntry(
                "If you want to disable Glamourers state-tracking and hooking, you will need to disable the entire Plugin via Dalamud now.", 1)
            .RegisterEntry("Added a reference to \"/glamour\" in the \"/glamourer help\" section.")
            .RegisterEntry("Updated BNPC Data with new crowd-sourced data from the gubal library.")
            .RegisterEntry("Fixed an issue with the quick design bar when no designs are saved.")
            .RegisterEntry("Fixed a problem with characters not redrawing after leaving GPose even if necessary.");

    private static void Add1_0_4_0(Changelog log)
        => log.NextVersion("Version 1.0.4.0")
            .RegisterEntry("The GPose target is now used for target-dependent functionality in GPose.")
            .RegisterEntry("Fixed a few issues with transformations, especially their weapons and head gear.")
            .RegisterEntry(
                "Previewing Offhand Models for both-handed weapons via right click is now possible (may need to wait for a not-yet released Penumbra update).")
            .RegisterEntry("Updated the known list of Battle NPCs.")
            .RegisterEntry("Removed another technically unrestricted item from restricted item list.")
            .RegisterEntry("Use local time for discerning the current day on start-up instead of UTC-time.")
            .RegisterEntry("Improved the Unlocks Table with additional info. (1.0.3.1)")
            .RegisterEntry("Added position locking option and more color options. (1.0.3.1)")
            .RegisterEntry("Removed the default key combination for toggling the quick bar. (1.0.3.1)");

    private static void Add1_0_3_0(Changelog log)
        => log.NextVersion("Version 1.0.3.0")
            .RegisterEntry("Hopefully improved Palette+ compatibility.")
            .RegisterHighlight(
                "Added a Quick Design Bar, which is a small bar in which you can select your designs and apply them to yourself or your target, or revert them.")
            .RegisterEntry("You can toggle visibility of this bar via keybinds, which you can set up in the settings tab.",     1)
            .RegisterEntry("You can also lock the bar, and enable or disable an additional, identical bar in the main window.", 1)
            .RegisterEntry("Disabled a sound that played on startup when a certain Dalamud setting was enabled.")
            .RegisterEntry("Fixed an issue with reading state for Who Am I!?!. (1.0.2.2)")
            .RegisterEntry("Fixed an issue where applying gear sets would not always update your dyes. (1.0.2.2)")
            .RegisterEntry("Fixed an issue where some errors due to missing null-checks wound up in the log. (1.0.2.2)")
            .RegisterEntry("Fixed an issue with hat visibility. (1.0.2.1 and 1.0.2.2)")
            .RegisterEntry("Improved some logging. (1.0.2.1)")
            .RegisterEntry("Improved notifications when encountering errors while loading automation sets. (1.0.2.1)")
            .RegisterEntry("Fixed another issue with monk fist weapons. (1.0.2.1)")
            .RegisterEntry("Added missing dot to changelog entry.");

    private static void Add1_0_2_0(Changelog log)
        => log.NextVersion("Version 1.0.2.0")
            .RegisterHighlight("Added option to favorite items so they appear first in the item selection combos.")
            .RegisterEntry(
                "The reordering in the combo only happens after closing and opening it again so items do not vanish from view when you (un)favor them.",
                1)
            .RegisterEntry("Favored items also get a highlighting border in the overview panels of the unlocks tab, but do not reorder those.",
                1)
            .RegisterEntry("In the details panel of the unlocks tab items can be sorted and filtered on favorites.", 1)
            .RegisterEntry("Added drag & drop support to drag an automated design into a different automated design set.")
            .RegisterEntry("This will remove said design from your current set and add it to the dragged-on set at the end.", 1)
            .RegisterEntry("Fixed ONE issue with hat visibility state. There are probably more. This is weird.")
            .RegisterEntry("Fixed minion placement for transformed Lalafell again.")
            .RegisterEntry("Fixed job flag filtering in detailed unlocks.")
            .RegisterEntry("Worked around a bug in the game's code breaking certain Monk Fist Weapons again... thanks SE.");

    private static void Add1_0_1_1(Changelog log)
        => log.NextVersion("Version 1.0.1.1")
            .RegisterImportant(
                "Updated for 6.5 - Square Enix shuffled around a lot of things this update, so some things still might not work but have not been noticed yet. Please report any issues.")
            .RegisterHighlight(
                "Added additional item data like Job Restrictions, Required Level and Dyability to Items to search or filter for in the Detailed Unlocks tab.")
            .RegisterEntry("Improved support for non-item Weapons like NPC-Weapons saved to designs.")
            .RegisterEntry(
                "Improved messaging: many warnings or errors appearing will stay a little longer and can now be looked at in a Messages tab (visible only if there have been any).")
            .RegisterEntry("Fixed an issue where moving automation sets caused editing them to not work correctly afterwards.")
            .RegisterEntry("Omega items are no longer restricted.")
            .RegisterEntry("Fixed reverting to game state not removing forced wetness.")
            .RegisterEntry(
                "Added some new cheat codes. You can now use the code 'Look at me, I'm your character now.' to add buttons that copy the actual state of yourself or your target into a clipboard-design, in case the randomizers gave you something cool!")
            .RegisterEntry("Other new codes will be published in other ways.", 1);

    private static void Add1_0_0_6(Changelog log)
        => log.NextVersion("Version 1.0.0.6")
            .RegisterHighlight(
                "Added two buttons to the top-right of the Glamourer window that allow you to revert your own player characters state to Game or to Automation state from any tab.")
            .RegisterEntry("Added the Shift/Control modifiers to the Apply buttons in the Designs tab, too.")
            .RegisterEntry("Fixed some issues with weapon types applying wrongly in automated designs.")
            .RegisterEntry(
                "Glamourer now removes designs you delete from all automation sets instead of screaming about them missing the next time you launch.")
            .RegisterEntry("Improved fixed design migration from pre 1.0 versions if anyone updates later.")
            .RegisterEntry("Added a line to warning messages for invalid entries that those entries are not saved with the warning.")
            .RegisterEntry("Added and improved some IPC for Mare.")
            .RegisterEntry("Broke and fixed some application rules for not-always available options.")
            .RegisterHighlight("This should fix tails and ears not being shared via Mare!", 1);

    private static void Add1_0_0_3(Changelog log)
        => log.NextVersion("Version 1.0.0.3")
            .RegisterHighlight("Reintroduced holding Control or Shift to apply only gear or customization changes.")
            .RegisterEntry("Deletion of multiple selected designs at once is now supported.")
            .RegisterEntry(
                "Added an 'Apply Mod Associations' button at the top row for designs. Hovering it tells you which collection it would edit.")
            .RegisterHighlight(
                "Turned 'Use Replacement Gear for Gear Unavailable to Your Race or Gender' OFF by default. If this setting confused you or you use mods that make those pieces available, please disable the setting.")
            .RegisterHighlight(
                "Added an option that a characters state reverts all manual changes after a zone change to simulate pre-rework behavior. This is OFF by default.")
            .RegisterEntry("Fixed some issues with chat command parsing and applying for NPCs.")
            .RegisterEntry("Turning into a Lalafell should now correctly cause minions to sit on your head instead your shoulders.")
            .RegisterEntry("Another, better, possibly working fix for Lalafell and Elezen ear shapes.")
            .RegisterEntry("Fixed a big issue with a memory leak concerning owned NPCs.")
            .RegisterEntry("Fixed some issues with non-zero model-ID but human characters, like Zero.")
            .RegisterEntry("Fixed an issue with weapons not respecting disabled automated designs.")
            .RegisterEntry("Maybe fixed an issue where unavailable customizations set to Apply still applied their invisible values.")
            .RegisterEntry(
                "Fixed display of automated design rows when unobtained item warnings were disabled but full checkmarks enabled, and the window was not wide enough for single row.")
            .RegisterEntry("Apply Forced Wetness state after creation of draw objects, maybe fixing it turning off on zone changes.");

    private static void Add1_0_0_2(Changelog log)
        => log.NextVersion("Version 1.0.0.2")
            .RegisterHighlight("Added support for 'Clipboard' as a design source for /glamour apply.")
            .RegisterEntry("Improved tooltips for tri-state toggles.")
            .RegisterEntry("Improved some labels of settings to clarify what they do.")
            .RegisterEntry("Improved vertical space for automated design sets.")
            .RegisterEntry(
                "Improved tooltips for renaming/moving designs via right-click context to make it clear that this does not rename the design itself.")
            .RegisterHighlight("Added new configuration to hide advanced application rule settings in automated design lines.")
            .RegisterHighlight("Added new configuration to hide unobtained item warnings in automated design lines.")
            .RegisterEntry("Removed some warning popups for temporary designs when sharing non-human actors via Mare (I guess?)")
            .RegisterEntry(
                "Fixed an issue with unnecessary redrawing in GPose when having applied a change that required redrawing after entering GPose.")
            .RegisterEntry("Fixed chat commands parsing concerning NPC identifiers.")
            .RegisterEntry("Fixed restricted racial gear applying to accessories by mistake.")
            .RegisterEntry("Maybe fixed Mare syncing having issues with restricted gear protection.")
            .RegisterEntry("Fixed the icon for disabled mods in associated mods.")
            .RegisterEntry("Fixed inability to remove associated mods except for the last one.")
            .RegisterEntry(
                "Fixed treating certain gloves as restricted because one restricted items sharing the model with identical, unrestricted gloves exists (wtf SE?).")
            .RegisterEntry(
                "Hopefully fixed ear shape numbering for Elezen and Lalafell (yes SE, just put a 1-indexed option in a sea of 0-indexed options, sure. Fuck off-by-one error).");

    private static void Add1_0_0_1(Changelog log)
        => log.NextVersion("Version 1.0.0.1")
            .RegisterImportant("Fixed Issue with Migration of identically named designs. "
              + "If you lost any designs during migration, try going to \"%appdata%\\XIVLauncher\\pluginConfigs\\Glamourer\\\" "
              + "and renaming the file \"Designs.json.bak\" to \"Designs.json\", then restarting.")
            .RegisterEntry("This may cause some duplicated entries", 1)
            .RegisterEntry("Added a highlight border around the Enable/Disable All toggle for Automated Designs.")
            .RegisterEntry("Fixed newly created designs not being moved to folders when using paths like 'path/to/design' anymore.")
            .RegisterEntry("Added a tooltip to clarify the intent of the Mod Associations tab.")
            .RegisterEntry("Fixed an issue with some weapons not being recognized as offhands correctly.");

    private static void Add1_0_0_0(Changelog log)
        => log.NextVersion("Version 1.0.0.0 (Full Rework)")
            .RegisterHighlight(
                "Glamourer has been reworked entirely. Basically everything has been written anew from the ground up, even though some things may look the same.")
            .RegisterEntry(
                "The new version has been tested for quite a while, but there still may be bugs, unintended changes or other issues that slipped through, given the limited amount of testers.",
                1)
            .RegisterEntry(
                "Migration of configuration and existing designs should mostly work, though some fixed designs may not migrate correctly.",  1)
            .RegisterEntry("All your data should be backed up before being changed, so restauration should always be possible in some way.", 1)
            .RegisterImportant(
                "If you encounter any problems, please report them on the discord. If you encounter data loss, please do so immediately.", 1)
            .RegisterHighlight("Major Changes")
            .RegisterEntry(
                "Redrawing characters is mostly gone. All equipment changes, and all customization changes except race, gender or face, can be applied instantaneously without redrawing.",
                1)
            .RegisterEntry(
                "As a side effect, Glamourer should no longer be dangerous to use with the aesthetician, since the games data of your character is no longer manipulated, only its visualization.",
                2)
            .RegisterEntry("Things like the Lalafell/Dwarf cave in Kholusia also no longer send invalid data to the server.", 2)
            .RegisterEntry("Portraits should also be entirely safe.",                                                         2)
            .RegisterEntry(
                "As another side effect, any changes you apply in any way will be kept across zone changes or character switches until they are actively overwritten by something or you restart the entire game, even without automation.",
                2)
            .RegisterImportant(
                "Compatibility with Anamnesis is questionable. Anamnesis will not be able to detect Glamourers changes, and changes in Anamnesis may confuse Glamourer.",
                2)
            .RegisterHighlight("Mare Synchronos compatibility is retained.", 2)
            .RegisterEntry("Reverting changes made works far more dependably.", 1)
            .RegisterEntry(
                "You can enable auto-reloading of gear, which will cause your equipment to be reloaded whenever you make changes to the mod collection affecting your character. Great for immediate comparisons of mod options!",
                1)
            .RegisterEntry("Customizations can be toggled to apply or not apply individually instead of as a group for each design.", 1)
            .RegisterImportant("Replacing your weapons was slightly restricted.", 1)
            .RegisterEntry(
                "Outside of GPose, you can only replace weapons with other weapons of the same type. In GPose, you should still be able to change across types.",
                2)
            .RegisterEntry(
                "This restriction is because changing some weapon types can lead to game freezes, crashes, soft- and hard locks of your character, and can transmit invalid data to the server.",
                2)
            .RegisterEntry(
                "Designs now can carry more information, like tags, their creation or last update date, a description, and associated mods.", 1)
            .RegisterEntry(
                "Fixed Designs are now called Automated Designs and can be found in the Automation tab. This tab has a help button in the selector.",
                1)
            .RegisterEntry("Automated designs use Penumbras way of identifying characters, thus they do not apply by pure name anymore.", 2)
            .RegisterImportant(
                "Please look through them after the migration, because not all names in fixed designs could necessarily be migrated.", 2)
            .RegisterEntry(
                "Glamourer can now keep track of which items and customizations have been seen on any of your characters on this installation, so you can have a Glamour log. "
              + "This log can optionally be used to restrict your own automated designs only to things you actually have unlocked, and can otherwise be used for browsing existing items.",
                1)
            .RegisterHighlight("Notable Minor Changes")
            .RegisterEntry("Hrothgar faces should be fixed.", 1)
            .RegisterEntry("Alpha value is gone. It may be brought back later on, but generally should not be, and was not often used, so eh.",
                1)
            .RegisterEntry(
                "Glamourer now can optionally protect you from gender- or race-restricted gear not appearing when you switch, by automatically using an appropriate replacement.",
                1)
            .RegisterEntry(
                "Glamourer has some fun optional easter eggs and cheat codes, like You've got to think for yourselves! You're all individuals!",
                1)
            .RegisterEntry("You can enable game context menus so you can equip linked items via Glamourer.",         1)
            .RegisterEntry("A lot of configuration and options was learned from Penumbra and is available, like...", 1)
            .RegisterEntry("... configurable color coding for the Glamourer UI.",                                    2)
            .RegisterEntry("... sort modes for your design list.",                                                   2)
            .RegisterEntry("... an automated backup system keeping up to 10 backups of your glamourer data.",        2)
            .RegisterEntry("... this Changelog!",                                                                    2)
            .RegisterEntry("... configurable deletion modifiers for fewer misclicks!",                               2);
}
