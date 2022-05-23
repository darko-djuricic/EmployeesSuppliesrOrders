using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drugi_projekat.TagHelpers
{
    [HtmlTargetElement("age")]
    public class AgeFromYearTagHelper : TagHelper
    {
        public DateTime? date { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            var today = DateTime.Today;
            var birthday = date ?? DateTime.MaxValue;
            var age = today.Year - birthday.Year;
            if (birthday.Date > today.AddYears(-age)) age--;
            output.Content.SetContent($"{age} years");
            base.Process(context, output);
        }


    }
}
