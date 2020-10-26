using System.ComponentModel.DataAnnotations;

namespace TigerAdmin.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}