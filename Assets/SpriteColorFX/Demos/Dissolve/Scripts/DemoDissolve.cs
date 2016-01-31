///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorDissolve Demo.
  /// </summary>
  [RequireComponent(typeof(SpriteColorDissolve))]
  public sealed class DemoDissolve : MonoBehaviour
  {
    public bool showGUI = true;

    private SpriteColorDissolve spriteColorDissolve;

    private static int knightGlobalID = 0;

    private int knightID;

    private void OnEnable()
    {
      spriteColorDissolve = gameObject.GetComponent<SpriteColorDissolve>();

      knightID = knightGlobalID++;
    }

    private void Update()
    {
      spriteColorDissolve.dissolveAmount = (0.75f + (knightID % 2 == 0 ? Mathf.Sin(Time.time * 0.75f) : Mathf.Cos(Time.time * -0.75f))) * 0.6f;
    }

    private void OnGUI()
    {
      if (showGUI == true)
      {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        GUILayout.BeginArea(new Rect(screenPosition.x - 75.0f, Screen.height - screenPosition.y + 96.0f, 100.0f, 50.0f));
        {
          GUILayout.BeginHorizontal();
          {
            GUILayout.FlexibleSpace();

            GUILayout.Label(spriteColorDissolve.dissolveTextureType.ToString());

            GUILayout.FlexibleSpace();
          }
          GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();
      }
    }
  }
}