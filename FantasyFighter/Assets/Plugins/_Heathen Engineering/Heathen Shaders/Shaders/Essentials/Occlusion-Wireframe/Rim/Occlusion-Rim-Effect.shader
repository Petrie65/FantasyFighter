Shader "Hidden/Heathen/Occlusion-Rim-Effect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_RimWidth ("Rim Weight", Range(0.01,2)) = 0.7
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimPower ("Rim Power", Range(0.1,1000)) = 20.0
	}
	SubShader {
	Tags { "RenderType"="Transparent" "Queue"="Overlay" }
		ZWrite Off
		ZTest Greater
		Blend One One
	     
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0
		struct Input {
		  float2 uv_BumpMap;
		  float2 uv_MainTex;
		  float3 viewDir;
		};
		sampler2D _BumpMap;
		float4 _RimColor;
		float _RimWidth;
		float _RimPower;
		void surf (Input IN, inout SurfaceOutput o) {
		  //o.Albedo = _RimColor.rgb;
		  o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
		  half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
		  //o.Albedo = _RimColor.rgb;
		  //o.Alpha = _RimColor.a;
		  o.Emission = (_RimColor.rgb * pow (rim, _RimWidth))* _RimPower;
		}
		ENDCG 
	} 
	FallBack "Diffuse"
}
