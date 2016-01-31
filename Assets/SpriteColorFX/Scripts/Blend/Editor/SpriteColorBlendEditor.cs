///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEditor;
using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorBlend editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorBlend))]
  public sealed class SpriteColorBlendEditor : SpriteColorBaseEditor
	{
    private SpriteColorBlend effect;

    /// <summary>
    /// Set the default values.
    /// </summary>
    protected override void ResetDefaultValues()
    {
      if (effect == null)
        effect = this.target as SpriteColorBlend;

      effect.strength = 1.0f;

      effect.SetPixelOp(SpriteColorHelper.PixelOp.Solid);

      base.ResetDefaultValues();
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
			if (effect == null)
        effect = base.target as SpriteColorBlend;

			EditorGUIUtility.fieldWidth = 40.0f;
			
      effect.strength = (float)SpriteColorFXEditorHelper.IntSliderWithReset(@"Strength", SpriteColorFXEditorHelper.TooltipStrength, Mathf.RoundToInt(effect.strength * 100.0f), 0, 100, 100) * 0.01f;

      SpriteColorHelper.PixelOp newPixelOp = (SpriteColorHelper.PixelOp)EditorGUILayout.EnumPopup(new GUIContent(@"Blend mode", @"Blend modes"), effect.pixelOp);
      if (newPixelOp != effect.pixelOp)
        effect.SetPixelOp(newPixelOp);
		}
	}
}
