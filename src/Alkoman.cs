namespace Alkonaut
{
    class Alkoman : FieldObject
    {
        const int SLEEP_PAUSE = 5;
        readonly string[] textureNames = new string[5] { "alkonaut_down.png", "alkonaut_up.png", "alkonaut_right.png", "alkonaut_left.png", "alkonaut_sleep.png" };

        public enum State { READY, SLEEP };
        public enum Moves { UP, DOWN, RIGHT, LEFT };

        int sleepingTickCounter = 0, currentTextureIndex = 0;
        State state = State.READY;

        public Alkoman(Translator translator)
            : base(translator, "alkonaut_down.png")
        {
            base.setupManyTextures(textureNames);
        }

        public override void OnRender()
        {
            base.setupTextureIndex(currentTextureIndex);
            base.OnRender();
        }

        public void Move(Moves direction)
        {
            if (state == State.READY)
            {
                bool wasMoved = tryMove(direction);
                if (wasMoved) { updateTextureRectangle(); }
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
                    currentTextureIndex = 1;
                    if (position.Y > 0)
                    {
                        if (position.Y - 1 == Column.Y && position.X == Column.X)
                        {
                            state = State.SLEEP;
                            currentTextureIndex = 4;
                        }
                        else
                        {                            
                            --position.Y;
                            ret = true;
                        }
                    }
                    break;

                case (Moves.DOWN):
                    currentTextureIndex = 0;
                    if (position.Y < Field.CELLS - 1)
                    {
                        if (position.Y + 1 == Column.Y && position.X == Column.X)
                        {
                            state = State.SLEEP;
                            currentTextureIndex = 4;
                        }
                        else
                        {
                            ++position.Y;
                            ret = true;
                        }
                    }
                    break;

                case (Moves.RIGHT):
                    currentTextureIndex = 2;
                    if (position.X < Field.CELLS - 1)
                    {
                        if (position.X + 1 == Column.X && position.Y == Column.Y)
                        {
                            state = State.SLEEP;
                            currentTextureIndex = 4;
                        }
                        else
                        {
                            ++position.X;
                            ret = true;
                        }
                    }
                    break;

                case (Moves.LEFT):
                    currentTextureIndex = 3;
                    if (position.X > 0)
                    {
                        if (position.X - 1 == Column.X && position.Y == Column.Y)
                        {
                            state = State.SLEEP;
                            currentTextureIndex = 4;
                        }
                        else
                        {
                            --position.X;
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
