using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAPI.Entities
{
	public class User : IdentityUser
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Role { get; set; } = string.Empty;
     
    }
}