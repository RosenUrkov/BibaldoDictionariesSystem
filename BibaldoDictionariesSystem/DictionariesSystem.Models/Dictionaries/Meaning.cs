﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Meaning
    {
        private const int MaxDescriptionLength = 200;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public virtual ICollection<Word> Words { get; set; }
    }
}