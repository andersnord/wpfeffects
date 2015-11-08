using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ANEffectsLibrary
{
    public class Transparency : ShaderEffect
    {
        static Transparency()
        {
            // Associate _pixelShader with our compiled pixel shader
            _pixelShader.UriSource = Global.MakePackUri("Effects/Shaders/Transparency.ps");
        }

        public Transparency()
        {
            // The inherited property PixelShader must be set
            this.PixelShader = _pixelShader;

            // Force the pixel shader to update all input values
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(OpacityProperty);
        }


        // Implicit input
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
          ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(Transparency), 0);


        // Custom shader parameter
        public double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }

        public static readonly DependencyProperty OpacityProperty =
          DependencyProperty.Register("Opacity", typeof(double), typeof(Transparency),
                    new UIPropertyMetadata(1.0d, PixelShaderConstantCallback(0)));


        // One instance of the compiled shader is enough for the whole class
        private static PixelShader _pixelShader = new PixelShader();
    }
}