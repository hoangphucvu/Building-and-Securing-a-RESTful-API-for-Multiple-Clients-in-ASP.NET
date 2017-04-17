using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class AuthorForCreationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string Genre { get; set; }
        /// <summary>
        /// using new list to prevent null ref exception
        /// </summary>

        public ICollection<BookForCreationDto> Books { get; set; } = new List<BookForCreationDto>();
    }
}