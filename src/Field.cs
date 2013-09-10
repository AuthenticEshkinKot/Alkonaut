using System;
using System.Drawing;

using OpenTK.Graphics.OpenGL;

namespace Alkonaut
{
    class Field : IGameObject
    {
        public const int CELLS = 15;
        const int BORDER_HALF_SIZE_PT = 2;
        const float FIELD_SCREEN_PART = .8f;

        readonly int screenWidth, screenHeight;
        int cellSize, frameSize, borderHalfSize, fieldSize, fieldX, fieldY;

        public Field(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void OnLoad()
        {
            fieldSize = (int)Math.Round(Math.Min(screenWidth, screenHeight) * FIELD_SCREEN_PART);
            cellSize = fieldSize / CELLS;
            fieldSize = cellSize * CELLS;

            borderHalfSize = (int)Math.Round(PtToPix(BORDER_HALF_SIZE_PT));
            frameSize = cellSize - 2 * borderHalfSize;
            fieldX = (screenWidth - fieldSize) / 2;
            fieldY = (screenHeight - fieldSize) / 2;
        }

        public void OnRender()
        {
            GL.Disable(EnableCap.Texture2D);

            GL.Begin(BeginMode.Quads);
            GL.Color3(Color.Black);            

            drawVerticalStripes();
            drawHorizontalStripes();

            GL.End();
        }

        private void drawVerticalStripes()
        {
            for (int i = 0, x = fieldX - borderHalfSize, y = fieldY - borderHalfSize; i < CELLS + 1; ++i)
            {
                GL.Vertex2(x + cellSize * i, y);
                GL.Vertex2(x + cellSize * i, y + fieldSize + 2 * borderHalfSize);
                GL.Vertex2(x + cellSize * i + 2 * borderHalfSize, y + fieldSize + 2 * borderHalfSize);
                GL.Vertex2(x + cellSize * i + 2 * borderHalfSize, y);                
            }
        }

        private void drawHorizontalStripes()
        {
            for (int i = 0, x = fieldX - borderHalfSize, y = fieldY - borderHalfSize; i < CELLS + 1; ++i)
            {
                GL.Vertex2(x, y + cellSize * i);
                GL.Vertex2(x, y + cellSize * i + 2 * borderHalfSize);
                GL.Vertex2(x + fieldSize + 2 * borderHalfSize, y + cellSize * i + 2 * borderHalfSize);
                GL.Vertex2(x + fieldSize + 2 * borderHalfSize, y + cellSize * i);          
            }
        }

        private float PtToPix(int pt)
        {
            return (cellSize / 100.0f) * pt;
        }

        public int BorderHalfsize
        {
            get { return borderHalfSize; }
        }

        public int CellSize
        {
            get { return cellSize; }
        }

        public int FrameSize
        {
            get { return frameSize; }
        }

        public int X
        {
            get { return fieldX; }
        }

        public int Y
        {
            get { return fieldY; }
        }
    }
}