using System.Drawing;

using OpenTK.Graphics.OpenGL;

namespace Alkonaut
{
    class FieldObject : IGameObject
    {
        protected readonly Translator translator;
        protected Point position = Point.Empty;

        string textureName;
        string[] textureNames = null;
        int[] textureIds = null;
        Rectangle texture;
        int textureId, textureIndex;
        bool hasManyTextures = false;

        protected FieldObject(Translator translator, string textureName, int x = 0, int y = 0)
        {
            this.translator = translator;
            this.textureName = textureName;
            position.X = x;
            position.Y = y;
        }

        public virtual void OnLoad()
        {
            updateTextureRectangle();
            if (hasManyTextures)
            {
                for (int i = 0; i < textureNames.Length; ++i)
                {
                    textureIds[i] = TextureLoader.LoadTexture("res/" + textureNames[i]);
                }
            }
            else { textureId = TextureLoader.LoadTexture("res/" + textureName); }
        }

        public virtual void OnRender()
        {
            if (hasManyTextures) { renderTexture(textureIds[textureIndex]); }
            else { renderTexture(textureId); }
        }

        protected void updateTextureRectangle()
        {
            texture = translator.GetTexture(position);
        }

        protected void setupTextureIndex(int index)
        {
            textureIndex = index;
        }

        protected void setupManyTextures(string[] textureNames)
        {
            if (textureNames != null)
            {
                this.textureNames = textureNames;
                textureIds = new int[textureNames.Length];
                hasManyTextures = true;
            }
        }

        private void renderTexture(int textureIndex)
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
    }
}
