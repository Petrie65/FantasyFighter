Shader "Heathen/Essentials/Occlusion-Wireframe/Pattern/Occluded Bumped" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
		_OcclusionPattern ("Pattern (A)", 2D) = "white" {}
    }
 
    SubShader 
 {
  Tags { "Queue" = "AlphaTest-1" "RenderType" = "Opaque" }
  
	UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/FORWARD"   
	UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/PREPASS"
	UsePass "Hidden/Heathen/Occlusion-Pattern-Effect_a/DEFERRED"
	
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
		o.Alpha = tex.a * _Color.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	}
	ENDCG 
 }
 
 Fallback "Diffuse", 0
}