using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditorInternal;
using UnityEngine;

namespace UnityTools.Editor
{


    public static class LayoutSwitcherTool
    {


        static bool OpenLayout(string name)
        {
            var path = GetWindowLayoutPath(name);

            if (string.IsNullOrWhiteSpace(path))
            {
             Debug.LogError($"Failed to find layout: {name}");
                return false;
            }

            Type windowLayoutType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.WindowLayout");

            if (windowLayoutType != null)
            {
                MethodInfo tryLoadingWindowLayoutMethod = windowLayoutType.GetMethod("TryLoadWindowLayout", BindingFlags.Public | BindingFlags.Static, 
                    null, new Type[] { typeof(string), typeof(bool) }, null);

                if (tryLoadingWindowLayoutMethod != null)
                {
                    var arguments = new object[] { path, false };
                    bool result = (bool)tryLoadingWindowLayoutMethod.Invoke(null, arguments);
                    return result;
                }
                else
                {
                    Debug.LogError("Failed to find method: TryLoadingWindowLayout");
                }
            }
            else
            {
                Debug.LogError("Failed to find type: UnityEditor.WindowLayout");
            }
            return false;
        }

        [Shortcut("LayoutSwitcher/Layout1", KeyCode.Alpha1, ShortcutModifiers.Alt)]
        public static void Layout1MenuItem()
        {
            Debug.Log("Switching to Layout 1");
            OpenLayout("DefaultElisa");
        }

        [Shortcut("LayoutSwitcher/Layout2", KeyCode.Alpha2, ShortcutModifiers.Alt)]
        public static void Layout2MenuItem()
        {
            Debug.Log("Switching to Layout 2");
            OpenLayout("GameTest");
        }

        [Shortcut("LayoutSwitcher/Layout3", KeyCode.Alpha3, ShortcutModifiers.Alt)]
        public static void Layout3MenuItem()
        {
            OpenLayout("ProBuilder");
        }

        [Shortcut("LayoutSwitcher/Layout4", KeyCode.Alpha4, ShortcutModifiers.Alt)]
        public static void Layout4MenuItem()
        {
            OpenLayout("Animation");
        }

        static string GetWindowLayoutPath(string name)
        {
            var layoutPreferencesPath = Path.Combine(InternalEditorUtility.unityPreferencesFolder, "Layouts");
            var layoutModePreferencesPath = Path.Combine(layoutPreferencesPath, ModeService.currentId);

            if(Directory.Exists(layoutModePreferencesPath))
            {
                string[] layoutPaths = Directory.GetFiles(layoutModePreferencesPath).Where(path => path.EndsWith(".wlt")).ToArray();

                if(layoutPaths != null)
                {
                    foreach (var layoutPath in layoutPaths)
                    {
                        if (string.Compare(name, Path.GetFileNameWithoutExtension(layoutPath)) == 0)
                        {
                            return layoutPath;
                        }
                    }
                }

            }
            return null;
        }
    }
  
}
  
