using System.ComponentModel.DataAnnotations;

namespace IncApi.DTOS;

public class TokenRequestDto
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}