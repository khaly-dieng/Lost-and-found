using Domain.DTO.ItemDTO;

namespace Domain.DTO.CategoryDTO
{
    public class GetCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Libelle { get; set; }
        public string Code { get; set; }
        public string CategoryImage { get; set; }
        public List<GetItemDto> Items { get; set; }
    }
}
