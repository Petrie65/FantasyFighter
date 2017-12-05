Shader "Hidden/Heathen/Occlusion-Basic-Effect-Standard" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
	}
	SubShader {
		
		ZWrite Off
		ZTest Greater
		Blend One One
		 
		CGPROGRAM
		#pragma surface surf Standard 
		
		sampler2D _BumpMap;
		struct Input {
		  float2 uv_MainTex;
	      float2 uv_BumpMap;
		  float3 viewDir;
		};
		float4 _OcclusionColor;
		void surf (Input IN, inout SurfaceOutputStandard o) {
	  	  o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
		  o.Albedo = _OcclusionColor.rgb;
		  o.Alpha = _OcclusionColor.a;
		  o.Emission = _OcclusionColor.rgb;
		}
		ENDCG 
	} 
	FallBack "Diffuse"
}
