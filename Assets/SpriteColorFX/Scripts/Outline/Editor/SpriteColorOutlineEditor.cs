﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace SpriteColorFX
{
  /// <summary>
  /// SpriteColorOutline editor.
  /// </summary>
  [CustomEditor(typeof(SpriteColorOutline))]
  public sealed class SpriteColorOutlineEditor : SpriteColorBaseEditor
  {
    private SpriteColorOutline effect;

    /// <summary>
    /// Set the default values.
    /// </summary>
    protected override void ResetDefaultValues()
    {
      if (effect == null)
        effect = this.target as SpriteColorOutline;

      effect.outlineSize = 0.0075f;
      effect.gradientScale = 1.0f;
      effect.gradientOffset = 0.0f;
      effect.outlineTextureUVParams = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
      effect.outlineTextureUVAngle = 0.0f;

      base.ResetDefaultValues();
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
      if (effect == null)
        effect = this.target as SpriteColorOutline;

      EditorGUIUtility.fieldWidth = 40.0f;

      effect.Mode = (SpriteColorOutline.OutlineMode)EditorGUILayout.EnumPopup(new GUIContent(@"Mode", @"Outline mode"), effect.Mode);

      effect.outlineSize = SpriteColorFXEditorHelper.SliderWithReset(@"Size", @"Outline width", effect.outlineSize * (100.0f / 0.03f), 0.0f, 100.0f, 25.0f) * (0.03f / 100.0f);

      if (effect.Mode == SpriteColorOutline.OutlineMode.Normal)
        effect.outlineColor = EditorGUILayout.ColorField(@"Color", effect.outlineColor);
      else if (effect.Mode == SpriteColorOutline.OutlineMode.Gradient)
      {
        effect.Direction = (SpriteColorOutline.GradientDirection)EditorGUILayout.EnumPopup(new GUIContent(@"Direction", @"Gradient direction"), effect.Direction);

        effect.outlineColor = EditorGUILayout.ColorField(@"Color #1", effect.outlineColor);

        effect.outlineColor2 = EditorGUILayout.ColorField(@"Color #2", effect.outlineColor2);

        effect.gradientScale = SpriteColorFXEditorHelper.SliderWithReset(@"Scale", @"Gradient scale", effect.gradientScale, -10.0f, 10.0f, 1.0f);

        effect.gradientOffset = SpriteColorFXEditorHelper.SliderWithReset(@"Offset", @"Gradient offset", effect.gradientOffset, 0.0f, 5.0f, 0.0f);
      }
      else
      {
        effect.outlineColor = EditorGUILayout.ColorField(@"Color", effect.outlineColor);

        effect.outlineTexture = EditorGUILayout.ObjectField(@"Outline texture", effect.outlineTexture, typeof(Texture), false) as Texture;

        effect.outlineTextureUVParams.x = SpriteColorFXEditorHelper.SliderWithReset(@"U coord scale", @"U texture coordinate scale", effect.outlineTextureUVParams.x, -5.0f, 5.0f, 1.0f);
        effect.outlineTextureUVParams.y = SpriteColorFXEditorHelper.SliderWithReset(@"V coord scale", @"V texture coordinate scale", effect.outlineTextureUVParams.y, -5.0f, 5.0f, 1.0f);
        effect.outlineTextureUVParams.z = SpriteColorFXEditorHelper.SliderWithReset(@"U coord vel", @"U texture coordinate velocity", effect.outlineTextureUVParams.z, -2.0f, 2.0f, 0.0f);
        effect.outlineTextureUVParams.w = SpriteColorFXEditorHelper.SliderWithReset(@"V coord vel", @"V texture coordinate velocity", effect.outlineTextureUVParams.w, -2.0f, 2.0f, 0.0f);
        effect.outlineTextureUVAngle = SpriteColorFXEditorHelper.SliderWithReset(@"UV angle", @"UV rotation angle", effect.outlineTextureUVAngle, 0.0f, 360.0f, 0.0f);
      }
    }
  }
}