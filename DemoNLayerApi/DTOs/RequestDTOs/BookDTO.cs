namespace DemoNLayerApi.DTOs.RequestDTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CategoryDTO> Categories { get; set; }

    }
}
