﻿namespace BusinessLayer.Bases
{
    public enum EntityStateOption
    {
        Active,
        Deleted
    }

    public abstract class EntityBase
    {
        public EntityStateOption EntityState { get; set; }
        public bool HasChanges { get; set; }
        public bool IsNew { get; private set; }
        public bool IsValid => Validate();

        public abstract bool Validate();

    }
}
