using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Metadata.Ecma335;
namespace EstacionamentoMVC.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve conter exatamente 7 caracteres.")]
        [RegularExpression(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", ErrorMessage = "Formato de placa inválido.")]
        public string Placa { get; set; }   
        public DateTime HoraEntrada { get; set; }
        public DateTime? HoraSaida { get; set; }


        public double? CalcularValor()
        {
            if (HoraSaida == null) return null;
            var duracao = HoraSaida.Value - HoraEntrada;
            double precoPorHora = 5.0;
            return Math.Ceiling(duracao.TotalHours) * precoPorHora;
        }

    }
}
