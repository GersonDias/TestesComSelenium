using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2.Models
{
    public class Cliente
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(minimum: 18, maximum: 100, ErrorMessage = "Cliente deve ser maior de idade")]
        public int Idade { get; set; }

        public async Task<bool> MetodoDemorado(int segundos)
        {
            await Task.Delay(segundos * 1000);

            return true;
        }
    }
}