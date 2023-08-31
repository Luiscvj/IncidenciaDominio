namespace IncApi.Helpers;


public class JWT
{
    public string HasKey { get; set; } 
    public string Issuer { get; set; }// quien genero el token
    public string Audience { get; set; } //A quien va dirigido el token
    public double  DurationInMinutes { get; set; }
}