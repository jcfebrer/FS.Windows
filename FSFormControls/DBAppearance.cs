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
        public Color BackColor { get; set; }
        public Color BackColor2 { get; set; }
        public Alpha BackColorAlpha { get; set; }
        public GradientAlignment? BackGradientAlignment { get; set; }
        public GradientStyle? BackGradientStyle { get; set; }
        public Color BorderColor { get; set; }
        public Font FontData { get; set; }
        public Color ForeColor { get; set; }
        public Bitmap Image { get; set; }
        public HAlign ImageHAlign { get; set; }
        public VAlign ImageVAlign { get; set; }
        public DBTextTrimming? TextTrimming { get; set; }

        public string TextHAlignAsString { get; set; }
        public string TextVAlignAsString { get; set; }

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