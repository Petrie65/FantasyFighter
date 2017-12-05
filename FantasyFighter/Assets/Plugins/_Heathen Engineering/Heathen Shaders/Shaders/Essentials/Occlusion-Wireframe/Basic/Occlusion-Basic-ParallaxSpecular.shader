Shader "Heathen/Essentials/Occlusion-Wireframe/Basic/Occluded Parallax Specular" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		_Parallax ("Height", Range (0.005, 0.08)) = 0.02
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_ParallaxMap ("Heightmap (A)", 2D) = "black" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
    }
 
    SubShader 
 {
  Tags { "Queue" = "AlphaTest-1" "RenderType" = "Opaque" }
  
	UsePass "Hidden/Heathen/Occlusion-Basic-Effect/FORWARD"     
	UsePass "Hidden/Heathen/Occlusion-Basic-Effect/PREPASS"      
	UsePass "Hidden/Heathen/Occlusion-Basic-Effect/DEFERRED"
	
	ZWrite On
	ZTest LEqual
	Blend Off
	
	CGPROGRAM
	#pragma surface surf BlinnPhong
	#pragma target 3.0

	sampler2D _MainTex;
	sampler2D _BumpMap;
	sampler2D _ParallaxMap;
	fixed4 _Color;
	half _Shininess;
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