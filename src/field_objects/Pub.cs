using System;
using System.Collections.Generic;
using System.Text;

namespace Alkonaut
{
    class Pub : FieldObject
    {
        public Pub(Translator translator) 
            : base(new SingleTextureRenderer(translator, -1, 0, "pub.png"))
        {
        }
    }
}
