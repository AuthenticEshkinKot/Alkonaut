using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Alkonaut
{
    class Game : GameWindow
    {
        GameLogic logic;
        IGameObject[] gameObjects;

        public Game(DisplayDevice device)
            : base(device.Width, device.Height, GraphicsMode.Default, "Alkonaut", GameWindowFlags.Fullscreen, device)
        {
            createGameObjects(device.Width, device.Height);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.7f, 0.8f, 0.15f, 1.0f);
            GL.Ortho(0, Bounds.Width, Bounds.Height, 0, -1, 1);
            GL.Viewport(0, 0, Bounds.Width, Bounds.Height);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);           

            loadGameObjects();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            foreach (IGameObject obj in gameObjects)
            {
                obj.OnRender();
            }

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            logic.OnUpdate();

            if (Keyboard[Key.Escape])
            {
                Exit();
            }
            else if (Keyboard[Key.R])
            {
                createGameObjects(Width, Height);
                loadGameObjects();
            }
        }

        private void createGameObjects(int screenWidth, int screenHeight)
        {
            Queue<IGameObject> gameObjectsStack = new Queue<IGameObject>();

            Field field = new Field(screenWidth, screenHeight);
            Translator translator = new Translator(field);
            FieldObject alkoman = new Alkoman(translator), column = new Column(translator), pub = new Pub(translator);
            logic = new GameLogic((Alkoman)alkoman);
            StepsViewer stepsViewer = new StepsViewer(field, logic, screenHeight / 2);

            gameObjectsStack.Enqueue(field);
            gameObjectsStack.Enqueue(alkoman);
            gameObjectsStack.Enqueue(column);
            gameObjectsStack.Enqueue(stepsViewer);
            gameObjectsStack.Enqueue(pub);

            gameObjects = gameObjectsStack.ToArray();
        }

        private void loadGameObjects()
        {
            TextureLoader.OnLoad();
            logic.OnLoad(TargetUpdateFrequency);

            foreach (IGameObject obj in gameObjects)
            {
                obj.OnLoad();
            }
        }
    }
}