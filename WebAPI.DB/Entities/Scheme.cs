﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Scheme
    {
        [Key]
        public int IdScheme {  get; set; }
        public string NameScheme { get; set; }

        public int IdBook { get; set; }

        [ForeignKey(nameof(IdBook))]
        public Book Book { get; set; }

    }
}