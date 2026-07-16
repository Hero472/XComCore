namespace XComCore.Grid.Cell
{
    public abstract class EffectElement : ICellElement
    {
        public CellLayer Layer => CellLayer.Effect;
    }
}