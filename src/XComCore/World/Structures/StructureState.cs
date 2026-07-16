namespace XComCore.World.Structures
{
    /// <summary>
    /// Represents the mutable runtime state of a structure.
    /// </summary>
    public sealed class StructureState
    {
        /// <summary>
        /// Current durability of the structure.
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Whether the structure has been destroyed.
        /// </summary>
        public bool IsDestroyed { get; private set; }

        public StructureState(int health)
        {
            Health = health;
            IsDestroyed = false;
        }

    }
}