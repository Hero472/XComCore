using XComCore.World.Definitions;
using XComCore.World.Structures;

namespace XComCore.Grid.Cell
{
    public sealed class StructureElement : ICellElement
    {
        public CellLayer Layer => CellLayer.Structure;

        public StructureDefinition Definition { get; }

        public StructureState State { get; }

        public bool IsDestroyed
        {
            get
            {
                return State.IsDestroyed;
            }
        }

        public StructureElement(
            StructureDefinition definition,
            StructureState state
        )
        {
            Definition = definition;
            State = state;
        }
    }
}