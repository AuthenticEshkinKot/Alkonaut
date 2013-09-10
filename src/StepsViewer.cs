using System.Drawing;

using OpenTK.Graphics.OpenGL;

using QuickFont;

namespace Alkonaut
{
    class StepsViewer : IGameObject
    {
        const float FONTSIZE = 72, OFFSET_X_PART = .2f;
        const string FONTNAME = "res/a song for jennifer.ttf", step = "Step:";
        static float goodFontsize = FONTSIZE;
        static QFont steps = new QFont(FONTNAME, goodFontsize, new QFontBuilderConfiguration(false));

        readonly Field field;
        readonly GameLogic logic;
        readonly int y;        
        
        float offsetX = 1, offsetY;        

        public StepsViewer(Field field, GameLogic logic, int screenHalfHeight)
        {
            y = screenHalfHeight;
            this.logic = logic;
            this.field = field;
        }

        public void OnLoad()
        {
            string longest = step.Length > logic.Steps.ToString().Length ? step : logic.Steps.ToString();
            float maxWidth = steps.Measure(longest, QFontAlignment.Left).Width,
                width;

            offsetX = OFFSET_X_PART * (field.X - field.BorderHalfsize) / 2;
            width = field.X - field.BorderHalfsize - 2 * offsetX;

            while (maxWidth > width)
            {
                goodFontsize = goodFontsize * (width / maxWidth) - 4;
                steps.Dispose();
                steps = new QFont(FONTNAME, goodFontsize, new QFontBuilderConfiguration(false));
                maxWidth = steps.Measure(longest, QFontAlignment.Left).Width;
            }

            offsetY = steps.Measure(step, QFontAlignment.Left).Height / 2;
        }

        public void OnRender()
        {
            QFont.Begin();
            GL.PushMatrix();

            GL.Translate(offsetX, y - offsetY, 0);
            steps.Print(logic.Steps.ToString(), QFontAlignment.Left);
            GL.Translate(0, - 2 * offsetY - 2, 0);
            steps.Print(step, QFontAlignment.Left);

            GL.PopMatrix();
            QFont.End();
        }
    }
}
