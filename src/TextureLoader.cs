using System.Drawing;
using System.Drawing.Imaging;

using OpenTK.Graphics.OpenGL;

namespace Alkonaut
{
    static class TextureLoader
    {
        const int MAX_TEXTURE_NUMBER = 7;
        static int[] TextureObjects = new int[MAX_TEXTURE_NUMBER];
        static int index;

        public static void OnLoad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(MAX_TEXTURE_NUMBER, TextureObjects);

            index = 0;
        }

        public static int LoadTexture(string path)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureObjects[index]);
            Bitmap bmp = new Bitmap(path);
            bmp.MakeTransparent(Color.FromArgb(10, 10, 10));

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);
            bmp.Dispose();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return index++;
        }

        public static int GetTextureObject(int i)
        {
            return TextureObjects[i];
        }
    }
}
