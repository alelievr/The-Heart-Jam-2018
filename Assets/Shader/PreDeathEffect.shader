Shader "Hidden/Custom/PreDeathEffect"
{
    HLSLINCLUDE

        #include "../PostProcessing/Shaders/StdLib.hlsl"
        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		sampler2D _DisplacementTex;
        sampler2D _DisplacementTexy;
        sampler2D _VeineTex;
		float _bwBlend;
		float _Strength;
		float _Zoom;
		float _Speed;
		float4 _CenterPoint;
        float _vignette_size;
        float _greystrenght;
        float _greytougoum;
        
        float _tougoumgreydist;


        float4 Frag(VaryingsDefault i) : SV_Target
        {

            // DISTORTION

            float2 tmp = ((i.texcoord + (_Time.x * _Speed) % 1) * 1);
            float distancetocenter = distance(i.texcoord, _CenterPoint.xy);

            tmp.y = tmp.y % 1;
            tmp.x = tmp.x % 1;
            float4 color = tex2D(_DisplacementTex, tmp) * _bwBlend;
            float2 d = color.yz;
            color = tex2D(_DisplacementTexy, tmp) * _bwBlend;
            d.x = color.z;
            float2 uv = i.texcoord;
            uv = ((uv * 2 - float2(1, 1)) * _Zoom) / 2 + float2(.5, .5);

            float actualstr = _Strength;
            if (distancetocenter > _vignette_size)
                actualstr *= distancetocenter;
            else
                actualstr = 0;
            uv += float2((d.x * 2) - 1, (d.y * 2) - 1) * actualstr;

            uv = uv % 1;
            color = float4(uv, 1, 1);
            color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

            // is in tougoum

            bool isintougoum = false;
            float tougoumprogression = 0;
            if (_greytougoum != 0)
            {
                isintougoum =  _Time.y % (1 / _greytougoum) < 1 / _greytougoum / 2;
                tougoumprogression = _Time.y % (1 / _greytougoum) * 2 * _greytougoum;
                _tougoumgreydist = 1;
            }

            // grisance

            float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
            float actualgrey = _greystrenght;
            // color.rgb = lerp(color.rgb, luminance.xxx, _greystrenght.xxx);
            if (isintougoum)
            {
                _tougoumgreydist = (tougoumprogression < 0.5) ? (0.5 - tougoumprogression) * 2 : (tougoumprogression - 0.5) * 2;
                actualgrey = (distancetocenter > _tougoumgreydist) ? 1 : actualgrey;
            }
            color.rgb = luminance.xxx * actualgrey + color.rgb * (1 - actualgrey);

            //veination

            // if (isintougoum)
            // {
            //     float4 veinator = tex2D(_VeineTex, i.texcoord);
            //     veinator.x *= 3;
            //     color.x = color.x * (1 - veinator.x) + 130. / 256. * veinator.x;
            //     color.y = color.y * (1 - veinator.x) + 70. / 256. * veinator.x;
            //     color.z = color.z * (1 - veinator.x) + 42. / 256. * veinator.x;
            // }
            // color = float4(tougoumprogression,tougoumprogression, tougoumprogression, 0);
            return color;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }

    }
}