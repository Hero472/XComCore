namespace XComCore.Grid.Cell
{
    public sealed class Cell
    {
        private readonly ICellElement[] _elements;

        public GridPosition Position { get; }

        internal Cell(GridPosition position)
        {
            Position = position;
            _elements = new ICellElement[5];
        }

        public T Get<T>(CellLayer layer)
            where T : class, ICellElement
        {
            return _elements[(int)layer] as T;
        }

        internal void Set(ICellElement element)
        {
            _elements[(int)element.Layer] = element;
        }

        internal void Add(ICellElement element)
        {
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}