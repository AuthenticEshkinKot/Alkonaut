using System.Drawing;

using OpenTK.Graphics.OpenGL;

namespace Alkonaut
{
    abstract class AbstractRenderer : IRenderer
    {
        Translator translator;
        Rectangle texture;
        Point position;
        int textureIndex;

        public AbstractRenderer(Point position, Translator translator)
        {
            this.position = position;
            this.translator = translator;
        }

        public virtual void OnLoad()
        {
            texture = translator.GetTexture(position);
        }

        public virtual void OnRender()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.GetTextureObject(textureIndex));
            GL.Begin(BeginMode.Quads);

            GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);

            GL.TexCoord2(0, 0); GL.Vertex2(texture.X, texture.Y);
            GL.TexCoord2(0, 1); GL.Vertex2(texture.X, texture.Y + texture.Height);
            GL.TexCoord2(1, 1); GL.Vertex2(texture.X + texture.Width, texture.Y + texture.Height);
            GL.TexCoord2(1, 0); GL.Vertex2(texture.X + texture.Width, texture.Y);

            GL.End();
        }

        public void Move(int xMove, int yMove)
        {
            position = new Point(position.X + xMove, position.Y + yMove);
            texture = translator.GetTexture(position);
        }        

        public int X
        {
            get { return position.X; } 
        }

        public int Y
        {
            get { return position.Y; }
        }

        protected int TextureIndex
        {
            set { textureIndex = value; }
        }
    }
}
