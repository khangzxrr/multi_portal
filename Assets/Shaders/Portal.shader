﻿// (c) 2018 Guidev
// This code is licensed under MIT license (see LICENSE.txt for details)

Shader "Custom/PortalDoor"
{
    Properties
    {
            _StencilPortal("Stencil Portal Index",Int) = 3
            
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        Zwrite Off
        ColorMask 0
                Cull off

        Pass
        {
            Stencil{
                Ref [_StencilPortal]
                Comp always
                Pass replace
            }

        // To further understand Stencil, check the link (EN):
        // https://docs.unity3d.com/Manual/SL-Stencil.html
        // or (CN)
        // https://blog.csdn.net/linjf520/article/details/94867519 

                    CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag

                    #include "UnityCG.cginc"

                    struct appdata
                    {
                        float4 vertex : POSITION;
                    };

                    struct v2f
                    {
                        float4 vertex : SV_POSITION;
                    };

                    v2f vert(appdata v)
                    {
                        v2f o;
                        o.vertex = UnityObjectToClipPos(v.vertex);
                        return o;
                    }

                    fixed4 frag(v2f i) : SV_Target
                    {
                        return fixed4(0.0, 0.0, 0.0, 0.0);
                    }
                    ENDCG
                }
    }
}