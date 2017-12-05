Shader "Heathen/Essentials/Occlusion-Wireframe/Rim/Occluded Bumped" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_RimWidth ("Rim Weight", Range(0.01,2)) = 0.7
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimPower ("Rim Power", Range(0.1,1000)) = 20.0
    }
 
    SubShader 
 {
  Tags { "Queue" = "AlphaTest-1" "RenderType" = "Opaque" }
  
	UsePass "Hidden/Heathen/Occlusion-Rim-Effect/FORWARD"  
	UsePass "Hidden/Heathen/Occlusion-Rim-Effect/PREPASS"
	UsePass "Hidden/Heathen/Occlusion-Rim-Effect/DEFERRED"
	
	ZWrite On
	ZTest LEqual
	Blend Off
	
	CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;

	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	}
	ENDCG 
 }
 
 Fallback "Diffuse", 0
}