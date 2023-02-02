using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sippedes.Cores.Entities
{
    [Table(name: "m_civil_data")]
    public class CivilData
    {
        [Key, Column(name: "id")] public string NIK { get; set; }

        [Column(name: "no_kk"), Required]
        public string NoKK { get; set; } = null!;

        [Column(name: "fullname"), Required]
        public string Fullname { get; set; } = null!;

        [Column(name: "gender"), Required]
        public string Gender { get; set; } = null!;

        [Column(name: "blood_type")]
        public string BloodType { get; set; }

        [Column(name: "education")]
        public string Education { get; set; }

        [Column(name: "birthdate")]
        public string BirthDate { get; set; }

        [Column(name: "address")]
        public string Address { get; set; }

        [Column(name: "province")]
        public string Province { get; set; }

        [Column(name: "city")]
        public string City { get; set; }

        [Column(name: "district")]
        public string District { get; set; }

        [Column(name: "village")]
        public string Village { get; set; }

        [Column(name: "religion")]
        public string Religion { get; set; }

    }

}
