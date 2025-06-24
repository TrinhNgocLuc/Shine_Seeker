Shader "Custom/FireShader"
{
    Properties
    {
        _MainTex ("Fire Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Color1 ("Fire Color 1 (Bottom)", Color) = (1, 0, 0, 1)
        _Color2 ("Fire Color 2 (Middle)", Color) = (1, 0.5, 0, 1) 
        _Color3 ("Fire Color 3 (Top)", Color) = (1, 1, 0, 1) 
        _Speed ("Flame Speed", Float) = 2.0
        _FlickerIntensity ("Flicker Intensity", Range(0, 1)) = 0.3
        _Brightness ("Brightness", Range(0, 5)) = 2.0
        _AlphaCutoff ("Alpha Cutoff", Range(0, 1)) = 0.1
        _FlameSharpness ("Flame Sharpness", Range(0, 10)) = 2.0 
        _FlameWidth ("Flame Width", Range(0, 1)) = 0.5 
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _Color1, _Color2, _Color3;
            float _Speed, _FlickerIntensity, _Brightness, _AlphaCutoff;
            float _FlameSharpness, _FlameWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Noise for flickering
                float2 noiseUV = i.uv + float2(0, _Time.y * _Speed);
                float noise = tex2D(_NoiseTex, noiseUV).r;

                // Add flicker effect using sin for oscillation
                float flicker = sin(_Time.y * _Speed * 2.0 + noise) * _FlickerIntensity + 1.0;

                // Main texture
                fixed4 tex = tex2D(_MainTex, i.uv);

                // Gradient for fire color
                float t = i.uv.y + noise * 0.2;
                fixed4 fireColor = lerp(_Color1, _Color2, t);
                fireColor = lerp(fireColor, _Color3, t * t);

                // Tạo hình dạng ngọn lửa
                float flameShape = 1.0 - abs(i.uv.x - 0.5) * 2.0;
                flameShape = pow(flameShape, _FlameSharpness);
                flameShape *= (1.0 - i.uv.y) * _FlameWidth; 
                flameShape = saturate(flameShape + noise * 0.3); 
                fixed4 col = tex * fireColor * flicker * _Brightness;
                col.a = tex.a * flameShape * (1.0 - t) * (noise + 0.5);
                clip(col.a - _AlphaCutoff);

                return col;
            }
            ENDCG
        }
    }
}