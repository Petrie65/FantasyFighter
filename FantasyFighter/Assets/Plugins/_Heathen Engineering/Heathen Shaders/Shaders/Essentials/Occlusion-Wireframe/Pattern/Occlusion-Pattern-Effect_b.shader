Shader "Hidden/Heathen/Occlusion-Pattern-Effect_b" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
		_OcclusionPattern ("Pattern (A)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue" = "AlphaTest-1" "RenderType"="Opaque" }
		LOD 200
		
		ZWrite Off
		ZTest Greater
		Blend One One
		Cull Off
	     
		CGPROGRAM
		#pragma surface surf Lambert
		sampler2D _OcclusionPattern;
		struct Input {
		  float2 uv_OcclusionPattern;
		  float3 viewDir;
		};
		float4 _OcclusionColor;
		void surf (Input IN, inout SurfaceOutput o) {
		  fixed4 c = tex2D(_OcclusionPattern, IN.uv_OcclusionPattern);
		  o.Albedo = c.a * _OcclusionColor.rgb;
		  o.Alpha = c.a;
		}
		ENDCG 
	} 
	FallBack "Diffuse"
}
