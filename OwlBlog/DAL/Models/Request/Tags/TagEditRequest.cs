﻿using System.ComponentModel.DataAnnotations;

namespace OwlBlog.DAL.Models.Request.Tags
{
    public class TagEditRequest
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }
    }
}
