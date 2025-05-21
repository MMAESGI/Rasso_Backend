using RassoApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace RassoApi.Models.EventModels
{
    public class EventStatus
    {
        public int Id { get; set; }


        [Required, MaxLength(50)]
        public StatusEnum Code { get; set; }

    }
}
