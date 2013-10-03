using System.Drawing;

namespace Alkonaut
{
    class SingleTextureRenderer : AbstractRenderer
    {
        readonly string textureName;

        public SingleTextureRenderer(Translator translator, int x, int y, string textureName) :
            base(new Point(x, y), translator)
        {
            this.textureName = textureName;
        }

        public override void OnLoad()
        {
            base.OnLoad();
            base.TextureIndex = TextureLoader.LoadTexture("res/" + textureName);
        }
    }
}
