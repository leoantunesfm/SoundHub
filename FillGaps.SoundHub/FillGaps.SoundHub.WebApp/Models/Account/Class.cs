using System.Text.Json.Serialization;

namespace FillGaps.SoundHub.WebApp.Models.Account
{
    public class LoginResponseViewModel
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
