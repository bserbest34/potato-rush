using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
    
[InitializeOnLoad]
public static class HeaderOrginazator
{
    static HeaderOrginazator()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null && gameObject.name.StartsWith("//", System.StringComparison.Ordinal))
        {
            EditorGUI.DrawRect(selectionRect, Color.gray);
            EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("//", "").ToUpperInvariant());
        }
    }
}

#endif