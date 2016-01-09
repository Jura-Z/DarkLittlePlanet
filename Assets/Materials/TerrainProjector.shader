Shader "Terrain projector shader" {
   Properties {
      _ShadowTex ("Projected Image", 2D) = "white" {}
   }
   SubShader {
      Pass {      
        // Blend DstColor One
        Blend SrcAlpha OneMinusSrcAlpha // attenuate color in framebuffer 
            // by 1 minus alpha of _ShadowTex 
         ZWrite Off // don't change depths
         Offset -1, -1 // avoid depth fighting
         ColorMask RGB
        
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         // User-specified properties
         uniform sampler2D _ShadowTex; 
         float4 _ShadowTex_ST;

         uniform float3 _PlayerWorld; 
 		 uniform float _SonarTime; 

 		 uniform float4 _SonarColor; 
 		 uniform float _CausticTint; 
 		 

         // Projector-specific uniforms
         uniform float4x4 _Projector; // transformation matrix 
            // from object space to projector space 
 
          struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posProj : TEXCOORD0;
            float4 posWorld : TEXCOORD1;
               // position in projector space
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.posProj = mul(_Projector, input.vertex);
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            output.posWorld = mul(_Object2World, input.vertex);
            return output;
         }

         #define TRANSFORM_TEX(tex,name) (tex.xy * name##_ST.xy + name##_ST.zw)

 
         float4 frag(vertexOutput input) : COLOR
         {
         	float4 result = float4(0,0,0,0);

         	float distToPlayer = length(_PlayerWorld - input.posWorld);

         	if (distToPlayer > _SonarTime * 0.97 && distToPlayer < _SonarTime * 1.02)
         	{
         		

         		result = _SonarColor;
         		return result;
         	}

            if (input.posProj.w > 0.0) // in front of projector?
            {
            	float2 texC = TRANSFORM_TEX((input.posProj.xy / input.posProj.w), _ShadowTex);
            	float4 stcolor = tex2D(_ShadowTex , texC );
               	result.rgb += stcolor;

               	result.a += _CausticTint;
            }

            return result;
         }
 
         ENDCG
      }
   }  
   Fallback "Projector/Light"
}