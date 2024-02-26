namespace EF.Infra.Entities
{
    public class Region
    {
        public int RegionId { get; set; }
        public required string RegionDescription { get; set; } = null!;
    }
}
