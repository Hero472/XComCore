namespace XComCore.World.Definitions
{
    public sealed class StructureDefinition
    {
        public string Id { get; }
        public string Name { get; }
        public ShapeDefinition Shape { get; }
        public MaterialDefinition Material { get; }
        public CollisionDefinition Collision { get; }
        public DestructionDefinition Destruction { get; }

        public StructureDefinition(
            string id,
            string name,
            ShapeDefinition shape,
            MaterialDefinition material,
            CollisionDefinition collision,
            DestructionDefinition destruction
        )
        {
            Id = id;
            Name = name;
            Shape = shape;
            Material = material;
            Collision = collision;
            Destruction = destruction;
        }
    }
}