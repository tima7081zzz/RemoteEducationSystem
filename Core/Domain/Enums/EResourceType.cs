using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum EResourceType : byte
{
    [Display(Name = "Unknown")]
    Unknown = 0,
    [Display(Name = "Book")]
    Book = 1,
    [Display(Name = "Article")]
    Article = 2,
    [Display(Name = "Video")]
    Video = 3,
}