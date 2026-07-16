namespace XComCore.Grid.Cell
{
    public abstract class UnitElement : ICellElement
    {
        public CellLayer Layer => CellLayer.Unit;
    }
}