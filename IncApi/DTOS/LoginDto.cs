using System.ComponentModel.DataAnnotations;

namespace IncApi.DTOS;


public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
} 