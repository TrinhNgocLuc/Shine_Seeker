Shader "Custom/DarkShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Darkness ("Darkness", Range(0, 1)) = 0.9
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0) // Vị trí trung tâm (UV space)
        _Width ("Width", Range(0, 1)) = 0.3 // Chiều rộng vùng sáng (UV space)
        _Height ("Height", Range(0, 1)) = 0.2 // Chiều cao vùng sáng (UV space)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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
            float _Darkness;
            float2 _Center; // Vị trí trung tâm
            float _Width;   // Chiều rộng vùng sáng
            float _Height;  // Chiều cao vùng sáng

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Tính khoảng cách theo trục X và Y từ pixel đến trung tâm
                float dx = abs(i.uv.x - _Center.x);
                float dy = abs(i.uv.y - _Center.y);
                
                // Nếu pixel nằm ngoài vùng chữ nhật, làm tối
                if (dx > _Width || dy > _Height)
                {
                    col.rgb *= (1 - _Darkness); // Làm tối khu vực ngoài vùng chữ nhật
                }
                
                return col;
            }
            ENDCG
        }
    }
}