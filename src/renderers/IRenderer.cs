namespace Alkonaut
{
    interface IRenderer
    {
        void OnLoad();
        void OnRender();
        void Move(int xMove, int yMove);

        int X
        { get; }
        int Y
        { get; }
    }
}
