﻿using System.ComponentModel.DataAnnotations;

namespace OwlBlog.DAL.Models.Request.Roles
{
    public class RoleRequest
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
