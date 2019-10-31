using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MediaHub.Model
{
    public class TestModel : BaseEntity
    {
        [Required]
        public string TestName{ get; set; }
    }
}
