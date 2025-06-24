﻿Shader "UI/ImageShakeEffect"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _ShakeIntensity ("Shake Intensity", Range(0, 0.1)) = 0.01
        _ShakeSpeed ("Shake Speed", Range(0, 100)) = 5
        _ShakeCycle ("Shake Cycle (seconds)", Range(0, 10)) = 2
        _Color ("Tint", Color) = (1,1,1,1)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ShakeIntensity;
            float _ShakeSpeed;
            float _ShakeCycle;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            float2 ShakeUV(float2 uv)
            {
                float time = _Time.y;
                // Tính toán chu kỳ rung dựa trên sin
                float cycle = _ShakeCycle > 0 ? abs(sin(time * 3.14159 / _ShakeCycle)) : 1;
                float shakeAmount = _ShakeIntensity * cycle;
                float2 offset;
                offset.x = sin(time * _ShakeSpeed) * shakeAmount;
                offset.y = cos(time * _ShakeSpeed) * shakeAmount;
                return uv + offset;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 shakenUV = ShakeUV(i.uv);
                fixed4 col = tex2D(_MainTex, shakenUV) * i.color;
                return col;
            }
            ENDCG
        }
    }
}