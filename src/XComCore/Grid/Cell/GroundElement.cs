namespace XComCore.Grid.Cell
{
    public abstract class GroundElement : ICellElement
    {
        public CellLayer Layer => CellLayer.Ground;
    }
}