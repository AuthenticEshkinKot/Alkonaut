using System;

namespace Alkonaut
{
    class GameLogic
    {
        public const int STEPS = 200;

        const double PERIOD = 0.5;        
        readonly Alkoman alkoman;
        readonly Random random = new Random(DateTime.Now.Millisecond);

        int tickCounter = 0, stepCounter = 0;
        double tickLimit = PERIOD;

        public GameLogic(Alkoman alkoman)
        {
            this.alkoman = alkoman;
        }

        public void OnLoad(double updateRate)
        {
            tickLimit = PERIOD * updateRate;
        }

        public void OnUpdate()
        {
            if (++tickCounter > tickLimit && stepCounter < STEPS)
            {
                double rnd = random.NextDouble();

                if (rnd < 0.25) { alkoman.Move(Alkoman.Moves.UP); }
                else if (rnd < 0.5) { alkoman.Move(Alkoman.Moves.DOWN); }
                else if (rnd < 0.75) { alkoman.Move(Alkoman.Moves.RIGHT); }
                else { alkoman.Move(Alkoman.Moves.LEFT); }

                ++stepCounter;
                tickCounter = 0;
            }
        }

        public int Steps
        {
            get { return stepCounter; }
        }
    }
}
