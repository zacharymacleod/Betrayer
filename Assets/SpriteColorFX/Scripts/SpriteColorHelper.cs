﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace SpriteColorFX
{
  /// <summary>
  /// Utilities for Sprite Color FX.
  /// </summary>
  public static class SpriteColorHelper
  {
    /// <summary>
    /// Pixel color operations.
    /// </summary>
    public enum PixelOp
    {
      Additive,
      Burn,
      Color,
      Darken,
      Darker,
      Difference,
      Divide,
      Dodge,
      HardMix,
      Hue,
      HardLight,
      Lighten,
      Lighter,
      Luminosity,
      Multiply,
      Overlay,
      PinLight,
      Saturation,
      Screen,
      Solid,
      SoftLight,
      Subtract,
      VividLight,
    }

    // Shaders params.
    public static readonly string ShaderStrengthParam = @"_Strength";
    public static readonly string ShaderGammaCorrectParam = @"_GammaCorrect";
    public static readonly string ShaderUVScrollParam = @"_UVScroll";
    public static readonly string ShaderInvertLumParam = @"_InvertLum";
    public static readonly string ShaderLumRangeMinParam = @"_LumRangeMin";
    public static readonly string ShaderLumRangeMaxParam = @"_LumRangeMax";
    public static readonly string ShaderRampIdxParam = @"_RampIdx";
    public static readonly string ShaderRampRedIdx = @"_RampRedIdx";
    public static readonly string ShaderRampGreenIdx = @"_RampGreenIdx";
    public static readonly string ShaderRampBlueIdx = @"_RampBlueIdx";
    public static readonly string ShaderMaskTex = @"_MaskTex";
    public static readonly string ShaderStrengthRedParam = @"_StrengthRed";
    public static readonly string ShaderPixelOpRedParam = @"_PixelOpRed";
    public static readonly string ShaderColorRedParam = @"_ColorRed";
    public static readonly string ShaderMaskRedParam = @"_MaskRedTex";
    public static readonly string ShaderUVRedParam = @"_UVRedTexParams";
    public static readonly string ShaderUVAngleRedParam = @"_UVRedTexAngle";
    public static readonly string ShaderStrengthGreenParam = @"_StrengthGreen";
    public static readonly string ShaderPixelOpGreenParam = @"_PixelOpGreen";
    public static readonly string ShaderColorGreenParam = @"_ColorGreen";
    public static readonly string ShaderMaskGreenParam = @"_MaskGreenTex";
    public static readonly string ShaderUVGreenParam = @"_UVGreenTexParams";
    public static readonly string ShaderUVAngleGreenParam = @"_UVGreenTexAngle";
    public static readonly string ShaderStrengthBlueParam = @"_StrengthBlue";
    public static readonly string ShaderPixelOpBlueParam = @"_PixelOpBlue";
    public static readonly string ShaderColorBlueParam = @"_ColorBlue";
    public static readonly string ShaderMaskBlueParam = @"_MaskBlueTex";
    public static readonly string ShaderUVBlueParam = @"_UVBlueTexParams";
    public static readonly string ShaderUVAngleBlueParam = @"_UVBlueTexAngle";
    public static readonly string ShaderNoiseAmountParam = @"_NoiseAmount";
    public static readonly string ShaderNoiseSpeedParam = @"_NoiseSpeed";
    public static readonly string ShaderRedShiftXParam = @"_RedShiftX";
    public static readonly string ShaderRedShiftYParam = @"_RedShiftY";
    public static readonly string ShaderGreenShiftXParam = @"_GreenShiftX";
    public static readonly string ShaderGreenShiftYParam = @"_GreenShiftY";
    public static readonly string ShaderBlueShiftXParam = @"_BlueShiftX";
    public static readonly string ShaderBlueShiftYParam = @"_BlueShiftY";
    public static readonly string ShaderBumpMapParam = @"_BumpMap";
    public static readonly string ShaderBumpIntensityParam = @"_BumpIntensity";
    public static readonly string ShaderShininessParam = @"_Shininess";
    public static readonly string ShaderSpecularColorParam = @"_SpecColor";
    public static readonly string ShaderBumpFactorChannelsParam = @"_BumpFactorChannels";
    public static readonly string ShaderOutlineSizeParam = @"_OutlineSize";
    public static readonly string ShaderOutlineColorParam = @"_OutlineColor";
    public static readonly string ShaderOutlineColor2Param = @"_OutlineColor2";
    public static readonly string ShaderGradientScaleParam = @"_GradientScale";
    public static readonly string ShaderGradientOffsetParam = @"_GradientOffset";
    public static readonly string ShaderOutlineTexParam = @"_OutlineTex";
    public static readonly string ShaderUVOutlineTexParamsParam = @"_UVOutlineTexParams";
    public static readonly string ShaderUVOutlineTexAngleParam = @"_UVOutlineTexAngle";
  }
}