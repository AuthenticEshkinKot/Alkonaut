using System.Drawing;

namespace Alkonaut
{
    class Translator
    {
        readonly Field field;

        public Translator(Field field)
        {
            this.field = field;
        }

        /// <summary>
        /// Translates field position of the object to its texture rectangle
        /// </summary>
        /// <param name="position">Field position</param>
        /// <returns>Texture rectangle</returns>
        public Rectangle GetTexture(Point position)
        {
            Rectangle ret = Rectangle.Empty;

            ret.Height = field.FrameSize;
            ret.Width = field.FrameSize;

            ret.X = field.X + field.BorderHalfsize + position.X * field.CellSize;
            ret.Y = field.Y + field.BorderHalfsize + position.Y * field.CellSize;

            return ret;
        }
    }
}
