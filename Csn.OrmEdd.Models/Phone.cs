namespace Csn.OrmEdd.Models
{
    using System;
    public class Phone
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string Number { get; set; }

        public override string ToString()
        {
            return String.Format("{0},{1}",
                Id, Number);
        }
    }
}
