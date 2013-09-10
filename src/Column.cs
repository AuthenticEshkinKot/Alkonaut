namespace Alkonaut
{
    class Column : FieldObject
    {
        public const int X = 7, Y = 7;

        public Column(Translator translator)
            : base(translator, "column.png", X, Y)
        {
        }
    }
}
