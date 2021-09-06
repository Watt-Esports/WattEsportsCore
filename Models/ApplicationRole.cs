using Microsoft.AspNetCore.Identity;
using System;

namespace WattEsportsCore.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }

        public ApplicationRole(string roleName, string description, DateTime creationDate) : base(roleName)
        {
            this.Description = description;
            this.CreationDate = creationDate;
        }

        /// <summary>
        /// Description of role
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Date role was created
        /// </summary>
        /// <value>DateTime</value>
        public DateTime CreationDate { get; set; }
    }
}