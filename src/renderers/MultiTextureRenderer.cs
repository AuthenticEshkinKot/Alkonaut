using System.Drawing;

namespace Alkonaut
{
    class MultiTextureRenderer : AbstractRenderer
    {
        readonly string[] textureNames;
        int[] textureIds;
        int textureNameIndex;

        public MultiTextureRenderer(Translator translator, int x, int y, string[] textureNames, int startTextureIndex) :
            base(new Point(x, y), translator)
        {
            this.textureNames = textureNames;
            textureNameIndex = startTextureIndex;
            textureIds = new int[textureNames.Length];
        }

        public override void OnLoad()
        {
            base.OnLoad();

            for (int i = 0; i < textureNames.Length; ++i)
            {
                textureIds[i] = TextureLoader.LoadTexture("res/" + textureNames[i]);
            }

            base.TextureIndex = textureIds[textureNameIndex];
        }

        public override void OnRender()
        {
            base.TextureIndex = textureIds[textureNameIndex];
            base.OnRender();
        }

        public int TextureNameIndex
        {
            set { textureNameIndex = value; }
        }
    }
}
