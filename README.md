###Universal Mod Organizer
A solution for your modded Paradox Games.

- Includes custom mod order so you can enforce correct mod loading order.

###### Work in progress...

### Features:
- **Mod Ordering** (just sort by order and drag)
- Multiple games support.
- Multiple profiles.
- Profile management *(Add/Remove/Import/Export/Copy/Rename)*
- Inline search

### Plans:
- Mod conflict tool detection.
- In-app mod merging and creatinon.
- Profile Duplication/Rename
- Ironman/Achievements Compatibility Check

### Supported Games:
 - Stellaris
 - Crusader Kings 2
 - Europa Universalis IV
 - Hearts of Iron IV

Other games should be supported without issues but I do not own them so I can not check how mods are kept.
I will need settings.txt and 2-3 example mods from the game to check and make it work. Contact me I will give you test build.

### Issues:
- No Linux/Mac support. Mono is not supported by third party lib so... Maybe in future...
- Well, something definitely may not work...

#### Q/A:
- How mod ordering works?
Technically mods get new names. It renames actual mods by adding a prefix (three digit number), so original launcher loads them the way you want, even if it tries to sort them alphabeticaly. The original name is stored as a comment on .mod file, that is absolutely OK and helps to keep a track.

###### Work in progress...

# Screenshots

![](https://github.com/ARZUMATA/Universal-Mod-Organizer/blob/master/Universal%20Mod%20Organizer/pub/interface.gif)

##### Latest changelog:

v0.4-alpha:
- Profile Duplication/Rename

v0.5-alpha:
- Achievements Checker (TEST)

v0.7-alpha:
- ADDED: Profile Reset Option
- FIXED: Adding default entries games/profile if one`s missing.
- FIXED: Not updating active mods list if there are none selected before.
- Minor Changes