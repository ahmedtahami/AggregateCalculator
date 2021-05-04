using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AggregateCalculator.Models
{
    public class ResultViewModel
    {
        public Guid Id { get; set; }
        public double Aggregate { get; set; }
        [Display(Name = "Fail/Pass Status")]
        public bool isFail { get; set; }
        [Display(Name = "Aggregate Category")]
        public AggregateCategoryEnum AggregateCategory { get; set; }
        public string ScoresRecordId { get; set; }
    }
    public class ScoresRecordViewModel
    {
        public Guid Id { get; set; }
        [Range(0,100)]
        [Display(Name = "Obtained Marks in Maths")]
        public double Maths { get; set; }
        [Range(0, 100)]
        [Display(Name = "Obtained Marks in Science")]
        public double Science { get; set; }
        [Range(0, 100)]
        [Display(Name = "Obtained Marks in Social")]
        public double Social { get; set; }
        [Range(0, 100)]
        [Display(Name = "Obtained Marks in English")]
        public double English { get; set; }
        [Range(0, 100)]
        [Display(Name = "Obtained Marks in Language-Two")]
        public double LanguageTwo { get; set; }
    }
}