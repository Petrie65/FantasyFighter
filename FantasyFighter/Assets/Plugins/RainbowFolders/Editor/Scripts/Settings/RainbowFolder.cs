using System;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor.Settings
{
    [Serializable]
    public class RainbowFolder
    {
        public KeyType Type;
        public string Key;
        public bool IsRecursive;

        public Texture2D SmallIcon;
        public Texture2D LargeIcon;

        //---------------------------------------------------------------------
        // Ctors
        //---------------------------------------------------------------------

        public RainbowFolder(RainbowFolder value)
        {
            Type = value.Type;
            Key = value.Key;
            IsRecursive = value.IsRecursive;
            SmallIcon = value.SmallIcon;
            LargeIcon = value.LargeIcon;
        }

        public RainbowFolder(KeyType type, string key)
        {
            Type = type;
            Key = key;
        }

        public RainbowFolder(KeyType type, string key, FolderIconPair icons)
        {
            Type = type;
            Key = key;
            SmallIcon = icons.SmallIcon;
            LargeIcon = icons.LargeIcon;
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public void CopyFrom(RainbowFolder target)
        {
            Type = target.Type;
            Key = target.Key;
            IsRecursive = target.IsRecursive;
            SmallIcon = target.SmallIcon;
            LargeIcon = target.LargeIcon;
        }

        public bool HasAtLeastOneIcon()
        {
            return SmallIcon != null || LargeIcon != null;
        }

        //---------------------------------------------------------------------
        // Nested
        //---------------------------------------------------------------------

        public enum KeyType
        {
            Name,
            Path
        }
    }
}