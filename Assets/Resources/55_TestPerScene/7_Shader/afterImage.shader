Shader "Custom/NewShader" 
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BackBuffer ("Albedo (RGB)", 2D) = "black" {}    
        _Distance ("_Distance", float) = 0.02
        _Alpha ("_Alpha", float) = 0.8
        _Color ("_Color", Color) = (0.0,0.0,0.0,0.0)
    }
    SubShader
    {        
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            sampler2D _BackBuffer;
 
            uniform float _Distance;
            uniform float _Alpha;
            uniform float4 _Color;
 
            fixed4 frag(v2f_img v2f) : COLOR
            {
                fixed4 col = tex2D(_BackBuffer, v2f.uv + float2(_Distance, 0.0));
                col.rgb *= _Color.rgb;    
                fixed4 main = tex2D(_MainTex, v2f.uv);
                col.a = _Alpha;
                return col - main;
            }
 
            ENDCG
        }
     
    }
    FallBack "Diffuse"
}
