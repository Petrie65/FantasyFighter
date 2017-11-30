using System.Linq;
using Borodar.RainbowFolders.Editor.Settings;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor
{
    public static class RainbowFoldersContextMenu
    {
        private const string MENU_BASE = "Assets/Rainbow Folders/";
        
        // Items
        private const string ITEM_CUSTOM = MENU_BASE + "Apply Custom";
        private const string ITEM_DEFAULT = MENU_BASE + "Revert to Default";
        private const string ITEM_SETTINGS = MENU_BASE + "Settings";
        
        // Sub-menus        
        private const string MENU_COLOR = MENU_BASE + "Color/";
        private const string MENU_TAG = MENU_BASE + "Tag/";
        private const string MENU_TYPE = MENU_BASE + "Type/";        
        private const string MENU_PLATFORM = MENU_BASE + "Platform/";        

        // Colors
        private const string COLOR_RED = MENU_COLOR + "Red";
        private const string COLOR_VERMILION = MENU_COLOR + "Vermilion";
        private const string COLOR_ORANGE = MENU_COLOR + "Orange";
        private const string COLOR_AMBER = MENU_COLOR + "Amber";
        private const string COLOR_YELLOW = MENU_COLOR + "Yellow";
        private const string COLOR_LIME = MENU_COLOR + "Lime";
        private const string COLOR_CHARTREUSE = MENU_COLOR + "Chartreuse";
        private const string COLOR_HARLEQUIN = MENU_COLOR + "Harlequin";
        private const string COLOR_GREEN = MENU_COLOR + "Green";
        private const string COLOR_EMERALD = MENU_COLOR + "Emerald";
        private const string COLOR_SPRING_GREEN = MENU_COLOR + "Spring-green";
        private const string COLOR_AQUAMARINE = MENU_COLOR + "Aquamarine";
        private const string COLOR_CYAN = MENU_COLOR + "Cyan";
        private const string COLOR_SKY_BLUE = MENU_COLOR + "Sky-blue";
        private const string COLOR_AZURE = MENU_COLOR + "Azure";
        private const string COLOR_CERULEAN = MENU_COLOR + "Cerulean";
        private const string COLOR_BLUE = MENU_COLOR + "Blue";
        private const string COLOR_INDIGO = MENU_COLOR + "Indigo";
        private const string COLOR_VIOLET = MENU_COLOR + "Violet";
        private const string COLOR_PURPLE = MENU_COLOR + "Purple";
        private const string COLOR_MAGENTA = MENU_COLOR + "Magenta";
        private const string COLOR_FUCHSIA = MENU_COLOR + "Fuchsia";
        private const string COLOR_ROSE = MENU_COLOR + "Rose";
        private const string COLOR_CRISMON = MENU_COLOR + "Crismon";

        // Tags
        private const string TAG_RED = MENU_TAG + "Red";
        private const string TAG_VERMILION = MENU_TAG + "Vermilion";
        private const string TAG_ORANGE = MENU_TAG + "Orange";
        private const string TAG_AMBER = MENU_TAG + "Amber";
        private const string TAG_YELLOW = MENU_TAG + "Yellow";
        private const string TAG_LIME = MENU_TAG + "Lime";
        private const string TAG_CHARTREUSE = MENU_TAG + "Chartreuse";
        private const string TAG_HARLEQUIN = MENU_TAG + "Harlequin";
        private const string TAG_GREEN = MENU_TAG + "Green";
        private const string TAG_EMERALD = MENU_TAG + "Emerald";
        private const string TAG_SPRING_GREEN = MENU_TAG + "Spring-green";
        private const string TAG_AQUAMARINE = MENU_TAG + "Aquamarine";
        private const string TAG_CYAN = MENU_TAG + "Cyan";
        private const string TAG_SKY_BLUE = MENU_TAG + "Sky-blue";
        private const string TAG_AZURE = MENU_TAG + "Azure";
        private const string TAG_CERULEAN = MENU_TAG + "Cerulean";
        private const string TAG_BLUE = MENU_TAG + "Blue";
        private const string TAG_INDIGO = MENU_TAG + "Indigo";
        private const string TAG_VIOLET = MENU_TAG + "Violet";
        private const string TAG_PURPLE = MENU_TAG + "Purple";
        private const string TAG_MAGENTA = MENU_TAG + "Magenta";
        private const string TAG_FUCHSIA = MENU_TAG + "Fuchsia";
        private const string TAG_ROSE = MENU_TAG + "Rose";
        private const string TAG_CRISMON = MENU_TAG + "Crismon";

        // Types
        private const string TYPE_PREFABS = MENU_TYPE + "Prefabs";
        private const string TYPE_SCENES = MENU_TYPE + "Scenes";
        private const string TYPE_SCRIPTS = MENU_TYPE + "Scripts";
        private const string TYPE_EXTENSIONS = MENU_TYPE + "Extensions";
        private const string TYPE_FLARES = MENU_TYPE + "Flares";
        private const string TYPE_PLUGINS = MENU_TYPE + "Plugins";
        private const string TYPE_TEXTURES = MENU_TYPE + "Textures";
        private const string TYPE_MATERIALS = MENU_TYPE + "Materials";
        private const string TYPE_AUDIO = MENU_TYPE + "Audio";
        private const string TYPE_PROJECT = MENU_TYPE + "Project";
        private const string TYPE_FONTS = MENU_TYPE + "Fonts";
        private const string TYPE_EDITOR = MENU_TYPE + "Editor";
        private const string TYPE_RESOURCES = MENU_TYPE + "Resources";
        private const string TYPE_SHADERS = MENU_TYPE + "Shaders";
        private const string TYPE_TERRAINS = MENU_TYPE + "Terrains";
        private const string TYPE_MESHES = MENU_TYPE + "Meshes";
        private const string TYPE_RAINBOW = MENU_TYPE + "Rainbow";
        private const string TYPE_ANIMATIONS = MENU_TYPE + "Animations";
        private const string TYPE_PHYSICS = MENU_TYPE + "Physics";

        // Platforms

        private const string PLATFORM_ANDROID = MENU_PLATFORM + "Android";
        private const string PLATFORM_IOS = MENU_PLATFORM + "iOS";
        private const string PLATFORM_MAC = MENU_PLATFORM + "Mac";
        private const string PLATFORM_WEBGL = MENU_PLATFORM + "WebGL";
        private const string PLATFORM_WINDOWS = MENU_PLATFORM + "Windows";

        // Items Priorites
        private const int DEFAULT_PRIORITY = 2100;
        private const int PRESET_PRIORITY = 2200;
        private const int SETTINGS_PRIORITY = 2300;

        //---------------------------------------------------------------------
        // Menu Items
        //---------------------------------------------------------------------

        [MenuItem(ITEM_CUSTOM, false, DEFAULT_PRIORITY)]
        public static void ApplyCustom()
        {
            var window = RainbowFoldersPopup.GetDraggableWindow();
            var position = RainbowFoldersEditorUtility.GetProjectWindow().position.position + new Vector2(10f, 30f);
            var paths = Selection.assetGUIDs.Select<string, string>(AssetDatabase.GUIDToAssetPath).Where(AssetDatabase.IsValidFolder).ToList();
            window.ShowWithParams(position, paths.ToList(), 0);
        }

        [MenuItem(ITEM_DEFAULT, false, DEFAULT_PRIORITY)]
        public static void RevertToDefault()
        {
            RevertSelectedFoldersToDefault();
        }

        [MenuItem(ITEM_SETTINGS, false, SETTINGS_PRIORITY)]
        public static void OpenSettings()
        {
            Selection.activeObject = RainbowFoldersSettings.Instance;
        }

        [MenuItem(ITEM_CUSTOM, true)]
        [MenuItem(ITEM_DEFAULT, true)]
        public static bool IsValidFolder()
        {
            var hasValidFolder = false;

            foreach (var guid in Selection.assetGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                hasValidFolder |= AssetDatabase.IsValidFolder(path);
            }

            return hasValidFolder;
        }

        // Colors

        [MenuItem(COLOR_RED, false, PRESET_PRIORITY)]
        public static void Red() { Colorize(FolderColorName.Red); }
        [MenuItem(COLOR_VERMILION, false, PRESET_PRIORITY)]
        public static void Vermilion() { Colorize(FolderColorName.Vermilion); }
        [MenuItem(COLOR_ORANGE, false, PRESET_PRIORITY)]
        public static void Orange() { Colorize(FolderColorName.Orange); }
        [MenuItem(COLOR_AMBER, false, PRESET_PRIORITY)]
        public static void Amber() { Colorize(FolderColorName.Amber); }
        [MenuItem(COLOR_YELLOW, false, PRESET_PRIORITY)]
        public static void Yellow() { Colorize(FolderColorName.Yellow); }
        [MenuItem(COLOR_LIME, false, PRESET_PRIORITY)]
        public static void Lime() { Colorize(FolderColorName.Lime); }
        [MenuItem(COLOR_CHARTREUSE, false, PRESET_PRIORITY)]
        public static void Chartreuse() { Colorize(FolderColorName.Chartreuse); }
        [MenuItem(COLOR_HARLEQUIN, false, PRESET_PRIORITY)]
        public static void Harlequin() { Colorize(FolderColorName.Harlequin); }
        [MenuItem(COLOR_GREEN, false, PRESET_PRIORITY + 100)]
        public static void Green() { Colorize(FolderColorName.Green); }
        [MenuItem(COLOR_EMERALD, false, PRESET_PRIORITY + 100)]
        public static void Emerald() { Colorize(FolderColorName.Emerald); }
        [MenuItem(COLOR_SPRING_GREEN, false, PRESET_PRIORITY + 100)]
        public static void SpringGreen() { Colorize(FolderColorName.SpringGreen); }
        [MenuItem(COLOR_AQUAMARINE, false, PRESET_PRIORITY  + 100)]
        public static void Aquamarine() { Colorize(FolderColorName.Aquamarine); }
        [MenuItem(COLOR_CYAN, false, PRESET_PRIORITY  + 100)]
        public static void BondiBlue() { Colorize(FolderColorName.Cyan); }
        [MenuItem(COLOR_SKY_BLUE, false, PRESET_PRIORITY  + 100)]
        public static void SkyBlue() { Colorize(FolderColorName.SkyBlue); }
        [MenuItem(COLOR_AZURE, false, PRESET_PRIORITY + 100)]
        public static void Azure() { Colorize(FolderColorName.Azure); }
        [MenuItem(COLOR_CERULEAN, false, PRESET_PRIORITY + 100)]
        public static void Cerulean() { Colorize(FolderColorName.Cerulean); }
        [MenuItem(COLOR_BLUE, false, PRESET_PRIORITY + 200)]
        public static void Blue() { Colorize(FolderColorName.Blue); }
        [MenuItem(COLOR_INDIGO, false, PRESET_PRIORITY + 200)]
        public static void Indigo() { Colorize(FolderColorName.Indigo); }
        [MenuItem(COLOR_VIOLET, false, PRESET_PRIORITY + 200)]
        public static void Violet() { Colorize(FolderColorName.Violet); }
        [MenuItem(COLOR_PURPLE, false, PRESET_PRIORITY + 200)]
        public static void Purple() { Colorize(FolderColorName.Purple); }
        [MenuItem(COLOR_MAGENTA, false, PRESET_PRIORITY + 200)]
        public static void Magenta() { Colorize(FolderColorName.Magenta); }
        [MenuItem(COLOR_FUCHSIA, false, PRESET_PRIORITY + 200)]
        public static void Fuchsia() { Colorize(FolderColorName.Fuchsia); }
        [MenuItem(COLOR_ROSE, false, PRESET_PRIORITY + 200)]
        public static void Rose() { Colorize(FolderColorName.Rose); }
        [MenuItem(COLOR_CRISMON, false, PRESET_PRIORITY + 200)]
        public static void Crismon() { Colorize(FolderColorName.Crismon); }

        // Tags

        [MenuItem(TAG_RED, false, PRESET_PRIORITY)]
        public static void TagRed() { AssignTag(FolderTagName.Red); }
        [MenuItem(TAG_VERMILION, false, PRESET_PRIORITY)]
        public static void TagVermilion() { AssignTag(FolderTagName.Vermilion); }
        [MenuItem(TAG_ORANGE, false, PRESET_PRIORITY)]
        public static void TagOrange() { AssignTag(FolderTagName.Orange); }
        [MenuItem(TAG_AMBER, false, PRESET_PRIORITY)]
        public static void TagAmber() { AssignTag(FolderTagName.Amber); }
        [MenuItem(TAG_YELLOW, false, PRESET_PRIORITY)]
        public static void TagYellow() { AssignTag(FolderTagName.Yellow); }
        [MenuItem(TAG_LIME, false, PRESET_PRIORITY)]
        public static void TagLime() { AssignTag(FolderTagName.Lime); }
        [MenuItem(TAG_CHARTREUSE, false, PRESET_PRIORITY)]
        public static void TagChartreuse() { AssignTag(FolderTagName.Chartreuse); }
        [MenuItem(TAG_HARLEQUIN, false, PRESET_PRIORITY)]
        public static void TagHarlequin() { AssignTag(FolderTagName.Harlequin); }
        [MenuItem(TAG_GREEN, false, PRESET_PRIORITY + 100)]
        public static void TagGreen() { AssignTag(FolderTagName.Green); }
        [MenuItem(TAG_EMERALD, false, PRESET_PRIORITY + 100)]
        public static void TagEmerald() { AssignTag(FolderTagName.Emerald); }
        [MenuItem(TAG_SPRING_GREEN, false, PRESET_PRIORITY + 100)]
        public static void TagSpringGreen() { AssignTag(FolderTagName.SpringGreen); }
        [MenuItem(TAG_AQUAMARINE, false, PRESET_PRIORITY  + 100)]
        public static void TagAquamarine() { AssignTag(FolderTagName.Aquamarine); }
        [MenuItem(TAG_CYAN, false, PRESET_PRIORITY  + 100)]
        public static void TagBondiBlue() { AssignTag(FolderTagName.Cyan); }
        [MenuItem(TAG_SKY_BLUE, false, PRESET_PRIORITY  + 100)]
        public static void TagSkyBlue() { AssignTag(FolderTagName.SkyBlue); }
        [MenuItem(TAG_AZURE, false, PRESET_PRIORITY + 100)]
        public static void TagAzure() { AssignTag(FolderTagName.Azure); }
        [MenuItem(TAG_CERULEAN, false, PRESET_PRIORITY + 100)]
        public static void TagCerulean() { AssignTag(FolderTagName.Cerulean); }
        [MenuItem(TAG_BLUE, false, PRESET_PRIORITY + 200)]
        public static void TagBlue() { AssignTag(FolderTagName.Blue); }
        [MenuItem(TAG_INDIGO, false, PRESET_PRIORITY + 200)]
        public static void TagIndigo() { AssignTag(FolderTagName.Indigo); }
        [MenuItem(TAG_VIOLET, false, PRESET_PRIORITY + 200)]
        public static void TagViolet() { AssignTag(FolderTagName.Violet); }
        [MenuItem(TAG_PURPLE, false, PRESET_PRIORITY + 200)]
        public static void TagPurple() { AssignTag(FolderTagName.Purple); }
        [MenuItem(TAG_MAGENTA, false, PRESET_PRIORITY + 200)]
        public static void TagMagenta() { AssignTag(FolderTagName.Magenta); }
        [MenuItem(TAG_FUCHSIA, false, PRESET_PRIORITY + 200)]
        public static void TagFuchsia() { AssignTag(FolderTagName.Fuchsia); }
        [MenuItem(TAG_ROSE, false, PRESET_PRIORITY + 200)]
        public static void TagRose() { AssignTag(FolderTagName.Rose); }
        [MenuItem(TAG_CRISMON, false, PRESET_PRIORITY + 200)]
        public static void TagCrismon() { AssignTag(FolderTagName.Crismon); }

        // Types

        [MenuItem(TYPE_ANIMATIONS, false, PRESET_PRIORITY)]
        public static void TypeAnimations() { AssingType(FolderTypeName.Animations); }
        [MenuItem(TYPE_AUDIO, false, PRESET_PRIORITY)]
        public static void TypeAudio() { AssingType(FolderTypeName.Audio); }
        [MenuItem(TYPE_EDITOR, false, PRESET_PRIORITY)]
        public static void TypeEditor() { AssingType(FolderTypeName.Editor); }
        [MenuItem(TYPE_EXTENSIONS, false, PRESET_PRIORITY)]
        public static void TypeExtensions() { AssingType(FolderTypeName.Extensions); }
        [MenuItem(TYPE_FLARES, false, PRESET_PRIORITY)]
        public static void TypeFlares() { AssingType(FolderTypeName.Flares); }
        [MenuItem(TYPE_FONTS, false, PRESET_PRIORITY)]
        public static void TypeFonts() { AssingType(FolderTypeName.Fonts); }
        [MenuItem(TYPE_MATERIALS, false, PRESET_PRIORITY)]
        public static void TypeMaterials() { AssingType(FolderTypeName.Materials); }
        [MenuItem(TYPE_MESHES, false, PRESET_PRIORITY)]
        public static void TypeMeshes() { AssingType(FolderTypeName.Meshes); }
        [MenuItem(TYPE_PHYSICS, false, PRESET_PRIORITY)]
        public static void TypePhysics() { AssingType(FolderTypeName.Physics); }
        [MenuItem(TYPE_PLUGINS, false, PRESET_PRIORITY)]
        public static void TypePlugins() { AssingType(FolderTypeName.Plugins); }
        [MenuItem(TYPE_PREFABS, false, PRESET_PRIORITY)]
        public static void TypePrefabs() { AssingType(FolderTypeName.Prefabs); }
        [MenuItem(TYPE_PROJECT, false, PRESET_PRIORITY)]
        public static void TypeProject() { AssingType(FolderTypeName.Project); }
        [MenuItem(TYPE_RAINBOW, false, PRESET_PRIORITY)]
        public static void TypeRainbow() { AssingType(FolderTypeName.Rainbow); }
        [MenuItem(TYPE_RESOURCES, false, PRESET_PRIORITY)]
        public static void TypeResources() { AssingType(FolderTypeName.Resources); }
        [MenuItem(TYPE_SCENES, false, PRESET_PRIORITY)]
        public static void TypeScenes() { AssingType(FolderTypeName.Scenes); }
        [MenuItem(TYPE_SCRIPTS, false, PRESET_PRIORITY)]
        public static void TypeScripts() { AssingType(FolderTypeName.Scripts); }
        [MenuItem(TYPE_SHADERS, false, PRESET_PRIORITY)]
        public static void TypeShaders() { AssingType(FolderTypeName.Shaders); }
        [MenuItem(TYPE_TERRAINS, false, PRESET_PRIORITY)]
        public static void TypeTerrains() { AssingType(FolderTypeName.Terrains); }
        [MenuItem(TYPE_TEXTURES, false, PRESET_PRIORITY)]
        public static void TypeTextures() { AssingType(FolderTypeName.Textures); }

        // Platforms
        [MenuItem(PLATFORM_ANDROID, false, PRESET_PRIORITY)]
        public static void PlatformAndroid() { AssingPlatform(FolderPlatformName.Android); }
        [MenuItem(PLATFORM_IOS, false, PRESET_PRIORITY)]
        public static void PlatformiOS() { AssingPlatform(FolderPlatformName.iOS); }
        [MenuItem(PLATFORM_MAC, false, PRESET_PRIORITY)]
        public static void PlatformMac() { AssingPlatform(FolderPlatformName.Mac); }
        [MenuItem(PLATFORM_WEBGL, false, PRESET_PRIORITY)]
        public static void PlatformWebGL() { AssingPlatform(FolderPlatformName.WebGL); }
        [MenuItem(PLATFORM_WINDOWS, false, PRESET_PRIORITY)]
        public static void PlatformWindows() { AssingPlatform(FolderPlatformName.Windows); }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static void Colorize(FolderColorName color)
        {
            var icons = FolderColorsStorage.Instance.GetIconsByColor(color);
            ChangeSelectedFoldersIcons(icons);
        }

        private static void AssignTag(FolderTagName tag)
        {
            var icons = FolderTagsStorage.Instance.GetIconsByTag(tag);
            ChangeSelectedFoldersIcons(icons);
        }

        private static void AssingType(FolderTypeName type)
        {
            var icons = FolderTypesStorage.Instance.GetIconsByType(type);
            ChangeSelectedFoldersIcons(icons);
        }

        private static void AssingPlatform(FolderPlatformName platform)
        {
            var icons = FolderPlatformsStorage.Instance.GetIconsByType(platform);
            ChangeSelectedFoldersIcons(icons);
        }

        private static void ChangeSelectedFoldersIcons(FolderIconPair icons)
        {
            Selection.assetGUIDs.ToList().ForEach(
                assetGuid =>
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                    if (!AssetDatabase.IsValidFolder(assetPath)) return;

                    var folder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(assetPath);
                    var path = AssetDatabase.GetAssetPath(folder);
                    RainbowFoldersSettings.Instance.ChangeFolderIconsByPath(path, icons);
                }
            );
        }

        private static void RevertSelectedFoldersToDefault()
        {
            Selection.assetGUIDs.ToList().ForEach(
                assetGuid =>
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                    if (AssetDatabase.IsValidFolder(assetPath))
                    {
                        RainbowFoldersSettings.Instance.RemoveAllByPath(assetPath);
                    }
                }
            );
        }
    }
}