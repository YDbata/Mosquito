using System;
using System.Collections;
using System.Collections.Generic;
using Mosquito.Utils;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    
    [Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    [SerializeField] public CursorType currentCursorType = CursorType.None; 
    [SerializeField]
    private CursorMapping[] cursorMappings = null;

    public void SetCursor(CursorType cursortype)
    {
        CursorMapping mapping = GetCursorMapping(cursortype);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        currentCursorType = cursortype;
    }
    
    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach(CursorMapping mapping in cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }
        return cursorMappings[0];
    }
}
