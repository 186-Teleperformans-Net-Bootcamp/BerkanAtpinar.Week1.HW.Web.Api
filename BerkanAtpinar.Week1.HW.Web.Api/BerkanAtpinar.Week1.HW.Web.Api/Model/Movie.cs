using System;

namespace BerkanAtpinar.Week1.HW.Web.Api.Model
{
    public class Movie
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string LeadRole { get; set; }
        public DateTime PublishYear { get; set; }
    }
}
