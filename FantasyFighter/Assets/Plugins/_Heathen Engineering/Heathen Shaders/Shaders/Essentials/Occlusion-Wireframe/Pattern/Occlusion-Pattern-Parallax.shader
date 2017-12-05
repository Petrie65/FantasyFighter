Shader "Heathen/Essentials/Occlusion-Wireframe/Pattern/Occluded Parallax" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_Parallax ("Height", Range (0.005, 0.08)) = 0.02
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_ParallaxMap ("Heightmap (A)", 2D) = "black" {}
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
	#pragma surface surf Lambert

	sampler2D _MainTex;
	sampler2D _BumpMap;
	sampler2D _ParallaxMap;
	fixed4 _Color;
	float _Parallax;

	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
		float3 viewDir;
	};

	void surf (Input IN, inout SurfaceOutput o) {
		half h = tex2D (_ParallaxMap, IN.uv_BumpMap).w;
		float2 offset = ParallaxOffset (h, _Parallax, IN.viewDir);
		IN.uv_MainTex += offset;
		IN.uv_BumpMap += offset;
		
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	}
	ENDCG 
 }
 
 Fallback "Diffuse", 0
}