using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteka.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Wprowadź tytuł!")]
        [MaxLength(200, ErrorMessage = "Wprowadzony tytuł ma za dużo znaków!(MAX 200)")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Wprowadź imie autora!")]
        [MaxLength(100, ErrorMessage = "Wprowadzone imie ma za dużo znaków!(MAX 100)")]
        [Display(Name = "Imie")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwisko autora!")]
        [MaxLength(150, ErrorMessage = "Wprowadzone nazwisko ma za dużo znaków!(MAX 150)")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Wprowadź kategorie!")]
        [MaxLength(100, ErrorMessage = "Wprowadzona kategoria ma za dużo znaków!(MAX 100)")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Wprowadź kod ISBN!")]
        [MinLength(13, ErrorMessage = "Za mała ilość znaków (wymagane 13)!")]
        [MaxLength(13, ErrorMessage = "Wprowadzony kod ISBN ma za dużo znaków!(MAX 13)")]
        [Display(Name = "Kod ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "Rok wydania")]
        public int? YearPublic { get; set; }

        [Display(Name = "Ilość stron")]
        public int? Page { get; set; }

        [Display(Name = "Tom")]
        public int? Tom { get; set; }

        [MaxLength(2000, ErrorMessage = "Wprowadzony opis ma za dużo znaków!(MAX 2000)")]
        [Display(Name = "Opis")]
        public string Summary { get; set; }

        [Display(Name = "Data przeczytania")]
        public DateTime? DateOfReading { get; set; }

        [Display(Name = "Przeczytana")]
        public bool ItWasRead { get; set; }

        [MaxLength(100, ErrorMessage = "Wprowadzony status ma za dużo znaków!(MAX 100)")]
        [Display(Name = "Status posiadania")]
        public string OwnershipStatus { get; set; }

        [Display(Name = "Zdjęcie")]
        public byte[] Image { get; set; }

        public string IdUser { get; set; }
    }
}