using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sippedes.Cores.Entities
{
    [Table(name: "m_letter")]
    public class Letter
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        
        [Column(name: "user_credential_id")]
        public Guid UserCredentialId { get; set; }

        [Column(name: "letter_category_id")]
        public Guid LetterCategoryId { get; set; }

        [Column(name: "tracking_status_id")]
        public Guid TrackingStatusId { get; set; }

        [Column(name: "fullname")]
        public string? FullName { get; set; }

        [Column(name: "nik")]
        public string NIK { get; set; }

        [Column(name: "phone_number")]
        public string? PhoneNumber { get; set; }

        [Column(name: "job")]
        public string? Job { get; set; }

        [Column(name: "date")]
        public DateTime Date  { get; set; }

        [Column(name: "type_of_business")]
        public string? TypeOfBusiness { get; set; }

        [Column(name: "long_business")]
        public string? LongBusiness { get; set; }

        [Column(name: "address")]
        public string? Address { get; set; }

        [Column(name: "nationality")]
        public string? Nationality { get; set; }

        [Column(name: "necessity")]
        public string? Necessity { get; set; }

        [Column(name: "marital_status")]
        public string? MaritalStatus { get; set; }

        [Column(name: "religion")]
        public string? Religion { get; set; }

        public virtual UserCredential? UserCredential { get; set; }
        public virtual LetterCategory? LetterCategory { get; set; }
        public virtual TrackingStatus? TrackingStatus { get; set; }
    }
}