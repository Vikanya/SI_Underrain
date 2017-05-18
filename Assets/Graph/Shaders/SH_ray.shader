// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/SH_ray"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Color0("Color 0", Color) = (0,1,0.8344827,0)
		_Color_out("Color_out", Color) = (0,0.006896496,1,0)
		_Speed("Speed", Float) = 5
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_VO_intensity("VO_intensity", Float) = 0.2
		_VO_tiling("VO_tiling", Vector) = (1,1,0,0)
		_Transition("Transition", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 texcoord_0;
			float2 uv_texcoord;
			float2 texcoord_1;
		};

		uniform float4 _Color_out;
		uniform float _Speed;
		uniform float4 _Color0;
		uniform sampler2D _TextureSample1;
		uniform float2 _VO_tiling;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Transition;
		uniform float _VO_intensity;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * _VO_tiling + float2( 0,0 );
			o.texcoord_1.xy = v.texcoord.xy * _VO_tiling + float2( 0,0 );
			float4 tex2DNode30 = tex2Dlod( _TextureSample1,float4( (abs( o.texcoord_1+_Time[1] * float2(1,1 ))), 0.0 , 0.0 ));
			v.vertex.xyz += ( ( tex2DNode30 * (0.0 + (_VO_intensity - 0.0) * (0.1 - 0.0) / (1.0 - 0.0)) ) * float4( v.normal , 0.0 ) ).xyz;
		}

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( s.Emission, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float temp_output_23_0 = sin( ( _Time.y * _Speed ) );
			float fresnelFinalVal20 = (0.0 + 2.0*pow( 1.0 - dot( ase_worldNormal, worldViewDir ) , ( (0.5 + (temp_output_23_0 - -1.0) * (1.5 - 0.5) / (1.0 - -1.0)) * 2.0 )));
			float4 temp_cast_0 = (fresnelFinalVal20).xxxx;
			float Time = temp_output_23_0;
			float4 tex2DNode30 = tex2D( _TextureSample1,(abs( i.texcoord_0+_Time[1] * float2(1,1 ))));
			float4 temp_cast_1 = (1).xxxx;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode15 = tex2D( _TextureSample0,uv_TextureSample0);
			o.Emission = lerp( _Color_out , ( ( temp_cast_0 + _Color0 ) * (0.7 + (Time - -1.0) * (1.0 - 0.7) / (1.0 - -1.0)) ) , lerp( tex2DNode30 , temp_cast_1 , tex2DNode15.r ).x ).rgb;
			o.Alpha = ( clamp( ( tex2DNode15.r + (0.0 + (Time - -1.0) * (0.2 - 0.0) / (1.0 - -1.0)) ) , 0.0 , 1.0 ) * _Transition );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.texcoords01.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
10;287;1531;746;907.269;182.6986;1.558945;True;True
Node;AmplifyShaderEditor.RangedFloatNode;27;-1532.116,-1168.577;Float;False;Property;_Speed;Speed;3;0;5;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;22;-1552.916,-1259.777;Float;False;0;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1312.916,-1202.177;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SinOpNode;23;-1128.517,-1201.578;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;29;-795.8171,-999.6778;Float;False;Constant;_Float0;Float 0;4;0;2;0;0;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;25;-915.7172,-1239.978;Float;True;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.5;False;4;FLOAT;1.5;False;FLOAT
Node;AmplifyShaderEditor.Vector2Node;38;-1340.98,414.5495;Float;False;Property;_VO_tiling;VO_tiling;6;0;1,1;FLOAT2;FLOAT;FLOAT
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;-920.5204,-1328.876;Float;False;Time;-1;True;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.GetLocalVarNode;44;-455.8699,2.53241;Float;False;39;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-1067.18,429.3495;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-565.3174,-1092.778;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.FresnelNode;20;-268.9173,-839.379;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;2.0;False;3;FLOAT;2.0;False;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;45;-249.5269,6.250272;Float;False;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.0;False;4;FLOAT;0.2;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;15;-367.9752,-287.9175;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Assets/Graph/Textures/Gameplay/Ray_mask.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;36;-652.9797,632.1492;Float;False;Property;_VO_intensity;VO_intensity;5;0;0.2;0;0;FLOAT
Node;AmplifyShaderEditor.PannerNode;33;-809.9794,445.3495;Float;False;1;1;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;FLOAT2
Node;AmplifyShaderEditor.ColorNode;18;-280.1314,-691.0359;Float;False;Property;_Color0;Color 0;1;0;0,1,0.8344827,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.GetLocalVarNode;40;-256.7495,-924.1484;Float;False;39;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;41;68.34879,-938.6691;Float;False;0;FLOAT;0.0;False;1;FLOAT;-1.0;False;2;FLOAT;1.0;False;3;FLOAT;0.7;False;4;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;43;23.59607,-86.6969;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;30;-586.8796,430.1496;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;Assets/Graph/Textures/Gameplay/Ray_mask2.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;21;110.2826,-758.5787;Float;False;0;FLOAT;0,0,0,0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.TFHCRemap;37;-456.1794,648.1492;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;0.0;False;4;FLOAT;0.1;False;FLOAT
Node;AmplifyShaderEditor.ColorNode;19;-293.8763,-486.0655;Float;False;Property;_Color_out;Color_out;1;0;0,0.006896496,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;49;417.8344,135.3262;Float;False;Property;_Transition;Transition;7;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;46;174.2692,-92.19996;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.LerpOp;47;111.2164,133.4965;Float;True;0;FLOAT4;0.0;False;1;FLOAT;1;False;2;FLOAT;0.0;False;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;286.7482,-820.0692;Float;False;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-229.5795,455.3496;Float;False;0;FLOAT4;0.0;False;1;FLOAT;0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.NormalVertexDataNode;32;-150.5795,683.5494;Float;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;555.0269,43.58947;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.LerpOp;2;457.8838,-434.5169;Float;True;0;COLOR;0.0;False;1;COLOR;1,0,0,0;False;2;FLOAT4;0.0;False;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;239.4206,581.1493;Float;False;0;FLOAT4;0.0,0,0;False;1;FLOAT3;0.0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;874.2999,-20.6;Float;False;True;2;Float;ASEMaterialInspector;0;Unlit;Custom/SH_ray;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Transparent;0.5;True;True;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;26;0;22;0
WireConnection;26;1;27;0
WireConnection;23;0;26;0
WireConnection;25;0;23;0
WireConnection;39;0;23;0
WireConnection;34;0;38;0
WireConnection;28;0;25;0
WireConnection;28;1;29;0
WireConnection;20;3;28;0
WireConnection;45;0;44;0
WireConnection;33;0;34;0
WireConnection;41;0;40;0
WireConnection;43;0;15;1
WireConnection;43;1;45;0
WireConnection;30;1;33;0
WireConnection;21;0;20;0
WireConnection;21;1;18;0
WireConnection;37;0;36;0
WireConnection;46;0;43;0
WireConnection;47;0;30;0
WireConnection;47;2;15;1
WireConnection;42;0;21;0
WireConnection;42;1;41;0
WireConnection;35;0;30;0
WireConnection;35;1;37;0
WireConnection;48;0;46;0
WireConnection;48;1;49;0
WireConnection;2;0;19;0
WireConnection;2;1;42;0
WireConnection;2;2;47;0
WireConnection;31;0;35;0
WireConnection;31;1;32;0
WireConnection;0;2;2;0
WireConnection;0;9;48;0
WireConnection;0;11;31;0
ASEEND*/
//CHKSM=4653C919F16D29FA14A66F83F00295D0B2CCDE61