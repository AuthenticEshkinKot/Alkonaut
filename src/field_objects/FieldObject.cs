namespace Alkonaut
{
    abstract class FieldObject : IGameObject
    {
        protected readonly IRenderer renderer;

        public FieldObject(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public void OnLoad()
        {
            renderer.OnLoad();
        }

        public void OnRender()
        {
            renderer.OnRender();
        }
    }
}