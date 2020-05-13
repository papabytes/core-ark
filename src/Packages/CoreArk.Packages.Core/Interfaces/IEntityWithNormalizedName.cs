namespace CoreArk.Packages.Core.Interfaces
{
    public interface IEntityWithNormalizedName
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}