Shader "Internal/GrayscaleGrabPass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {   
        // Draw after all opaque geometry
        Tags { "Queue" = "Transparent" }

        // Grab the screen behind the object into _BackgroundTexture
        GrabPass { "_GrabTexture" }

        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = ComputeGrabScreenPos(o.vertex);
                return o;
            }
            
            sampler2D _MainTex;
            sampler2D _GrabTexture;

            fixed4 frag (v2f i) : SV_Target
            {
                // grab texture from the background
                fixed4 col = tex2D(_GrabTexture, i.uv);
    
                // make a matrix for sepia tone
                fixed4x4 mat = fixed4x4(
                    0.393, 0.349, 0.272, 0.0,
                    0.769, 0.686, 0.534, 0.0,
                    0.189, 0.168, 0.131, 0.0,
                    0.0, 0.0, 0.0, 1.0  
                    );
                return mul(col, mat);
            }
            ENDCG
        }
    }
}
