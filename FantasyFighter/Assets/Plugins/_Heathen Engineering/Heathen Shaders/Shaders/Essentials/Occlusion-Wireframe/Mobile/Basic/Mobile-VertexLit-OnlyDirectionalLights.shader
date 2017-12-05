// Simplified VertexLit shader, optimized for high-poly meshes. Differences from regular VertexLit one:
// - less per-vertex work compared with Mobile-VertexLit
// - supports only DIRECTIONAL lights and ambient term, saves some vertex processing power
// - no per-material color
// - no specular
// - no emission

Shader "Heathen/Mobile/Occlusion-Wireframe/Basic/Basic VertexLit" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_OcclusionColor ("Base (RGB)", Color) = (0.5,1,1,0.25)
	}
	SubShader {
		Tags {"Queue" = "AlphaTest-1" "RenderType"="Opaque" }
		LOD 80

		UsePass "Hidden/Heathen/Occlusion-Basic-Effect/FORWARD"  
		UsePass "Hidden/Heathen/Occlusion-Basic-Effect/PREPASS" 
		UsePass "Hidden/Heathen/Occlusion-Basic-Effect/DEFERRED"

		ZWrite On
		ZTest LEqual
		Blend Off

		UsePass "Heathen/Essentials/Occlusion-Wireframe/Mobile/No Occlude/Basic VertexLit/FORWARD"
	}


FallBack "Mobile/VertexLit"
}