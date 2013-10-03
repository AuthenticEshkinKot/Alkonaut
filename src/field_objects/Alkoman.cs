namespace Alkonaut
{
    class Alkoman : FieldObject
    {
        const int SLEEP_PAUSE = 5;

        public enum State { READY, SLEEP };
        public enum Moves { UP, DOWN, RIGHT, LEFT };

        static readonly string[] textureNames = new string[5] { "alkonaut_down.png", "alkonaut_up.png", "alkonaut_right.png", "alkonaut_left.png", "alkonaut_sleep.png" };
        
        readonly MultiTextureRenderer myRenderer;

        int sleepingTickCounter = 0;
        State state = State.READY;       

        public Alkoman(Translator translator)
            : base(new MultiTextureRenderer(translator, 0, 0, textureNames, 0))
        {
            myRenderer = (MultiTextureRenderer)renderer;
        }

        public void Move(Moves direction)
        {
            if (state == State.READY)
            {
                tryMove(direction);
            }
            else if (++sleepingTickCounter == SLEEP_PAUSE)
            {
                sleepingTickCounter = 0;
                state = State.READY;
            }
        }

        private bool tryMove(Moves direction)
        {
            bool ret = false;

            switch (direction)
            {
                case (Moves.UP):                    
                    myRenderer.TextureNameIndex = 1;
                    if (renderer.Y > 0)
                    {
                        if (renderer.Y - 1 == Column.Y && renderer.X == Column.X)
                        {
                            state = State.SLEEP;
                            myRenderer.TextureNameIndex = 4;
                        }
                        else
                        {
                            renderer.Move(0, -1);
                            ret = true;
                        }
                    }
                    break;

                case (Moves.DOWN):
                    myRenderer.TextureNameIndex = 0;
                    if (renderer.Y < Field.CELLS - 1)
                    {
                        if (renderer.Y + 1 == Column.Y && renderer.X == Column.X)
                        {
                            state = State.SLEEP;
                            myRenderer.TextureNameIndex = 4;
                        }
                        else
                        {
                            renderer.Move(0, 1);
                            ret = true;
                        }
                    }
                    break;

                case (Moves.RIGHT):
                    myRenderer.TextureNameIndex = 2;
                    if (renderer.X < Field.CELLS - 1)
                    {
                        if (renderer.X + 1 == Column.X && renderer.Y == Column.Y)
                        {
                            state = State.SLEEP;
                            myRenderer.TextureNameIndex = 4;
                        }
                        else
                        {
                            renderer.Move(1, 0);
                            ret = true;
                        }
                    }
                    break;

                case (Moves.LEFT):
                    myRenderer.TextureNameIndex = 3;
                    if (renderer.X > 0)
                    {
                        if (renderer.X - 1 == Column.X && renderer.Y == Column.Y)
                        {
                            state = State.SLEEP;
                            myRenderer.TextureNameIndex = 4;
                        }
                        else
                        {
                            renderer.Move(-1, 0);
                            ret = true;
                        }
                    }
                    break;
            }

            return ret;
        }

        public State GetState
        {
            get { return state; }
        }
    }
}
