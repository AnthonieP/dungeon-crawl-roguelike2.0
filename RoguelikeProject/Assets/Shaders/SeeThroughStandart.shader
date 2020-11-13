// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SeeThroughStandart"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _BackColor ("SeeThroughColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        
        #pragma surface surf Standard fullforwardshadows

        
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        
        UNITY_INSTANCING_BUFFER_START(Props)
        
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
		Pass
		{
			ZTest Greater
			Cull Back

			CGPROGRAM
			#pragma vertex vert            
			#pragma fragment frag

			half4 _BackColor;

			float4 vert(float4 pos : POSITION) : SV_POSITION
			{
				float4 viewPos = UnityObjectToClipPos(pos);
				return viewPos;
			}

				half4 frag(float4 pos : SV_POSITION) : COLOR
			{
				return _BackColor;
			}

			ENDCG
		}
    }
    FallBack "Diffuse"
}
