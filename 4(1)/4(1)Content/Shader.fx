sampler firstSampler;
int modus=0;


float3 ImageTone = {0.815,0.666,0};
float3 DarkTone = {0.313,0.258,0};
float3 GreyTransfer = {0.3,0.59,0.11};

// absolute alpha value returned for the pixel color
float GlobalAlpha
<
string UIWidget = "slider";
string UIMin = "0.0";
string UIMax = "1.0";
string UIStep = "0.1";
string UIName = "Global Alpha";
> = 1.0;

float Desaturation
<
string UIWidget = "slider";
string UIMin = "0.0";
string UIMax = "1.0";
string UIStep = "0.1";
string UIName = "Desaturation";
> = 1.0f;

float Toning
<
string UIWidget = "slider";
string UIMin = "0.0";
string UIMax = "1.0";
string UIStep = "0.05";
string UIName = "Toning";
> = 0.35f;

float4 PS_COLOR(float2 texCoord: TEXCOORD0) : COLOR
{
   float4 color = tex2D(firstSampler, texCoord);   

   if (modus==0){

   }
   else
     if (modus==1){
	  // color.rgb = dot(color.rgb, float3(0.3, 0.59, 0.11));
	          float value = (color.r + color.g + color.b) / 3; 
    color.r = value;
    color.g = value;
    color.b = value;
   }
   else
     if (modus==2){
	   if (color.r < 0.3f || color.r > 0.8f)
{
   color.rgb = 0.0f;
} else {
   color.rgb = 1.0f;
   color.r = 139.0f/255.0f;
   color.g = 90.0f/255.0f;
   color.b = 43.0f/255.0f;
}
   }
     else
     if (modus==3){
       float4 outputColor = color;
    outputColor.r = (color.r * 0.393) + (color.g * 0.769) + (color.b * 0.189);
    outputColor.g = (color.r * 0.349) + (color.g * 0.686) + (color.b * 0.168);    
    outputColor.b = (color.r * 0.272) + (color.g * 0.534) + (color.b * 0.131);
	return outputColor;
   }
   else
   if (modus==4){
	float4 pixelColor = color;

float3 scene = pixelColor * ImageTone;

//return float4(scene, 0//);

float grey = dot(GreyTransfer, scene);
float3 muted = lerp(scene, grey.xxx, Desaturation);
float3 sepia = lerp(DarkTone, ImageTone, grey);

float4 farbe = float4(lerp(muted, sepia, Toning), pixelColor.a);

	   if (color.r < 0.01f || color.g < 0.01f || color.b < 0.01f)
{
   farbe.rgb = 0.0f;
}

return farbe;

   }


   return color;
} 

float4 PS_COLORNO(float2 texCoord: TEXCOORD0) : COLOR
{
   float4 color = tex2D(firstSampler, texCoord);   
   
   return color;
} 

technique Shader
{
   pass pass0
   {
      PixelShader = compile ps_2_0 PS_COLOR();
   }
   
    pass pass1
   {
      PixelShader = compile ps_2_0 PS_COLORNO();
   }
} 

technique NoShader
{
   pass pass0
   {
      PixelShader = compile ps_2_0 PS_COLORNO();
   }
} 


