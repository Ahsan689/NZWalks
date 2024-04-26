namespace NZWalks.API.Model.DTO
{
    public class UpdateWalksRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double? LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
    }
}
