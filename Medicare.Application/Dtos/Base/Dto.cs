namespace Medicare.Application.Dtos.Base
{
    public abstract class Dto : IEquatable<Dto>
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; } = false;

        public override bool Equals(object obj)
        {
            return Equals(obj as Dto);
        }

        public bool Equals(Dto other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Dto left, Dto right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Dto left, Dto right)
        {
            return !(left == right);
        }
    }
}
