using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Core;

public partial class VRCSdkControlPanel : EditorWindow
{
    private static GUIStyle ChangeImage;
    Vector2 changesScroll;
	
    void ShowChanges()
    {

        ChangeImage = new GUIStyle
        {
            normal =
            {
             background = Resources.Load("SettingsChanges") as Texture2D,
            },
            fixedHeight = 100
        };
        GUI.backgroundColor = Color.white;
        GUILayout.Box("", ChangeImage);
        GUI.backgroundColor = new Color(
UnityEditor.EditorPrefs.GetFloat("SDKColor_R"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_G"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_B"),
UnityEditor.EditorPrefs.GetFloat("SDKColor_A")
);

        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("The Black Arms Website"))
        {
            Application.OpenURL("http://theblackarms.servehttp.com/");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal(boxGuiStyle, GUILayout.Height(26));
        if (GUILayout.Button("Latest SDX Release Page"))
        {
            Application.OpenURL("https://www.github.com/TheBlackArms/TheBlackArmsSDX/releases/latest");
        }
        if (GUILayout.Button("SDX Support Server"))
        {
            Application.OpenURL("https://discord.gg/A9dca3N");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical(boxGuiStyle);
        EditorGUILayout.LabelField("The Black Arms Discord: theblackarms.cf", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("The Black Arms SDX Dev: PhoenixAceVFX", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("The Black Arms SDX Contributors", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Wolfie (Huge help with legal and github!)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("PoH (taught me how to mod upload panels)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("TheGamingBram (awesome script work)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("ODDS (SDX is based in ODDS)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Controser (AntiYoink[In 3.0] and SENTINEL IMPORTER)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Zombie2312 (Total VPS Hosting)", EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();

        changesScroll = EditorGUILayout.BeginScrollView(changesScroll, GUILayout.Width(SdkWindowWidth));
		EditorGUILayout.BeginVertical();
		GUILayout.Label(
        @"You are fully free to contribute to the SDX
All edits still remain under my copyright ownership
This is done via CLA

Note: If your going to ask me to make you a SDK/SDX
I will only make a THEME PACK for my SDX as it is
Much easier to do than to make an entire remake
This is completely free of charge as I refuse to make
Any money or profit from my SDX and I will not rebrand
My SDX, this decision is final and wont be changed.

The Black Arms SDX 2.3.1
Removed copyright fraud issue (Bunny had stolen the scripts, I met the real maker)
The creator also wishes to remain anonymous with their scripts

The Black Arms SDX 2.3
Replaced Links on control panel to all go to website now
New discord as the old got just got false banned again

The Black Arms SDX 2.2.1
Updated Links for Import Panel (webserver url change)

The Black Arms SDX 2.2
Migrated EVERYTHING to a dedicated host!
-Huge thanks to Zombie2312 for the hosting!

The Black Arms SDX 2.1.1
Added a few extra assets

The Black Arms SDX 2.1
Added several utilities (thanks to bunny!)
-AddVRCAvatarPedestals
-ChangePropertiesOfProject
-CopyBones
-CopyComponents
-Duplicator
-FindMaterial
-FindWrongScripts
-GetPropertiesOfObject
-LoadBundle (Unreal Loader)
-MeshGenerator
-MeshToAsset

The Black Arms SDX 2.0.3
TBA got banned again so I updated links
SDX Support server is now SDX community server
New Main Servers (Invite via TBA Discord & Lonely Souls Buttons)

The Black Arms SDX 2.0.2
Integrated Sentinels Importer (No Errors)

The Black Arms SDX 2.0.1
Improved some performance issues
Invisible control pane buttons should be fixed

The Black Arms SDX 2.0
Thats right Version 2
We have updated many things in this update
Several scripts have been modified
Antiyoink is now in both revisions
Upload methods changed again (sorry)
Lots of bug fixes (too long of a list)
File structure changes completed (that wont happen for awhile now)
Now converting assets to import system (like sentinel importer)
Overhaul of some DLLs (many edits to those)

The Black Arms SDX 1.12.5
File Structure Changes

The Black Arms SDX 1.12.4
Updated File Appends

The Black Arms SDX 1.12.3
New upload method implemented.

The Black Arms SDX 1.12.3 BETA
Testing new Upload Method

The Black Arms SDX 1.12.2
Added more Editor Tools to Assets
Fixed bug with SDX Control Panel Buttons
-They are no longer invisible on first import!

The Black Arms SDX 1.12.1
Moved SENTINEL IMPORTER from pre-imported to external
-This is in the assets panel until further notice
-Be sure to remove it when uploading or errors occur

The Black Arms SDX 1.12
Added SENTINEL IMPORTER (The Free One)

The Black Arms SDX 1.11.1
Fixed compile error (WHOOPS)

The Black arms SDX 1.11
Rewritten a few things
Preparing for FULL REWRITE of the SDX
Added a few new functions
Adding AntiYoink to this version soon

The Black Arms SDX 1.10.10
Slight expansion to Imports Pane

The Black Arms SDX 1.10.9
Fixed position of color selector in Control Panel Settings

The Black Arms SDX 1.10.8
Added Support Server Links

The Black Arms SDX 1.10.7
Updated Control Panel Layouts

The Black Arms SDX 1.10.6
Fixed DEDSEC Invite Links

The Black Arms SDX 1.10.5
Repaired Discord Links
Added Guilded.gg Links

The Black Arms SDX 1.10.4
Updated Discord Links for The Black Arms

The Black Arms SDX 1.10.3
Fixed Overlapping Text on Upload Panels (whoops)

The Black Arms SDX 1.10.2
Fixed RGB Text on Upload Panels (I cant believe it broke)

The Black Arms SDX 1.10.1
Forgot to add the new Assets for 1.10 (lol)
Removed dead links (I think I got them all)

The Black Arms SDX 1.10
Updated version scheme!
Added a new Loadbundle as a COMPONENT script
-Add it to an object to use it
Modified Changelog Pane

The Black Arms SDX 1.9.2.3-1R
Updated a few minor things
Now under CLA instead of licensing

The Black Arms SDX 1.9.2.3R
Now home in 2 Discords
DEDSEC and The Black Arms: UNLEASHED
Fixed a DLL issue
Fixed bug with ESR crashing unity
LICENSE Information added to Github
Thanks to Frostbite for allowing TBA SDX to be home
-In DEDSEC

The Black Arms SDX 1.9.2.2-5R
Updated DiscordRPC
My discord just got false banned unfairly
We are merging with DEDSEC

The Black Arms SDX 1.9.2.2-4R
OG DISCORD LINK IS BACK!
Huge thanks to W0lfy for boosting the server 12 times!

The Black Arms SDX 1.9.2.2-3R
Refixed all discord links (server got shadow banned)

The Black Arms SDX 1.9.2.2-2 RELEASE
Cleaned the ASSETS PANEL (For legal reasons)
-Compressed it too so its not as lengthy
-Added theme packs to ADDONS subsection
-All paid assets removed from ASSETS PANEL

The Black Arms SDX 1.9.2.2-1 RELEASE
Finalized the LICENSE
Fixed minor underlying bugs
Theme packs are now officially suppoerted by the SDX

The Black Arms SDX 1.9.2.2 RELEASE
License is now official in full effect
Theme Packs are on the way!
-This will allow me to make custom looks to the SDX
-All without having to remake the SDX repeatedly!
All functions are tested and ready for RELEASE
Splash Screen modified
-Now points out that there is a license

The Black Arms SDX 1.9.2.2 BETA
Updated Banners (planning to release theme packages)
Improved DLL optimizations (hard to notice lol)
The SDX made it to GITHUB!
Product is fully OPEN SOURCED
License added (Because I have to)

The Black Arms SDX 1.9.2.1 RELEASE
-This is a small update
Moved ALL menu items from SDX to Phoenix/<choices>
-Thanks to TheGamingBram for the DLL work!
-This also includes the TBA functions shared (Now in normal font!)
All 'The Gaming Evolution' funcions are now under Phoenix/TGE/<Choices here>
Fixed a minor crash issue
Added new ASSETS to the ASSETS Panel (Still external downloads)

The Black Arms SDX 1.9.2 RELEASE
Obfuscation of the SDX is coming, I have no choice
Added EMPTY SCRIPT REMOVER (its about fucking time)
-This is found under Phoenix/SDK/Utility/ESR
-Cant locate the TRUE creator so I cannot credit them
Improved a few things
Revised Assets Panel
Preparing for 2.0.0 is very close, updates become rare now

The Black Arms SDX 1.9.2 BETA
Improving asset bundling
Updated to newest VRCSDK2 revision (2020.06.16.20.54)
Changed upload method, seeing if it is faster or slower
Think I fixed a bug relating to world uploads processing so slow
Reworked some weird code in hopes to fix crashing issues
This is not a TRUE RELEASE, this is a OPTIONAL BETA

The Black Arms SDX 1.9.1 RELEASE
Proper credits given where they are due
Redone control panel images (yes they are high res)
Reworked some code (trying to fix the colorless control panel issue)
A few minor changes thats all

The Black Arms SDX 1.9.0 RELEASE
Loadbundle should be able to load worlds now
New HWID and IP Spoofing method needing testing
2FA support bug fixed
Issue with UI buttons being glitched in LIGHT THEME should be fixed
-I honestly dont know if I fixed this issue if not then its unity
and not the SDX having this issue
Fixed new upload method to make avatar uploads faster and smaller
Added new VRC Avatar Editor to imports

The Black Arms SDX 1.8.9 RELEASE
Cleaned some code (literally)
Fixed Upload Panels (dunno how tf i broke that)
Changed Upload Method trying to speed up uploads

The Black Arms SDX 1.8.8 RELEASE
Fixed minor bugs
Updated some resources
Directly modified some DLLs to enforce spoofed information

The Black Arms SDX 1.8.7 RELEASE
Fixed minor issue I didn't notice at first
Avatar Upload Panel Fixed
Backgrounds are always animated now (whoops xD)
Audio keeps playing now (whoops again)

The Black Arms SDX 1.8.6 RELEASE
Repaired missing options from BETA
Added to import panel BIG TIME (go nuts!)
Resolved IP and HWID spoofing bug (kek, cant ban us!)
Added very useful features from TBA SDK (no joke, good stuff!)
-Yes i have permission as credit is given where credit is due
Expanded SDX Addons
Expanded Shader List
Expanded Avatar List
Removed Duped Imports

The Black Arms SDX 1.8.6 BETA (BETA UPDATE)
Added image banners to the control panels (why not xD)
Optimizing things
BETA?!
This is a BETA build of the SDX, some features are missing as I am
trying to fix these issues before pushing the final build, this is
why i made this a BETA build.

The Black Arms SDX 1.8.5 (MAJOR UPDATE)
I found a way to LOWER the SDX size (bout fuckin time)
Changed IMPORT PANEL to ASSETS PANEL
All IMPORTS are now DOWNLOADED EXTERNALLY with the ASSETS PANEL buttons
File size cut back heavily
Updated to latest VRCSDK2 revision
Updated SDX Banner (Yes its my own)
Bug fixes

The Black Arms SDX 1.8.4 (Minor)
Fixed a few typos (whoops)
Updated DiscordRPC (now it says I made this too)
Fixed a few scripts (how tf does the API scripts bug out?)
Fixed DLLs (dont ask, it was a pain to fix)
Updated to new VRCSDK2 revision
IP+HWID Spoofing (fuck bans)
Repaired buggy upload button

The Black Arms 1.8.3 SDX (LoadBundle)
Loadbundle now natively in Control Panel
Hopefully fixed upload time issues
Fixed a bug that caused unity crashes
Removed obsolete scripts from old version

The Black Arms SDX 1.8.2 (Compression Update)
Heavy increase in import speed
SDX Addons (Importables now)
-Compacted importables (enable them using [Unpack Importable Assets])
-Reduced base upload panel heavily (bring it back with [Animated Backgrounds & Background Music])
New assets!
-Poiyomi Patreon Pack

The Black Arms SDX 1.8.1
Updated importable assets
Repairs
Fixed typos
ONLY SUPPORTS 2018.4.20f1

The Black Arms SDX 1.8.0
Added an SDK Color Bar (Got from TBA SDK by TheGamingBram)
Redone avatar panel (upload button appears when tos box checked)
VRCSDK3 support!

The Black Arms SDX 1.7.9 (Minor Update)
Minor Fixes
Finally modified world upload panel placement
Fixed some buggy code
Updated to newest VRCSDK2 revision

The Black Arms SDX 1.7.8 (Major Update)
Name Change! VFX SDK is not the name anymore
Minor changes in this update
Fixed a few buggy scripts
Added a crap ton of music to the SDK
Massive File (Sorry cant avoid this)
Supports new Unity version
Supports new VRChat update
Limits still remain BROKEN

VFX SDK 1.7.7 (OVERHAUL UPDATE)
Mass overhaul throughout the entire sdk
Moved all panels to CONTROL PANEL
Moved over to ODDS SDK
Used quite a bit of code from The Gaming Evolution SDK
Much bigger file (sadly)
UI Overhaul
Upload panel modifications
-Volume bar for upload panels
-Reworked upload panels setup
-Added many songs and videos
Removed all NanoSDK files

VFX SDK 1.7.6
Condensed Import Panel
-Removed the word Import from the buttons
-Added Phoenix Eye (yoinked shader)
Cleaned UI
-Toolbar button now is working in conjunction with PheonixFX

VFX SDK 1.7.5
Compacted the import panel
-2 assets per line now (Smaller menu)
Added more assets
-Gecko (Avatar)
-Inventory System Scripts (Make toggles for emote menu)
-ZalgoBLYAT (Shader)
-Question of time shader (Shadertoy)
Fixed buggy DiscordRPC again (fucking discord-rpc.dll)

VFX SDK 1.7.4
Added new importables (Increased size again, would make it smaller with more if possible)
-Dope shader (ScreenFX)
-GVAS Scripts (Like Bitanimator but more advanced)
-Xffect Pro (Effect Editor)
-Cartoon FX 3 (Effect Prefabs)
Fixed UI Issues with Fonts
-Changed some fonts (3 Fonts total)
-Fixed issues with fonts losing color (RGB Script was being weird)
Bug Fixes
-Upload errors should be 73% less likely than they were (originally was a 13.7% chance)
-Fixed compression type issues (Server wouldn't accept compression type, now pretends to be normal type to bypass this)
-Fixed issue with possible compiling errors (DiscordRPC was giving issues, snapped my fingers on some code)
-Fixed import panel issue (sometimes didn't import at all)
-Fixed issue with API errors (these shouldn't happen again)
-Fixed VPN issues (sometimes server rejected VPN connections, shouldn't happen now)
-Fixed issue with DiscordRPC not loading (Had to replace DiscordRPC.dll)
-NanoSDK buttons are GONE! (Found the last bastard hiding in a DLL)
Bypasses
-Limits Bypassed (Polys, Particles, Meshes, Etc)
-Bans dont record unity info (Fuck you tupper!)
-Generates fake HWID to ensure ban prevention (Again, Fuck you tupper!)
-IP Spoofing (Not sure if this is working yet, Core DLLs should manage this)
Options
-Can toggle video background from VFX SDK/Settings
-Can toggle DiscordRPC from VFX SDK/Settings
Quick build panel
-Nonfunctional (Causes insane lag for some reason, cant remove without compiling errors all over)
Auto Visemes
-FIXED! (Should map Visemes for you now!)
Quest Bypasses
-WIP (How the fuck did they secure that?)

VFX SDK 1.7.3
UI changes
-Ui text boxes are now fully transparent
changed the compression type for a smaller upload size
-faster uploads come with this

VFX SDK 1.7.2
Ui changes
-Changed UI buttons from Black to Red (cause unity light theme font color issues)
-fixed font issue

VFX SDK 1.7.1
Minor UI Changes
-Changed Upload Button
-Changed some Text Labels
-Added static background if video background is disabled
-Changed background audio when uploading

VFX SDK 1.7
UNITY UI BUTTONS ARE NOW BLACK
FULL RGB TEXT ON UPLOAD PANELS
ADDED MUSIC TO UPLOAD SCREENS (BG AUDIO)

VFX SDK 1.6.9
FIXED IMPORT PANEL
Assets are now available in the IMPORT PANEL
Compressed EVERYTHING to the proper format
Changed Compression Type in attempt to IMPROVE upload speeds

VFX SDK 1.6.8
Credit given to PoH for base used and for help
fixed upload UI
compressed a few files
fixed the DiscordRPC

VFX SDK 1.6.7
Forgot to fix import panel again xD
expanded all assets I could so they're pre imported
finished cleaning a few things
PHOENIX FX ADDED
import time increased (cause of all the shaders i added)

VFX SDK 1.6.5
imports not working, import manually from the following location
Assets/VRCSDK/nanoSDK/Importables
Few bug fixes
Auto vismes fixed
FIRST PUBLIC RELEASE!");
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
    }
}
