Shader "Heathen/Essentials/Occlusion-Wireframe/Rim/Occluded Bumped Specular" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
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
	#pragma surface surf BlinnPhong
	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;
	half _Shininess;

	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = tex.rgb * _Color.rgb;
		o.Gloss = tex.a;
		o.Alpha = tex.a * _Color.a;
		o.Specular = _Shininess;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	}
	ENDCG 
 }
 
 Fallback "Diffuse", 0
}