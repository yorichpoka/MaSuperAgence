namespace MaSuperAgence.Api.Models.Dtos
{
  public class PhotoDto : BaseDto
  {
    public string FileName { get; set; }
    public byte[] Content { get; set; }
    public int IdProperty { get; set; }
  }
}
