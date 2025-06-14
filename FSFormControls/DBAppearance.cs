using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    /// <summary>
    /// Compatibilidad con Infragistics
    /// </summary>
    public class DBAppearance
    {
        public DBAppearance()
        {
        }

        public enum Alpha
        {
            Default,
            Transparent,
            Opaque,
            UseAlphaLevel
        }
        public enum DBTextTrimming
        {
            Default,
            None,
            Character,
            EllipsisCharacter,
            EllipsisPath,
            EllipsisWord,
            Word,
            NoneWithLineLimit,
            CharacterWithLineLimit,
            EllipsisCharacterWithLineLimit,
            EllipsisPathWithLineLimit,
            EllipsisWordWithLineLimit,
            WordWithLineLimit
        }
        public enum GradientAlignment
        {
            Default,
            Element,
            Form,
            Container,
            Client
        }
        public enum GradientStyle
        {
            Default,
            None,
            Vertical,
            Horizontal,
            BackwardDiagonal,
            ForwardDiagonal,
            HorizontalBump,
            VerticalBump,
            Circular,
            Rectangular,
            Elliptical,
            GlassTop20,
            GlassTop37,
            GlassTop50,
            GlassBottom20,
            GlassBottom37,
            GlassBottom50,
            GlassLeft20,
            GlassLeft37,
            GlassLeft50,
            GlassRight20,
            GlassRight37,
            GlassRight50,
            GlassTop20Bright,
            GlassTop37Bright,
            GlassTop50Bright,
            GlassBottom20Bright,
            GlassBottom37Bright,
            GlassBottom50Bright,
            GlassLeft20Bright,
            GlassLeft37Bright,
            GlassLeft50Bright,
            GlassRight20Bright,
            GlassRight37Bright,
            GlassRight50Bright,
            HorizontalWithGlassTop50,
            VerticalWithGlassRight50,
            HorizontalWithGlassBottom50,
            VerticalWithGlassLeft50
        }
        public enum HAlign
        {
            Default,
            Left,
            Center,
            Right
        }
        public enum VAlign
        {
            Default,
            Top,
            Middle,
            Bottom
        }

        public HorizontalAlignment Alignment { get; set; }
        public Color BackColor;
        public Color BackColor2;
        public Alpha BackColorAlpha;
        public GradientAlignment? BackGradientAlignment;
        public GradientStyle? BackGradientStyle;
        public Color BorderColor;
        public Font FontData;
        public Color ForeColor;
        public Bitmap Image;
        public HAlign ImageHAlign;
        public VAlign ImageVAlign;
        public string TextHAlignAsString;
        public DBTextTrimming? TextTrimming;
        public string TextVAlignAsString;
        
        public HAlign TextHAlign { get; set; }
        public VAlign TextVAlign { get; set; }
        public DBAppearance AppearanceOnToolbar { get; set; }
        public DBAppearance PressedAppearanceOnToolbar { get; set; }

        public void ResetBackColor()
        {
        }

        public void ResetForeColor()
        {
        }
    }
}