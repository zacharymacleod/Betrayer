﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Sprite Color FX.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Sprites/Sprite Color FX/Sprite Color Outline Texture Bump Lit"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    [PerRendererData]
	_MainTex("Base (RGB)", 2D) = "white" {}

    _Color("Tint", Color) = (1, 1, 1, 1)

	[MaterialToggle]
	PixelSnap("Pixel snap", Float) = 0

    [PerRendererData]
    _BumpMap("Normalmap", 2D) = "bump" {}

    _BumpIntensity("NormalMap intensity", Float) = 1

	_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)

	_Shininess("Shininess", Range(0.03, 1)) = 0.078125

	_OutlineTex("Outline (RGB)", 2D) = "white" {}
    _OutlineSize("Outline size", Range(0, 0.03)) = 0.0075
    _OutlineColor("Outline color", Color) = (1, 1, 1, 1)
  }

  // Techniques (http://unity3d.com/support/documentation/Components/SL-SubShader.html).
  SubShader
  {
    // Tags (http://docs.unity3d.com/Manual/SL-CullAndDepth.html).
    Tags
    { 
      "Queue" = "Transparent"
      "IgnoreProjector" = "True"
      "RenderType" = "Transparent"
      "PreviewType" = "Plane"
      "CanUseSpriteAtlas" = "True"
    }

    Cull Off
    Lighting On
    ZWrite Off
    Fog { Mode Off }

	CGINCLUDE
	//#include "UnityCG.cginc"
    #include "../../SpriteColorFXCG.cginc"

    sampler2D _MainTex;
    sampler2D _BumpMap;
    fixed4 _Color;
    fixed _Shininess;
    fixed _BumpIntensity;
    fixed4 _BumpFactorChannels;

    float _OutlineSize;
    fixed4 _OutlineColor;
    fixed4 _UVOutlineTexParams;
	fixed _UVOutlineTexAngle;
    sampler2D _OutlineTex;
	ENDCG

    CGPROGRAM
    #pragma surface surf BlinnPhong alpha vertex:vert nofog
    #pragma fragmentoption ARB_precision_hint_fastest
    #pragma multi_compile DUMMY PIXELSNAP_ON
    #pragma target 3.0
	
	struct Input
    {
      float2 uv_MainTex;
      float2 uv_BumpMap;
      fixed4 color;
    };

    inline fixed2 UVCoordOp(fixed2 uv, float2 scale, float2 velocity, float angle)
    {
      float cosAngle = cos(angle);
      float sinAngle = sin(angle);
    
      uv = mul(uv, float2x2(cosAngle, -sinAngle, sinAngle, cosAngle));
      uv *= scale;
      uv += velocity * _Time.y;

      return uv;
    }

    void vert(inout appdata_full v, out Input o)
    {
#if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
      v.vertex = UnityPixelSnap(v.vertex);
#endif
        
      v.normal = float3(0.0, 0.0, -1.0);
      v.tangent = float4(1.0, 0.0, 0.0, -1.0);

      UNITY_INITIALIZE_OUTPUT(Input, o);

      o.color = _Color * v.color;
    }

    void surf(Input IN, inout SurfaceOutput o)
	{
      fixed4 outlinePixel = tex2D(_MainTex, IN.uv_MainTex + float2(_OutlineSize, _OutlineSize)) +
	                        tex2D(_MainTex, IN.uv_MainTex + float2(_OutlineSize, -_OutlineSize)) +
	                        tex2D(_MainTex, IN.uv_MainTex + float2(-_OutlineSize, _OutlineSize)) +
                            tex2D(_MainTex, IN.uv_MainTex + float2(-_OutlineSize, -_OutlineSize));

	  if (outlinePixel.a > 0.1)
      {
        outlinePixel.a = 1.0;
		outlinePixel.rgb = tex2D(_OutlineTex, UVCoordOp(IN.uv_MainTex, _UVOutlineTexParams.xy, _UVOutlineTexParams.zw, _UVOutlineTexAngle)).rgb * _OutlineColor * IN.color;
      }

      fixed4 mainColor = _OutlineColor.a * outlinePixel;

	  fixed4 spriteColor = tex2D(_MainTex, IN.uv_MainTex) * IN.color;

	  if (spriteColor.a > 0.9)
        mainColor = spriteColor;

      o.Albedo = mainColor.rgb;
      o.Alpha = mainColor.a;
      o.Gloss = mainColor.a;
      o.Specular = _Shininess;
	  o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
      o.Normal = fixed3(o.Normal.x * _BumpIntensity * _BumpFactorChannels.x, o.Normal.y * _BumpIntensity * _BumpFactorChannels.y, o.Normal.z);
    }
    ENDCG
  }

  Fallback "Sprites/Default"
}